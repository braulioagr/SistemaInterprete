﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Braulio García R\Documents\GitHub\SistemaInterprete\Practica1\Practica1\Combined1.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Practica1 {

using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="ICombined1Listener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class Combined1BaseListener : ICombined1Listener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="Combined1Parser.programa"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPrograma([NotNull] Combined1Parser.ProgramaContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Combined1Parser.programa"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPrograma([NotNull] Combined1Parser.ProgramaContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Combined1Parser.stat"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStat([NotNull] Combined1Parser.StatContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Combined1Parser.stat"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStat([NotNull] Combined1Parser.StatContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Combined1Parser.expresion"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpresion([NotNull] Combined1Parser.ExpresionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Combined1Parser.expresion"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpresion([NotNull] Combined1Parser.ExpresionContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Combined1Parser.multiplicacion"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterMultiplicacion([NotNull] Combined1Parser.MultiplicacionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Combined1Parser.multiplicacion"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitMultiplicacion([NotNull] Combined1Parser.MultiplicacionContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="Combined1Parser.numero"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNumero([NotNull] Combined1Parser.NumeroContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="Combined1Parser.numero"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNumero([NotNull] Combined1Parser.NumeroContext context) { }

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
} // namespace Practica1
