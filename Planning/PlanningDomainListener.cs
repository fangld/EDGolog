//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.2.1-SNAPSHOT
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from E:\EDGolog\LanguageRecognition\PlanningDomain.g4 by ANTLR 4.2.1-SNAPSHOT

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace LanguageRecognition {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="PlanningDomainParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.1-SNAPSHOT")]
[System.CLSCompliant(false)]
public interface IPlanningDomainListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.listName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListName([NotNull] PlanningDomainParser.ListNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.listName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListName([NotNull] PlanningDomainParser.ListNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.actBodyDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActBodyDef([NotNull] PlanningDomainParser.ActBodyDefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.actBodyDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActBodyDef([NotNull] PlanningDomainParser.ActBodyDefContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.gd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGd([NotNull] PlanningDomainParser.GdContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.gd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGd([NotNull] PlanningDomainParser.GdContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.reqDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReqDef([NotNull] PlanningDomainParser.ReqDefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.reqDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReqDef([NotNull] PlanningDomainParser.ReqDefContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.predDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPredDef([NotNull] PlanningDomainParser.PredDefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.predDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPredDef([NotNull] PlanningDomainParser.PredDefContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.prefGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrefGD([NotNull] PlanningDomainParser.PrefGDContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.prefGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrefGD([NotNull] PlanningDomainParser.PrefGDContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.listVar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListVar([NotNull] PlanningDomainParser.ListVarContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.listVar"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListVar([NotNull] PlanningDomainParser.ListVarContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.atomicForm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtomicForm([NotNull] PlanningDomainParser.AtomicFormContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.atomicForm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtomicForm([NotNull] PlanningDomainParser.AtomicFormContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerm([NotNull] PlanningDomainParser.TermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerm([NotNull] PlanningDomainParser.TermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.actSym"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActSym([NotNull] PlanningDomainParser.ActSymContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.actSym"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActSym([NotNull] PlanningDomainParser.ActSymContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.funTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunTerm([NotNull] PlanningDomainParser.FunTermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.funTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunTerm([NotNull] PlanningDomainParser.FunTermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.domain"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDomain([NotNull] PlanningDomainParser.DomainContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.domain"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDomain([NotNull] PlanningDomainParser.DomainContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.reqKey"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReqKey([NotNull] PlanningDomainParser.ReqKeyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.reqKey"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReqKey([NotNull] PlanningDomainParser.ReqKeyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.typeDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTypeDef([NotNull] PlanningDomainParser.TypeDefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.typeDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTypeDef([NotNull] PlanningDomainParser.TypeDefContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.prefName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrefName([NotNull] PlanningDomainParser.PrefNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.prefName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrefName([NotNull] PlanningDomainParser.PrefNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.actDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActDef([NotNull] PlanningDomainParser.ActDefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.actDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActDef([NotNull] PlanningDomainParser.ActDefContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] PlanningDomainParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] PlanningDomainParser.LiteralContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.preGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPreGD([NotNull] PlanningDomainParser.PreGDContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.preGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPreGD([NotNull] PlanningDomainParser.PreGDContext context);
}
} // namespace LanguageRecognition
