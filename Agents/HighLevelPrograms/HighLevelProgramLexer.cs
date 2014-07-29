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
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.2.2-SNAPSHOT")]
[System.CLSCompliant(false)]
public partial class HighLevelProgramLexer : Lexer {
	public const int
		IF=1, THEN=2, ELSE=3, ENDIF=4, WHILE=5, DO=6, ENDWHILE=7, KNOW=8, BEL=9, 
		LB=10, RB=11, COLON=12, SEMICOLON=13, QM=14, COMMA=15, POINT=16, UL=17, 
		DASH=18, AND=19, OR=20, NOT=21, IMPLY=22, FORALL=23, EXISTS=24, OBJECT=25, 
		AGENT=26, LETTER=27, DIGIT=28, NAME=29, CHAR=30, NUMBER=31, DECIMAL=32, 
		VAR=33, FUNSYM=34, WS=35;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] tokenNames = {
		"<INVALID>",
		"'if'", "'then'", "'else'", "'endif'", "'while'", "'do'", "'endwhile'", 
		"'know'", "'bel'", "'('", "')'", "':'", "';'", "'?'", "','", "'.'", "'_'", 
		"'-'", "'and'", "'or'", "'not'", "'imply'", "'forall'", "'exists'", "'object'", 
		"'agent'", "LETTER", "DIGIT", "NAME", "CHAR", "NUMBER", "DECIMAL", "VAR", 
		"FUNSYM", "WS"
	};
	public static readonly string[] ruleNames = {
		"IF", "THEN", "ELSE", "ENDIF", "WHILE", "DO", "ENDWHILE", "KNOW", "BEL", 
		"LB", "RB", "COLON", "SEMICOLON", "QM", "COMMA", "POINT", "UL", "DASH", 
		"AND", "OR", "NOT", "IMPLY", "FORALL", "EXISTS", "OBJECT", "AGENT", "LETTER", 
		"DIGIT", "NAME", "CHAR", "NUMBER", "DECIMAL", "VAR", "FUNSYM", "WS"
	};


	public HighLevelProgramLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	public override string GrammarFileName { get { return "HighLevelProgram.g4"; } }

	public override string[] TokenNames { get { return tokenNames; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2%\xE0\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10"+
		"\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15"+
		"\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B"+
		"\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t \x4!"+
		"\t!\x4\"\t\"\x4#\t#\x4$\t$\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5\x3\x5"+
		"\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3"+
		"\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3"+
		"\n\x3\v\x3\v\x3\f\x3\f\x3\r\x3\r\x3\xE\x3\xE\x3\xF\x3\xF\x3\x10\x3\x10"+
		"\x3\x11\x3\x11\x3\x12\x3\x12\x3\x13\x3\x13\x3\x14\x3\x14\x3\x14\x3\x14"+
		"\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3\x17"+
		"\x3\x17\x3\x17\x3\x17\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18"+
		"\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x3\x1A\x3\x1A\x3\x1A"+
		"\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1B\x3\x1B"+
		"\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1E\x3\x1E\a\x1E\xBC\n\x1E\f\x1E\xE\x1E"+
		"\xBF\v\x1E\x3\x1F\x3\x1F\x3\x1F\x3\x1F\x5\x1F\xC5\n\x1F\x3 \x6 \xC8\n"+
		" \r \xE \xC9\x3 \x5 \xCD\n \x3!\x3!\x6!\xD1\n!\r!\xE!\xD2\x3\"\x3\"\x3"+
		"\"\x3#\x3#\x3$\x6$\xDB\n$\r$\xE$\xDC\x3$\x3$\x2\x2\x2%\x3\x2\x3\x5\x2"+
		"\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17"+
		"\x2\r\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14"+
		"\'\x2\x15)\x2\x16+\x2\x17-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2"+
		"\x1C\x37\x2\x1D\x39\x2\x1E;\x2\x1F=\x2 ?\x2!\x41\x2\"\x43\x2#\x45\x2$"+
		"G\x2%\x3\x2\x5\x4\x2\x43\\\x63|\x3\x2\x32;\x5\x2\v\f\xF\xF\"\"\xE7\x2"+
		"\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2"+
		"\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2"+
		"\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2"+
		"\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2"+
		"\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2"+
		"\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2"+
		"\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2"+
		"\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2\x2\x2\x2\x41\x3\x2\x2\x2"+
		"\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2\x2\x2G\x3\x2\x2\x2\x3I\x3\x2\x2\x2"+
		"\x5L\x3\x2\x2\x2\aQ\x3\x2\x2\x2\tV\x3\x2\x2\x2\v\\\x3\x2\x2\x2\r\x62\x3"+
		"\x2\x2\x2\xF\x65\x3\x2\x2\x2\x11n\x3\x2\x2\x2\x13s\x3\x2\x2\x2\x15w\x3"+
		"\x2\x2\x2\x17y\x3\x2\x2\x2\x19{\x3\x2\x2\x2\x1B}\x3\x2\x2\x2\x1D\x7F\x3"+
		"\x2\x2\x2\x1F\x81\x3\x2\x2\x2!\x83\x3\x2\x2\x2#\x85\x3\x2\x2\x2%\x87\x3"+
		"\x2\x2\x2\'\x89\x3\x2\x2\x2)\x8D\x3\x2\x2\x2+\x90\x3\x2\x2\x2-\x94\x3"+
		"\x2\x2\x2/\x9A\x3\x2\x2\x2\x31\xA1\x3\x2\x2\x2\x33\xA8\x3\x2\x2\x2\x35"+
		"\xAF\x3\x2\x2\x2\x37\xB5\x3\x2\x2\x2\x39\xB7\x3\x2\x2\x2;\xB9\x3\x2\x2"+
		"\x2=\xC4\x3\x2\x2\x2?\xC7\x3\x2\x2\x2\x41\xCE\x3\x2\x2\x2\x43\xD4\x3\x2"+
		"\x2\x2\x45\xD7\x3\x2\x2\x2G\xDA\x3\x2\x2\x2IJ\ak\x2\x2JK\ah\x2\x2K\x4"+
		"\x3\x2\x2\x2LM\av\x2\x2MN\aj\x2\x2NO\ag\x2\x2OP\ap\x2\x2P\x6\x3\x2\x2"+
		"\x2QR\ag\x2\x2RS\an\x2\x2ST\au\x2\x2TU\ag\x2\x2U\b\x3\x2\x2\x2VW\ag\x2"+
		"\x2WX\ap\x2\x2XY\a\x66\x2\x2YZ\ak\x2\x2Z[\ah\x2\x2[\n\x3\x2\x2\x2\\]\a"+
		"y\x2\x2]^\aj\x2\x2^_\ak\x2\x2_`\an\x2\x2`\x61\ag\x2\x2\x61\f\x3\x2\x2"+
		"\x2\x62\x63\a\x66\x2\x2\x63\x64\aq\x2\x2\x64\xE\x3\x2\x2\x2\x65\x66\a"+
		"g\x2\x2\x66g\ap\x2\x2gh\a\x66\x2\x2hi\ay\x2\x2ij\aj\x2\x2jk\ak\x2\x2k"+
		"l\an\x2\x2lm\ag\x2\x2m\x10\x3\x2\x2\x2no\am\x2\x2op\ap\x2\x2pq\aq\x2\x2"+
		"qr\ay\x2\x2r\x12\x3\x2\x2\x2st\a\x64\x2\x2tu\ag\x2\x2uv\an\x2\x2v\x14"+
		"\x3\x2\x2\x2wx\a*\x2\x2x\x16\x3\x2\x2\x2yz\a+\x2\x2z\x18\x3\x2\x2\x2{"+
		"|\a<\x2\x2|\x1A\x3\x2\x2\x2}~\a=\x2\x2~\x1C\x3\x2\x2\x2\x7F\x80\a\x41"+
		"\x2\x2\x80\x1E\x3\x2\x2\x2\x81\x82\a.\x2\x2\x82 \x3\x2\x2\x2\x83\x84\a"+
		"\x30\x2\x2\x84\"\x3\x2\x2\x2\x85\x86\a\x61\x2\x2\x86$\x3\x2\x2\x2\x87"+
		"\x88\a/\x2\x2\x88&\x3\x2\x2\x2\x89\x8A\a\x63\x2\x2\x8A\x8B\ap\x2\x2\x8B"+
		"\x8C\a\x66\x2\x2\x8C(\x3\x2\x2\x2\x8D\x8E\aq\x2\x2\x8E\x8F\at\x2\x2\x8F"+
		"*\x3\x2\x2\x2\x90\x91\ap\x2\x2\x91\x92\aq\x2\x2\x92\x93\av\x2\x2\x93,"+
		"\x3\x2\x2\x2\x94\x95\ak\x2\x2\x95\x96\ao\x2\x2\x96\x97\ar\x2\x2\x97\x98"+
		"\an\x2\x2\x98\x99\a{\x2\x2\x99.\x3\x2\x2\x2\x9A\x9B\ah\x2\x2\x9B\x9C\a"+
		"q\x2\x2\x9C\x9D\at\x2\x2\x9D\x9E\a\x63\x2\x2\x9E\x9F\an\x2\x2\x9F\xA0"+
		"\an\x2\x2\xA0\x30\x3\x2\x2\x2\xA1\xA2\ag\x2\x2\xA2\xA3\az\x2\x2\xA3\xA4"+
		"\ak\x2\x2\xA4\xA5\au\x2\x2\xA5\xA6\av\x2\x2\xA6\xA7\au\x2\x2\xA7\x32\x3"+
		"\x2\x2\x2\xA8\xA9\aq\x2\x2\xA9\xAA\a\x64\x2\x2\xAA\xAB\al\x2\x2\xAB\xAC"+
		"\ag\x2\x2\xAC\xAD\a\x65\x2\x2\xAD\xAE\av\x2\x2\xAE\x34\x3\x2\x2\x2\xAF"+
		"\xB0\a\x63\x2\x2\xB0\xB1\ai\x2\x2\xB1\xB2\ag\x2\x2\xB2\xB3\ap\x2\x2\xB3"+
		"\xB4\av\x2\x2\xB4\x36\x3\x2\x2\x2\xB5\xB6\t\x2\x2\x2\xB6\x38\x3\x2\x2"+
		"\x2\xB7\xB8\t\x3\x2\x2\xB8:\x3\x2\x2\x2\xB9\xBD\x5\x37\x1C\x2\xBA\xBC"+
		"\x5=\x1F\x2\xBB\xBA\x3\x2\x2\x2\xBC\xBF\x3\x2\x2\x2\xBD\xBB\x3\x2\x2\x2"+
		"\xBD\xBE\x3\x2\x2\x2\xBE<\x3\x2\x2\x2\xBF\xBD\x3\x2\x2\x2\xC0\xC5\x5\x37"+
		"\x1C\x2\xC1\xC5\x5\x39\x1D\x2\xC2\xC5\x5%\x13\x2\xC3\xC5\x5#\x12\x2\xC4"+
		"\xC0\x3\x2\x2\x2\xC4\xC1\x3\x2\x2\x2\xC4\xC2\x3\x2\x2\x2\xC4\xC3\x3\x2"+
		"\x2\x2\xC5>\x3\x2\x2\x2\xC6\xC8\x5\x39\x1D\x2\xC7\xC6\x3\x2\x2\x2\xC8"+
		"\xC9\x3\x2\x2\x2\xC9\xC7\x3\x2\x2\x2\xC9\xCA\x3\x2\x2\x2\xCA\xCC\x3\x2"+
		"\x2\x2\xCB\xCD\x5\x41!\x2\xCC\xCB\x3\x2\x2\x2\xCC\xCD\x3\x2\x2\x2\xCD"+
		"@\x3\x2\x2\x2\xCE\xD0\x5!\x11\x2\xCF\xD1\x5\x39\x1D\x2\xD0\xCF\x3\x2\x2"+
		"\x2\xD1\xD2\x3\x2\x2\x2\xD2\xD0\x3\x2\x2\x2\xD2\xD3\x3\x2\x2\x2\xD3\x42"+
		"\x3\x2\x2\x2\xD4\xD5\x5\x1D\xF\x2\xD5\xD6\x5;\x1E\x2\xD6\x44\x3\x2\x2"+
		"\x2\xD7\xD8\x5;\x1E\x2\xD8\x46\x3\x2\x2\x2\xD9\xDB\t\x4\x2\x2\xDA\xD9"+
		"\x3\x2\x2\x2\xDB\xDC\x3\x2\x2\x2\xDC\xDA\x3\x2\x2\x2\xDC\xDD\x3\x2\x2"+
		"\x2\xDD\xDE\x3\x2\x2\x2\xDE\xDF\b$\x2\x2\xDFH\x3\x2\x2\x2\t\x2\xBD\xC4"+
		"\xC9\xCC\xD2\xDC\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace LanguageRecognition
