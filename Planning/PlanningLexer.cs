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
		OBJ=11, EITHER=12, OBJS=13, INIT=14, STRIPS=15, TYPING=16, LB=17, RB=18, 
		LSB=19, RSB=20, COLON=21, QM=22, COMMA=23, UL=24, DASH=25, PLUS=26, MINUS=27, 
		MULT=28, DIV=29, EQ=30, LT=31, LEQ=32, GT=33, GEQ=34, AND=35, OR=36, NOT=37, 
		IMPLY=38, FORALL=39, EXISTS=40, WHEN=41, PREF=42, BINCOMP=43, BINOP=44, 
		LETTER=45, DIGIT=46, NAME=47, CHAR=48, NUMBER=49, DECIMAL=50, VAR=51, 
		FUNSYM=52, WS=53;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] tokenNames = {
		"<INVALID>",
		"'domain'", "'problem'", "'define'", "'requirements'", "'types'", "'predicates'", 
		"'action'", "'parameters'", "'precondition'", "'effect'", "'object'", 
		"'either'", "'objects'", "'init'", "'strips'", "'typing'", "'('", "')'", 
		"'['", "']'", "':'", "'?'", "'.'", "'_'", "DASH", "'+'", "MINUS", "'*'", 
		"'/'", "'='", "'<'", "'<='", "'>'", "'>='", "'and'", "'or'", "'not'", 
		"'imply'", "'forall'", "'exists'", "'when'", "'preference'", "BINCOMP", 
		"BINOP", "LETTER", "DIGIT", "NAME", "CHAR", "NUMBER", "DECIMAL", "VAR", 
		"FUNSYM", "WS"
	};
	public static readonly string[] ruleNames = {
		"DOM", "PROM", "DEF", "REQ", "TYPE", "PRED", "ACT", "PARM", "PRE", "EFF", 
		"OBJ", "EITHER", "OBJS", "INIT", "STRIPS", "TYPING", "LB", "RB", "LSB", 
		"RSB", "COLON", "QM", "COMMA", "UL", "DASH", "PLUS", "MINUS", "MULT", 
		"DIV", "EQ", "LT", "LEQ", "GT", "GEQ", "AND", "OR", "NOT", "IMPLY", "FORALL", 
		"EXISTS", "WHEN", "PREF", "BINCOMP", "BINOP", "LETTER", "DIGIT", "NAME", 
		"CHAR", "NUMBER", "DECIMAL", "VAR", "FUNSYM", "WS"
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x37\x17D\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A"+
		"\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 "+
		"\t \x4!\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t"+
		")\x4*\t*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31"+
		"\x4\x32\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35\x4\x36\t\x36\x3\x2"+
		"\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5"+
		"\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3"+
		"\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3"+
		"\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3"+
		"\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3"+
		"\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3"+
		"\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3"+
		"\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\xF"+
		"\x3\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11"+
		"\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x13\x3\x13\x3\x14"+
		"\x3\x14\x3\x15\x3\x15\x3\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18\x3\x19"+
		"\x3\x19\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1E"+
		"\x3\x1E\x3\x1F\x3\x1F\x3 \x3 \x3!\x3!\x3!\x3\"\x3\"\x3#\x3#\x3#\x3$\x3"+
		"$\x3$\x3$\x3%\x3%\x3%\x3&\x3&\x3&\x3&\x3\'\x3\'\x3\'\x3\'\x3\'\x3\'\x3"+
		"(\x3(\x3(\x3(\x3(\x3(\x3(\x3)\x3)\x3)\x3)\x3)\x3)\x3)\x3*\x3*\x3*\x3*"+
		"\x3*\x3+\x3+\x3+\x3+\x3+\x3+\x3+\x3+\x3+\x3+\x3+\x3,\x3,\x3,\x3,\x3,\x5"+
		",\x14B\n,\x3-\x3-\x3-\x3-\x5-\x151\n-\x3.\x3.\x3/\x3/\x3\x30\x3\x30\a"+
		"\x30\x159\n\x30\f\x30\xE\x30\x15C\v\x30\x3\x31\x3\x31\x3\x31\x3\x31\x5"+
		"\x31\x162\n\x31\x3\x32\x6\x32\x165\n\x32\r\x32\xE\x32\x166\x3\x32\x5\x32"+
		"\x16A\n\x32\x3\x33\x3\x33\x6\x33\x16E\n\x33\r\x33\xE\x33\x16F\x3\x34\x3"+
		"\x34\x3\x34\x3\x35\x3\x35\x3\x36\x6\x36\x178\n\x36\r\x36\xE\x36\x179\x3"+
		"\x36\x3\x36\x2\x2\x2\x37\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2"+
		"\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D"+
		"\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2"+
		"\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2"+
		"\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2"+
		"*S\x2+U\x2,W\x2-Y\x2.[\x2/]\x2\x30_\x2\x31\x61\x2\x32\x63\x2\x33\x65\x2"+
		"\x34g\x2\x35i\x2\x36k\x2\x37\x3\x2\x5\x4\x2\x43\\\x63|\x3\x2\x32;\x5\x2"+
		"\v\f\xF\xF\"\"\x18B\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2"+
		"\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2"+
		"\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3"+
		"\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2"+
		"\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'"+
		"\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2"+
		"\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37"+
		"\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3"+
		"\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2"+
		"G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x2O\x3\x2"+
		"\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3\x2\x2\x2\x2U\x3\x2\x2\x2\x2W\x3\x2\x2\x2"+
		"\x2Y\x3\x2\x2\x2\x2[\x3\x2\x2\x2\x2]\x3\x2\x2\x2\x2_\x3\x2\x2\x2\x2\x61"+
		"\x3\x2\x2\x2\x2\x63\x3\x2\x2\x2\x2\x65\x3\x2\x2\x2\x2g\x3\x2\x2\x2\x2"+
		"i\x3\x2\x2\x2\x2k\x3\x2\x2\x2\x3m\x3\x2\x2\x2\x5t\x3\x2\x2\x2\a|\x3\x2"+
		"\x2\x2\t\x83\x3\x2\x2\x2\v\x90\x3\x2\x2\x2\r\x96\x3\x2\x2\x2\xF\xA1\x3"+
		"\x2\x2\x2\x11\xA8\x3\x2\x2\x2\x13\xB3\x3\x2\x2\x2\x15\xC0\x3\x2\x2\x2"+
		"\x17\xC7\x3\x2\x2\x2\x19\xCE\x3\x2\x2\x2\x1B\xD5\x3\x2\x2\x2\x1D\xDD\x3"+
		"\x2\x2\x2\x1F\xE2\x3\x2\x2\x2!\xE9\x3\x2\x2\x2#\xF0\x3\x2\x2\x2%\xF2\x3"+
		"\x2\x2\x2\'\xF4\x3\x2\x2\x2)\xF6\x3\x2\x2\x2+\xF8\x3\x2\x2\x2-\xFA\x3"+
		"\x2\x2\x2/\xFC\x3\x2\x2\x2\x31\xFE\x3\x2\x2\x2\x33\x100\x3\x2\x2\x2\x35"+
		"\x102\x3\x2\x2\x2\x37\x104\x3\x2\x2\x2\x39\x106\x3\x2\x2\x2;\x108\x3\x2"+
		"\x2\x2=\x10A\x3\x2\x2\x2?\x10C\x3\x2\x2\x2\x41\x10E\x3\x2\x2\x2\x43\x111"+
		"\x3\x2\x2\x2\x45\x113\x3\x2\x2\x2G\x116\x3\x2\x2\x2I\x11A\x3\x2\x2\x2"+
		"K\x11D\x3\x2\x2\x2M\x121\x3\x2\x2\x2O\x127\x3\x2\x2\x2Q\x12E\x3\x2\x2"+
		"\x2S\x135\x3\x2\x2\x2U\x13A\x3\x2\x2\x2W\x14A\x3\x2\x2\x2Y\x150\x3\x2"+
		"\x2\x2[\x152\x3\x2\x2\x2]\x154\x3\x2\x2\x2_\x156\x3\x2\x2\x2\x61\x161"+
		"\x3\x2\x2\x2\x63\x164\x3\x2\x2\x2\x65\x16B\x3\x2\x2\x2g\x171\x3\x2\x2"+
		"\x2i\x174\x3\x2\x2\x2k\x177\x3\x2\x2\x2mn\a\x66\x2\x2no\aq\x2\x2op\ao"+
		"\x2\x2pq\a\x63\x2\x2qr\ak\x2\x2rs\ap\x2\x2s\x4\x3\x2\x2\x2tu\ar\x2\x2"+
		"uv\at\x2\x2vw\aq\x2\x2wx\a\x64\x2\x2xy\an\x2\x2yz\ag\x2\x2z{\ao\x2\x2"+
		"{\x6\x3\x2\x2\x2|}\a\x66\x2\x2}~\ag\x2\x2~\x7F\ah\x2\x2\x7F\x80\ak\x2"+
		"\x2\x80\x81\ap\x2\x2\x81\x82\ag\x2\x2\x82\b\x3\x2\x2\x2\x83\x84\at\x2"+
		"\x2\x84\x85\ag\x2\x2\x85\x86\as\x2\x2\x86\x87\aw\x2\x2\x87\x88\ak\x2\x2"+
		"\x88\x89\at\x2\x2\x89\x8A\ag\x2\x2\x8A\x8B\ao\x2\x2\x8B\x8C\ag\x2\x2\x8C"+
		"\x8D\ap\x2\x2\x8D\x8E\av\x2\x2\x8E\x8F\au\x2\x2\x8F\n\x3\x2\x2\x2\x90"+
		"\x91\av\x2\x2\x91\x92\a{\x2\x2\x92\x93\ar\x2\x2\x93\x94\ag\x2\x2\x94\x95"+
		"\au\x2\x2\x95\f\x3\x2\x2\x2\x96\x97\ar\x2\x2\x97\x98\at\x2\x2\x98\x99"+
		"\ag\x2\x2\x99\x9A\a\x66\x2\x2\x9A\x9B\ak\x2\x2\x9B\x9C\a\x65\x2\x2\x9C"+
		"\x9D\a\x63\x2\x2\x9D\x9E\av\x2\x2\x9E\x9F\ag\x2\x2\x9F\xA0\au\x2\x2\xA0"+
		"\xE\x3\x2\x2\x2\xA1\xA2\a\x63\x2\x2\xA2\xA3\a\x65\x2\x2\xA3\xA4\av\x2"+
		"\x2\xA4\xA5\ak\x2\x2\xA5\xA6\aq\x2\x2\xA6\xA7\ap\x2\x2\xA7\x10\x3\x2\x2"+
		"\x2\xA8\xA9\ar\x2\x2\xA9\xAA\a\x63\x2\x2\xAA\xAB\at\x2\x2\xAB\xAC\a\x63"+
		"\x2\x2\xAC\xAD\ao\x2\x2\xAD\xAE\ag\x2\x2\xAE\xAF\av\x2\x2\xAF\xB0\ag\x2"+
		"\x2\xB0\xB1\at\x2\x2\xB1\xB2\au\x2\x2\xB2\x12\x3\x2\x2\x2\xB3\xB4\ar\x2"+
		"\x2\xB4\xB5\at\x2\x2\xB5\xB6\ag\x2\x2\xB6\xB7\a\x65\x2\x2\xB7\xB8\aq\x2"+
		"\x2\xB8\xB9\ap\x2\x2\xB9\xBA\a\x66\x2\x2\xBA\xBB\ak\x2\x2\xBB\xBC\av\x2"+
		"\x2\xBC\xBD\ak\x2\x2\xBD\xBE\aq\x2\x2\xBE\xBF\ap\x2\x2\xBF\x14\x3\x2\x2"+
		"\x2\xC0\xC1\ag\x2\x2\xC1\xC2\ah\x2\x2\xC2\xC3\ah\x2\x2\xC3\xC4\ag\x2\x2"+
		"\xC4\xC5\a\x65\x2\x2\xC5\xC6\av\x2\x2\xC6\x16\x3\x2\x2\x2\xC7\xC8\aq\x2"+
		"\x2\xC8\xC9\a\x64\x2\x2\xC9\xCA\al\x2\x2\xCA\xCB\ag\x2\x2\xCB\xCC\a\x65"+
		"\x2\x2\xCC\xCD\av\x2\x2\xCD\x18\x3\x2\x2\x2\xCE\xCF\ag\x2\x2\xCF\xD0\a"+
		"k\x2\x2\xD0\xD1\av\x2\x2\xD1\xD2\aj\x2\x2\xD2\xD3\ag\x2\x2\xD3\xD4\at"+
		"\x2\x2\xD4\x1A\x3\x2\x2\x2\xD5\xD6\aq\x2\x2\xD6\xD7\a\x64\x2\x2\xD7\xD8"+
		"\al\x2\x2\xD8\xD9\ag\x2\x2\xD9\xDA\a\x65\x2\x2\xDA\xDB\av\x2\x2\xDB\xDC"+
		"\au\x2\x2\xDC\x1C\x3\x2\x2\x2\xDD\xDE\ak\x2\x2\xDE\xDF\ap\x2\x2\xDF\xE0"+
		"\ak\x2\x2\xE0\xE1\av\x2\x2\xE1\x1E\x3\x2\x2\x2\xE2\xE3\au\x2\x2\xE3\xE4"+
		"\av\x2\x2\xE4\xE5\at\x2\x2\xE5\xE6\ak\x2\x2\xE6\xE7\ar\x2\x2\xE7\xE8\a"+
		"u\x2\x2\xE8 \x3\x2\x2\x2\xE9\xEA\av\x2\x2\xEA\xEB\a{\x2\x2\xEB\xEC\ar"+
		"\x2\x2\xEC\xED\ak\x2\x2\xED\xEE\ap\x2\x2\xEE\xEF\ai\x2\x2\xEF\"\x3\x2"+
		"\x2\x2\xF0\xF1\a*\x2\x2\xF1$\x3\x2\x2\x2\xF2\xF3\a+\x2\x2\xF3&\x3\x2\x2"+
		"\x2\xF4\xF5\a]\x2\x2\xF5(\x3\x2\x2\x2\xF6\xF7\a_\x2\x2\xF7*\x3\x2\x2\x2"+
		"\xF8\xF9\a<\x2\x2\xF9,\x3\x2\x2\x2\xFA\xFB\a\x41\x2\x2\xFB.\x3\x2\x2\x2"+
		"\xFC\xFD\a\x30\x2\x2\xFD\x30\x3\x2\x2\x2\xFE\xFF\a\x61\x2\x2\xFF\x32\x3"+
		"\x2\x2\x2\x100\x101\a/\x2\x2\x101\x34\x3\x2\x2\x2\x102\x103\a-\x2\x2\x103"+
		"\x36\x3\x2\x2\x2\x104\x105\a/\x2\x2\x105\x38\x3\x2\x2\x2\x106\x107\a,"+
		"\x2\x2\x107:\x3\x2\x2\x2\x108\x109\a\x31\x2\x2\x109<\x3\x2\x2\x2\x10A"+
		"\x10B\a?\x2\x2\x10B>\x3\x2\x2\x2\x10C\x10D\a>\x2\x2\x10D@\x3\x2\x2\x2"+
		"\x10E\x10F\a>\x2\x2\x10F\x110\a?\x2\x2\x110\x42\x3\x2\x2\x2\x111\x112"+
		"\a@\x2\x2\x112\x44\x3\x2\x2\x2\x113\x114\a@\x2\x2\x114\x115\a?\x2\x2\x115"+
		"\x46\x3\x2\x2\x2\x116\x117\a\x63\x2\x2\x117\x118\ap\x2\x2\x118\x119\a"+
		"\x66\x2\x2\x119H\x3\x2\x2\x2\x11A\x11B\aq\x2\x2\x11B\x11C\at\x2\x2\x11C"+
		"J\x3\x2\x2\x2\x11D\x11E\ap\x2\x2\x11E\x11F\aq\x2\x2\x11F\x120\av\x2\x2"+
		"\x120L\x3\x2\x2\x2\x121\x122\ak\x2\x2\x122\x123\ao\x2\x2\x123\x124\ar"+
		"\x2\x2\x124\x125\an\x2\x2\x125\x126\a{\x2\x2\x126N\x3\x2\x2\x2\x127\x128"+
		"\ah\x2\x2\x128\x129\aq\x2\x2\x129\x12A\at\x2\x2\x12A\x12B\a\x63\x2\x2"+
		"\x12B\x12C\an\x2\x2\x12C\x12D\an\x2\x2\x12DP\x3\x2\x2\x2\x12E\x12F\ag"+
		"\x2\x2\x12F\x130\az\x2\x2\x130\x131\ak\x2\x2\x131\x132\au\x2\x2\x132\x133"+
		"\av\x2\x2\x133\x134\au\x2\x2\x134R\x3\x2\x2\x2\x135\x136\ay\x2\x2\x136"+
		"\x137\aj\x2\x2\x137\x138\ag\x2\x2\x138\x139\ap\x2\x2\x139T\x3\x2\x2\x2"+
		"\x13A\x13B\ar\x2\x2\x13B\x13C\at\x2\x2\x13C\x13D\ag\x2\x2\x13D\x13E\a"+
		"h\x2\x2\x13E\x13F\ag\x2\x2\x13F\x140\at\x2\x2\x140\x141\ag\x2\x2\x141"+
		"\x142\ap\x2\x2\x142\x143\a\x65\x2\x2\x143\x144\ag\x2\x2\x144V\x3\x2\x2"+
		"\x2\x145\x14B\x5=\x1F\x2\x146\x14B\x5? \x2\x147\x14B\x5\x43\"\x2\x148"+
		"\x14B\x5\x41!\x2\x149\x14B\x5\x45#\x2\x14A\x145\x3\x2\x2\x2\x14A\x146"+
		"\x3\x2\x2\x2\x14A\x147\x3\x2\x2\x2\x14A\x148\x3\x2\x2\x2\x14A\x149\x3"+
		"\x2\x2\x2\x14BX\x3\x2\x2\x2\x14C\x151\x5\x35\x1B\x2\x14D\x151\x5\x37\x1C"+
		"\x2\x14E\x151\x5\x39\x1D\x2\x14F\x151\x5;\x1E\x2\x150\x14C\x3\x2\x2\x2"+
		"\x150\x14D\x3\x2\x2\x2\x150\x14E\x3\x2\x2\x2\x150\x14F\x3\x2\x2\x2\x151"+
		"Z\x3\x2\x2\x2\x152\x153\t\x2\x2\x2\x153\\\x3\x2\x2\x2\x154\x155\t\x3\x2"+
		"\x2\x155^\x3\x2\x2\x2\x156\x15A\x5[.\x2\x157\x159\x5\x61\x31\x2\x158\x157"+
		"\x3\x2\x2\x2\x159\x15C\x3\x2\x2\x2\x15A\x158\x3\x2\x2\x2\x15A\x15B\x3"+
		"\x2\x2\x2\x15B`\x3\x2\x2\x2\x15C\x15A\x3\x2\x2\x2\x15D\x162\x5[.\x2\x15E"+
		"\x162\x5]/\x2\x15F\x162\x5\x33\x1A\x2\x160\x162\x5\x31\x19\x2\x161\x15D"+
		"\x3\x2\x2\x2\x161\x15E\x3\x2\x2\x2\x161\x15F\x3\x2\x2\x2\x161\x160\x3"+
		"\x2\x2\x2\x162\x62\x3\x2\x2\x2\x163\x165\x5]/\x2\x164\x163\x3\x2\x2\x2"+
		"\x165\x166\x3\x2\x2\x2\x166\x164\x3\x2\x2\x2\x166\x167\x3\x2\x2\x2\x167"+
		"\x169\x3\x2\x2\x2\x168\x16A\x5\x65\x33\x2\x169\x168\x3\x2\x2\x2\x169\x16A"+
		"\x3\x2\x2\x2\x16A\x64\x3\x2\x2\x2\x16B\x16D\x5/\x18\x2\x16C\x16E\x5]/"+
		"\x2\x16D\x16C\x3\x2\x2\x2\x16E\x16F\x3\x2\x2\x2\x16F\x16D\x3\x2\x2\x2"+
		"\x16F\x170\x3\x2\x2\x2\x170\x66\x3\x2\x2\x2\x171\x172\x5-\x17\x2\x172"+
		"\x173\x5_\x30\x2\x173h\x3\x2\x2\x2\x174\x175\x5_\x30\x2\x175j\x3\x2\x2"+
		"\x2\x176\x178\t\x4\x2\x2\x177\x176\x3\x2\x2\x2\x178\x179\x3\x2\x2\x2\x179"+
		"\x177\x3\x2\x2\x2\x179\x17A\x3\x2\x2\x2\x17A\x17B\x3\x2\x2\x2\x17B\x17C"+
		"\b\x36\x2\x2\x17Cl\x3\x2\x2\x2\v\x2\x14A\x150\x15A\x161\x166\x169\x16F"+
		"\x179\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LanguageRecognition
