﻿using Microsoft.VisualStudio.Text.Tagging;

namespace Spect.Net.VsPackage.CustomEditors.AsmEditor
{
    /// <summary>
    /// This class defines the a token tag used in Z80 assembly
    /// </summary>
    public class Z80AsmTokenTag: ITextMarkerTag
    {
        /// <summary>
        /// The type of the token
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        public Z80AsmTokenTag(string type)
        {
            Type = type;
        }
    }

    /// <summary>
    /// The available token types in Z80 assembly
    /// </summary>
    public enum Z80AsmTokenType
    {
        None,
        Label,
        Pragma,
        Directive,
        Instruction,
        Comment,
        Number,
        Identifier,
        Breakpoint,
        CurrentBreakpoint
    }
}