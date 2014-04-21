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
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="PlanningParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.2-SNAPSHOT")]
[System.CLSCompliant(false)]
public interface IPlanningVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.typeDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeDefine([NotNull] PlanningParser.TypeDefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.listName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListName([NotNull] PlanningParser.ListNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPredicate([NotNull] PlanningParser.PredicateContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.listVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListVariable([NotNull] PlanningParser.ListVariableContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.gd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGd([NotNull] PlanningParser.GdContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.actionSymbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActionSymbol([NotNull] PlanningParser.ActionSymbolContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] PlanningParser.TypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.agentDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAgentDefine([NotNull] PlanningParser.AgentDefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.actionDefBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActionDefBody([NotNull] PlanningParser.ActionDefBodyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.structureDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructureDefine([NotNull] PlanningParser.StructureDefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.predicatesDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPredicatesDefine([NotNull] PlanningParser.PredicatesDefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.literalTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralTerm([NotNull] PlanningParser.LiteralTermContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.emptyOrEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEmptyOrEffect([NotNull] PlanningParser.EmptyOrEffectContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.condEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondEffect([NotNull] PlanningParser.CondEffectContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.actionDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActionDefine([NotNull] PlanningParser.ActionDefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.domain"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDomain([NotNull] PlanningParser.DomainContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.problemName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProblemName([NotNull] PlanningParser.ProblemNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.literalName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralName([NotNull] PlanningParser.LiteralNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.atomicFormulaSkeleton"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAtomicFormulaSkeleton([NotNull] PlanningParser.AtomicFormulaSkeletonContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.atomicFormulaName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAtomicFormulaName([NotNull] PlanningParser.AtomicFormulaNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.cEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCEffect([NotNull] PlanningParser.CEffectContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.init"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInit([NotNull] PlanningParser.InitContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.domainName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDomainName([NotNull] PlanningParser.DomainNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.problem"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProblem([NotNull] PlanningParser.ProblemContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.objectDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectDeclaration([NotNull] PlanningParser.ObjectDeclarationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.effect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEffect([NotNull] PlanningParser.EffectContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTerm([NotNull] PlanningParser.TermContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.primitiveType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrimitiveType([NotNull] PlanningParser.PrimitiveTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.gdName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGdName([NotNull] PlanningParser.GdNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.atomicFormulaTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAtomicFormulaTerm([NotNull] PlanningParser.AtomicFormulaTermContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningParser.emptyOrPreGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEmptyOrPreGD([NotNull] PlanningParser.EmptyOrPreGDContext context);
}
} // namespace LanguageRecognition
