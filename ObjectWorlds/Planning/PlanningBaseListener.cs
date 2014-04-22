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
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IPlanningListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.2-SNAPSHOT")]
[System.CLSCompliant(false)]
public partial class PlanningBaseListener : IPlanningListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.typeDefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTypeDefine([NotNull] PlanningParser.TypeDefineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.typeDefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTypeDefine([NotNull] PlanningParser.TypeDefineContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.listName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterListName([NotNull] PlanningParser.ListNameContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.listName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitListName([NotNull] PlanningParser.ListNameContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPredicate([NotNull] PlanningParser.PredicateContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.predicate"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPredicate([NotNull] PlanningParser.PredicateContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.listVariable"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterListVariable([NotNull] PlanningParser.ListVariableContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.listVariable"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitListVariable([NotNull] PlanningParser.ListVariableContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.gd"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterGd([NotNull] PlanningParser.GdContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.gd"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitGd([NotNull] PlanningParser.GdContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.actionSymbol"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterActionSymbol([NotNull] PlanningParser.ActionSymbolContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.actionSymbol"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitActionSymbol([NotNull] PlanningParser.ActionSymbolContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.type"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterType([NotNull] PlanningParser.TypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.type"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitType([NotNull] PlanningParser.TypeContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.agentDefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAgentDefine([NotNull] PlanningParser.AgentDefineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.agentDefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAgentDefine([NotNull] PlanningParser.AgentDefineContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.actionDefBody"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterActionDefBody([NotNull] PlanningParser.ActionDefBodyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.actionDefBody"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitActionDefBody([NotNull] PlanningParser.ActionDefBodyContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.initBelief"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterInitBelief([NotNull] PlanningParser.InitBeliefContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.initBelief"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitInitBelief([NotNull] PlanningParser.InitBeliefContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.structureDefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStructureDefine([NotNull] PlanningParser.StructureDefineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.structureDefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStructureDefine([NotNull] PlanningParser.StructureDefineContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.predicatesDefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPredicatesDefine([NotNull] PlanningParser.PredicatesDefineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.predicatesDefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPredicatesDefine([NotNull] PlanningParser.PredicatesDefineContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.clientProblem"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterClientProblem([NotNull] PlanningParser.ClientProblemContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.clientProblem"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitClientProblem([NotNull] PlanningParser.ClientProblemContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.literalTerm"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLiteralTerm([NotNull] PlanningParser.LiteralTermContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.literalTerm"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLiteralTerm([NotNull] PlanningParser.LiteralTermContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.emptyOrEffect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEmptyOrEffect([NotNull] PlanningParser.EmptyOrEffectContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.emptyOrEffect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEmptyOrEffect([NotNull] PlanningParser.EmptyOrEffectContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.condEffect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCondEffect([NotNull] PlanningParser.CondEffectContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.condEffect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCondEffect([NotNull] PlanningParser.CondEffectContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.initKnowledge"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterInitKnowledge([NotNull] PlanningParser.InitKnowledgeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.initKnowledge"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitInitKnowledge([NotNull] PlanningParser.InitKnowledgeContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.actionDefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterActionDefine([NotNull] PlanningParser.ActionDefineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.actionDefine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitActionDefine([NotNull] PlanningParser.ActionDefineContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.domain"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDomain([NotNull] PlanningParser.DomainContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.domain"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDomain([NotNull] PlanningParser.DomainContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.problemName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterProblemName([NotNull] PlanningParser.ProblemNameContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.problemName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitProblemName([NotNull] PlanningParser.ProblemNameContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.literalName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLiteralName([NotNull] PlanningParser.LiteralNameContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.literalName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLiteralName([NotNull] PlanningParser.LiteralNameContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.atomicFormulaSkeleton"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAtomicFormulaSkeleton([NotNull] PlanningParser.AtomicFormulaSkeletonContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.atomicFormulaSkeleton"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAtomicFormulaSkeleton([NotNull] PlanningParser.AtomicFormulaSkeletonContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.atomicFormulaName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAtomicFormulaName([NotNull] PlanningParser.AtomicFormulaNameContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.atomicFormulaName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAtomicFormulaName([NotNull] PlanningParser.AtomicFormulaNameContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.cEffect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCEffect([NotNull] PlanningParser.CEffectContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.cEffect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCEffect([NotNull] PlanningParser.CEffectContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.serverProblem"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterServerProblem([NotNull] PlanningParser.ServerProblemContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.serverProblem"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitServerProblem([NotNull] PlanningParser.ServerProblemContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.agentId"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAgentId([NotNull] PlanningParser.AgentIdContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.agentId"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAgentId([NotNull] PlanningParser.AgentIdContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.init"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterInit([NotNull] PlanningParser.InitContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.init"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitInit([NotNull] PlanningParser.InitContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.domainName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDomainName([NotNull] PlanningParser.DomainNameContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.domainName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDomainName([NotNull] PlanningParser.DomainNameContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.objectDeclaration"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterObjectDeclaration([NotNull] PlanningParser.ObjectDeclarationContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.objectDeclaration"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitObjectDeclaration([NotNull] PlanningParser.ObjectDeclarationContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.effect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEffect([NotNull] PlanningParser.EffectContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.effect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEffect([NotNull] PlanningParser.EffectContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.term"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTerm([NotNull] PlanningParser.TermContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.term"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTerm([NotNull] PlanningParser.TermContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.primitiveType"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPrimitiveType([NotNull] PlanningParser.PrimitiveTypeContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.primitiveType"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPrimitiveType([NotNull] PlanningParser.PrimitiveTypeContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.gdName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterGdName([NotNull] PlanningParser.GdNameContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.gdName"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitGdName([NotNull] PlanningParser.GdNameContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.atomicFormulaTerm"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAtomicFormulaTerm([NotNull] PlanningParser.AtomicFormulaTermContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.atomicFormulaTerm"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAtomicFormulaTerm([NotNull] PlanningParser.AtomicFormulaTermContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="PlanningParser.emptyOrPreGD"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterEmptyOrPreGD([NotNull] PlanningParser.EmptyOrPreGDContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="PlanningParser.emptyOrPreGD"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitEmptyOrPreGD([NotNull] PlanningParser.EmptyOrPreGDContext context) { }

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
} // namespace LanguageRecognition
