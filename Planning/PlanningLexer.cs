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
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.1-SNAPSHOT")]
[System.CLSCompliant(false)]
public partial class PlanningLexer : Lexer {
	public const int
		DOM=1, PROM=2, DEF=3, REQ=4, TYPE=5, PRED=6, ACT=7, PARM=8, PRE=9, EFF=10, 
		OBJ=11, EITHER=12, OBJS=13, INIT=14, GOAL=15, AT=16, STRIPS=17, TYPING=18, 
		LB=19, RB=20, LSB=21, RSB=22, COLON=23, QM=24, COMMA=25, UL=26, DASH=27, 
		PLUS=28, MINUS=29, MULT=30, DIV=31, EQ=32, LT=33, LEQ=34, GT=35, GEQ=36, 
		AND=37, OR=38, NOT=39, IMPLY=40, FORALL=41, EXISTS=42, WHEN=43, PREF=44, 
		BINCOMP=45, BINOP=46, LETTER=47, DIGIT=48, NAME=49, CHAR=50, NUMBER=51, 
		DECIMAL=52, VAR=53, FUNSYM=54, WS=55;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] tokenNames = {
		"<INVALID>",
		"'domain'", "'problem'", "'define'", "'requirements'", "'types'", "'predicates'", 
		"'action'", "'parameters'", "'precondition'", "'effect'", "'object'", 
		"'either'", "'objects'", "'init'", "'goal'", "'at'", "'strips'", "'typing'", 
		"'('", "')'", "'['", "']'", "':'", "'?'", "'.'", "'_'", "DASH", "'+'", 
		"MINUS", "'*'", "'/'", "'='", "'<'", "'<='", "'>'", "'>='", "'and'", "'or'", 
		"'not'", "'imply'", "'forall'", "'exists'", "'when'", "'preference'", 
		"BINCOMP", "BINOP", "LETTER", "DIGIT", "NAME", "CHAR", "NUMBER", "DECIMAL", 
		"VAR", "FUNSYM", "WS"
	};
	public static readonly string[] ruleNames = {
		"DOM", "PROM", "DEF", "REQ", "TYPE", "PRED", "ACT", "PARM", "PRE", "EFF", 
		"OBJ", "EITHER", "OBJS", "INIT", "GOAL", "AT", "STRIPS", "TYPING", "LB", 
		"RB", "LSB", "RSB", "COLON", "QM", "COMMA", "UL", "DASH", "PLUS", "MINUS", 
		"MULT", "DIV", "EQ", "LT", "LEQ", "GT", "GEQ", "AND", "OR", "NOT", "IMPLY", 
		"FORALL", "EXISTS", "WHEN", "PREF", "BINCOMP", "BINOP", "LETTER", "DIGIT", 
		"NAME", "CHAR", "NUMBER", "DECIMAL", "VAR", "FUNSYM", "WS"
	};


	public PlanningLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	public override string GrammarFileName { get { return "Planning.g4"; } }

	public override string[] TokenNames { get { return tokenNames; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x39\x189\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A"+
		"\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 "+
		"\t \x4!\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t"+
		")\x4*\t*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31"+
		"\x4\x32\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35\x4\x36\t\x36\x4\x37"+
		"\t\x37\x4\x38\t\x38\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3"+
		"\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3"+
		"\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3"+
		"\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\n\x3"+
		"\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3"+
		"\v\x3\v\x3\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3"+
		"\r\x3\r\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE"+
		"\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x11"+
		"\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13"+
		"\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x14\x3\x14\x3\x15\x3\x15"+
		"\x3\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18\x3\x19\x3\x19\x3\x1A\x3\x1A"+
		"\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3\x1F\x3\x1F"+
		"\x3 \x3 \x3!\x3!\x3\"\x3\"\x3#\x3#\x3#\x3$\x3$\x3%\x3%\x3%\x3&\x3&\x3"+
		"&\x3&\x3\'\x3\'\x3\'\x3(\x3(\x3(\x3(\x3)\x3)\x3)\x3)\x3)\x3)\x3*\x3*\x3"+
		"*\x3*\x3*\x3*\x3*\x3+\x3+\x3+\x3+\x3+\x3+\x3+\x3,\x3,\x3,\x3,\x3,\x3-"+
		"\x3-\x3-\x3-\x3-\x3-\x3-\x3-\x3-\x3-\x3-\x3.\x3.\x3.\x3.\x3.\x5.\x157"+
		"\n.\x3/\x3/\x3/\x3/\x5/\x15D\n/\x3\x30\x3\x30\x3\x31\x3\x31\x3\x32\x3"+
		"\x32\a\x32\x165\n\x32\f\x32\xE\x32\x168\v\x32\x3\x33\x3\x33\x3\x33\x3"+
		"\x33\x5\x33\x16E\n\x33\x3\x34\x6\x34\x171\n\x34\r\x34\xE\x34\x172\x3\x34"+
		"\x5\x34\x176\n\x34\x3\x35\x3\x35\x6\x35\x17A\n\x35\r\x35\xE\x35\x17B\x3"+
		"\x36\x3\x36\x3\x36\x3\x37\x3\x37\x3\x38\x6\x38\x184\n\x38\r\x38\xE\x38"+
		"\x185\x3\x38\x3\x38\x2\x2\x2\x39\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2"+
		"\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2"+
		"\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2"+
		"\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2"+
		"\x1E;\x2\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O"+
		"\x2)Q\x2*S\x2+U\x2,W\x2-Y\x2.[\x2/]\x2\x30_\x2\x31\x61\x2\x32\x63\x2\x33"+
		"\x65\x2\x34g\x2\x35i\x2\x36k\x2\x37m\x2\x38o\x2\x39\x3\x2\x5\x4\x2\x43"+
		"\\\x63|\x3\x2\x32;\x5\x2\v\f\xF\xF\"\"\x197\x2\x3\x3\x2\x2\x2\x2\x5\x3"+
		"\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2"+
		"\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15"+
		"\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2"+
		"\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2"+
		"\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-"+
		"\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2"+
		"\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2"+
		"\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2"+
		"\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x2"+
		"M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3\x2\x2\x2\x2U\x3\x2"+
		"\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2\x2\x2\x2]\x3\x2\x2\x2"+
		"\x2_\x3\x2\x2\x2\x2\x61\x3\x2\x2\x2\x2\x63\x3\x2\x2\x2\x2\x65\x3\x2\x2"+
		"\x2\x2g\x3\x2\x2\x2\x2i\x3\x2\x2\x2\x2k\x3\x2\x2\x2\x2m\x3\x2\x2\x2\x2"+
		"o\x3\x2\x2\x2\x3q\x3\x2\x2\x2\x5x\x3\x2\x2\x2\a\x80\x3\x2\x2\x2\t\x87"+
		"\x3\x2\x2\x2\v\x94\x3\x2\x2\x2\r\x9A\x3\x2\x2\x2\xF\xA5\x3\x2\x2\x2\x11"+
		"\xAC\x3\x2\x2\x2\x13\xB7\x3\x2\x2\x2\x15\xC4\x3\x2\x2\x2\x17\xCB\x3\x2"+
		"\x2\x2\x19\xD2\x3\x2\x2\x2\x1B\xD9\x3\x2\x2\x2\x1D\xE1\x3\x2\x2\x2\x1F"+
		"\xE6\x3\x2\x2\x2!\xEB\x3\x2\x2\x2#\xEE\x3\x2\x2\x2%\xF5\x3\x2\x2\x2\'"+
		"\xFC\x3\x2\x2\x2)\xFE\x3\x2\x2\x2+\x100\x3\x2\x2\x2-\x102\x3\x2\x2\x2"+
		"/\x104\x3\x2\x2\x2\x31\x106\x3\x2\x2\x2\x33\x108\x3\x2\x2\x2\x35\x10A"+
		"\x3\x2\x2\x2\x37\x10C\x3\x2\x2\x2\x39\x10E\x3\x2\x2\x2;\x110\x3\x2\x2"+
		"\x2=\x112\x3\x2\x2\x2?\x114\x3\x2\x2\x2\x41\x116\x3\x2\x2\x2\x43\x118"+
		"\x3\x2\x2\x2\x45\x11A\x3\x2\x2\x2G\x11D\x3\x2\x2\x2I\x11F\x3\x2\x2\x2"+
		"K\x122\x3\x2\x2\x2M\x126\x3\x2\x2\x2O\x129\x3\x2\x2\x2Q\x12D\x3\x2\x2"+
		"\x2S\x133\x3\x2\x2\x2U\x13A\x3\x2\x2\x2W\x141\x3\x2\x2\x2Y\x146\x3\x2"+
		"\x2\x2[\x156\x3\x2\x2\x2]\x15C\x3\x2\x2\x2_\x15E\x3\x2\x2\x2\x61\x160"+
		"\x3\x2\x2\x2\x63\x162\x3\x2\x2\x2\x65\x16D\x3\x2\x2\x2g\x170\x3\x2\x2"+
		"\x2i\x177\x3\x2\x2\x2k\x17D\x3\x2\x2\x2m\x180\x3\x2\x2\x2o\x183\x3\x2"+
		"\x2\x2qr\a\x66\x2\x2rs\aq\x2\x2st\ao\x2\x2tu\a\x63\x2\x2uv\ak\x2\x2vw"+
		"\ap\x2\x2w\x4\x3\x2\x2\x2xy\ar\x2\x2yz\at\x2\x2z{\aq\x2\x2{|\a\x64\x2"+
		"\x2|}\an\x2\x2}~\ag\x2\x2~\x7F\ao\x2\x2\x7F\x6\x3\x2\x2\x2\x80\x81\a\x66"+
		"\x2\x2\x81\x82\ag\x2\x2\x82\x83\ah\x2\x2\x83\x84\ak\x2\x2\x84\x85\ap\x2"+
		"\x2\x85\x86\ag\x2\x2\x86\b\x3\x2\x2\x2\x87\x88\at\x2\x2\x88\x89\ag\x2"+
		"\x2\x89\x8A\as\x2\x2\x8A\x8B\aw\x2\x2\x8B\x8C\ak\x2\x2\x8C\x8D\at\x2\x2"+
		"\x8D\x8E\ag\x2\x2\x8E\x8F\ao\x2\x2\x8F\x90\ag\x2\x2\x90\x91\ap\x2\x2\x91"+
		"\x92\av\x2\x2\x92\x93\au\x2\x2\x93\n\x3\x2\x2\x2\x94\x95\av\x2\x2\x95"+
		"\x96\a{\x2\x2\x96\x97\ar\x2\x2\x97\x98\ag\x2\x2\x98\x99\au\x2\x2\x99\f"+
		"\x3\x2\x2\x2\x9A\x9B\ar\x2\x2\x9B\x9C\at\x2\x2\x9C\x9D\ag\x2\x2\x9D\x9E"+
		"\a\x66\x2\x2\x9E\x9F\ak\x2\x2\x9F\xA0\a\x65\x2\x2\xA0\xA1\a\x63\x2\x2"+
		"\xA1\xA2\av\x2\x2\xA2\xA3\ag\x2\x2\xA3\xA4\au\x2\x2\xA4\xE\x3\x2\x2\x2"+
		"\xA5\xA6\a\x63\x2\x2\xA6\xA7\a\x65\x2\x2\xA7\xA8\av\x2\x2\xA8\xA9\ak\x2"+
		"\x2\xA9\xAA\aq\x2\x2\xAA\xAB\ap\x2\x2\xAB\x10\x3\x2\x2\x2\xAC\xAD\ar\x2"+
		"\x2\xAD\xAE\a\x63\x2\x2\xAE\xAF\at\x2\x2\xAF\xB0\a\x63\x2\x2\xB0\xB1\a"+
		"o\x2\x2\xB1\xB2\ag\x2\x2\xB2\xB3\av\x2\x2\xB3\xB4\ag\x2\x2\xB4\xB5\at"+
		"\x2\x2\xB5\xB6\au\x2\x2\xB6\x12\x3\x2\x2\x2\xB7\xB8\ar\x2\x2\xB8\xB9\a"+
		"t\x2\x2\xB9\xBA\ag\x2\x2\xBA\xBB\a\x65\x2\x2\xBB\xBC\aq\x2\x2\xBC\xBD"+
		"\ap\x2\x2\xBD\xBE\a\x66\x2\x2\xBE\xBF\ak\x2\x2\xBF\xC0\av\x2\x2\xC0\xC1"+
		"\ak\x2\x2\xC1\xC2\aq\x2\x2\xC2\xC3\ap\x2\x2\xC3\x14\x3\x2\x2\x2\xC4\xC5"+
		"\ag\x2\x2\xC5\xC6\ah\x2\x2\xC6\xC7\ah\x2\x2\xC7\xC8\ag\x2\x2\xC8\xC9\a"+
		"\x65\x2\x2\xC9\xCA\av\x2\x2\xCA\x16\x3\x2\x2\x2\xCB\xCC\aq\x2\x2\xCC\xCD"+
		"\a\x64\x2\x2\xCD\xCE\al\x2\x2\xCE\xCF\ag\x2\x2\xCF\xD0\a\x65\x2\x2\xD0"+
		"\xD1\av\x2\x2\xD1\x18\x3\x2\x2\x2\xD2\xD3\ag\x2\x2\xD3\xD4\ak\x2\x2\xD4"+
		"\xD5\av\x2\x2\xD5\xD6\aj\x2\x2\xD6\xD7\ag\x2\x2\xD7\xD8\at\x2\x2\xD8\x1A"+
		"\x3\x2\x2\x2\xD9\xDA\aq\x2\x2\xDA\xDB\a\x64\x2\x2\xDB\xDC\al\x2\x2\xDC"+
		"\xDD\ag\x2\x2\xDD\xDE\a\x65\x2\x2\xDE\xDF\av\x2\x2\xDF\xE0\au\x2\x2\xE0"+
		"\x1C\x3\x2\x2\x2\xE1\xE2\ak\x2\x2\xE2\xE3\ap\x2\x2\xE3\xE4\ak\x2\x2\xE4"+
		"\xE5\av\x2\x2\xE5\x1E\x3\x2\x2\x2\xE6\xE7\ai\x2\x2\xE7\xE8\aq\x2\x2\xE8"+
		"\xE9\a\x63\x2\x2\xE9\xEA\an\x2\x2\xEA \x3\x2\x2\x2\xEB\xEC\a\x63\x2\x2"+
		"\xEC\xED\av\x2\x2\xED\"\x3\x2\x2\x2\xEE\xEF\au\x2\x2\xEF\xF0\av\x2\x2"+
		"\xF0\xF1\at\x2\x2\xF1\xF2\ak\x2\x2\xF2\xF3\ar\x2\x2\xF3\xF4\au\x2\x2\xF4"+
		"$\x3\x2\x2\x2\xF5\xF6\av\x2\x2\xF6\xF7\a{\x2\x2\xF7\xF8\ar\x2\x2\xF8\xF9"+
		"\ak\x2\x2\xF9\xFA\ap\x2\x2\xFA\xFB\ai\x2\x2\xFB&\x3\x2\x2\x2\xFC\xFD\a"+
		"*\x2\x2\xFD(\x3\x2\x2\x2\xFE\xFF\a+\x2\x2\xFF*\x3\x2\x2\x2\x100\x101\a"+
		"]\x2\x2\x101,\x3\x2\x2\x2\x102\x103\a_\x2\x2\x103.\x3\x2\x2\x2\x104\x105"+
		"\a<\x2\x2\x105\x30\x3\x2\x2\x2\x106\x107\a\x41\x2\x2\x107\x32\x3\x2\x2"+
		"\x2\x108\x109\a\x30\x2\x2\x109\x34\x3\x2\x2\x2\x10A\x10B\a\x61\x2\x2\x10B"+
		"\x36\x3\x2\x2\x2\x10C\x10D\a/\x2\x2\x10D\x38\x3\x2\x2\x2\x10E\x10F\a-"+
		"\x2\x2\x10F:\x3\x2\x2\x2\x110\x111\a/\x2\x2\x111<\x3\x2\x2\x2\x112\x113"+
		"\a,\x2\x2\x113>\x3\x2\x2\x2\x114\x115\a\x31\x2\x2\x115@\x3\x2\x2\x2\x116"+
		"\x117\a?\x2\x2\x117\x42\x3\x2\x2\x2\x118\x119\a>\x2\x2\x119\x44\x3\x2"+
		"\x2\x2\x11A\x11B\a>\x2\x2\x11B\x11C\a?\x2\x2\x11C\x46\x3\x2\x2\x2\x11D"+
		"\x11E\a@\x2\x2\x11EH\x3\x2\x2\x2\x11F\x120\a@\x2\x2\x120\x121\a?\x2\x2"+
		"\x121J\x3\x2\x2\x2\x122\x123\a\x63\x2\x2\x123\x124\ap\x2\x2\x124\x125"+
		"\a\x66\x2\x2\x125L\x3\x2\x2\x2\x126\x127\aq\x2\x2\x127\x128\at\x2\x2\x128"+
		"N\x3\x2\x2\x2\x129\x12A\ap\x2\x2\x12A\x12B\aq\x2\x2\x12B\x12C\av\x2\x2"+
		"\x12CP\x3\x2\x2\x2\x12D\x12E\ak\x2\x2\x12E\x12F\ao\x2\x2\x12F\x130\ar"+
		"\x2\x2\x130\x131\an\x2\x2\x131\x132\a{\x2\x2\x132R\x3\x2\x2\x2\x133\x134"+
		"\ah\x2\x2\x134\x135\aq\x2\x2\x135\x136\at\x2\x2\x136\x137\a\x63\x2\x2"+
		"\x137\x138\an\x2\x2\x138\x139\an\x2\x2\x139T\x3\x2\x2\x2\x13A\x13B\ag"+
		"\x2\x2\x13B\x13C\az\x2\x2\x13C\x13D\ak\x2\x2\x13D\x13E\au\x2\x2\x13E\x13F"+
		"\av\x2\x2\x13F\x140\au\x2\x2\x140V\x3\x2\x2\x2\x141\x142\ay\x2\x2\x142"+
		"\x143\aj\x2\x2\x143\x144\ag\x2\x2\x144\x145\ap\x2\x2\x145X\x3\x2\x2\x2"+
		"\x146\x147\ar\x2\x2\x147\x148\at\x2\x2\x148\x149\ag\x2\x2\x149\x14A\a"+
		"h\x2\x2\x14A\x14B\ag\x2\x2\x14B\x14C\at\x2\x2\x14C\x14D\ag\x2\x2\x14D"+
		"\x14E\ap\x2\x2\x14E\x14F\a\x65\x2\x2\x14F\x150\ag\x2\x2\x150Z\x3\x2\x2"+
		"\x2\x151\x157\x5\x41!\x2\x152\x157\x5\x43\"\x2\x153\x157\x5G$\x2\x154"+
		"\x157\x5\x45#\x2\x155\x157\x5I%\x2\x156\x151\x3\x2\x2\x2\x156\x152\x3"+
		"\x2\x2\x2\x156\x153\x3\x2\x2\x2\x156\x154\x3\x2\x2\x2\x156\x155\x3\x2"+
		"\x2\x2\x157\\\x3\x2\x2\x2\x158\x15D\x5\x39\x1D\x2\x159\x15D\x5;\x1E\x2"+
		"\x15A\x15D\x5=\x1F\x2\x15B\x15D\x5? \x2\x15C\x158\x3\x2\x2\x2\x15C\x159"+
		"\x3\x2\x2\x2\x15C\x15A\x3\x2\x2\x2\x15C\x15B\x3\x2\x2\x2\x15D^\x3\x2\x2"+
		"\x2\x15E\x15F\t\x2\x2\x2\x15F`\x3\x2\x2\x2\x160\x161\t\x3\x2\x2\x161\x62"+
		"\x3\x2\x2\x2\x162\x166\x5_\x30\x2\x163\x165\x5\x65\x33\x2\x164\x163\x3"+
		"\x2\x2\x2\x165\x168\x3\x2\x2\x2\x166\x164\x3\x2\x2\x2\x166\x167\x3\x2"+
		"\x2\x2\x167\x64\x3\x2\x2\x2\x168\x166\x3\x2\x2\x2\x169\x16E\x5_\x30\x2"+
		"\x16A\x16E\x5\x61\x31\x2\x16B\x16E\x5\x37\x1C\x2\x16C\x16E\x5\x35\x1B"+
		"\x2\x16D\x169\x3\x2\x2\x2\x16D\x16A\x3\x2\x2\x2\x16D\x16B\x3\x2\x2\x2"+
		"\x16D\x16C\x3\x2\x2\x2\x16E\x66\x3\x2\x2\x2\x16F\x171\x5\x61\x31\x2\x170"+
		"\x16F\x3\x2\x2\x2\x171\x172\x3\x2\x2\x2\x172\x170\x3\x2\x2\x2\x172\x173"+
		"\x3\x2\x2\x2\x173\x175\x3\x2\x2\x2\x174\x176\x5i\x35\x2\x175\x174\x3\x2"+
		"\x2\x2\x175\x176\x3\x2\x2\x2\x176h\x3\x2\x2\x2\x177\x179\x5\x33\x1A\x2"+
		"\x178\x17A\x5\x61\x31\x2\x179\x178\x3\x2\x2\x2\x17A\x17B\x3\x2\x2\x2\x17B"+
		"\x179\x3\x2\x2\x2\x17B\x17C\x3\x2\x2\x2\x17Cj\x3\x2\x2\x2\x17D\x17E\x5"+
		"\x31\x19\x2\x17E\x17F\x5\x63\x32\x2\x17Fl\x3\x2\x2\x2\x180\x181\x5\x63"+
		"\x32\x2\x181n\x3\x2\x2\x2\x182\x184\t\x4\x2\x2\x183\x182\x3\x2\x2\x2\x184"+
		"\x185\x3\x2\x2\x2\x185\x183\x3\x2\x2\x2\x185\x186\x3\x2\x2\x2\x186\x187"+
		"\x3\x2\x2\x2\x187\x188\b\x38\x2\x2\x188p\x3\x2\x2\x2\v\x2\x156\x15C\x166"+
		"\x16D\x172\x175\x17B\x185\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LanguageRecognition
