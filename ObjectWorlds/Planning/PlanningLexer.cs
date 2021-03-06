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
		DOM=1, PROM=2, DEF=3, AGENTID=4, TYPE=5, PRED=6, ACT=7, PARM=8, PRE=9, 
		EFF=10, OBJ=11, EITHER=12, INITKNOWLEDGE=13, INITBELIEF=14, OBJS=15, INIT=16, 
		AGENTS=17, GOAL=18, LB=19, RB=20, LSB=21, RSB=22, COLON=23, QM=24, POINT=25, 
		UL=26, DASH=27, AND=28, OR=29, NOT=30, IMPLY=31, FORALL=32, EXISTS=33, 
		WHEN=34, LETTER=35, DIGIT=36, NAME=37, CHAR=38, NUMBER=39, DECIMAL=40, 
		VAR=41, FUNSYM=42, WS=43;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] tokenNames = {
		"<INVALID>",
		"'domain'", "'problem'", "'define'", "'agentid'", "'types'", "'predicates'", 
		"'action'", "'parameters'", "'precondition'", "'effect'", "'object'", 
		"'either'", "'initknowledge'", "'initbelief'", "'objects'", "'init'", 
		"'agents'", "'goal'", "'('", "')'", "'['", "']'", "':'", "'?'", "'.'", 
		"'_'", "'-'", "'and'", "'or'", "'not'", "'imply'", "'forall'", "'exists'", 
		"'when'", "LETTER", "DIGIT", "NAME", "CHAR", "NUMBER", "DECIMAL", "VAR", 
		"FUNSYM", "WS"
	};
	public static readonly string[] ruleNames = {
		"DOM", "PROM", "DEF", "AGENTID", "TYPE", "PRED", "ACT", "PARM", "PRE", 
		"EFF", "OBJ", "EITHER", "INITKNOWLEDGE", "INITBELIEF", "OBJS", "INIT", 
		"AGENTS", "GOAL", "LB", "RB", "LSB", "RSB", "COLON", "QM", "POINT", "UL", 
		"DASH", "AND", "OR", "NOT", "IMPLY", "FORALL", "EXISTS", "WHEN", "LETTER", 
		"DIGIT", "NAME", "CHAR", "NUMBER", "DECIMAL", "VAR", "FUNSYM", "WS"
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2-\x14F\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t)\x4*\t"+
		"*\x4+\t+\x4,\t,\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3"+
		"\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6"+
		"\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a"+
		"\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\t"+
		"\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n"+
		"\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\f\x3\f"+
		"\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\xE\x3"+
		"\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE"+
		"\x3\xE\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3"+
		"\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x11\x3"+
		"\x11\x3\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3\x12\x3"+
		"\x12\x3\x13\x3\x13\x3\x13\x3\x13\x3\x13\x3\x14\x3\x14\x3\x15\x3\x15\x3"+
		"\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18\x3\x19\x3\x19\x3\x1A\x3\x1A\x3"+
		"\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1D\x3\x1D\x3\x1E\x3\x1E\x3"+
		"\x1E\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3 \x3 \x3 \x3 \x3 \x3 \x3!\x3!\x3!\x3"+
		"!\x3!\x3!\x3!\x3\"\x3\"\x3\"\x3\"\x3\"\x3\"\x3\"\x3#\x3#\x3#\x3#\x3#\x3"+
		"$\x3$\x3%\x3%\x3&\x3&\a&\x12B\n&\f&\xE&\x12E\v&\x3\'\x3\'\x3\'\x3\'\x5"+
		"\'\x134\n\'\x3(\x6(\x137\n(\r(\xE(\x138\x3(\x5(\x13C\n(\x3)\x3)\x6)\x140"+
		"\n)\r)\xE)\x141\x3*\x3*\x3*\x3+\x3+\x3,\x6,\x14A\n,\r,\xE,\x14B\x3,\x3"+
		",\x2\x2\x2-\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11"+
		"\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2"+
		"\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/\x2\x19"+
		"\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F=\x2 ?"+
		"\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2*S\x2+U\x2"+
		",W\x2-\x3\x2\x5\x4\x2\x43\\\x63|\x3\x2\x32;\x5\x2\v\f\xF\xF\"\"\x156\x2"+
		"\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2"+
		"\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2"+
		"\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2"+
		"\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2"+
		"\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2"+
		"\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2"+
		"\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2"+
		"\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2"+
		"\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2"+
		"\x2K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3"+
		"\x2\x2\x2\x2U\x3\x2\x2\x2\x2W\x3\x2\x2\x2\x3Y\x3\x2\x2\x2\x5`\x3\x2\x2"+
		"\x2\ah\x3\x2\x2\x2\to\x3\x2\x2\x2\vw\x3\x2\x2\x2\r}\x3\x2\x2\x2\xF\x88"+
		"\x3\x2\x2\x2\x11\x8F\x3\x2\x2\x2\x13\x9A\x3\x2\x2\x2\x15\xA7\x3\x2\x2"+
		"\x2\x17\xAE\x3\x2\x2\x2\x19\xB5\x3\x2\x2\x2\x1B\xBC\x3\x2\x2\x2\x1D\xCA"+
		"\x3\x2\x2\x2\x1F\xD5\x3\x2\x2\x2!\xDD\x3\x2\x2\x2#\xE2\x3\x2\x2\x2%\xE9"+
		"\x3\x2\x2\x2\'\xEE\x3\x2\x2\x2)\xF0\x3\x2\x2\x2+\xF2\x3\x2\x2\x2-\xF4"+
		"\x3\x2\x2\x2/\xF6\x3\x2\x2\x2\x31\xF8\x3\x2\x2\x2\x33\xFA\x3\x2\x2\x2"+
		"\x35\xFC\x3\x2\x2\x2\x37\xFE\x3\x2\x2\x2\x39\x100\x3\x2\x2\x2;\x104\x3"+
		"\x2\x2\x2=\x107\x3\x2\x2\x2?\x10B\x3\x2\x2\x2\x41\x111\x3\x2\x2\x2\x43"+
		"\x118\x3\x2\x2\x2\x45\x11F\x3\x2\x2\x2G\x124\x3\x2\x2\x2I\x126\x3\x2\x2"+
		"\x2K\x128\x3\x2\x2\x2M\x133\x3\x2\x2\x2O\x136\x3\x2\x2\x2Q\x13D\x3\x2"+
		"\x2\x2S\x143\x3\x2\x2\x2U\x146\x3\x2\x2\x2W\x149\x3\x2\x2\x2YZ\a\x66\x2"+
		"\x2Z[\aq\x2\x2[\\\ao\x2\x2\\]\a\x63\x2\x2]^\ak\x2\x2^_\ap\x2\x2_\x4\x3"+
		"\x2\x2\x2`\x61\ar\x2\x2\x61\x62\at\x2\x2\x62\x63\aq\x2\x2\x63\x64\a\x64"+
		"\x2\x2\x64\x65\an\x2\x2\x65\x66\ag\x2\x2\x66g\ao\x2\x2g\x6\x3\x2\x2\x2"+
		"hi\a\x66\x2\x2ij\ag\x2\x2jk\ah\x2\x2kl\ak\x2\x2lm\ap\x2\x2mn\ag\x2\x2"+
		"n\b\x3\x2\x2\x2op\a\x63\x2\x2pq\ai\x2\x2qr\ag\x2\x2rs\ap\x2\x2st\av\x2"+
		"\x2tu\ak\x2\x2uv\a\x66\x2\x2v\n\x3\x2\x2\x2wx\av\x2\x2xy\a{\x2\x2yz\a"+
		"r\x2\x2z{\ag\x2\x2{|\au\x2\x2|\f\x3\x2\x2\x2}~\ar\x2\x2~\x7F\at\x2\x2"+
		"\x7F\x80\ag\x2\x2\x80\x81\a\x66\x2\x2\x81\x82\ak\x2\x2\x82\x83\a\x65\x2"+
		"\x2\x83\x84\a\x63\x2\x2\x84\x85\av\x2\x2\x85\x86\ag\x2\x2\x86\x87\au\x2"+
		"\x2\x87\xE\x3\x2\x2\x2\x88\x89\a\x63\x2\x2\x89\x8A\a\x65\x2\x2\x8A\x8B"+
		"\av\x2\x2\x8B\x8C\ak\x2\x2\x8C\x8D\aq\x2\x2\x8D\x8E\ap\x2\x2\x8E\x10\x3"+
		"\x2\x2\x2\x8F\x90\ar\x2\x2\x90\x91\a\x63\x2\x2\x91\x92\at\x2\x2\x92\x93"+
		"\a\x63\x2\x2\x93\x94\ao\x2\x2\x94\x95\ag\x2\x2\x95\x96\av\x2\x2\x96\x97"+
		"\ag\x2\x2\x97\x98\at\x2\x2\x98\x99\au\x2\x2\x99\x12\x3\x2\x2\x2\x9A\x9B"+
		"\ar\x2\x2\x9B\x9C\at\x2\x2\x9C\x9D\ag\x2\x2\x9D\x9E\a\x65\x2\x2\x9E\x9F"+
		"\aq\x2\x2\x9F\xA0\ap\x2\x2\xA0\xA1\a\x66\x2\x2\xA1\xA2\ak\x2\x2\xA2\xA3"+
		"\av\x2\x2\xA3\xA4\ak\x2\x2\xA4\xA5\aq\x2\x2\xA5\xA6\ap\x2\x2\xA6\x14\x3"+
		"\x2\x2\x2\xA7\xA8\ag\x2\x2\xA8\xA9\ah\x2\x2\xA9\xAA\ah\x2\x2\xAA\xAB\a"+
		"g\x2\x2\xAB\xAC\a\x65\x2\x2\xAC\xAD\av\x2\x2\xAD\x16\x3\x2\x2\x2\xAE\xAF"+
		"\aq\x2\x2\xAF\xB0\a\x64\x2\x2\xB0\xB1\al\x2\x2\xB1\xB2\ag\x2\x2\xB2\xB3"+
		"\a\x65\x2\x2\xB3\xB4\av\x2\x2\xB4\x18\x3\x2\x2\x2\xB5\xB6\ag\x2\x2\xB6"+
		"\xB7\ak\x2\x2\xB7\xB8\av\x2\x2\xB8\xB9\aj\x2\x2\xB9\xBA\ag\x2\x2\xBA\xBB"+
		"\at\x2\x2\xBB\x1A\x3\x2\x2\x2\xBC\xBD\ak\x2\x2\xBD\xBE\ap\x2\x2\xBE\xBF"+
		"\ak\x2\x2\xBF\xC0\av\x2\x2\xC0\xC1\am\x2\x2\xC1\xC2\ap\x2\x2\xC2\xC3\a"+
		"q\x2\x2\xC3\xC4\ay\x2\x2\xC4\xC5\an\x2\x2\xC5\xC6\ag\x2\x2\xC6\xC7\a\x66"+
		"\x2\x2\xC7\xC8\ai\x2\x2\xC8\xC9\ag\x2\x2\xC9\x1C\x3\x2\x2\x2\xCA\xCB\a"+
		"k\x2\x2\xCB\xCC\ap\x2\x2\xCC\xCD\ak\x2\x2\xCD\xCE\av\x2\x2\xCE\xCF\a\x64"+
		"\x2\x2\xCF\xD0\ag\x2\x2\xD0\xD1\an\x2\x2\xD1\xD2\ak\x2\x2\xD2\xD3\ag\x2"+
		"\x2\xD3\xD4\ah\x2\x2\xD4\x1E\x3\x2\x2\x2\xD5\xD6\aq\x2\x2\xD6\xD7\a\x64"+
		"\x2\x2\xD7\xD8\al\x2\x2\xD8\xD9\ag\x2\x2\xD9\xDA\a\x65\x2\x2\xDA\xDB\a"+
		"v\x2\x2\xDB\xDC\au\x2\x2\xDC \x3\x2\x2\x2\xDD\xDE\ak\x2\x2\xDE\xDF\ap"+
		"\x2\x2\xDF\xE0\ak\x2\x2\xE0\xE1\av\x2\x2\xE1\"\x3\x2\x2\x2\xE2\xE3\a\x63"+
		"\x2\x2\xE3\xE4\ai\x2\x2\xE4\xE5\ag\x2\x2\xE5\xE6\ap\x2\x2\xE6\xE7\av\x2"+
		"\x2\xE7\xE8\au\x2\x2\xE8$\x3\x2\x2\x2\xE9\xEA\ai\x2\x2\xEA\xEB\aq\x2\x2"+
		"\xEB\xEC\a\x63\x2\x2\xEC\xED\an\x2\x2\xED&\x3\x2\x2\x2\xEE\xEF\a*\x2\x2"+
		"\xEF(\x3\x2\x2\x2\xF0\xF1\a+\x2\x2\xF1*\x3\x2\x2\x2\xF2\xF3\a]\x2\x2\xF3"+
		",\x3\x2\x2\x2\xF4\xF5\a_\x2\x2\xF5.\x3\x2\x2\x2\xF6\xF7\a<\x2\x2\xF7\x30"+
		"\x3\x2\x2\x2\xF8\xF9\a\x41\x2\x2\xF9\x32\x3\x2\x2\x2\xFA\xFB\a\x30\x2"+
		"\x2\xFB\x34\x3\x2\x2\x2\xFC\xFD\a\x61\x2\x2\xFD\x36\x3\x2\x2\x2\xFE\xFF"+
		"\a/\x2\x2\xFF\x38\x3\x2\x2\x2\x100\x101\a\x63\x2\x2\x101\x102\ap\x2\x2"+
		"\x102\x103\a\x66\x2\x2\x103:\x3\x2\x2\x2\x104\x105\aq\x2\x2\x105\x106"+
		"\at\x2\x2\x106<\x3\x2\x2\x2\x107\x108\ap\x2\x2\x108\x109\aq\x2\x2\x109"+
		"\x10A\av\x2\x2\x10A>\x3\x2\x2\x2\x10B\x10C\ak\x2\x2\x10C\x10D\ao\x2\x2"+
		"\x10D\x10E\ar\x2\x2\x10E\x10F\an\x2\x2\x10F\x110\a{\x2\x2\x110@\x3\x2"+
		"\x2\x2\x111\x112\ah\x2\x2\x112\x113\aq\x2\x2\x113\x114\at\x2\x2\x114\x115"+
		"\a\x63\x2\x2\x115\x116\an\x2\x2\x116\x117\an\x2\x2\x117\x42\x3\x2\x2\x2"+
		"\x118\x119\ag\x2\x2\x119\x11A\az\x2\x2\x11A\x11B\ak\x2\x2\x11B\x11C\a"+
		"u\x2\x2\x11C\x11D\av\x2\x2\x11D\x11E\au\x2\x2\x11E\x44\x3\x2\x2\x2\x11F"+
		"\x120\ay\x2\x2\x120\x121\aj\x2\x2\x121\x122\ag\x2\x2\x122\x123\ap\x2\x2"+
		"\x123\x46\x3\x2\x2\x2\x124\x125\t\x2\x2\x2\x125H\x3\x2\x2\x2\x126\x127"+
		"\t\x3\x2\x2\x127J\x3\x2\x2\x2\x128\x12C\x5G$\x2\x129\x12B\x5M\'\x2\x12A"+
		"\x129\x3\x2\x2\x2\x12B\x12E\x3\x2\x2\x2\x12C\x12A\x3\x2\x2\x2\x12C\x12D"+
		"\x3\x2\x2\x2\x12DL\x3\x2\x2\x2\x12E\x12C\x3\x2\x2\x2\x12F\x134\x5G$\x2"+
		"\x130\x134\x5I%\x2\x131\x134\x5\x37\x1C\x2\x132\x134\x5\x35\x1B\x2\x133"+
		"\x12F\x3\x2\x2\x2\x133\x130\x3\x2\x2\x2\x133\x131\x3\x2\x2\x2\x133\x132"+
		"\x3\x2\x2\x2\x134N\x3\x2\x2\x2\x135\x137\x5I%\x2\x136\x135\x3\x2\x2\x2"+
		"\x137\x138\x3\x2\x2\x2\x138\x136\x3\x2\x2\x2\x138\x139\x3\x2\x2\x2\x139"+
		"\x13B\x3\x2\x2\x2\x13A\x13C\x5Q)\x2\x13B\x13A\x3\x2\x2\x2\x13B\x13C\x3"+
		"\x2\x2\x2\x13CP\x3\x2\x2\x2\x13D\x13F\x5\x33\x1A\x2\x13E\x140\x5I%\x2"+
		"\x13F\x13E\x3\x2\x2\x2\x140\x141\x3\x2\x2\x2\x141\x13F\x3\x2\x2\x2\x141"+
		"\x142\x3\x2\x2\x2\x142R\x3\x2\x2\x2\x143\x144\x5\x31\x19\x2\x144\x145"+
		"\x5K&\x2\x145T\x3\x2\x2\x2\x146\x147\x5K&\x2\x147V\x3\x2\x2\x2\x148\x14A"+
		"\t\x4\x2\x2\x149\x148\x3\x2\x2\x2\x14A\x14B\x3\x2\x2\x2\x14B\x149\x3\x2"+
		"\x2\x2\x14B\x14C\x3\x2\x2\x2\x14C\x14D\x3\x2\x2\x2\x14D\x14E\b,\x2\x2"+
		"\x14EX\x3\x2\x2\x2\t\x2\x12C\x133\x138\x13B\x141\x14B\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LanguageRecognition
