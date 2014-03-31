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
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.typeDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTypeDefine([NotNull] PlanningDomainParser.TypeDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.typeDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTypeDefine([NotNull] PlanningDomainParser.TypeDefineContext context);

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
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.pEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPEffect([NotNull] PlanningDomainParser.PEffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.pEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPEffect([NotNull] PlanningDomainParser.PEffectContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.requireDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRequireDefine([NotNull] PlanningDomainParser.RequireDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.requireDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRequireDefine([NotNull] PlanningDomainParser.RequireDefineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPredicate([NotNull] PlanningDomainParser.PredicateContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPredicate([NotNull] PlanningDomainParser.PredicateContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.listVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListVariable([NotNull] PlanningDomainParser.ListVariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.listVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListVariable([NotNull] PlanningDomainParser.ListVariableContext context);

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
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.actionSymbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActionSymbol([NotNull] PlanningDomainParser.ActionSymbolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.actionSymbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActionSymbol([NotNull] PlanningDomainParser.ActionSymbolContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterType([NotNull] PlanningDomainParser.TypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitType([NotNull] PlanningDomainParser.TypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.actionDefBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActionDefBody([NotNull] PlanningDomainParser.ActionDefBodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.actionDefBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActionDefBody([NotNull] PlanningDomainParser.ActionDefBodyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.atomicFormula"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtomicFormula([NotNull] PlanningDomainParser.AtomicFormulaContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.atomicFormula"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtomicFormula([NotNull] PlanningDomainParser.AtomicFormulaContext context);

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
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.structureDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStructureDefine([NotNull] PlanningDomainParser.StructureDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.structureDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStructureDefine([NotNull] PlanningDomainParser.StructureDefineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.predicatesDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPredicatesDefine([NotNull] PlanningDomainParser.PredicatesDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.predicatesDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPredicatesDefine([NotNull] PlanningDomainParser.PredicatesDefineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.emptyOrEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEmptyOrEffect([NotNull] PlanningDomainParser.EmptyOrEffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.emptyOrEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEmptyOrEffect([NotNull] PlanningDomainParser.EmptyOrEffectContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.condEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondEffect([NotNull] PlanningDomainParser.CondEffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.condEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondEffect([NotNull] PlanningDomainParser.CondEffectContext context);

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
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.actionDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActionDefine([NotNull] PlanningDomainParser.ActionDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.actionDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActionDefine([NotNull] PlanningDomainParser.ActionDefineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.typing"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTyping([NotNull] PlanningDomainParser.TypingContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.typing"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTyping([NotNull] PlanningDomainParser.TypingContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.atomicFormulaSkeleton"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtomicFormulaSkeleton([NotNull] PlanningDomainParser.AtomicFormulaSkeletonContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.atomicFormulaSkeleton"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtomicFormulaSkeleton([NotNull] PlanningDomainParser.AtomicFormulaSkeletonContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.cEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCEffect([NotNull] PlanningDomainParser.CEffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.cEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCEffect([NotNull] PlanningDomainParser.CEffectContext context);

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

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.functionTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionTerm([NotNull] PlanningDomainParser.FunctionTermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.functionTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionTerm([NotNull] PlanningDomainParser.FunctionTermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.requireKey"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRequireKey([NotNull] PlanningDomainParser.RequireKeyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.requireKey"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRequireKey([NotNull] PlanningDomainParser.RequireKeyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.effect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEffect([NotNull] PlanningDomainParser.EffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.effect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEffect([NotNull] PlanningDomainParser.EffectContext context);

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
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.primitiveType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrimitiveType([NotNull] PlanningDomainParser.PrimitiveTypeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.primitiveType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrimitiveType([NotNull] PlanningDomainParser.PrimitiveTypeContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.strips"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStrips([NotNull] PlanningDomainParser.StripsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.strips"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStrips([NotNull] PlanningDomainParser.StripsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.emptyOrPreGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEmptyOrPreGD([NotNull] PlanningDomainParser.EmptyOrPreGDContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.emptyOrPreGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEmptyOrPreGD([NotNull] PlanningDomainParser.EmptyOrPreGDContext context);

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
	/// Enter a parse tree produced by <see cref="PlanningDomainParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] PlanningDomainParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningDomainParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] PlanningDomainParser.LiteralContext context);
}
} // namespace LanguageRecognition