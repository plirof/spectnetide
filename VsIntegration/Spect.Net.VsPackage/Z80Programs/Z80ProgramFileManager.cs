﻿using System;
using System.IO;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Spect.Net.Assembler.Assembler;
using Spect.Net.VsPackage.Vsx;
// ReSharper disable SuspiciousTypeConversion.Global

namespace Spect.Net.VsPackage.Z80Programs
{
    /// <summary>
    /// This class is responsible for managing Z80 program files
    /// </summary>
    public class Z80ProgramFileManager
    {
        public SpectNetPackage Package { get; }
        /// <summary>
        /// The hierarchy information of the associated item
        /// </summary>
        public IVsHierarchy Hierarchy { get; }

        /// <summary>
        /// The Id information of the associated item
        /// </summary>
        public uint ItemId { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public Z80ProgramFileManager(IVsHierarchy hierarchy, uint itemId)
        {
            Package = VsxPackage.GetPackage<SpectNetPackage>();
            Hierarchy = hierarchy;
            ItemId = itemId;
        }

        /// <summary>
        /// The error list
        /// </summary>
        public ErrorListWindow ErrorList => Package.ErrorList;

        /// <summary>
        /// The full path of the item behind this Z80 program file
        /// </summary>
        protected string ItemPath
        {
            get
            {
                var singleItem = SpectNetPackage.IsSingleProjectItemSelection(out var hierarchy, out var itemId);
                if (!singleItem) return null;

                if (!(hierarchy is IVsProject project)) return null;

                project.GetMkDocument(itemId, out var itemFullPath);
                return itemFullPath;
            }
        }

        /// <summary>
        /// Compiles the Z80 code file
        /// </summary>
        /// <returns></returns>
        public bool Compile()
        {
            Package.ApplicationObject.ExecuteCommand("File.SaveAll");
            ErrorList.Clear();

            var code = File.ReadAllText(ItemPath);
            var compiler = new Z80Assembler();
            var output = compiler.Compile(code);

            if (output.ErrorCount == 0)
            {
                return true;
            }

            foreach (var error in output.Errors)
            {
                var errorTask = new ErrorTask
                {
                    Category = TaskCategory.User,
                    ErrorCategory = TaskErrorCategory.Error,
                    HierarchyItem = Hierarchy,
                    Document = ItemPath,
                    Line = error.Line,
                    Column = error.Column,
                    Text = error.ErrorCode == null
                        ? error.Message
                        : $"{error.ErrorCode}: {error.Message}"
                };
                errorTask.Navigate += ErrorTaskOnNavigate;
                ErrorList.AddErrorTask(errorTask);
            }

            Package.ApplicationObject.ExecuteCommand("View.ErrorList");
            return false;
        }

        /// <summary>
        /// Navigate to the sender task.
        /// </summary>
        private void ErrorTaskOnNavigate(object sender, EventArgs eventArgs)
        {
            if (sender is ErrorTask task)
            {
                ErrorList.Navigate(task);
            }
        }
    }
}