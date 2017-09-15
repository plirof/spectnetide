//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.4
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\dotne\Source\Repos\spectnetide\Assembler\AntlrZ80AsmParserGenerator\ParserGenerator\Z80Asm.g4 by ANTLR 4.6.4

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Spect.Net.Assembler.Generated {

using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IZ80AsmListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.4")]
[System.CLSCompliant(false)]
public partial class Z80AsmBaseListener : IZ80AsmListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.compileUnit"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompileUnit([NotNull] Z80AsmParser.CompileUnitContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.compileUnit"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompileUnit([NotNull] Z80AsmParser.CompileUnitContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.asmline"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAsmline([NotNull] Z80AsmParser.AsmlineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.asmline"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAsmline([NotNull] Z80AsmParser.AsmlineContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.label"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLabel([NotNull] Z80AsmParser.LabelContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.label"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLabel([NotNull] Z80AsmParser.LabelContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.pragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPragma([NotNull] Z80AsmParser.PragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.pragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPragma([NotNull] Z80AsmParser.PragmaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.directive"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDirective([NotNull] Z80AsmParser.DirectiveContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.directive"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDirective([NotNull] Z80AsmParser.DirectiveContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.orgPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOrgPragma([NotNull] Z80AsmParser.OrgPragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.orgPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOrgPragma([NotNull] Z80AsmParser.OrgPragmaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.entPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEntPragma([NotNull] Z80AsmParser.EntPragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.entPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEntPragma([NotNull] Z80AsmParser.EntPragmaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.dispPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDispPragma([NotNull] Z80AsmParser.DispPragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.dispPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDispPragma([NotNull] Z80AsmParser.DispPragmaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.equPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEquPragma([NotNull] Z80AsmParser.EquPragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.equPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEquPragma([NotNull] Z80AsmParser.EquPragmaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.defbPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDefbPragma([NotNull] Z80AsmParser.DefbPragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.defbPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDefbPragma([NotNull] Z80AsmParser.DefbPragmaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.defwPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDefwPragma([NotNull] Z80AsmParser.DefwPragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.defwPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDefwPragma([NotNull] Z80AsmParser.DefwPragmaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.defmPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDefmPragma([NotNull] Z80AsmParser.DefmPragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.defmPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDefmPragma([NotNull] Z80AsmParser.DefmPragmaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.skipPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSkipPragma([NotNull] Z80AsmParser.SkipPragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.skipPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSkipPragma([NotNull] Z80AsmParser.SkipPragmaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.externPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExternPragma([NotNull] Z80AsmParser.ExternPragmaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.externPragma"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExternPragma([NotNull] Z80AsmParser.ExternPragmaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.operation"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOperation([NotNull] Z80AsmParser.OperationContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.operation"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOperation([NotNull] Z80AsmParser.OperationContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.trivialOperation"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTrivialOperation([NotNull] Z80AsmParser.TrivialOperationContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.trivialOperation"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTrivialOperation([NotNull] Z80AsmParser.TrivialOperationContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.compoundOperation"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompoundOperation([NotNull] Z80AsmParser.CompoundOperationContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.compoundOperation"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompoundOperation([NotNull] Z80AsmParser.CompoundOperationContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.operand"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOperand([NotNull] Z80AsmParser.OperandContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.operand"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOperand([NotNull] Z80AsmParser.OperandContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.reg8"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReg8([NotNull] Z80AsmParser.Reg8Context context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.reg8"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReg8([NotNull] Z80AsmParser.Reg8Context context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.reg8Idx"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReg8Idx([NotNull] Z80AsmParser.Reg8IdxContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.reg8Idx"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReg8Idx([NotNull] Z80AsmParser.Reg8IdxContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.reg8Spec"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReg8Spec([NotNull] Z80AsmParser.Reg8SpecContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.reg8Spec"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReg8Spec([NotNull] Z80AsmParser.Reg8SpecContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.reg16"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReg16([NotNull] Z80AsmParser.Reg16Context context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.reg16"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReg16([NotNull] Z80AsmParser.Reg16Context context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.reg16Idx"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReg16Idx([NotNull] Z80AsmParser.Reg16IdxContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.reg16Idx"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReg16Idx([NotNull] Z80AsmParser.Reg16IdxContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.reg16Spec"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReg16Spec([NotNull] Z80AsmParser.Reg16SpecContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.reg16Spec"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReg16Spec([NotNull] Z80AsmParser.Reg16SpecContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.regIndirect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterRegIndirect([NotNull] Z80AsmParser.RegIndirectContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.regIndirect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitRegIndirect([NotNull] Z80AsmParser.RegIndirectContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.cPort"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCPort([NotNull] Z80AsmParser.CPortContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.cPort"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCPort([NotNull] Z80AsmParser.CPortContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.memIndirect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterMemIndirect([NotNull] Z80AsmParser.MemIndirectContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.memIndirect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitMemIndirect([NotNull] Z80AsmParser.MemIndirectContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.indexedAddr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIndexedAddr([NotNull] Z80AsmParser.IndexedAddrContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.indexedAddr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIndexedAddr([NotNull] Z80AsmParser.IndexedAddrContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.condition"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCondition([NotNull] Z80AsmParser.ConditionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.condition"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCondition([NotNull] Z80AsmParser.ConditionContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr([NotNull] Z80AsmParser.ExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr([NotNull] Z80AsmParser.ExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.orExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOrExpr([NotNull] Z80AsmParser.OrExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.orExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOrExpr([NotNull] Z80AsmParser.OrExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.xorExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterXorExpr([NotNull] Z80AsmParser.XorExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.xorExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitXorExpr([NotNull] Z80AsmParser.XorExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.andExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAndExpr([NotNull] Z80AsmParser.AndExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.andExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAndExpr([NotNull] Z80AsmParser.AndExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.equExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEquExpr([NotNull] Z80AsmParser.EquExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.equExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEquExpr([NotNull] Z80AsmParser.EquExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.relExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterRelExpr([NotNull] Z80AsmParser.RelExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.relExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitRelExpr([NotNull] Z80AsmParser.RelExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.shiftExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterShiftExpr([NotNull] Z80AsmParser.ShiftExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.shiftExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitShiftExpr([NotNull] Z80AsmParser.ShiftExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.addExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAddExpr([NotNull] Z80AsmParser.AddExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.addExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAddExpr([NotNull] Z80AsmParser.AddExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.multExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterMultExpr([NotNull] Z80AsmParser.MultExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.multExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitMultExpr([NotNull] Z80AsmParser.MultExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.unaryExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterUnaryExpr([NotNull] Z80AsmParser.UnaryExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.unaryExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitUnaryExpr([NotNull] Z80AsmParser.UnaryExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.literalExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLiteralExpr([NotNull] Z80AsmParser.LiteralExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.literalExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLiteralExpr([NotNull] Z80AsmParser.LiteralExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Z80AsmParser.symbolExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSymbolExpr([NotNull] Z80AsmParser.SymbolExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Z80AsmParser.symbolExpr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSymbolExpr([NotNull] Z80AsmParser.SymbolExprContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace Spect.Net.Assembler.Generated
