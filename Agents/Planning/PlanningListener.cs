//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.2.2-SNAPSHOT
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from E:\EDGolog\LanguageRecognition\Planning.g4 by ANTLR 4.2.2-SNAPSHOT

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
/// <see cref="PlanningParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.2-SNAPSHOT")]
[System.CLSCompliant(false)]
public interface IPlanningListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.typeDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTypeDefine([NotNull] PlanningParser.TypeDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.typeDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTypeDefine([NotNull] PlanningParser.TypeDefineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.listName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListName([NotNull] PlanningParser.ListNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.listName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListName([NotNull] PlanningParser.ListNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPredicate([NotNull] PlanningParser.PredicateContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPredicate([NotNull] PlanningParser.PredicateContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.hostId"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterHostId([NotNull] PlanningParser.HostIdContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.hostId"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitHostId([NotNull] PlanningParser.HostIdContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.listVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListVariable([NotNull] PlanningParser.ListVariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.listVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListVariable([NotNull] PlanningParser.ListVariableContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.gd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGd([NotNull] PlanningParser.GdContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.gd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGd([NotNull] PlanningParser.GdContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.actionSymbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActionSymbol([NotNull] PlanningParser.ActionSymbolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.actionSymbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActionSymbol([NotNull] PlanningParser.ActionSymbolContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] PlanningParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] PlanningParser.TypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.agentDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAgentDefine([NotNull] PlanningParser.AgentDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.agentDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAgentDefine([NotNull] PlanningParser.AgentDefineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.actionDefBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActionDefBody([NotNull] PlanningParser.ActionDefBodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.actionDefBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActionDefBody([NotNull] PlanningParser.ActionDefBodyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.structureDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStructureDefine([NotNull] PlanningParser.StructureDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.structureDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStructureDefine([NotNull] PlanningParser.StructureDefineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.predicatesDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPredicatesDefine([NotNull] PlanningParser.PredicatesDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.predicatesDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPredicatesDefine([NotNull] PlanningParser.PredicatesDefineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.literalTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteralTerm([NotNull] PlanningParser.LiteralTermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.literalTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteralTerm([NotNull] PlanningParser.LiteralTermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.emptyOrEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEmptyOrEffect([NotNull] PlanningParser.EmptyOrEffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.emptyOrEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEmptyOrEffect([NotNull] PlanningParser.EmptyOrEffectContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.condEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondEffect([NotNull] PlanningParser.CondEffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.condEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondEffect([NotNull] PlanningParser.CondEffectContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.actionDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActionDefine([NotNull] PlanningParser.ActionDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.actionDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActionDefine([NotNull] PlanningParser.ActionDefineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.domain"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDomain([NotNull] PlanningParser.DomainContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.domain"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDomain([NotNull] PlanningParser.DomainContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.problemName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProblemName([NotNull] PlanningParser.ProblemNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.problemName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProblemName([NotNull] PlanningParser.ProblemNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.literalName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteralName([NotNull] PlanningParser.LiteralNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.literalName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteralName([NotNull] PlanningParser.LiteralNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.atomicFormulaSkeleton"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtomicFormulaSkeleton([NotNull] PlanningParser.AtomicFormulaSkeletonContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.atomicFormulaSkeleton"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtomicFormulaSkeleton([NotNull] PlanningParser.AtomicFormulaSkeletonContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.atomicFormulaName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtomicFormulaName([NotNull] PlanningParser.AtomicFormulaNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.atomicFormulaName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtomicFormulaName([NotNull] PlanningParser.AtomicFormulaNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.cEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCEffect([NotNull] PlanningParser.CEffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.cEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCEffect([NotNull] PlanningParser.CEffectContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.init"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInit([NotNull] PlanningParser.InitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.init"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInit([NotNull] PlanningParser.InitContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.domainName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDomainName([NotNull] PlanningParser.DomainNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.domainName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDomainName([NotNull] PlanningParser.DomainNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.problem"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProblem([NotNull] PlanningParser.ProblemContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.problem"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProblem([NotNull] PlanningParser.ProblemContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.objectDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterObjectDeclaration([NotNull] PlanningParser.ObjectDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.objectDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitObjectDeclaration([NotNull] PlanningParser.ObjectDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.effect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEffect([NotNull] PlanningParser.EffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.effect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEffect([NotNull] PlanningParser.EffectContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTerm([NotNull] PlanningParser.TermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTerm([NotNull] PlanningParser.TermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.primitiveType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrimitiveType([NotNull] PlanningParser.PrimitiveTypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.primitiveType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrimitiveType([NotNull] PlanningParser.PrimitiveTypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.gdName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGdName([NotNull] PlanningParser.GdNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.gdName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGdName([NotNull] PlanningParser.GdNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.atomicFormulaTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtomicFormulaTerm([NotNull] PlanningParser.AtomicFormulaTermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.atomicFormulaTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtomicFormulaTerm([NotNull] PlanningParser.AtomicFormulaTermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.emptyOrPreGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEmptyOrPreGD([NotNull] PlanningParser.EmptyOrPreGDContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.emptyOrPreGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEmptyOrPreGD([NotNull] PlanningParser.EmptyOrPreGDContext context);
}
} // namespace LanguageRecognition
