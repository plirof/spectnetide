﻿using System;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using GalaSoft.MvvmLight.Messaging;
using Spect.Net.Assembler.Assembler;
using Spect.Net.SpectrumEmu.Abstraction.Providers;
using Spect.Net.SpectrumEmu.Machine;
using Spect.Net.VsPackage.CustomEditors.AsmEditor;
using Spect.Net.Wpf.Mvvm.Messages;

namespace Spect.Net.VsPackage.Z80Programs.Debugging
{
    /// <summary>
    /// This class provides VS-integrated debug information 
    /// </summary>
    public class VsIntegratedSpectrumDebugInfoProvider: ISpectrumDebugInfoProvider
    {
        /// <summary>
        /// The owner package
        /// </summary>
        public SpectNetPackage Package { get; }

        /// <summary>
        /// Contains the compiled output, provided the compilation was successful
        /// </summary>
        public AssemblerOutput CompiledOutput { get; set; }

        /// <summary>
        /// Stores the taggers so that their view could be notified about
        /// breakpoint changed
        /// </summary>
        internal Dictionary<string, Z80DebugTokenTagger> Z80AsmTaggers;

        /// <summary>
        /// The name of the file with the current breakpoint
        /// </summary>
        public string CurrentBreakpointFile { get; private set; }

        /// <summary>
        /// The line number within the current breakpoint file
        /// </summary>
        public int CurrentBreakpointLine { get; private set; }

        /// <summary>
        /// Registers a new tagger
        /// </summary>
        /// <param name="document">Owner document</param>
        /// <param name="tagger">Tagger instance</param>
        internal void RegisterTagger(string document, Z80DebugTokenTagger tagger)
        {
            Z80AsmTaggers[document] = tagger;
        }

        /// <summary>
        /// The component provider should be able to reset itself
        /// </summary>
        public void Reset()
        {
        }

        /// <summary>
        /// Clears the provider
        /// </summary>
        public void Clear()
        {
            Breakpoints.Clear();
            Z80AsmTaggers.Clear();
        }

        /// <summary>
        /// The currently defined breakpoints
        /// </summary>
        public BreakpointCollection Breakpoints { get; }

        /// <summary>
        /// Gets or sets an imminent breakpoint
        /// </summary>
        public ushort? ImminentBreakpoint { get; set; }

        /// <summary>
        /// Us this method to prepare the breakpoints when running the
        /// virtual machine in debug mode
        /// </summary>
        public void PrepareBreakpoints()
        {
            // --- Keep CPU breakpoints set through the Disassembler tool
            var cpuBreakPoints = Breakpoints.Where(bp => bp.Value.IsCpuBreakpoint);
            Breakpoints.Clear();
            foreach (var bpItem in cpuBreakPoints)
            {
                Breakpoints.Add(bpItem.Key, bpItem.Value);
            }

            // --- Merge breakpoints set in Visual Studio
            if (CompiledOutput == null) return;
            foreach (Breakpoint breakpoint in Package.ApplicationObject.Debugger.Breakpoints)
            {
                // --- Check for the file
                int fileIndex = -1;
                for (var i = 0; i < CompiledOutput.SourceFileList.Count; i++)
                {
                    if (string.Compare(breakpoint.File, CompiledOutput.SourceFileList[i].Filename,
                            StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        fileIndex = i;
                        break;
                    }
                }
                if (fileIndex < 0) continue;

                // --- Check the breakpoint address
                if (CompiledOutput.AddressMap.TryGetValue((fileIndex, breakpoint.FileLine), out var address))
                {
                    Breakpoints.Add(address, new BreakpointInfo
                    {
                        File = CompiledOutput.SourceFileList[fileIndex].Filename,
                        FileLine = breakpoint.FileLine,
                        Type = BreakpointType.NoCondition
                    });
                }
            }
        }

        /// <summary>
        /// Checks if the virtual machine should stop at the specified address
        /// </summary>
        /// <param name="address">Address to check</param>
        /// <returns>
        /// True, if the address means a breakpoint to stop; otherwise, false
        /// </returns>
        public bool ShouldBreakAtAddress(ushort address)
        {
            return Breakpoints.ContainsKey(address);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public VsIntegratedSpectrumDebugInfoProvider(SpectNetPackage package)
        {
            Package = package;
            Z80AsmTaggers = new Dictionary<string, Z80DebugTokenTagger>(StringComparer.InvariantCultureIgnoreCase);
            Breakpoints = new BreakpointCollection();
            Messenger.Default.Register<VmStateChangedMessage>(this, OnVmStateChanged);
        }

        /// <summary>
        /// Updates the layout of the specified document file
        /// </summary>
        /// <param name="filename">Document file to update</param>
        public void UpdateLayoutWithDebugInfo(string filename)
        {
            if (Z80AsmTaggers.TryGetValue(filename, out var tagger))
            {
                tagger.UpdateLayout();
            }
        }

        /// <summary>
        /// Responds to virtual machine state changes
        /// </summary>
        /// <param name="msg"></param>
        private void OnVmStateChanged(VmStateChangedMessage msg)
        {
            if (msg.NewState == VmState.Running)
            {
                var prevFile = CurrentBreakpointFile;
                var prevLine = CurrentBreakpointLine;

                // --- Remove current breakpoint information
                CurrentBreakpointFile = null;
                CurrentBreakpointLine = -1;
                UpdateBreakpointVisuals(prevFile, prevLine, false);
            }
            if (msg.NewState == VmState.Paused)
            {
                // --- Set up breakpoint information
                var address = Package.MachineViewModel.SpectrumVm.Cpu.Registers.PC;
                if (CompiledOutput.SourceMap.TryGetValue(address, out var fileInfo))
                {
                    CurrentBreakpointFile = CompiledOutput
                        .SourceFileList[fileInfo.FileIndex].Filename;
                    CurrentBreakpointLine = fileInfo.Line - 1;
                    Package.ApplicationObject.Documents.Open(CurrentBreakpointFile);
                    UpdateBreakpointVisuals(CurrentBreakpointFile, CurrentBreakpointLine, true);
                }
            }
        }

        /// <summary>
        /// Updates the visual properties
        /// </summary>
        /// <param name="breakpointFile">File with breakpoint</param>
        /// <param name="breakpointLine">Breakpoint line</param>
        /// <param name="isCurrent">Is this the current breakpoint?</param>
        private void UpdateBreakpointVisuals(string breakpointFile, int breakpointLine, bool isCurrent)
        {
            if (breakpointFile == null || breakpointLine < 0) return;
            if (Z80AsmTaggers.TryGetValue(breakpointFile, out var tagger))
            {
                tagger.UpdateLine(breakpointLine, isCurrent);
            }
        }
    }
}