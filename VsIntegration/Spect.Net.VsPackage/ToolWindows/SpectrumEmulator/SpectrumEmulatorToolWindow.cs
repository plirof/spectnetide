﻿using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Spect.Net.SpectrumEmu.Machine;
using Spect.Net.VsPackage.Vsx;

// ReSharper disable VirtualMemberCallInConstructor

namespace Spect.Net.VsPackage.ToolWindows.SpectrumEmulator
{
    /// <summary>
    /// This class implements the Spectrum emulator tool window.
    /// </summary>
    [Guid("de41a21a-714d-495e-9b3f-830965f9332b")]
    [Caption("ZX Spectrum Emulator")]
    [ToolWindowToolbar(typeof(SpectNetCommandSet), 0x1010)]
    public class SpectrumEmulatorToolWindow : 
        SpectrumToolWindowPane<SpectrumEmulatorToolWindowControl, SpectrumEmulatorToolWindowViewModel>
    {
        /// <summary>
        /// Creates a new view model every time a new solution is opened.
        /// </summary>
        protected override void OnSolutionOpened()
        {
            base.OnSolutionOpened();
            (Content as ISupportsMvvm<SpectrumEmulatorToolWindowViewModel>)
                .SetVm(new SpectrumEmulatorToolWindowViewModel());
        }

        /// <summary>Called when the active IVsWindowFrame changes.</summary>
        /// <param name="oldFrame">The old active frame.</param>
        /// <param name="newFrame">The new active frame.</param>
        public override void OnActiveFrameChanged(IVsWindowFrame oldFrame, IVsWindowFrame newFrame)
        {
            if (Content?.Vm?.MachineViewModel != null)
            {
                Content.Vm.MachineViewModel.AllowKeyboardScan = newFrame == Frame;
            }
        }

        /// <summary>
        /// Obtains the current state of the virtual machine
        /// </summary>
        /// <param name="package">Package instance</param>
        /// <returns>Virtual machine state</returns>
        public static VmState GetVmState(SpectNetPackage package) 
            => package.MachineViewModel?.VmState ?? VmState.None;

        /// <summary>
        /// Prepares the virtual machine execution options
        /// </summary>
        private static void PrepareRunOptions()
        {
            var package = SpectNetPackage.Default;
            var state = GetVmState(package);
            if (state == VmState.None
                || state == VmState.BuildingMachine
                || state == VmState.Stopped)
            {
                package.MachineViewModel.FastTapeMode = package.Options.UseFastLoad;
            }
            package.MachineViewModel.SkipInterruptRoutine = package.Options.SkipInterruptRoutine;
        }

        /// <summary>
        /// Starts the ZX Spectrum virtual machine
        /// </summary>
        [CommandId(0x1081)]
        public class StartVmCommand :
            VsxCommand<SpectNetPackage, SpectNetCommandSet>
        {
            protected override void OnExecute()
            {
                PrepareRunOptions();
                Package.MachineViewModel.StartVm();
            }

            protected override void OnQueryStatus(OleMenuCommand mc) 
                => mc.Enabled = GetVmState(Package) != VmState.Running;
        }

        /// <summary>
        /// Stops the ZX Spectrum virtual machine
        /// </summary>
        [CommandId(0x1082)]
        public class StopVmCommand :
            VsxCommand<SpectNetPackage, SpectNetCommandSet>
        {
            protected override void OnExecute() => 
                Package.MachineViewModel.StopVm();

            protected override void OnQueryStatus(OleMenuCommand mc)
            {
                var state = GetVmState(Package);
                mc.Enabled = state == VmState.Running
                             || state == VmState.Paused;
            }
        }

        /// <summary>
        /// Pauses the ZX Spectrum virtual machine
        /// </summary>
        [CommandId(0x1083)]
        public class PauseVmCommand :
            VsxCommand<SpectNetPackage, SpectNetCommandSet>
        {
            protected override void OnExecute()
                => Package.MachineViewModel.PauseVm();

            protected override void OnQueryStatus(OleMenuCommand mc)
                => mc.Enabled = GetVmState(Package) == VmState.Running;
        }

        /// <summary>
        /// Resets the ZX Spectrum virtual machine
        /// </summary>
        [CommandId(0x1084)]
        public class ResetVmCommand :
            VsxCommand<SpectNetPackage, SpectNetCommandSet>
        {
            protected override void OnExecute()
                => Package.MachineViewModel.ResetVm();

            protected override void OnQueryStatus(OleMenuCommand mc)
                => mc.Enabled = GetVmState(Package) == VmState.Running;
        }

        /// <summary>
        /// Starts the ZX Spectrum virtual machine in debug mode
        /// </summary>
        [CommandId(0x1085)]
        public class StartDebugVmCommand :
            VsxCommand<SpectNetPackage, SpectNetCommandSet>
        {
            protected override void OnExecute()
            {
                PrepareRunOptions();
                Package.MachineViewModel.StartDebugVm();
            }

            protected override void OnQueryStatus(OleMenuCommand mc)
                => mc.Enabled = GetVmState(Package) != VmState.Running;
        }

        /// <summary>
        /// Steps into the next Z80 instruction in debug mode
        /// </summary>
        [CommandId(0x1086)]
        public class StepIntoCommand :
            VsxCommand<SpectNetPackage, SpectNetCommandSet>
        {
            protected override void OnExecute()
            {
                PrepareRunOptions();
                Package.MachineViewModel.StepInto();
            }

            protected override void OnQueryStatus(OleMenuCommand mc)
                => mc.Enabled = GetVmState(Package) == VmState.Paused;
        }

        /// <summary>
        /// Steps into the next Z80 instruction in debug mode
        /// </summary>
        [CommandId(0x1087)]
        public class StepOverCommand :
            VsxCommand<SpectNetPackage, SpectNetCommandSet>
        {
            protected override void OnExecute()
            {
                PrepareRunOptions();
                Package.MachineViewModel.StepOver();
            }

            protected override void OnQueryStatus(OleMenuCommand mc)
                => mc.Enabled = GetVmState(Package) == VmState.Paused;
        }
    }
}
