//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.2.1-SNAPSHOT
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from E:\EDGolog\LanguageRecognition\PlanningLexer.g4 by ANTLR 4.2.1-SNAPSHOT

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
		DOM=1, DEF=2, REQ=3, TYPE=4, PRED=5, ACT=6, PARM=7, PRE=8, EFF=9, OBJ=10, 
		EITHER=11, STRIPS=12, TYPING=13, LB=14, RB=15, LSB=16, RSB=17, COLON=18, 
		QM=19, COMMA=20, UL=21, DASH=22, PLUS=23, MINUS=24, MULT=25, DIV=26, EQ=27, 
		LT=28, LEQ=29, GT=30, GEQ=31, AND=32, OR=33, NOT=34, IMPLY=35, FORALL=36, 
		EXISTS=37, WHEN=38, PREF=39, BINCOMP=40, BINOP=41, LETTER=42, DIGIT=43, 
		NAME=44, CHAR=45, NUMBER=46, DECIMAL=47, VAR=48, FUNSYM=49, WS=50;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] tokenNames = {
		"<INVALID>",
		"'domain'", "'define'", "'requirements'", "'types'", "'predicates'", "'action'", 
		"'parameters'", "'precondition'", "'effect'", "'object'", "'either'", 
		"'strips'", "'typing'", "'('", "')'", "'['", "']'", "':'", "'?'", "'.'", 
		"'_'", "DASH", "'+'", "MINUS", "'*'", "'/'", "'='", "'<'", "'<='", "'>'", 
		"'>='", "'and'", "'or'", "'not'", "'imply'", "'forall'", "'exists'", "'when'", 
		"'preference'", "BINCOMP", "BINOP", "LETTER", "DIGIT", "NAME", "CHAR", 
		"NUMBER", "DECIMAL", "VAR", "FUNSYM", "WS"
	};
	public static readonly string[] ruleNames = {
		"DOM", "DEF", "REQ", "TYPE", "PRED", "ACT", "PARM", "PRE", "EFF", "OBJ", 
		"EITHER", "STRIPS", "TYPING", "LB", "RB", "LSB", "RSB", "COLON", "QM", 
		"COMMA", "UL", "DASH", "PLUS", "MINUS", "MULT", "DIV", "EQ", "LT", "LEQ", 
		"GT", "GEQ", "AND", "OR", "NOT", "IMPLY", "FORALL", "EXISTS", "WHEN", 
		"PREF", "BINCOMP", "BINOP", "LETTER", "DIGIT", "NAME", "CHAR", "NUMBER", 
		"DECIMAL", "VAR", "FUNSYM", "WS"
	};


	public PlanningLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	public override string GrammarFileName { get { return "PlanningLexer.g4"; } }

	public override string[] TokenNames { get { return tokenNames; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x34\x162\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A"+
		"\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 "+
		"\t \x4!\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t"+
		")\x4*\t*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31"+
		"\x4\x32\t\x32\x4\x33\t\x33\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3"+
		"\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6"+
		"\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3"+
		"\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3"+
		"\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3"+
		"\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3"+
		"\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE\x3\xE"+
		"\x3\xF\x3\xF\x3\x10\x3\x10\x3\x11\x3\x11\x3\x12\x3\x12\x3\x13\x3\x13\x3"+
		"\x14\x3\x14\x3\x15\x3\x15\x3\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18\x3"+
		"\x19\x3\x19\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3"+
		"\x1E\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3 \x3 \x3 \x3!\x3!\x3!\x3!\x3\"\x3\""+
		"\x3\"\x3#\x3#\x3#\x3#\x3$\x3$\x3$\x3$\x3$\x3$\x3%\x3%\x3%\x3%\x3%\x3%"+
		"\x3%\x3&\x3&\x3&\x3&\x3&\x3&\x3&\x3\'\x3\'\x3\'\x3\'\x3\'\x3(\x3(\x3("+
		"\x3(\x3(\x3(\x3(\x3(\x3(\x3(\x3(\x3)\x3)\x3)\x3)\x3)\x5)\x130\n)\x3*\x3"+
		"*\x3*\x3*\x5*\x136\n*\x3+\x3+\x3,\x3,\x3-\x3-\a-\x13E\n-\f-\xE-\x141\v"+
		"-\x3.\x3.\x3.\x3.\x5.\x147\n.\x3/\x6/\x14A\n/\r/\xE/\x14B\x3/\x5/\x14F"+
		"\n/\x3\x30\x3\x30\x6\x30\x153\n\x30\r\x30\xE\x30\x154\x3\x31\x3\x31\x3"+
		"\x31\x3\x32\x3\x32\x3\x33\x6\x33\x15D\n\x33\r\x33\xE\x33\x15E\x3\x33\x3"+
		"\x33\x2\x2\x2\x34\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2"+
		"\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10"+
		"\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/"+
		"\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F"+
		"=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2*S\x2"+
		"+U\x2,W\x2-Y\x2.[\x2/]\x2\x30_\x2\x31\x61\x2\x32\x63\x2\x33\x65\x2\x34"+
		"\x3\x2\x5\x4\x2\x43\\\x63|\x3\x2\x32;\x5\x2\v\f\xF\xF\"\"\x170\x2\x3\x3"+
		"\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3"+
		"\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13"+
		"\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2"+
		"\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2"+
		"\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2"+
		"+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33"+
		"\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2"+
		"\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2"+
		"\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2"+
		"K\x3\x2\x2\x2\x2M\x3\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3\x2"+
		"\x2\x2\x2U\x3\x2\x2\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2\x2\x2"+
		"\x2]\x3\x2\x2\x2\x2_\x3\x2\x2\x2\x2\x61\x3\x2\x2\x2\x2\x63\x3\x2\x2\x2"+
		"\x2\x65\x3\x2\x2\x2\x3g\x3\x2\x2\x2\x5n\x3\x2\x2\x2\au\x3\x2\x2\x2\t\x82"+
		"\x3\x2\x2\x2\v\x88\x3\x2\x2\x2\r\x93\x3\x2\x2\x2\xF\x9A\x3\x2\x2\x2\x11"+
		"\xA5\x3\x2\x2\x2\x13\xB2\x3\x2\x2\x2\x15\xB9\x3\x2\x2\x2\x17\xC0\x3\x2"+
		"\x2\x2\x19\xC7\x3\x2\x2\x2\x1B\xCE\x3\x2\x2\x2\x1D\xD5\x3\x2\x2\x2\x1F"+
		"\xD7\x3\x2\x2\x2!\xD9\x3\x2\x2\x2#\xDB\x3\x2\x2\x2%\xDD\x3\x2\x2\x2\'"+
		"\xDF\x3\x2\x2\x2)\xE1\x3\x2\x2\x2+\xE3\x3\x2\x2\x2-\xE5\x3\x2\x2\x2/\xE7"+
		"\x3\x2\x2\x2\x31\xE9\x3\x2\x2\x2\x33\xEB\x3\x2\x2\x2\x35\xED\x3\x2\x2"+
		"\x2\x37\xEF\x3\x2\x2\x2\x39\xF1\x3\x2\x2\x2;\xF3\x3\x2\x2\x2=\xF6\x3\x2"+
		"\x2\x2?\xF8\x3\x2\x2\x2\x41\xFB\x3\x2\x2\x2\x43\xFF\x3\x2\x2\x2\x45\x102"+
		"\x3\x2\x2\x2G\x106\x3\x2\x2\x2I\x10C\x3\x2\x2\x2K\x113\x3\x2\x2\x2M\x11A"+
		"\x3\x2\x2\x2O\x11F\x3\x2\x2\x2Q\x12F\x3\x2\x2\x2S\x135\x3\x2\x2\x2U\x137"+
		"\x3\x2\x2\x2W\x139\x3\x2\x2\x2Y\x13B\x3\x2\x2\x2[\x146\x3\x2\x2\x2]\x149"+
		"\x3\x2\x2\x2_\x150\x3\x2\x2\x2\x61\x156\x3\x2\x2\x2\x63\x159\x3\x2\x2"+
		"\x2\x65\x15C\x3\x2\x2\x2gh\a\x66\x2\x2hi\aq\x2\x2ij\ao\x2\x2jk\a\x63\x2"+
		"\x2kl\ak\x2\x2lm\ap\x2\x2m\x4\x3\x2\x2\x2no\a\x66\x2\x2op\ag\x2\x2pq\a"+
		"h\x2\x2qr\ak\x2\x2rs\ap\x2\x2st\ag\x2\x2t\x6\x3\x2\x2\x2uv\at\x2\x2vw"+
		"\ag\x2\x2wx\as\x2\x2xy\aw\x2\x2yz\ak\x2\x2z{\at\x2\x2{|\ag\x2\x2|}\ao"+
		"\x2\x2}~\ag\x2\x2~\x7F\ap\x2\x2\x7F\x80\av\x2\x2\x80\x81\au\x2\x2\x81"+
		"\b\x3\x2\x2\x2\x82\x83\av\x2\x2\x83\x84\a{\x2\x2\x84\x85\ar\x2\x2\x85"+
		"\x86\ag\x2\x2\x86\x87\au\x2\x2\x87\n\x3\x2\x2\x2\x88\x89\ar\x2\x2\x89"+
		"\x8A\at\x2\x2\x8A\x8B\ag\x2\x2\x8B\x8C\a\x66\x2\x2\x8C\x8D\ak\x2\x2\x8D"+
		"\x8E\a\x65\x2\x2\x8E\x8F\a\x63\x2\x2\x8F\x90\av\x2\x2\x90\x91\ag\x2\x2"+
		"\x91\x92\au\x2\x2\x92\f\x3\x2\x2\x2\x93\x94\a\x63\x2\x2\x94\x95\a\x65"+
		"\x2\x2\x95\x96\av\x2\x2\x96\x97\ak\x2\x2\x97\x98\aq\x2\x2\x98\x99\ap\x2"+
		"\x2\x99\xE\x3\x2\x2\x2\x9A\x9B\ar\x2\x2\x9B\x9C\a\x63\x2\x2\x9C\x9D\a"+
		"t\x2\x2\x9D\x9E\a\x63\x2\x2\x9E\x9F\ao\x2\x2\x9F\xA0\ag\x2\x2\xA0\xA1"+
		"\av\x2\x2\xA1\xA2\ag\x2\x2\xA2\xA3\at\x2\x2\xA3\xA4\au\x2\x2\xA4\x10\x3"+
		"\x2\x2\x2\xA5\xA6\ar\x2\x2\xA6\xA7\at\x2\x2\xA7\xA8\ag\x2\x2\xA8\xA9\a"+
		"\x65\x2\x2\xA9\xAA\aq\x2\x2\xAA\xAB\ap\x2\x2\xAB\xAC\a\x66\x2\x2\xAC\xAD"+
		"\ak\x2\x2\xAD\xAE\av\x2\x2\xAE\xAF\ak\x2\x2\xAF\xB0\aq\x2\x2\xB0\xB1\a"+
		"p\x2\x2\xB1\x12\x3\x2\x2\x2\xB2\xB3\ag\x2\x2\xB3\xB4\ah\x2\x2\xB4\xB5"+
		"\ah\x2\x2\xB5\xB6\ag\x2\x2\xB6\xB7\a\x65\x2\x2\xB7\xB8\av\x2\x2\xB8\x14"+
		"\x3\x2\x2\x2\xB9\xBA\aq\x2\x2\xBA\xBB\a\x64\x2\x2\xBB\xBC\al\x2\x2\xBC"+
		"\xBD\ag\x2\x2\xBD\xBE\a\x65\x2\x2\xBE\xBF\av\x2\x2\xBF\x16\x3\x2\x2\x2"+
		"\xC0\xC1\ag\x2\x2\xC1\xC2\ak\x2\x2\xC2\xC3\av\x2\x2\xC3\xC4\aj\x2\x2\xC4"+
		"\xC5\ag\x2\x2\xC5\xC6\at\x2\x2\xC6\x18\x3\x2\x2\x2\xC7\xC8\au\x2\x2\xC8"+
		"\xC9\av\x2\x2\xC9\xCA\at\x2\x2\xCA\xCB\ak\x2\x2\xCB\xCC\ar\x2\x2\xCC\xCD"+
		"\au\x2\x2\xCD\x1A\x3\x2\x2\x2\xCE\xCF\av\x2\x2\xCF\xD0\a{\x2\x2\xD0\xD1"+
		"\ar\x2\x2\xD1\xD2\ak\x2\x2\xD2\xD3\ap\x2\x2\xD3\xD4\ai\x2\x2\xD4\x1C\x3"+
		"\x2\x2\x2\xD5\xD6\a*\x2\x2\xD6\x1E\x3\x2\x2\x2\xD7\xD8\a+\x2\x2\xD8 \x3"+
		"\x2\x2\x2\xD9\xDA\a]\x2\x2\xDA\"\x3\x2\x2\x2\xDB\xDC\a_\x2\x2\xDC$\x3"+
		"\x2\x2\x2\xDD\xDE\a<\x2\x2\xDE&\x3\x2\x2\x2\xDF\xE0\a\x41\x2\x2\xE0(\x3"+
		"\x2\x2\x2\xE1\xE2\a\x30\x2\x2\xE2*\x3\x2\x2\x2\xE3\xE4\a\x61\x2\x2\xE4"+
		",\x3\x2\x2\x2\xE5\xE6\a/\x2\x2\xE6.\x3\x2\x2\x2\xE7\xE8\a-\x2\x2\xE8\x30"+
		"\x3\x2\x2\x2\xE9\xEA\a/\x2\x2\xEA\x32\x3\x2\x2\x2\xEB\xEC\a,\x2\x2\xEC"+
		"\x34\x3\x2\x2\x2\xED\xEE\a\x31\x2\x2\xEE\x36\x3\x2\x2\x2\xEF\xF0\a?\x2"+
		"\x2\xF0\x38\x3\x2\x2\x2\xF1\xF2\a>\x2\x2\xF2:\x3\x2\x2\x2\xF3\xF4\a>\x2"+
		"\x2\xF4\xF5\a?\x2\x2\xF5<\x3\x2\x2\x2\xF6\xF7\a@\x2\x2\xF7>\x3\x2\x2\x2"+
		"\xF8\xF9\a@\x2\x2\xF9\xFA\a?\x2\x2\xFA@\x3\x2\x2\x2\xFB\xFC\a\x63\x2\x2"+
		"\xFC\xFD\ap\x2\x2\xFD\xFE\a\x66\x2\x2\xFE\x42\x3\x2\x2\x2\xFF\x100\aq"+
		"\x2\x2\x100\x101\at\x2\x2\x101\x44\x3\x2\x2\x2\x102\x103\ap\x2\x2\x103"+
		"\x104\aq\x2\x2\x104\x105\av\x2\x2\x105\x46\x3\x2\x2\x2\x106\x107\ak\x2"+
		"\x2\x107\x108\ao\x2\x2\x108\x109\ar\x2\x2\x109\x10A\an\x2\x2\x10A\x10B"+
		"\a{\x2\x2\x10BH\x3\x2\x2\x2\x10C\x10D\ah\x2\x2\x10D\x10E\aq\x2\x2\x10E"+
		"\x10F\at\x2\x2\x10F\x110\a\x63\x2\x2\x110\x111\an\x2\x2\x111\x112\an\x2"+
		"\x2\x112J\x3\x2\x2\x2\x113\x114\ag\x2\x2\x114\x115\az\x2\x2\x115\x116"+
		"\ak\x2\x2\x116\x117\au\x2\x2\x117\x118\av\x2\x2\x118\x119\au\x2\x2\x119"+
		"L\x3\x2\x2\x2\x11A\x11B\ay\x2\x2\x11B\x11C\aj\x2\x2\x11C\x11D\ag\x2\x2"+
		"\x11D\x11E\ap\x2\x2\x11EN\x3\x2\x2\x2\x11F\x120\ar\x2\x2\x120\x121\at"+
		"\x2\x2\x121\x122\ag\x2\x2\x122\x123\ah\x2\x2\x123\x124\ag\x2\x2\x124\x125"+
		"\at\x2\x2\x125\x126\ag\x2\x2\x126\x127\ap\x2\x2\x127\x128\a\x65\x2\x2"+
		"\x128\x129\ag\x2\x2\x129P\x3\x2\x2\x2\x12A\x130\x5\x37\x1C\x2\x12B\x130"+
		"\x5\x39\x1D\x2\x12C\x130\x5=\x1F\x2\x12D\x130\x5;\x1E\x2\x12E\x130\x5"+
		"? \x2\x12F\x12A\x3\x2\x2\x2\x12F\x12B\x3\x2\x2\x2\x12F\x12C\x3\x2\x2\x2"+
		"\x12F\x12D\x3\x2\x2\x2\x12F\x12E\x3\x2\x2\x2\x130R\x3\x2\x2\x2\x131\x136"+
		"\x5/\x18\x2\x132\x136\x5\x31\x19\x2\x133\x136\x5\x33\x1A\x2\x134\x136"+
		"\x5\x35\x1B\x2\x135\x131\x3\x2\x2\x2\x135\x132\x3\x2\x2\x2\x135\x133\x3"+
		"\x2\x2\x2\x135\x134\x3\x2\x2\x2\x136T\x3\x2\x2\x2\x137\x138\t\x2\x2\x2"+
		"\x138V\x3\x2\x2\x2\x139\x13A\t\x3\x2\x2\x13AX\x3\x2\x2\x2\x13B\x13F\x5"+
		"U+\x2\x13C\x13E\x5[.\x2\x13D\x13C\x3\x2\x2\x2\x13E\x141\x3\x2\x2\x2\x13F"+
		"\x13D\x3\x2\x2\x2\x13F\x140\x3\x2\x2\x2\x140Z\x3\x2\x2\x2\x141\x13F\x3"+
		"\x2\x2\x2\x142\x147\x5U+\x2\x143\x147\x5W,\x2\x144\x147\x5-\x17\x2\x145"+
		"\x147\x5+\x16\x2\x146\x142\x3\x2\x2\x2\x146\x143\x3\x2\x2\x2\x146\x144"+
		"\x3\x2\x2\x2\x146\x145\x3\x2\x2\x2\x147\\\x3\x2\x2\x2\x148\x14A\x5W,\x2"+
		"\x149\x148\x3\x2\x2\x2\x14A\x14B\x3\x2\x2\x2\x14B\x149\x3\x2\x2\x2\x14B"+
		"\x14C\x3\x2\x2\x2\x14C\x14E\x3\x2\x2\x2\x14D\x14F\x5_\x30\x2\x14E\x14D"+
		"\x3\x2\x2\x2\x14E\x14F\x3\x2\x2\x2\x14F^\x3\x2\x2\x2\x150\x152\x5)\x15"+
		"\x2\x151\x153\x5W,\x2\x152\x151\x3\x2\x2\x2\x153\x154\x3\x2\x2\x2\x154"+
		"\x152\x3\x2\x2\x2\x154\x155\x3\x2\x2\x2\x155`\x3\x2\x2\x2\x156\x157\x5"+
		"\'\x14\x2\x157\x158\x5Y-\x2\x158\x62\x3\x2\x2\x2\x159\x15A\x5Y-\x2\x15A"+
		"\x64\x3\x2\x2\x2\x15B\x15D\t\x4\x2\x2\x15C\x15B\x3\x2\x2\x2\x15D\x15E"+
		"\x3\x2\x2\x2\x15E\x15C\x3\x2\x2\x2\x15E\x15F\x3\x2\x2\x2\x15F\x160\x3"+
		"\x2\x2\x2\x160\x161\b\x33\x2\x2\x161\x66\x3\x2\x2\x2\v\x2\x12F\x135\x13F"+
		"\x146\x14B\x14E\x154\x15E\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LanguageRecognition
