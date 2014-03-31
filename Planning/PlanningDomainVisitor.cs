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
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="PlanningDomainParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.1-SNAPSHOT")]
[System.CLSCompliant(false)]
public interface IPlanningDomainVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.typeDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeDefine([NotNull] PlanningDomainParser.TypeDefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.listName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListName([NotNull] PlanningDomainParser.ListNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.pEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPEffect([NotNull] PlanningDomainParser.PEffectContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.requireDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRequireDefine([NotNull] PlanningDomainParser.RequireDefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPredicate([NotNull] PlanningDomainParser.PredicateContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.listVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListVariable([NotNull] PlanningDomainParser.ListVariableContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.gd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGd([NotNull] PlanningDomainParser.GdContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.actionSymbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActionSymbol([NotNull] PlanningDomainParser.ActionSymbolContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitType([NotNull] PlanningDomainParser.TypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.actionDefBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActionDefBody([NotNull] PlanningDomainParser.ActionDefBodyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.atomicFormula"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAtomicFormula([NotNull] PlanningDomainParser.AtomicFormulaContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.prefGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrefGD([NotNull] PlanningDomainParser.PrefGDContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.structureDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructureDefine([NotNull] PlanningDomainParser.StructureDefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.predicatesDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPredicatesDefine([NotNull] PlanningDomainParser.PredicatesDefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.emptyOrEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEmptyOrEffect([NotNull] PlanningDomainParser.EmptyOrEffectContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.condEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCondEffect([NotNull] PlanningDomainParser.CondEffectContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.domain"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDomain([NotNull] PlanningDomainParser.DomainContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.actionDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitActionDefine([NotNull] PlanningDomainParser.ActionDefineContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.typing"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTyping([NotNull] PlanningDomainParser.TypingContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.atomicFormulaSkeleton"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAtomicFormulaSkeleton([NotNull] PlanningDomainParser.AtomicFormulaSkeletonContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.cEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCEffect([NotNull] PlanningDomainParser.CEffectContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.preGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPreGD([NotNull] PlanningDomainParser.PreGDContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.functionTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionTerm([NotNull] PlanningDomainParser.FunctionTermContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.requireKey"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRequireKey([NotNull] PlanningDomainParser.RequireKeyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.effect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEffect([NotNull] PlanningDomainParser.EffectContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTerm([NotNull] PlanningDomainParser.TermContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.primitiveType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrimitiveType([NotNull] PlanningDomainParser.PrimitiveTypeContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.strips"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStrips([NotNull] PlanningDomainParser.StripsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.emptyOrPreGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEmptyOrPreGD([NotNull] PlanningDomainParser.EmptyOrPreGDContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.prefName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrefName([NotNull] PlanningDomainParser.PrefNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="PlanningDomainParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral([NotNull] PlanningDomainParser.LiteralContext context);
}
} // namespace LanguageRecognition
