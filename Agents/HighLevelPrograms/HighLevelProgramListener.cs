//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.2.2-SNAPSHOT
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from E:\EDGolog\LanguageRecognition\HighLevelProgram.g4 by ANTLR 4.2.2-SNAPSHOT

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
/// <see cref="HighLevelProgramParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.2-SNAPSHOT")]
[System.CLSCompliant(false)]
public interface IHighLevelProgramListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="HighLevelProgramParser.listName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterListName([NotNull] HighLevelProgramParser.ListNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HighLevelProgramParser.listName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitListName([NotNull] HighLevelProgramParser.ListNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HighLevelProgramParser.subjectFormula"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSubjectFormula([NotNull] HighLevelProgramParser.SubjectFormulaContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HighLevelProgramParser.subjectFormula"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSubjectFormula([NotNull] HighLevelProgramParser.SubjectFormulaContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HighLevelProgramParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] HighLevelProgramParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HighLevelProgramParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] HighLevelProgramParser.ProgramContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HighLevelProgramParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPredicate([NotNull] HighLevelProgramParser.PredicateContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HighLevelProgramParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPredicate([NotNull] HighLevelProgramParser.PredicateContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HighLevelProgramParser.action"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAction([NotNull] HighLevelProgramParser.ActionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HighLevelProgramParser.action"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAction([NotNull] HighLevelProgramParser.ActionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="HighLevelProgramParser.objectFormula"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterObjectFormula([NotNull] HighLevelProgramParser.ObjectFormulaContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="HighLevelProgramParser.objectFormula"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitObjectFormula([NotNull] HighLevelProgramParser.ObjectFormulaContext context);
}
} // namespace LanguageRecognition
