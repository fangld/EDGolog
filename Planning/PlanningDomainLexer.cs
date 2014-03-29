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
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.1-SNAPSHOT")]
[System.CLSCompliant(false)]
public partial class PlanningDomainLexer : Lexer {
	public const int
		DOM=1, DEF=2, REQ=3, TYPE=4, PRED=5, ACT=6, PARM=7, PRE=8, EFF=9, OBJ=10, 
		EITHER=11, LB=12, RB=13, LSB=14, RSB=15, COLON=16, QM=17, COMMA=18, UL=19, 
		DASH=20, PLUS=21, MINUS=22, MULT=23, DIV=24, EQ=25, LT=26, LEQ=27, GT=28, 
		GEQ=29, AND=30, OR=31, NOT=32, IMPLY=33, FORALL=34, EXISTS=35, PREF=36, 
		BINARYCOMP=37, BINARYOP=38, LETTER=39, DIGIT=40, NAME=41, CHAR=42, NUMBER=43, 
		DECIMAL=44, VAR=45, FUNSYM=46, STRIPS=47, TYPING=48, WS=49;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] tokenNames = {
		"<INVALID>",
		"'domain'", "'define'", "'requirements'", "'types'", "'predicates'", "'action'", 
		"'parameters'", "'precondition'", "'effect'", "'object'", "'either'", 
		"'('", "')'", "'['", "']'", "':'", "'?'", "'.'", "'_'", "DASH", "'+'", 
		"MINUS", "'*'", "'/'", "'='", "'<'", "'<='", "'>'", "'>='", "'and'", "'or'", 
		"'not'", "'imply'", "'forall'", "'exists'", "'preference'", "BINARYCOMP", 
		"BINARYOP", "LETTER", "DIGIT", "NAME", "CHAR", "NUMBER", "DECIMAL", "VAR", 
		"FUNSYM", "STRIPS", "TYPING", "WS"
	};
	public static readonly string[] ruleNames = {
		"DOM", "DEF", "REQ", "TYPE", "PRED", "ACT", "PARM", "PRE", "EFF", "OBJ", 
		"EITHER", "LB", "RB", "LSB", "RSB", "COLON", "QM", "COMMA", "UL", "DASH", 
		"PLUS", "MINUS", "MULT", "DIV", "EQ", "LT", "LEQ", "GT", "GEQ", "AND", 
		"OR", "NOT", "IMPLY", "FORALL", "EXISTS", "PREF", "BINARYCOMP", "BINARYOP", 
		"LETTER", "DIGIT", "NAME", "CHAR", "NUMBER", "DECIMAL", "VAR", "FUNSYM", 
		"STRIPS", "TYPING", "WS"
	};


	public PlanningDomainLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	public override string GrammarFileName { get { return "PlanningDomain.g4"; } }

	public override string[] TokenNames { get { return tokenNames; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x33\x15D\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A"+
		"\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 "+
		"\t \x4!\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t(\x4)\t"+
		")\x4*\t*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t\x30\x4\x31\t\x31"+
		"\x4\x32\t\x32\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3"+
		"\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6"+
		"\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b"+
		"\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t"+
		"\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v"+
		"\x3\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\xE\x3"+
		"\xE\x3\xF\x3\xF\x3\x10\x3\x10\x3\x11\x3\x11\x3\x12\x3\x12\x3\x13\x3\x13"+
		"\x3\x14\x3\x14\x3\x15\x3\x15\x3\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18"+
		"\x3\x19\x3\x19\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1D"+
		"\x3\x1D\x3\x1E\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x3 \x3 \x3 \x3"+
		"!\x3!\x3!\x3!\x3\"\x3\"\x3\"\x3\"\x3\"\x3\"\x3#\x3#\x3#\x3#\x3#\x3#\x3"+
		"#\x3$\x3$\x3$\x3$\x3$\x3$\x3$\x3%\x3%\x3%\x3%\x3%\x3%\x3%\x3%\x3%\x3%"+
		"\x3%\x3&\x3&\x3&\x3&\x3&\x5&\x11B\n&\x3\'\x3\'\x3\'\x3\'\x5\'\x121\n\'"+
		"\x3(\x3(\x3)\x3)\x3*\x3*\a*\x129\n*\f*\xE*\x12C\v*\x3+\x3+\x3+\x3+\x5"+
		"+\x132\n+\x3,\x6,\x135\n,\r,\xE,\x136\x3,\x5,\x13A\n,\x3-\x3-\x6-\x13E"+
		"\n-\r-\xE-\x13F\x3.\x3.\x3.\x3/\x3/\x3\x30\x3\x30\x3\x30\x3\x30\x3\x30"+
		"\x3\x30\x3\x30\x3\x30\x3\x31\x3\x31\x3\x31\x3\x31\x3\x31\x3\x31\x3\x31"+
		"\x3\x31\x3\x32\x6\x32\x158\n\x32\r\x32\xE\x32\x159\x3\x32\x3\x32\x2\x2"+
		"\x2\x33\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2"+
		"\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11"+
		"!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/\x2\x19\x31"+
		"\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F=\x2 ?\x2!"+
		"\x41\x2\"\x43\x2#\x45\x2$G\x2%I\x2&K\x2\'M\x2(O\x2)Q\x2*S\x2+U\x2,W\x2"+
		"-Y\x2.[\x2/]\x2\x30_\x2\x31\x61\x2\x32\x63\x2\x33\x3\x2\x5\x4\x2\x43\\"+
		"\x63|\x3\x2\x32;\x5\x2\v\f\xF\xF\"\"\x16B\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2"+
		"\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2"+
		"\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3"+
		"\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2"+
		"\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2"+
		"%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3"+
		"\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2\x35"+
		"\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2\x2"+
		"=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x2"+
		"\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x2M\x3"+
		"\x2\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2S\x3\x2\x2\x2\x2U\x3\x2\x2"+
		"\x2\x2W\x3\x2\x2\x2\x2Y\x3\x2\x2\x2\x2[\x3\x2\x2\x2\x2]\x3\x2\x2\x2\x2"+
		"_\x3\x2\x2\x2\x2\x61\x3\x2\x2\x2\x2\x63\x3\x2\x2\x2\x3\x65\x3\x2\x2\x2"+
		"\x5l\x3\x2\x2\x2\as\x3\x2\x2\x2\t\x80\x3\x2\x2\x2\v\x86\x3\x2\x2\x2\r"+
		"\x91\x3\x2\x2\x2\xF\x98\x3\x2\x2\x2\x11\xA3\x3\x2\x2\x2\x13\xB0\x3\x2"+
		"\x2\x2\x15\xB7\x3\x2\x2\x2\x17\xBE\x3\x2\x2\x2\x19\xC5\x3\x2\x2\x2\x1B"+
		"\xC7\x3\x2\x2\x2\x1D\xC9\x3\x2\x2\x2\x1F\xCB\x3\x2\x2\x2!\xCD\x3\x2\x2"+
		"\x2#\xCF\x3\x2\x2\x2%\xD1\x3\x2\x2\x2\'\xD3\x3\x2\x2\x2)\xD5\x3\x2\x2"+
		"\x2+\xD7\x3\x2\x2\x2-\xD9\x3\x2\x2\x2/\xDB\x3\x2\x2\x2\x31\xDD\x3\x2\x2"+
		"\x2\x33\xDF\x3\x2\x2\x2\x35\xE1\x3\x2\x2\x2\x37\xE3\x3\x2\x2\x2\x39\xE6"+
		"\x3\x2\x2\x2;\xE8\x3\x2\x2\x2=\xEB\x3\x2\x2\x2?\xEF\x3\x2\x2\x2\x41\xF2"+
		"\x3\x2\x2\x2\x43\xF6\x3\x2\x2\x2\x45\xFC\x3\x2\x2\x2G\x103\x3\x2\x2\x2"+
		"I\x10A\x3\x2\x2\x2K\x11A\x3\x2\x2\x2M\x120\x3\x2\x2\x2O\x122\x3\x2\x2"+
		"\x2Q\x124\x3\x2\x2\x2S\x126\x3\x2\x2\x2U\x131\x3\x2\x2\x2W\x134\x3\x2"+
		"\x2\x2Y\x13B\x3\x2\x2\x2[\x141\x3\x2\x2\x2]\x144\x3\x2\x2\x2_\x146\x3"+
		"\x2\x2\x2\x61\x14E\x3\x2\x2\x2\x63\x157\x3\x2\x2\x2\x65\x66\a\x66\x2\x2"+
		"\x66g\aq\x2\x2gh\ao\x2\x2hi\a\x63\x2\x2ij\ak\x2\x2jk\ap\x2\x2k\x4\x3\x2"+
		"\x2\x2lm\a\x66\x2\x2mn\ag\x2\x2no\ah\x2\x2op\ak\x2\x2pq\ap\x2\x2qr\ag"+
		"\x2\x2r\x6\x3\x2\x2\x2st\at\x2\x2tu\ag\x2\x2uv\as\x2\x2vw\aw\x2\x2wx\a"+
		"k\x2\x2xy\at\x2\x2yz\ag\x2\x2z{\ao\x2\x2{|\ag\x2\x2|}\ap\x2\x2}~\av\x2"+
		"\x2~\x7F\au\x2\x2\x7F\b\x3\x2\x2\x2\x80\x81\av\x2\x2\x81\x82\a{\x2\x2"+
		"\x82\x83\ar\x2\x2\x83\x84\ag\x2\x2\x84\x85\au\x2\x2\x85\n\x3\x2\x2\x2"+
		"\x86\x87\ar\x2\x2\x87\x88\at\x2\x2\x88\x89\ag\x2\x2\x89\x8A\a\x66\x2\x2"+
		"\x8A\x8B\ak\x2\x2\x8B\x8C\a\x65\x2\x2\x8C\x8D\a\x63\x2\x2\x8D\x8E\av\x2"+
		"\x2\x8E\x8F\ag\x2\x2\x8F\x90\au\x2\x2\x90\f\x3\x2\x2\x2\x91\x92\a\x63"+
		"\x2\x2\x92\x93\a\x65\x2\x2\x93\x94\av\x2\x2\x94\x95\ak\x2\x2\x95\x96\a"+
		"q\x2\x2\x96\x97\ap\x2\x2\x97\xE\x3\x2\x2\x2\x98\x99\ar\x2\x2\x99\x9A\a"+
		"\x63\x2\x2\x9A\x9B\at\x2\x2\x9B\x9C\a\x63\x2\x2\x9C\x9D\ao\x2\x2\x9D\x9E"+
		"\ag\x2\x2\x9E\x9F\av\x2\x2\x9F\xA0\ag\x2\x2\xA0\xA1\at\x2\x2\xA1\xA2\a"+
		"u\x2\x2\xA2\x10\x3\x2\x2\x2\xA3\xA4\ar\x2\x2\xA4\xA5\at\x2\x2\xA5\xA6"+
		"\ag\x2\x2\xA6\xA7\a\x65\x2\x2\xA7\xA8\aq\x2\x2\xA8\xA9\ap\x2\x2\xA9\xAA"+
		"\a\x66\x2\x2\xAA\xAB\ak\x2\x2\xAB\xAC\av\x2\x2\xAC\xAD\ak\x2\x2\xAD\xAE"+
		"\aq\x2\x2\xAE\xAF\ap\x2\x2\xAF\x12\x3\x2\x2\x2\xB0\xB1\ag\x2\x2\xB1\xB2"+
		"\ah\x2\x2\xB2\xB3\ah\x2\x2\xB3\xB4\ag\x2\x2\xB4\xB5\a\x65\x2\x2\xB5\xB6"+
		"\av\x2\x2\xB6\x14\x3\x2\x2\x2\xB7\xB8\aq\x2\x2\xB8\xB9\a\x64\x2\x2\xB9"+
		"\xBA\al\x2\x2\xBA\xBB\ag\x2\x2\xBB\xBC\a\x65\x2\x2\xBC\xBD\av\x2\x2\xBD"+
		"\x16\x3\x2\x2\x2\xBE\xBF\ag\x2\x2\xBF\xC0\ak\x2\x2\xC0\xC1\av\x2\x2\xC1"+
		"\xC2\aj\x2\x2\xC2\xC3\ag\x2\x2\xC3\xC4\at\x2\x2\xC4\x18\x3\x2\x2\x2\xC5"+
		"\xC6\a*\x2\x2\xC6\x1A\x3\x2\x2\x2\xC7\xC8\a+\x2\x2\xC8\x1C\x3\x2\x2\x2"+
		"\xC9\xCA\a]\x2\x2\xCA\x1E\x3\x2\x2\x2\xCB\xCC\a_\x2\x2\xCC \x3\x2\x2\x2"+
		"\xCD\xCE\a<\x2\x2\xCE\"\x3\x2\x2\x2\xCF\xD0\a\x41\x2\x2\xD0$\x3\x2\x2"+
		"\x2\xD1\xD2\a\x30\x2\x2\xD2&\x3\x2\x2\x2\xD3\xD4\a\x61\x2\x2\xD4(\x3\x2"+
		"\x2\x2\xD5\xD6\a/\x2\x2\xD6*\x3\x2\x2\x2\xD7\xD8\a-\x2\x2\xD8,\x3\x2\x2"+
		"\x2\xD9\xDA\a/\x2\x2\xDA.\x3\x2\x2\x2\xDB\xDC\a,\x2\x2\xDC\x30\x3\x2\x2"+
		"\x2\xDD\xDE\a\x31\x2\x2\xDE\x32\x3\x2\x2\x2\xDF\xE0\a?\x2\x2\xE0\x34\x3"+
		"\x2\x2\x2\xE1\xE2\a>\x2\x2\xE2\x36\x3\x2\x2\x2\xE3\xE4\a>\x2\x2\xE4\xE5"+
		"\a?\x2\x2\xE5\x38\x3\x2\x2\x2\xE6\xE7\a@\x2\x2\xE7:\x3\x2\x2\x2\xE8\xE9"+
		"\a@\x2\x2\xE9\xEA\a?\x2\x2\xEA<\x3\x2\x2\x2\xEB\xEC\a\x63\x2\x2\xEC\xED"+
		"\ap\x2\x2\xED\xEE\a\x66\x2\x2\xEE>\x3\x2\x2\x2\xEF\xF0\aq\x2\x2\xF0\xF1"+
		"\at\x2\x2\xF1@\x3\x2\x2\x2\xF2\xF3\ap\x2\x2\xF3\xF4\aq\x2\x2\xF4\xF5\a"+
		"v\x2\x2\xF5\x42\x3\x2\x2\x2\xF6\xF7\ak\x2\x2\xF7\xF8\ao\x2\x2\xF8\xF9"+
		"\ar\x2\x2\xF9\xFA\an\x2\x2\xFA\xFB\a{\x2\x2\xFB\x44\x3\x2\x2\x2\xFC\xFD"+
		"\ah\x2\x2\xFD\xFE\aq\x2\x2\xFE\xFF\at\x2\x2\xFF\x100\a\x63\x2\x2\x100"+
		"\x101\an\x2\x2\x101\x102\an\x2\x2\x102\x46\x3\x2\x2\x2\x103\x104\ag\x2"+
		"\x2\x104\x105\az\x2\x2\x105\x106\ak\x2\x2\x106\x107\au\x2\x2\x107\x108"+
		"\av\x2\x2\x108\x109\au\x2\x2\x109H\x3\x2\x2\x2\x10A\x10B\ar\x2\x2\x10B"+
		"\x10C\at\x2\x2\x10C\x10D\ag\x2\x2\x10D\x10E\ah\x2\x2\x10E\x10F\ag\x2\x2"+
		"\x10F\x110\at\x2\x2\x110\x111\ag\x2\x2\x111\x112\ap\x2\x2\x112\x113\a"+
		"\x65\x2\x2\x113\x114\ag\x2\x2\x114J\x3\x2\x2\x2\x115\x11B\x5\x33\x1A\x2"+
		"\x116\x11B\x5\x35\x1B\x2\x117\x11B\x5\x39\x1D\x2\x118\x11B\x5\x37\x1C"+
		"\x2\x119\x11B\x5;\x1E\x2\x11A\x115\x3\x2\x2\x2\x11A\x116\x3\x2\x2\x2\x11A"+
		"\x117\x3\x2\x2\x2\x11A\x118\x3\x2\x2\x2\x11A\x119\x3\x2\x2\x2\x11BL\x3"+
		"\x2\x2\x2\x11C\x121\x5+\x16\x2\x11D\x121\x5-\x17\x2\x11E\x121\x5/\x18"+
		"\x2\x11F\x121\x5\x31\x19\x2\x120\x11C\x3\x2\x2\x2\x120\x11D\x3\x2\x2\x2"+
		"\x120\x11E\x3\x2\x2\x2\x120\x11F\x3\x2\x2\x2\x121N\x3\x2\x2\x2\x122\x123"+
		"\t\x2\x2\x2\x123P\x3\x2\x2\x2\x124\x125\t\x3\x2\x2\x125R\x3\x2\x2\x2\x126"+
		"\x12A\x5O(\x2\x127\x129\x5U+\x2\x128\x127\x3\x2\x2\x2\x129\x12C\x3\x2"+
		"\x2\x2\x12A\x128\x3\x2\x2\x2\x12A\x12B\x3\x2\x2\x2\x12BT\x3\x2\x2\x2\x12C"+
		"\x12A\x3\x2\x2\x2\x12D\x132\x5O(\x2\x12E\x132\x5Q)\x2\x12F\x132\x5)\x15"+
		"\x2\x130\x132\x5\'\x14\x2\x131\x12D\x3\x2\x2\x2\x131\x12E\x3\x2\x2\x2"+
		"\x131\x12F\x3\x2\x2\x2\x131\x130\x3\x2\x2\x2\x132V\x3\x2\x2\x2\x133\x135"+
		"\x5Q)\x2\x134\x133\x3\x2\x2\x2\x135\x136\x3\x2\x2\x2\x136\x134\x3\x2\x2"+
		"\x2\x136\x137\x3\x2\x2\x2\x137\x139\x3\x2\x2\x2\x138\x13A\x5Y-\x2\x139"+
		"\x138\x3\x2\x2\x2\x139\x13A\x3\x2\x2\x2\x13AX\x3\x2\x2\x2\x13B\x13D\x5"+
		"%\x13\x2\x13C\x13E\x5Q)\x2\x13D\x13C\x3\x2\x2\x2\x13E\x13F\x3\x2\x2\x2"+
		"\x13F\x13D\x3\x2\x2\x2\x13F\x140\x3\x2\x2\x2\x140Z\x3\x2\x2\x2\x141\x142"+
		"\x5#\x12\x2\x142\x143\x5S*\x2\x143\\\x3\x2\x2\x2\x144\x145\x5S*\x2\x145"+
		"^\x3\x2\x2\x2\x146\x147\x5!\x11\x2\x147\x148\au\x2\x2\x148\x149\av\x2"+
		"\x2\x149\x14A\at\x2\x2\x14A\x14B\ak\x2\x2\x14B\x14C\ar\x2\x2\x14C\x14D"+
		"\au\x2\x2\x14D`\x3\x2\x2\x2\x14E\x14F\x5!\x11\x2\x14F\x150\av\x2\x2\x150"+
		"\x151\a{\x2\x2\x151\x152\ar\x2\x2\x152\x153\ak\x2\x2\x153\x154\ap\x2\x2"+
		"\x154\x155\ai\x2\x2\x155\x62\x3\x2\x2\x2\x156\x158\t\x4\x2\x2\x157\x156"+
		"\x3\x2\x2\x2\x158\x159\x3\x2\x2\x2\x159\x157\x3\x2\x2\x2\x159\x15A\x3"+
		"\x2\x2\x2\x15A\x15B\x3\x2\x2\x2\x15B\x15C\b\x32\x2\x2\x15C\x64\x3\x2\x2"+
		"\x2\v\x2\x11A\x120\x12A\x131\x136\x139\x13F\x159\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LanguageRecognition
