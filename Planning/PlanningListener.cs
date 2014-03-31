//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.2.1-SNAPSHOT
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from E:\EDGolog\LanguageRecognition\Planning.g4 by ANTLR 4.2.1-SNAPSHOT

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
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.1-SNAPSHOT")]
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
	/// Enter a parse tree produced by <see cref="PlanningParser.pEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPEffect([NotNull] PlanningParser.PEffectContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.pEffect"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPEffect([NotNull] PlanningParser.PEffectContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.requireDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRequireDefine([NotNull] PlanningParser.RequireDefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.requireDefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRequireDefine([NotNull] PlanningParser.RequireDefineContext context);

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
	/// Enter a parse tree produced by <see cref="PlanningParser.atomicFormula"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtomicFormula([NotNull] PlanningParser.AtomicFormulaContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.atomicFormula"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtomicFormula([NotNull] PlanningParser.AtomicFormulaContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.prefGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrefGD([NotNull] PlanningParser.PrefGDContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.prefGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrefGD([NotNull] PlanningParser.PrefGDContext context);

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
	/// Enter a parse tree produced by <see cref="PlanningParser.typing"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTyping([NotNull] PlanningParser.TypingContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.typing"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTyping([NotNull] PlanningParser.TypingContext context);

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
	/// Enter a parse tree produced by <see cref="PlanningParser.preGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPreGD([NotNull] PlanningParser.PreGDContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.preGD"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPreGD([NotNull] PlanningParser.PreGDContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.functionTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionTerm([NotNull] PlanningParser.FunctionTermContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.functionTerm"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionTerm([NotNull] PlanningParser.FunctionTermContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.requireKey"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRequireKey([NotNull] PlanningParser.RequireKeyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.requireKey"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRequireKey([NotNull] PlanningParser.RequireKeyContext context);

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
	/// Enter a parse tree produced by <see cref="PlanningParser.strips"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStrips([NotNull] PlanningParser.StripsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.strips"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStrips([NotNull] PlanningParser.StripsContext context);

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

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.prefName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrefName([NotNull] PlanningParser.PrefNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.prefName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrefName([NotNull] PlanningParser.PrefNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLiteral([NotNull] PlanningParser.LiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.literal"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLiteral([NotNull] PlanningParser.LiteralContext context);
}
} // namespace LanguageRecognition
