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
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.2-SNAPSHOT")]
[System.CLSCompliant(false)]
public partial class PlanningLexer : Lexer {
	public const int
		DOM=1, PROM=2, DEF=3, HOSTID=4, TYPE=5, PRED=6, ACT=7, PARM=8, PRE=9, 
		EFF=10, OBJ=11, EITHER=12, OBJS=13, INIT=14, AGENTS=15, GOAL=16, LB=17, 
		RB=18, LSB=19, RSB=20, COLON=21, QM=22, POINT=23, UL=24, DASH=25, AND=26, 
		OR=27, NOT=28, IMPLY=29, FORALL=30, EXISTS=31, WHEN=32, LETTER=33, DIGIT=34, 
		NAME=35, CHAR=36, NUMBER=37, DECIMAL=38, VAR=39, FUNSYM=40, WS=41;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] tokenNames = {
		"<INVALID>",
		"'domain'", "'problem'", "'define'", "'hostid'", "'types'", "'predicates'", 
		"'action'", "'parameters'", "'precondition'", "'effect'", "'object'", 
		"'either'", "'objects'", "'init'", "'agents'", "'goal'", "'('", "')'", 
		"'['", "']'", "':'", "'?'", "'.'", "'_'", "'-'", "'and'", "'or'", "'not'", 
		"'imply'", "'forall'", "'exists'", "'when'", "LETTER", "DIGIT", "NAME", 
		"CHAR", "NUMBER", "DECIMAL", "VAR", "FUNSYM", "WS"
	};
	public static readonly string[] ruleNames = {
		"DOM", "PROM", "DEF", "HOSTID", "TYPE", "PRED", "ACT", "PARM", "PRE", 
		"EFF", "OBJ", "EITHER", "OBJS", "INIT", "AGENTS", "GOAL", "LB", "RB", 
		"LSB", "RSB", "COLON", "QM", "POINT", "UL", "DASH", "AND", "OR", "NOT", 
		"IMPLY", "FORALL", "EXISTS", "WHEN", "LETTER", "DIGIT", "NAME", "CHAR", 
		"NUMBER", "DECIMAL", "VAR", "FUNSYM", "WS"
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2+\x131\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5"+
		"\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3"+
		"\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3"+
		"\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3"+
		"\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3"+
		"\n\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3"+
		"\f\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3"+
		"\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\x10\x3\x10\x3\x10\x3"+
		"\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x12\x3"+
		"\x12\x3\x13\x3\x13\x3\x14\x3\x14\x3\x15\x3\x15\x3\x16\x3\x16\x3\x17\x3"+
		"\x17\x3\x18\x3\x18\x3\x19\x3\x19\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B\x3"+
		"\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3"+
		"\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3"+
		"\x1F\x3 \x3 \x3 \x3 \x3 \x3 \x3 \x3!\x3!\x3!\x3!\x3!\x3\"\x3\"\x3#\x3"+
		"#\x3$\x3$\a$\x10D\n$\f$\xE$\x110\v$\x3%\x3%\x3%\x3%\x5%\x116\n%\x3&\x6"+
		"&\x119\n&\r&\xE&\x11A\x3&\x5&\x11E\n&\x3\'\x3\'\x6\'\x122\n\'\r\'\xE\'"+
		"\x123\x3(\x3(\x3(\x3)\x3)\x3*\x6*\x12C\n*\r*\xE*\x12D\x3*\x3*\x2\x2\x2"+
		"+\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13"+
		"\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2"+
		"\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/\x2\x19\x31\x2\x1A"+
		"\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F=\x2 ?\x2!\x41\x2"+
		"\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2*S\x2+\x3\x2\x5\x4\x2"+
		"\x43\\\x63|\x3\x2\x32;\x5\x2\v\f\xF\xF\"\"\x138\x2\x3\x3\x2\x2\x2\x2\x5"+
		"\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3"+
		"\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15"+
		"\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2"+
		"\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2"+
		"\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-"+
		"\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2"+
		"\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2"+
		"\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2"+
		"\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x2"+
		"M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3\x2\x2\x2\x3U\x3\x2"+
		"\x2\x2\x5\\\x3\x2\x2\x2\a\x64\x3\x2\x2\x2\tk\x3\x2\x2\x2\vr\x3\x2\x2\x2"+
		"\rx\x3\x2\x2\x2\xF\x83\x3\x2\x2\x2\x11\x8A\x3\x2\x2\x2\x13\x95\x3\x2\x2"+
		"\x2\x15\xA2\x3\x2\x2\x2\x17\xA9\x3\x2\x2\x2\x19\xB0\x3\x2\x2\x2\x1B\xB7"+
		"\x3\x2\x2\x2\x1D\xBF\x3\x2\x2\x2\x1F\xC4\x3\x2\x2\x2!\xCB\x3\x2\x2\x2"+
		"#\xD0\x3\x2\x2\x2%\xD2\x3\x2\x2\x2\'\xD4\x3\x2\x2\x2)\xD6\x3\x2\x2\x2"+
		"+\xD8\x3\x2\x2\x2-\xDA\x3\x2\x2\x2/\xDC\x3\x2\x2\x2\x31\xDE\x3\x2\x2\x2"+
		"\x33\xE0\x3\x2\x2\x2\x35\xE2\x3\x2\x2\x2\x37\xE6\x3\x2\x2\x2\x39\xE9\x3"+
		"\x2\x2\x2;\xED\x3\x2\x2\x2=\xF3\x3\x2\x2\x2?\xFA\x3\x2\x2\x2\x41\x101"+
		"\x3\x2\x2\x2\x43\x106\x3\x2\x2\x2\x45\x108\x3\x2\x2\x2G\x10A\x3\x2\x2"+
		"\x2I\x115\x3\x2\x2\x2K\x118\x3\x2\x2\x2M\x11F\x3\x2\x2\x2O\x125\x3\x2"+
		"\x2\x2Q\x128\x3\x2\x2\x2S\x12B\x3\x2\x2\x2UV\a\x66\x2\x2VW\aq\x2\x2WX"+
		"\ao\x2\x2XY\a\x63\x2\x2YZ\ak\x2\x2Z[\ap\x2\x2[\x4\x3\x2\x2\x2\\]\ar\x2"+
		"\x2]^\at\x2\x2^_\aq\x2\x2_`\a\x64\x2\x2`\x61\an\x2\x2\x61\x62\ag\x2\x2"+
		"\x62\x63\ao\x2\x2\x63\x6\x3\x2\x2\x2\x64\x65\a\x66\x2\x2\x65\x66\ag\x2"+
		"\x2\x66g\ah\x2\x2gh\ak\x2\x2hi\ap\x2\x2ij\ag\x2\x2j\b\x3\x2\x2\x2kl\a"+
		"j\x2\x2lm\aq\x2\x2mn\au\x2\x2no\av\x2\x2op\ak\x2\x2pq\a\x66\x2\x2q\n\x3"+
		"\x2\x2\x2rs\av\x2\x2st\a{\x2\x2tu\ar\x2\x2uv\ag\x2\x2vw\au\x2\x2w\f\x3"+
		"\x2\x2\x2xy\ar\x2\x2yz\at\x2\x2z{\ag\x2\x2{|\a\x66\x2\x2|}\ak\x2\x2}~"+
		"\a\x65\x2\x2~\x7F\a\x63\x2\x2\x7F\x80\av\x2\x2\x80\x81\ag\x2\x2\x81\x82"+
		"\au\x2\x2\x82\xE\x3\x2\x2\x2\x83\x84\a\x63\x2\x2\x84\x85\a\x65\x2\x2\x85"+
		"\x86\av\x2\x2\x86\x87\ak\x2\x2\x87\x88\aq\x2\x2\x88\x89\ap\x2\x2\x89\x10"+
		"\x3\x2\x2\x2\x8A\x8B\ar\x2\x2\x8B\x8C\a\x63\x2\x2\x8C\x8D\at\x2\x2\x8D"+
		"\x8E\a\x63\x2\x2\x8E\x8F\ao\x2\x2\x8F\x90\ag\x2\x2\x90\x91\av\x2\x2\x91"+
		"\x92\ag\x2\x2\x92\x93\at\x2\x2\x93\x94\au\x2\x2\x94\x12\x3\x2\x2\x2\x95"+
		"\x96\ar\x2\x2\x96\x97\at\x2\x2\x97\x98\ag\x2\x2\x98\x99\a\x65\x2\x2\x99"+
		"\x9A\aq\x2\x2\x9A\x9B\ap\x2\x2\x9B\x9C\a\x66\x2\x2\x9C\x9D\ak\x2\x2\x9D"+
		"\x9E\av\x2\x2\x9E\x9F\ak\x2\x2\x9F\xA0\aq\x2\x2\xA0\xA1\ap\x2\x2\xA1\x14"+
		"\x3\x2\x2\x2\xA2\xA3\ag\x2\x2\xA3\xA4\ah\x2\x2\xA4\xA5\ah\x2\x2\xA5\xA6"+
		"\ag\x2\x2\xA6\xA7\a\x65\x2\x2\xA7\xA8\av\x2\x2\xA8\x16\x3\x2\x2\x2\xA9"+
		"\xAA\aq\x2\x2\xAA\xAB\a\x64\x2\x2\xAB\xAC\al\x2\x2\xAC\xAD\ag\x2\x2\xAD"+
		"\xAE\a\x65\x2\x2\xAE\xAF\av\x2\x2\xAF\x18\x3\x2\x2\x2\xB0\xB1\ag\x2\x2"+
		"\xB1\xB2\ak\x2\x2\xB2\xB3\av\x2\x2\xB3\xB4\aj\x2\x2\xB4\xB5\ag\x2\x2\xB5"+
		"\xB6\at\x2\x2\xB6\x1A\x3\x2\x2\x2\xB7\xB8\aq\x2\x2\xB8\xB9\a\x64\x2\x2"+
		"\xB9\xBA\al\x2\x2\xBA\xBB\ag\x2\x2\xBB\xBC\a\x65\x2\x2\xBC\xBD\av\x2\x2"+
		"\xBD\xBE\au\x2\x2\xBE\x1C\x3\x2\x2\x2\xBF\xC0\ak\x2\x2\xC0\xC1\ap\x2\x2"+
		"\xC1\xC2\ak\x2\x2\xC2\xC3\av\x2\x2\xC3\x1E\x3\x2\x2\x2\xC4\xC5\a\x63\x2"+
		"\x2\xC5\xC6\ai\x2\x2\xC6\xC7\ag\x2\x2\xC7\xC8\ap\x2\x2\xC8\xC9\av\x2\x2"+
		"\xC9\xCA\au\x2\x2\xCA \x3\x2\x2\x2\xCB\xCC\ai\x2\x2\xCC\xCD\aq\x2\x2\xCD"+
		"\xCE\a\x63\x2\x2\xCE\xCF\an\x2\x2\xCF\"\x3\x2\x2\x2\xD0\xD1\a*\x2\x2\xD1"+
		"$\x3\x2\x2\x2\xD2\xD3\a+\x2\x2\xD3&\x3\x2\x2\x2\xD4\xD5\a]\x2\x2\xD5("+
		"\x3\x2\x2\x2\xD6\xD7\a_\x2\x2\xD7*\x3\x2\x2\x2\xD8\xD9\a<\x2\x2\xD9,\x3"+
		"\x2\x2\x2\xDA\xDB\a\x41\x2\x2\xDB.\x3\x2\x2\x2\xDC\xDD\a\x30\x2\x2\xDD"+
		"\x30\x3\x2\x2\x2\xDE\xDF\a\x61\x2\x2\xDF\x32\x3\x2\x2\x2\xE0\xE1\a/\x2"+
		"\x2\xE1\x34\x3\x2\x2\x2\xE2\xE3\a\x63\x2\x2\xE3\xE4\ap\x2\x2\xE4\xE5\a"+
		"\x66\x2\x2\xE5\x36\x3\x2\x2\x2\xE6\xE7\aq\x2\x2\xE7\xE8\at\x2\x2\xE8\x38"+
		"\x3\x2\x2\x2\xE9\xEA\ap\x2\x2\xEA\xEB\aq\x2\x2\xEB\xEC\av\x2\x2\xEC:\x3"+
		"\x2\x2\x2\xED\xEE\ak\x2\x2\xEE\xEF\ao\x2\x2\xEF\xF0\ar\x2\x2\xF0\xF1\a"+
		"n\x2\x2\xF1\xF2\a{\x2\x2\xF2<\x3\x2\x2\x2\xF3\xF4\ah\x2\x2\xF4\xF5\aq"+
		"\x2\x2\xF5\xF6\at\x2\x2\xF6\xF7\a\x63\x2\x2\xF7\xF8\an\x2\x2\xF8\xF9\a"+
		"n\x2\x2\xF9>\x3\x2\x2\x2\xFA\xFB\ag\x2\x2\xFB\xFC\az\x2\x2\xFC\xFD\ak"+
		"\x2\x2\xFD\xFE\au\x2\x2\xFE\xFF\av\x2\x2\xFF\x100\au\x2\x2\x100@\x3\x2"+
		"\x2\x2\x101\x102\ay\x2\x2\x102\x103\aj\x2\x2\x103\x104\ag\x2\x2\x104\x105"+
		"\ap\x2\x2\x105\x42\x3\x2\x2\x2\x106\x107\t\x2\x2\x2\x107\x44\x3\x2\x2"+
		"\x2\x108\x109\t\x3\x2\x2\x109\x46\x3\x2\x2\x2\x10A\x10E\x5\x43\"\x2\x10B"+
		"\x10D\x5I%\x2\x10C\x10B\x3\x2\x2\x2\x10D\x110\x3\x2\x2\x2\x10E\x10C\x3"+
		"\x2\x2\x2\x10E\x10F\x3\x2\x2\x2\x10FH\x3\x2\x2\x2\x110\x10E\x3\x2\x2\x2"+
		"\x111\x116\x5\x43\"\x2\x112\x116\x5\x45#\x2\x113\x116\x5\x33\x1A\x2\x114"+
		"\x116\x5\x31\x19\x2\x115\x111\x3\x2\x2\x2\x115\x112\x3\x2\x2\x2\x115\x113"+
		"\x3\x2\x2\x2\x115\x114\x3\x2\x2\x2\x116J\x3\x2\x2\x2\x117\x119\x5\x45"+
		"#\x2\x118\x117\x3\x2\x2\x2\x119\x11A\x3\x2\x2\x2\x11A\x118\x3\x2\x2\x2"+
		"\x11A\x11B\x3\x2\x2\x2\x11B\x11D\x3\x2\x2\x2\x11C\x11E\x5M\'\x2\x11D\x11C"+
		"\x3\x2\x2\x2\x11D\x11E\x3\x2\x2\x2\x11EL\x3\x2\x2\x2\x11F\x121\x5/\x18"+
		"\x2\x120\x122\x5\x45#\x2\x121\x120\x3\x2\x2\x2\x122\x123\x3\x2\x2\x2\x123"+
		"\x121\x3\x2\x2\x2\x123\x124\x3\x2\x2\x2\x124N\x3\x2\x2\x2\x125\x126\x5"+
		"-\x17\x2\x126\x127\x5G$\x2\x127P\x3\x2\x2\x2\x128\x129\x5G$\x2\x129R\x3"+
		"\x2\x2\x2\x12A\x12C\t\x4\x2\x2\x12B\x12A\x3\x2\x2\x2\x12C\x12D\x3\x2\x2"+
		"\x2\x12D\x12B\x3\x2\x2\x2\x12D\x12E\x3\x2\x2\x2\x12E\x12F\x3\x2\x2\x2"+
		"\x12F\x130\b*\x2\x2\x130T\x3\x2\x2\x2\t\x2\x10E\x115\x11A\x11D\x123\x12D"+
		"\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LanguageRecognition
