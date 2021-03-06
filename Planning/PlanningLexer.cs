//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from E:\EDGolog\LanguageRecognition\Planning.g4 by ANTLR 4.3

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

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.3")]
[System.CLSCompliant(false)]
public partial class PlanningLexer : Lexer {
	public const int
		DOMAIN=1, PROBLEM=2, DEFINE=3, AGENTID=4, CONST=5, TYPE=6, PREDICATE=7, 
		ACTION=8, EVENT=9, EVENTS=10, PLDEGREE=11, EVENTMODEL=12, PARAMETER=13, 
		PRECONDITION=14, RESPONSE=15, OBSERVATION=16, MIN=17, MAX=18, NUMS=19, 
		EFFECT=20, OBJECT=21, AGENT=22, EITHER=23, INITKNOWLEDGE=24, INITBELIEF=25, 
		SEQ=26, IF=27, WHILE=28, KNOW=29, BEL=30, OBJS=31, INIT=32, GOAL=33, LB=34, 
		RB=35, LSB=36, RSB=37, COLON=38, QM=39, POINT=40, UL=41, MINUS=42, PLUS=43, 
		MULT=44, DIV=45, EQ=46, NEQ=47, LT=48, LEQ=49, GT=50, GEQ=51, AND=52, 
		OR=53, NOT=54, ONEOF=55, IMPLY=56, FORALL=57, EXISTS=58, WHEN=59, NAME=60, 
		INTEGER=61, VAR=62, WS=63;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] tokenNames = {
		"'\\u0000'", "'\\u0001'", "'\\u0002'", "'\\u0003'", "'\\u0004'", "'\\u0005'", 
		"'\\u0006'", "'\\u0007'", "'\b'", "'\t'", "'\n'", "'\\u000B'", "'\f'", 
		"'\r'", "'\\u000E'", "'\\u000F'", "'\\u0010'", "'\\u0011'", "'\\u0012'", 
		"'\\u0013'", "'\\u0014'", "'\\u0015'", "'\\u0016'", "'\\u0017'", "'\\u0018'", 
		"'\\u0019'", "'\\u001A'", "'\\u001B'", "'\\u001C'", "'\\u001D'", "'\\u001E'", 
		"'\\u001F'", "' '", "'!'", "'\"'", "'#'", "'$'", "'%'", "'&'", "'''", 
		"'('", "')'", "'*'", "'+'", "','", "'-'", "'.'", "'/'", "'0'", "'1'", 
		"'2'", "'3'", "'4'", "'5'", "'6'", "'7'", "'8'", "'9'", "':'", "';'", 
		"'<'", "'='", "'>'", "'?'"
	};
	public static readonly string[] ruleNames = {
		"DOMAIN", "PROBLEM", "DEFINE", "AGENTID", "CONST", "TYPE", "PREDICATE", 
		"ACTION", "EVENT", "EVENTS", "PLDEGREE", "EVENTMODEL", "PARAMETER", "PRECONDITION", 
		"RESPONSE", "OBSERVATION", "MIN", "MAX", "NUMS", "EFFECT", "OBJECT", "AGENT", 
		"EITHER", "INITKNOWLEDGE", "INITBELIEF", "SEQ", "IF", "WHILE", "KNOW", 
		"BEL", "OBJS", "INIT", "GOAL", "LB", "RB", "LSB", "RSB", "COLON", "QM", 
		"POINT", "UL", "MINUS", "PLUS", "MULT", "DIV", "EQ", "NEQ", "LT", "LEQ", 
		"GT", "GEQ", "AND", "OR", "NOT", "ONEOF", "IMPLY", "FORALL", "EXISTS", 
		"WHEN", "NAME", "INTEGER", "LETTER", "DIGIT", "CHAR", "VAR", "WS"
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x41\x1F2\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A"+
		"\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 "+
		"\t \x4!\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t"+
		")\x4*\t*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31"+
		"\x4\x32\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35\x4\x36\t\x36\x4\x37"+
		"\t\x37\x4\x38\t\x38\x4\x39\t\x39\x4:\t:\x4;\t;\x4<\t<\x4=\t=\x4>\t>\x4"+
		"?\t?\x4@\t@\x4\x41\t\x41\x4\x42\t\x42\x4\x43\t\x43\x3\x2\x3\x2\x3\x2\x3"+
		"\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3"+
		"\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6"+
		"\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3"+
		"\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\n\x3"+
		"\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\f\x3\f\x3"+
		"\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3"+
		"\r\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE"+
		"\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3"+
		"\xF\x3\xF\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10"+
		"\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11"+
		"\x3\x11\x3\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13"+
		"\x3\x13\x3\x13\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14\x3\x14"+
		"\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16"+
		"\x3\x16\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17"+
		"\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x19\x3\x19\x3\x19"+
		"\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19"+
		"\x3\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1A"+
		"\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1D"+
		"\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E"+
		"\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3!\x3!\x3"+
		"!\x3!\x3!\x3\"\x3\"\x3\"\x3\"\x3\"\x3#\x3#\x3$\x3$\x3%\x3%\x3&\x3&\x3"+
		"\'\x3\'\x3(\x3(\x3)\x3)\x3*\x3*\x3+\x3+\x3,\x3,\x3-\x3-\x3.\x3.\x3/\x3"+
		"/\x3\x30\x3\x30\x3\x30\x3\x31\x3\x31\x3\x32\x3\x32\x3\x32\x3\x33\x3\x33"+
		"\x3\x34\x3\x34\x3\x34\x3\x35\x3\x35\x3\x35\x3\x35\x3\x36\x3\x36\x3\x36"+
		"\x3\x37\x3\x37\x3\x37\x3\x37\x3\x38\x3\x38\x3\x38\x3\x38\x3\x38\x3\x38"+
		"\x3\x39\x3\x39\x3\x39\x3\x39\x3\x39\x3\x39\x3:\x3:\x3:\x3:\x3:\x3:\x3"+
		":\x3;\x3;\x3;\x3;\x3;\x3;\x3;\x3<\x3<\x3<\x3<\x3<\x3=\x3=\a=\x1D5\n=\f"+
		"=\xE=\x1D8\v=\x3>\x6>\x1DB\n>\r>\xE>\x1DC\x3?\x3?\x3@\x3@\x3\x41\x3\x41"+
		"\x3\x41\x3\x41\x5\x41\x1E7\n\x41\x3\x42\x3\x42\x3\x42\x3\x43\x6\x43\x1ED"+
		"\n\x43\r\x43\xE\x43\x1EE\x3\x43\x3\x43\x2\x2\x2\x44\x3\x2\x3\x5\x2\x4"+
		"\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17"+
		"\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14"+
		"\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2"+
		"\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$"+
		"G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2*S\x2+U\x2,W\x2-Y\x2.[\x2/]\x2\x30_\x2\x31"+
		"\x61\x2\x32\x63\x2\x33\x65\x2\x34g\x2\x35i\x2\x36k\x2\x37m\x2\x38o\x2"+
		"\x39q\x2:s\x2;u\x2<w\x2=y\x2>{\x2?}\x2\x2\x7F\x2\x2\x81\x2\x2\x83\x2@"+
		"\x85\x2\x41\x3\x2\x5\x4\x2\x43\\\x63|\x3\x2\x32;\x5\x2\v\f\xF\xF\"\"\x1F4"+
		"\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2"+
		"\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2"+
		"\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2"+
		"\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3"+
		"\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2"+
		"\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2"+
		"\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2"+
		"\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2"+
		"\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2"+
		"\x2\x2K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2"+
		"S\x3\x2\x2\x2\x2U\x3\x2\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2"+
		"\x2\x2\x2]\x3\x2\x2\x2\x2_\x3\x2\x2\x2\x2\x61\x3\x2\x2\x2\x2\x63\x3\x2"+
		"\x2\x2\x2\x65\x3\x2\x2\x2\x2g\x3\x2\x2\x2\x2i\x3\x2\x2\x2\x2k\x3\x2\x2"+
		"\x2\x2m\x3\x2\x2\x2\x2o\x3\x2\x2\x2\x2q\x3\x2\x2\x2\x2s\x3\x2\x2\x2\x2"+
		"u\x3\x2\x2\x2\x2w\x3\x2\x2\x2\x2y\x3\x2\x2\x2\x2{\x3\x2\x2\x2\x2\x83\x3"+
		"\x2\x2\x2\x2\x85\x3\x2\x2\x2\x3\x87\x3\x2\x2\x2\x5\x8E\x3\x2\x2\x2\a\x96"+
		"\x3\x2\x2\x2\t\x9D\x3\x2\x2\x2\v\xA5\x3\x2\x2\x2\r\xAF\x3\x2\x2\x2\xF"+
		"\xB5\x3\x2\x2\x2\x11\xC0\x3\x2\x2\x2\x13\xC7\x3\x2\x2\x2\x15\xCD\x3\x2"+
		"\x2\x2\x17\xD4\x3\x2\x2\x2\x19\xDD\x3\x2\x2\x2\x1B\xE8\x3\x2\x2\x2\x1D"+
		"\xF3\x3\x2\x2\x2\x1F\x100\x3\x2\x2\x2!\x109\x3\x2\x2\x2#\x115\x3\x2\x2"+
		"\x2%\x119\x3\x2\x2\x2\'\x11D\x3\x2\x2\x2)\x125\x3\x2\x2\x2+\x12C\x3\x2"+
		"\x2\x2-\x133\x3\x2\x2\x2/\x139\x3\x2\x2\x2\x31\x140\x3\x2\x2\x2\x33\x14E"+
		"\x3\x2\x2\x2\x35\x159\x3\x2\x2\x2\x37\x15D\x3\x2\x2\x2\x39\x160\x3\x2"+
		"\x2\x2;\x166\x3\x2\x2\x2=\x16B\x3\x2\x2\x2?\x16F\x3\x2\x2\x2\x41\x177"+
		"\x3\x2\x2\x2\x43\x17C\x3\x2\x2\x2\x45\x181\x3\x2\x2\x2G\x183\x3\x2\x2"+
		"\x2I\x185\x3\x2\x2\x2K\x187\x3\x2\x2\x2M\x189\x3\x2\x2\x2O\x18B\x3\x2"+
		"\x2\x2Q\x18D\x3\x2\x2\x2S\x18F\x3\x2\x2\x2U\x191\x3\x2\x2\x2W\x193\x3"+
		"\x2\x2\x2Y\x195\x3\x2\x2\x2[\x197\x3\x2\x2\x2]\x199\x3\x2\x2\x2_\x19B"+
		"\x3\x2\x2\x2\x61\x19E\x3\x2\x2\x2\x63\x1A0\x3\x2\x2\x2\x65\x1A3\x3\x2"+
		"\x2\x2g\x1A5\x3\x2\x2\x2i\x1A8\x3\x2\x2\x2k\x1AC\x3\x2\x2\x2m\x1AF\x3"+
		"\x2\x2\x2o\x1B3\x3\x2\x2\x2q\x1B9\x3\x2\x2\x2s\x1BF\x3\x2\x2\x2u\x1C6"+
		"\x3\x2\x2\x2w\x1CD\x3\x2\x2\x2y\x1D2\x3\x2\x2\x2{\x1DA\x3\x2\x2\x2}\x1DE"+
		"\x3\x2\x2\x2\x7F\x1E0\x3\x2\x2\x2\x81\x1E6\x3\x2\x2\x2\x83\x1E8\x3\x2"+
		"\x2\x2\x85\x1EC\x3\x2\x2\x2\x87\x88\a\x66\x2\x2\x88\x89\aq\x2\x2\x89\x8A"+
		"\ao\x2\x2\x8A\x8B\a\x63\x2\x2\x8B\x8C\ak\x2\x2\x8C\x8D\ap\x2\x2\x8D\x4"+
		"\x3\x2\x2\x2\x8E\x8F\ar\x2\x2\x8F\x90\at\x2\x2\x90\x91\aq\x2\x2\x91\x92"+
		"\a\x64\x2\x2\x92\x93\an\x2\x2\x93\x94\ag\x2\x2\x94\x95\ao\x2\x2\x95\x6"+
		"\x3\x2\x2\x2\x96\x97\a\x66\x2\x2\x97\x98\ag\x2\x2\x98\x99\ah\x2\x2\x99"+
		"\x9A\ak\x2\x2\x9A\x9B\ap\x2\x2\x9B\x9C\ag\x2\x2\x9C\b\x3\x2\x2\x2\x9D"+
		"\x9E\a\x63\x2\x2\x9E\x9F\ai\x2\x2\x9F\xA0\ag\x2\x2\xA0\xA1\ap\x2\x2\xA1"+
		"\xA2\av\x2\x2\xA2\xA3\ak\x2\x2\xA3\xA4\a\x66\x2\x2\xA4\n\x3\x2\x2\x2\xA5"+
		"\xA6\a\x65\x2\x2\xA6\xA7\aq\x2\x2\xA7\xA8\ap\x2\x2\xA8\xA9\au\x2\x2\xA9"+
		"\xAA\av\x2\x2\xAA\xAB\a\x63\x2\x2\xAB\xAC\ap\x2\x2\xAC\xAD\av\x2\x2\xAD"+
		"\xAE\au\x2\x2\xAE\f\x3\x2\x2\x2\xAF\xB0\av\x2\x2\xB0\xB1\a{\x2\x2\xB1"+
		"\xB2\ar\x2\x2\xB2\xB3\ag\x2\x2\xB3\xB4\au\x2\x2\xB4\xE\x3\x2\x2\x2\xB5"+
		"\xB6\ar\x2\x2\xB6\xB7\at\x2\x2\xB7\xB8\ag\x2\x2\xB8\xB9\a\x66\x2\x2\xB9"+
		"\xBA\ak\x2\x2\xBA\xBB\a\x65\x2\x2\xBB\xBC\a\x63\x2\x2\xBC\xBD\av\x2\x2"+
		"\xBD\xBE\ag\x2\x2\xBE\xBF\au\x2\x2\xBF\x10\x3\x2\x2\x2\xC0\xC1\a\x63\x2"+
		"\x2\xC1\xC2\a\x65\x2\x2\xC2\xC3\av\x2\x2\xC3\xC4\ak\x2\x2\xC4\xC5\aq\x2"+
		"\x2\xC5\xC6\ap\x2\x2\xC6\x12\x3\x2\x2\x2\xC7\xC8\ag\x2\x2\xC8\xC9\ax\x2"+
		"\x2\xC9\xCA\ag\x2\x2\xCA\xCB\ap\x2\x2\xCB\xCC\av\x2\x2\xCC\x14\x3\x2\x2"+
		"\x2\xCD\xCE\ag\x2\x2\xCE\xCF\ax\x2\x2\xCF\xD0\ag\x2\x2\xD0\xD1\ap\x2\x2"+
		"\xD1\xD2\av\x2\x2\xD2\xD3\au\x2\x2\xD3\x16\x3\x2\x2\x2\xD4\xD5\ar\x2\x2"+
		"\xD5\xD6\an\x2\x2\xD6\xD7\a\x66\x2\x2\xD7\xD8\ag\x2\x2\xD8\xD9\ai\x2\x2"+
		"\xD9\xDA\at\x2\x2\xDA\xDB\ag\x2\x2\xDB\xDC\ag\x2\x2\xDC\x18\x3\x2\x2\x2"+
		"\xDD\xDE\ag\x2\x2\xDE\xDF\ax\x2\x2\xDF\xE0\ag\x2\x2\xE0\xE1\ap\x2\x2\xE1"+
		"\xE2\av\x2\x2\xE2\xE3\ao\x2\x2\xE3\xE4\aq\x2\x2\xE4\xE5\a\x66\x2\x2\xE5"+
		"\xE6\ag\x2\x2\xE6\xE7\an\x2\x2\xE7\x1A\x3\x2\x2\x2\xE8\xE9\ar\x2\x2\xE9"+
		"\xEA\a\x63\x2\x2\xEA\xEB\at\x2\x2\xEB\xEC\a\x63\x2\x2\xEC\xED\ao\x2\x2"+
		"\xED\xEE\ag\x2\x2\xEE\xEF\av\x2\x2\xEF\xF0\ag\x2\x2\xF0\xF1\at\x2\x2\xF1"+
		"\xF2\au\x2\x2\xF2\x1C\x3\x2\x2\x2\xF3\xF4\ar\x2\x2\xF4\xF5\at\x2\x2\xF5"+
		"\xF6\ag\x2\x2\xF6\xF7\a\x65\x2\x2\xF7\xF8\aq\x2\x2\xF8\xF9\ap\x2\x2\xF9"+
		"\xFA\a\x66\x2\x2\xFA\xFB\ak\x2\x2\xFB\xFC\av\x2\x2\xFC\xFD\ak\x2\x2\xFD"+
		"\xFE\aq\x2\x2\xFE\xFF\ap\x2\x2\xFF\x1E\x3\x2\x2\x2\x100\x101\at\x2\x2"+
		"\x101\x102\ag\x2\x2\x102\x103\au\x2\x2\x103\x104\ar\x2\x2\x104\x105\a"+
		"q\x2\x2\x105\x106\ap\x2\x2\x106\x107\au\x2\x2\x107\x108\ag\x2\x2\x108"+
		" \x3\x2\x2\x2\x109\x10A\aq\x2\x2\x10A\x10B\a\x64\x2\x2\x10B\x10C\au\x2"+
		"\x2\x10C\x10D\ag\x2\x2\x10D\x10E\at\x2\x2\x10E\x10F\ax\x2\x2\x10F\x110"+
		"\a\x63\x2\x2\x110\x111\av\x2\x2\x111\x112\ak\x2\x2\x112\x113\aq\x2\x2"+
		"\x113\x114\ap\x2\x2\x114\"\x3\x2\x2\x2\x115\x116\ao\x2\x2\x116\x117\a"+
		"k\x2\x2\x117\x118\ap\x2\x2\x118$\x3\x2\x2\x2\x119\x11A\ao\x2\x2\x11A\x11B"+
		"\a\x63\x2\x2\x11B\x11C\az\x2\x2\x11C&\x3\x2\x2\x2\x11D\x11E\ap\x2\x2\x11E"+
		"\x11F\aw\x2\x2\x11F\x120\ao\x2\x2\x120\x121\a\x64\x2\x2\x121\x122\ag\x2"+
		"\x2\x122\x123\at\x2\x2\x123\x124\au\x2\x2\x124(\x3\x2\x2\x2\x125\x126"+
		"\ag\x2\x2\x126\x127\ah\x2\x2\x127\x128\ah\x2\x2\x128\x129\ag\x2\x2\x129"+
		"\x12A\a\x65\x2\x2\x12A\x12B\av\x2\x2\x12B*\x3\x2\x2\x2\x12C\x12D\aq\x2"+
		"\x2\x12D\x12E\a\x64\x2\x2\x12E\x12F\al\x2\x2\x12F\x130\ag\x2\x2\x130\x131"+
		"\a\x65\x2\x2\x131\x132\av\x2\x2\x132,\x3\x2\x2\x2\x133\x134\a\x63\x2\x2"+
		"\x134\x135\ai\x2\x2\x135\x136\ag\x2\x2\x136\x137\ap\x2\x2\x137\x138\a"+
		"v\x2\x2\x138.\x3\x2\x2\x2\x139\x13A\ag\x2\x2\x13A\x13B\ak\x2\x2\x13B\x13C"+
		"\av\x2\x2\x13C\x13D\aj\x2\x2\x13D\x13E\ag\x2\x2\x13E\x13F\at\x2\x2\x13F"+
		"\x30\x3\x2\x2\x2\x140\x141\ak\x2\x2\x141\x142\ap\x2\x2\x142\x143\ak\x2"+
		"\x2\x143\x144\av\x2\x2\x144\x145\am\x2\x2\x145\x146\ap\x2\x2\x146\x147"+
		"\aq\x2\x2\x147\x148\ay\x2\x2\x148\x149\an\x2\x2\x149\x14A\ag\x2\x2\x14A"+
		"\x14B\a\x66\x2\x2\x14B\x14C\ai\x2\x2\x14C\x14D\ag\x2\x2\x14D\x32\x3\x2"+
		"\x2\x2\x14E\x14F\ak\x2\x2\x14F\x150\ap\x2\x2\x150\x151\ak\x2\x2\x151\x152"+
		"\av\x2\x2\x152\x153\a\x64\x2\x2\x153\x154\ag\x2\x2\x154\x155\an\x2\x2"+
		"\x155\x156\ak\x2\x2\x156\x157\ag\x2\x2\x157\x158\ah\x2\x2\x158\x34\x3"+
		"\x2\x2\x2\x159\x15A\au\x2\x2\x15A\x15B\ag\x2\x2\x15B\x15C\as\x2\x2\x15C"+
		"\x36\x3\x2\x2\x2\x15D\x15E\ak\x2\x2\x15E\x15F\ah\x2\x2\x15F\x38\x3\x2"+
		"\x2\x2\x160\x161\ay\x2\x2\x161\x162\aj\x2\x2\x162\x163\ak\x2\x2\x163\x164"+
		"\an\x2\x2\x164\x165\ag\x2\x2\x165:\x3\x2\x2\x2\x166\x167\am\x2\x2\x167"+
		"\x168\ap\x2\x2\x168\x169\aq\x2\x2\x169\x16A\ay\x2\x2\x16A<\x3\x2\x2\x2"+
		"\x16B\x16C\a\x64\x2\x2\x16C\x16D\ag\x2\x2\x16D\x16E\an\x2\x2\x16E>\x3"+
		"\x2\x2\x2\x16F\x170\aq\x2\x2\x170\x171\a\x64\x2\x2\x171\x172\al\x2\x2"+
		"\x172\x173\ag\x2\x2\x173\x174\a\x65\x2\x2\x174\x175\av\x2\x2\x175\x176"+
		"\au\x2\x2\x176@\x3\x2\x2\x2\x177\x178\ak\x2\x2\x178\x179\ap\x2\x2\x179"+
		"\x17A\ak\x2\x2\x17A\x17B\av\x2\x2\x17B\x42\x3\x2\x2\x2\x17C\x17D\ai\x2"+
		"\x2\x17D\x17E\aq\x2\x2\x17E\x17F\a\x63\x2\x2\x17F\x180\an\x2\x2\x180\x44"+
		"\x3\x2\x2\x2\x181\x182\a*\x2\x2\x182\x46\x3\x2\x2\x2\x183\x184\a+\x2\x2"+
		"\x184H\x3\x2\x2\x2\x185\x186\a]\x2\x2\x186J\x3\x2\x2\x2\x187\x188\a_\x2"+
		"\x2\x188L\x3\x2\x2\x2\x189\x18A\a<\x2\x2\x18AN\x3\x2\x2\x2\x18B\x18C\a"+
		"\x41\x2\x2\x18CP\x3\x2\x2\x2\x18D\x18E\a\x30\x2\x2\x18ER\x3\x2\x2\x2\x18F"+
		"\x190\a\x61\x2\x2\x190T\x3\x2\x2\x2\x191\x192\a/\x2\x2\x192V\x3\x2\x2"+
		"\x2\x193\x194\a-\x2\x2\x194X\x3\x2\x2\x2\x195\x196\a,\x2\x2\x196Z\x3\x2"+
		"\x2\x2\x197\x198\a\x31\x2\x2\x198\\\x3\x2\x2\x2\x199\x19A\a?\x2\x2\x19A"+
		"^\x3\x2\x2\x2\x19B\x19C\a#\x2\x2\x19C\x19D\a?\x2\x2\x19D`\x3\x2\x2\x2"+
		"\x19E\x19F\a>\x2\x2\x19F\x62\x3\x2\x2\x2\x1A0\x1A1\a>\x2\x2\x1A1\x1A2"+
		"\a?\x2\x2\x1A2\x64\x3\x2\x2\x2\x1A3\x1A4\a@\x2\x2\x1A4\x66\x3\x2\x2\x2"+
		"\x1A5\x1A6\a@\x2\x2\x1A6\x1A7\a?\x2\x2\x1A7h\x3\x2\x2\x2\x1A8\x1A9\a\x63"+
		"\x2\x2\x1A9\x1AA\ap\x2\x2\x1AA\x1AB\a\x66\x2\x2\x1ABj\x3\x2\x2\x2\x1AC"+
		"\x1AD\aq\x2\x2\x1AD\x1AE\at\x2\x2\x1AEl\x3\x2\x2\x2\x1AF\x1B0\ap\x2\x2"+
		"\x1B0\x1B1\aq\x2\x2\x1B1\x1B2\av\x2\x2\x1B2n\x3\x2\x2\x2\x1B3\x1B4\aq"+
		"\x2\x2\x1B4\x1B5\ap\x2\x2\x1B5\x1B6\ag\x2\x2\x1B6\x1B7\aq\x2\x2\x1B7\x1B8"+
		"\ah\x2\x2\x1B8p\x3\x2\x2\x2\x1B9\x1BA\ak\x2\x2\x1BA\x1BB\ao\x2\x2\x1BB"+
		"\x1BC\ar\x2\x2\x1BC\x1BD\an\x2\x2\x1BD\x1BE\a{\x2\x2\x1BEr\x3\x2\x2\x2"+
		"\x1BF\x1C0\ah\x2\x2\x1C0\x1C1\aq\x2\x2\x1C1\x1C2\at\x2\x2\x1C2\x1C3\a"+
		"\x63\x2\x2\x1C3\x1C4\an\x2\x2\x1C4\x1C5\an\x2\x2\x1C5t\x3\x2\x2\x2\x1C6"+
		"\x1C7\ag\x2\x2\x1C7\x1C8\az\x2\x2\x1C8\x1C9\ak\x2\x2\x1C9\x1CA\au\x2\x2"+
		"\x1CA\x1CB\av\x2\x2\x1CB\x1CC\au\x2\x2\x1CCv\x3\x2\x2\x2\x1CD\x1CE\ay"+
		"\x2\x2\x1CE\x1CF\aj\x2\x2\x1CF\x1D0\ag\x2\x2\x1D0\x1D1\ap\x2\x2\x1D1x"+
		"\x3\x2\x2\x2\x1D2\x1D6\x5}?\x2\x1D3\x1D5\x5\x81\x41\x2\x1D4\x1D3\x3\x2"+
		"\x2\x2\x1D5\x1D8\x3\x2\x2\x2\x1D6\x1D4\x3\x2\x2\x2\x1D6\x1D7\x3\x2\x2"+
		"\x2\x1D7z\x3\x2\x2\x2\x1D8\x1D6\x3\x2\x2\x2\x1D9\x1DB\x5\x7F@\x2\x1DA"+
		"\x1D9\x3\x2\x2\x2\x1DB\x1DC\x3\x2\x2\x2\x1DC\x1DA\x3\x2\x2\x2\x1DC\x1DD"+
		"\x3\x2\x2\x2\x1DD|\x3\x2\x2\x2\x1DE\x1DF\t\x2\x2\x2\x1DF~\x3\x2\x2\x2"+
		"\x1E0\x1E1\t\x3\x2\x2\x1E1\x80\x3\x2\x2\x2\x1E2\x1E7\x5}?\x2\x1E3\x1E7"+
		"\x5\x7F@\x2\x1E4\x1E7\x5U+\x2\x1E5\x1E7\x5S*\x2\x1E6\x1E2\x3\x2\x2\x2"+
		"\x1E6\x1E3\x3\x2\x2\x2\x1E6\x1E4\x3\x2\x2\x2\x1E6\x1E5\x3\x2\x2\x2\x1E7"+
		"\x82\x3\x2\x2\x2\x1E8\x1E9\x5O(\x2\x1E9\x1EA\x5y=\x2\x1EA\x84\x3\x2\x2"+
		"\x2\x1EB\x1ED\t\x4\x2\x2\x1EC\x1EB\x3\x2\x2\x2\x1ED\x1EE\x3\x2\x2\x2\x1EE"+
		"\x1EC\x3\x2\x2\x2\x1EE\x1EF\x3\x2\x2\x2\x1EF\x1F0\x3\x2\x2\x2\x1F0\x1F1"+
		"\b\x43\x2\x2\x1F1\x86\x3\x2\x2\x2\a\x2\x1D6\x1DC\x1E6\x1EE\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LanguageRecognition
