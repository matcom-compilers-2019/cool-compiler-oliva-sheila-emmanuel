//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Ale\Desktop\Compiler Sheila\Compiler\Logic\Parser\Grammar\coolgrammar.g4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public partial class coolgrammarLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, STR=20, CLASS=21, INHERITS=22, LET=23, IN=24, WS=25, 
		IF=26, THEN=27, ELSE=28, FI=29, WHILE=30, LOOP=31, POOL=32, NEW=33, TYPE=34, 
		ISVOID=35, INTEGER=36, TRUE=37, FALSE=38, NOT=39, CASE=40, OF=41, ESAC=42, 
		ID=43;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "STR", "CLASS", "INHERITS", "LET", "IN", "WS", "IF", 
		"THEN", "ELSE", "FI", "WHILE", "LOOP", "POOL", "NEW", "TYPE", "ISVOID", 
		"INTEGER", "TRUE", "FALSE", "NOT", "CASE", "OF", "ESAC", "ID", "ESC", 
		"UNICODE", "HEX"
	};


	public coolgrammarLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public coolgrammarLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "';'", "'{'", "'}'", "'('", "')'", "':'", "'<-'", "'@'", "'.'", 
		"'~'", "'*'", "'/'", "'+'", "'-'", "'<'", "'<='", "'='", "','", "'=>'", 
		null, "'class'", "'inherits'", "'let'", "'in'", null, "'if'", "'then'", 
		"'else'", "'fi'", "'while'", "'loop'", "'pool'", "'new'", null, "'isvoid'", 
		null, "'true'", "'false'", "'not'", "'case'", "'of'", "'esac'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, "STR", "CLASS", "INHERITS", 
		"LET", "IN", "WS", "IF", "THEN", "ELSE", "FI", "WHILE", "LOOP", "POOL", 
		"NEW", "TYPE", "ISVOID", "INTEGER", "TRUE", "FALSE", "NOT", "CASE", "OF", 
		"ESAC", "ID"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "coolgrammar.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static coolgrammarLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '-', '\x116', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', '\x4', 
		' ', '\t', ' ', '\x4', '!', '\t', '!', '\x4', '\"', '\t', '\"', '\x4', 
		'#', '\t', '#', '\x4', '$', '\t', '$', '\x4', '%', '\t', '%', '\x4', '&', 
		'\t', '&', '\x4', '\'', '\t', '\'', '\x4', '(', '\t', '(', '\x4', ')', 
		'\t', ')', '\x4', '*', '\t', '*', '\x4', '+', '\t', '+', '\x4', ',', '\t', 
		',', '\x4', '-', '\t', '-', '\x4', '.', '\t', '.', '\x4', '/', '\t', '/', 
		'\x3', '\x2', '\x3', '\x2', '\x3', '\x3', '\x3', '\x3', '\x3', '\x4', 
		'\x3', '\x4', '\x3', '\x5', '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', 
		'\x3', '\a', '\x3', '\a', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', 
		'\t', '\x3', '\t', '\x3', '\n', '\x3', '\n', '\x3', '\v', '\x3', '\v', 
		'\x3', '\f', '\x3', '\f', '\x3', '\r', '\x3', '\r', '\x3', '\xE', '\x3', 
		'\xE', '\x3', '\xF', '\x3', '\xF', '\x3', '\x10', '\x3', '\x10', '\x3', 
		'\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x12', '\x3', '\x12', '\x3', 
		'\x13', '\x3', '\x13', '\x3', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', 
		'\x15', '\x3', '\x15', '\x3', '\x15', '\a', '\x15', '\x8C', '\n', '\x15', 
		'\f', '\x15', '\xE', '\x15', '\x8F', '\v', '\x15', '\x3', '\x15', '\x3', 
		'\x15', '\x3', '\x16', '\x3', '\x16', '\x3', '\x16', '\x3', '\x16', '\x3', 
		'\x16', '\x3', '\x16', '\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', 
		'\x17', '\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', '\x17', '\x3', 
		'\x17', '\x3', '\x18', '\x3', '\x18', '\x3', '\x18', '\x3', '\x18', '\x3', 
		'\x19', '\x3', '\x19', '\x3', '\x19', '\x3', '\x1A', '\x6', '\x1A', '\xAA', 
		'\n', '\x1A', '\r', '\x1A', '\xE', '\x1A', '\xAB', '\x3', '\x1A', '\x3', 
		'\x1A', '\x3', '\x1B', '\x3', '\x1B', '\x3', '\x1B', '\x3', '\x1C', '\x3', 
		'\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1D', '\x3', 
		'\x1D', '\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1E', '\x3', 
		'\x1E', '\x3', '\x1E', '\x3', '\x1F', '\x3', '\x1F', '\x3', '\x1F', '\x3', 
		'\x1F', '\x3', '\x1F', '\x3', '\x1F', '\x3', ' ', '\x3', ' ', '\x3', ' ', 
		'\x3', ' ', '\x3', ' ', '\x3', '!', '\x3', '!', '\x3', '!', '\x3', '!', 
		'\x3', '!', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\x3', 
		'#', '\x3', '#', '\a', '#', '\xD6', '\n', '#', '\f', '#', '\xE', '#', 
		'\xD9', '\v', '#', '\x3', '$', '\x3', '$', '\x3', '$', '\x3', '$', '\x3', 
		'$', '\x3', '$', '\x3', '$', '\x3', '%', '\x6', '%', '\xE3', '\n', '%', 
		'\r', '%', '\xE', '%', '\xE4', '\x3', '&', '\x3', '&', '\x3', '&', '\x3', 
		'&', '\x3', '&', '\x3', '\'', '\x3', '\'', '\x3', '\'', '\x3', '\'', '\x3', 
		'\'', '\x3', '\'', '\x3', '(', '\x3', '(', '\x3', '(', '\x3', '(', '\x3', 
		')', '\x3', ')', '\x3', ')', '\x3', ')', '\x3', ')', '\x3', '*', '\x3', 
		'*', '\x3', '*', '\x3', '+', '\x3', '+', '\x3', '+', '\x3', '+', '\x3', 
		'+', '\x3', ',', '\x3', ',', '\a', ',', '\x105', '\n', ',', '\f', ',', 
		'\xE', ',', '\x108', '\v', ',', '\x3', '-', '\x3', '-', '\x3', '-', '\x5', 
		'-', '\x10D', '\n', '-', '\x3', '.', '\x3', '.', '\x3', '.', '\x3', '.', 
		'\x3', '.', '\x3', '.', '\x3', '/', '\x3', '/', '\x2', '\x2', '\x30', 
		'\x3', '\x3', '\x5', '\x4', '\a', '\x5', '\t', '\x6', '\v', '\a', '\r', 
		'\b', '\xF', '\t', '\x11', '\n', '\x13', '\v', '\x15', '\f', '\x17', '\r', 
		'\x19', '\xE', '\x1B', '\xF', '\x1D', '\x10', '\x1F', '\x11', '!', '\x12', 
		'#', '\x13', '%', '\x14', '\'', '\x15', ')', '\x16', '+', '\x17', '-', 
		'\x18', '/', '\x19', '\x31', '\x1A', '\x33', '\x1B', '\x35', '\x1C', '\x37', 
		'\x1D', '\x39', '\x1E', ';', '\x1F', '=', ' ', '?', '!', '\x41', '\"', 
		'\x43', '#', '\x45', '$', 'G', '%', 'I', '&', 'K', '\'', 'M', '(', 'O', 
		')', 'Q', '*', 'S', '+', 'U', ',', 'W', '-', 'Y', '\x2', '[', '\x2', ']', 
		'\x2', '\x3', '\x2', '\n', '\x4', '\x2', '$', '$', '^', '^', '\x5', '\x2', 
		'\v', '\f', '\xF', '\xF', '\"', '\"', '\x3', '\x2', '\x43', '\\', '\x6', 
		'\x2', '\x32', ';', '\x43', '\\', '\x61', '\x61', '\x63', '|', '\x3', 
		'\x2', '\x32', ';', '\x3', '\x2', '\x63', '|', '\n', '\x2', '$', '$', 
		'\x31', '\x31', '^', '^', '\x64', '\x64', 'h', 'h', 'p', 'p', 't', 't', 
		'v', 'v', '\x5', '\x2', '\x32', ';', '\x43', 'H', '\x63', 'h', '\x2', 
		'\x119', '\x2', '\x3', '\x3', '\x2', '\x2', '\x2', '\x2', '\x5', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\a', '\x3', '\x2', '\x2', '\x2', '\x2', '\t', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\r', '\x3', '\x2', '\x2', '\x2', '\x2', '\xF', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x11', '\x3', '\x2', '\x2', '\x2', '\x2', '\x13', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x15', '\x3', '\x2', '\x2', '\x2', '\x2', '\x17', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x19', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x1B', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1D', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x1F', '\x3', '\x2', '\x2', '\x2', '\x2', '!', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '#', '\x3', '\x2', '\x2', '\x2', '\x2', '%', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\'', '\x3', '\x2', '\x2', '\x2', '\x2', 
		')', '\x3', '\x2', '\x2', '\x2', '\x2', '+', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '-', '\x3', '\x2', '\x2', '\x2', '\x2', '/', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x31', '\x3', '\x2', '\x2', '\x2', '\x2', '\x33', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x35', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x37', '\x3', '\x2', '\x2', '\x2', '\x2', '\x39', '\x3', '\x2', '\x2', 
		'\x2', '\x2', ';', '\x3', '\x2', '\x2', '\x2', '\x2', '=', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '?', '\x3', '\x2', '\x2', '\x2', '\x2', '\x41', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x43', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x45', '\x3', '\x2', '\x2', '\x2', '\x2', 'G', '\x3', '\x2', '\x2', '\x2', 
		'\x2', 'I', '\x3', '\x2', '\x2', '\x2', '\x2', 'K', '\x3', '\x2', '\x2', 
		'\x2', '\x2', 'M', '\x3', '\x2', '\x2', '\x2', '\x2', 'O', '\x3', '\x2', 
		'\x2', '\x2', '\x2', 'Q', '\x3', '\x2', '\x2', '\x2', '\x2', 'S', '\x3', 
		'\x2', '\x2', '\x2', '\x2', 'U', '\x3', '\x2', '\x2', '\x2', '\x2', 'W', 
		'\x3', '\x2', '\x2', '\x2', '\x3', '_', '\x3', '\x2', '\x2', '\x2', '\x5', 
		'\x61', '\x3', '\x2', '\x2', '\x2', '\a', '\x63', '\x3', '\x2', '\x2', 
		'\x2', '\t', '\x65', '\x3', '\x2', '\x2', '\x2', '\v', 'g', '\x3', '\x2', 
		'\x2', '\x2', '\r', 'i', '\x3', '\x2', '\x2', '\x2', '\xF', 'k', '\x3', 
		'\x2', '\x2', '\x2', '\x11', 'n', '\x3', '\x2', '\x2', '\x2', '\x13', 
		'p', '\x3', '\x2', '\x2', '\x2', '\x15', 'r', '\x3', '\x2', '\x2', '\x2', 
		'\x17', 't', '\x3', '\x2', '\x2', '\x2', '\x19', 'v', '\x3', '\x2', '\x2', 
		'\x2', '\x1B', 'x', '\x3', '\x2', '\x2', '\x2', '\x1D', 'z', '\x3', '\x2', 
		'\x2', '\x2', '\x1F', '|', '\x3', '\x2', '\x2', '\x2', '!', '~', '\x3', 
		'\x2', '\x2', '\x2', '#', '\x81', '\x3', '\x2', '\x2', '\x2', '%', '\x83', 
		'\x3', '\x2', '\x2', '\x2', '\'', '\x85', '\x3', '\x2', '\x2', '\x2', 
		')', '\x88', '\x3', '\x2', '\x2', '\x2', '+', '\x92', '\x3', '\x2', '\x2', 
		'\x2', '-', '\x98', '\x3', '\x2', '\x2', '\x2', '/', '\xA1', '\x3', '\x2', 
		'\x2', '\x2', '\x31', '\xA5', '\x3', '\x2', '\x2', '\x2', '\x33', '\xA9', 
		'\x3', '\x2', '\x2', '\x2', '\x35', '\xAF', '\x3', '\x2', '\x2', '\x2', 
		'\x37', '\xB2', '\x3', '\x2', '\x2', '\x2', '\x39', '\xB7', '\x3', '\x2', 
		'\x2', '\x2', ';', '\xBC', '\x3', '\x2', '\x2', '\x2', '=', '\xBF', '\x3', 
		'\x2', '\x2', '\x2', '?', '\xC5', '\x3', '\x2', '\x2', '\x2', '\x41', 
		'\xCA', '\x3', '\x2', '\x2', '\x2', '\x43', '\xCF', '\x3', '\x2', '\x2', 
		'\x2', '\x45', '\xD3', '\x3', '\x2', '\x2', '\x2', 'G', '\xDA', '\x3', 
		'\x2', '\x2', '\x2', 'I', '\xE2', '\x3', '\x2', '\x2', '\x2', 'K', '\xE6', 
		'\x3', '\x2', '\x2', '\x2', 'M', '\xEB', '\x3', '\x2', '\x2', '\x2', 'O', 
		'\xF1', '\x3', '\x2', '\x2', '\x2', 'Q', '\xF5', '\x3', '\x2', '\x2', 
		'\x2', 'S', '\xFA', '\x3', '\x2', '\x2', '\x2', 'U', '\xFD', '\x3', '\x2', 
		'\x2', '\x2', 'W', '\x102', '\x3', '\x2', '\x2', '\x2', 'Y', '\x109', 
		'\x3', '\x2', '\x2', '\x2', '[', '\x10E', '\x3', '\x2', '\x2', '\x2', 
		']', '\x114', '\x3', '\x2', '\x2', '\x2', '_', '`', '\a', '=', '\x2', 
		'\x2', '`', '\x4', '\x3', '\x2', '\x2', '\x2', '\x61', '\x62', '\a', '}', 
		'\x2', '\x2', '\x62', '\x6', '\x3', '\x2', '\x2', '\x2', '\x63', '\x64', 
		'\a', '\x7F', '\x2', '\x2', '\x64', '\b', '\x3', '\x2', '\x2', '\x2', 
		'\x65', '\x66', '\a', '*', '\x2', '\x2', '\x66', '\n', '\x3', '\x2', '\x2', 
		'\x2', 'g', 'h', '\a', '+', '\x2', '\x2', 'h', '\f', '\x3', '\x2', '\x2', 
		'\x2', 'i', 'j', '\a', '<', '\x2', '\x2', 'j', '\xE', '\x3', '\x2', '\x2', 
		'\x2', 'k', 'l', '\a', '>', '\x2', '\x2', 'l', 'm', '\a', '/', '\x2', 
		'\x2', 'm', '\x10', '\x3', '\x2', '\x2', '\x2', 'n', 'o', '\a', '\x42', 
		'\x2', '\x2', 'o', '\x12', '\x3', '\x2', '\x2', '\x2', 'p', 'q', '\a', 
		'\x30', '\x2', '\x2', 'q', '\x14', '\x3', '\x2', '\x2', '\x2', 'r', 's', 
		'\a', '\x80', '\x2', '\x2', 's', '\x16', '\x3', '\x2', '\x2', '\x2', 't', 
		'u', '\a', ',', '\x2', '\x2', 'u', '\x18', '\x3', '\x2', '\x2', '\x2', 
		'v', 'w', '\a', '\x31', '\x2', '\x2', 'w', '\x1A', '\x3', '\x2', '\x2', 
		'\x2', 'x', 'y', '\a', '-', '\x2', '\x2', 'y', '\x1C', '\x3', '\x2', '\x2', 
		'\x2', 'z', '{', '\a', '/', '\x2', '\x2', '{', '\x1E', '\x3', '\x2', '\x2', 
		'\x2', '|', '}', '\a', '>', '\x2', '\x2', '}', ' ', '\x3', '\x2', '\x2', 
		'\x2', '~', '\x7F', '\a', '>', '\x2', '\x2', '\x7F', '\x80', '\a', '?', 
		'\x2', '\x2', '\x80', '\"', '\x3', '\x2', '\x2', '\x2', '\x81', '\x82', 
		'\a', '?', '\x2', '\x2', '\x82', '$', '\x3', '\x2', '\x2', '\x2', '\x83', 
		'\x84', '\a', '.', '\x2', '\x2', '\x84', '&', '\x3', '\x2', '\x2', '\x2', 
		'\x85', '\x86', '\a', '?', '\x2', '\x2', '\x86', '\x87', '\a', '@', '\x2', 
		'\x2', '\x87', '(', '\x3', '\x2', '\x2', '\x2', '\x88', '\x8D', '\a', 
		'$', '\x2', '\x2', '\x89', '\x8C', '\x5', 'Y', '-', '\x2', '\x8A', '\x8C', 
		'\n', '\x2', '\x2', '\x2', '\x8B', '\x89', '\x3', '\x2', '\x2', '\x2', 
		'\x8B', '\x8A', '\x3', '\x2', '\x2', '\x2', '\x8C', '\x8F', '\x3', '\x2', 
		'\x2', '\x2', '\x8D', '\x8B', '\x3', '\x2', '\x2', '\x2', '\x8D', '\x8E', 
		'\x3', '\x2', '\x2', '\x2', '\x8E', '\x90', '\x3', '\x2', '\x2', '\x2', 
		'\x8F', '\x8D', '\x3', '\x2', '\x2', '\x2', '\x90', '\x91', '\a', '$', 
		'\x2', '\x2', '\x91', '*', '\x3', '\x2', '\x2', '\x2', '\x92', '\x93', 
		'\a', '\x65', '\x2', '\x2', '\x93', '\x94', '\a', 'n', '\x2', '\x2', '\x94', 
		'\x95', '\a', '\x63', '\x2', '\x2', '\x95', '\x96', '\a', 'u', '\x2', 
		'\x2', '\x96', '\x97', '\a', 'u', '\x2', '\x2', '\x97', ',', '\x3', '\x2', 
		'\x2', '\x2', '\x98', '\x99', '\a', 'k', '\x2', '\x2', '\x99', '\x9A', 
		'\a', 'p', '\x2', '\x2', '\x9A', '\x9B', '\a', 'j', '\x2', '\x2', '\x9B', 
		'\x9C', '\a', 'g', '\x2', '\x2', '\x9C', '\x9D', '\a', 't', '\x2', '\x2', 
		'\x9D', '\x9E', '\a', 'k', '\x2', '\x2', '\x9E', '\x9F', '\a', 'v', '\x2', 
		'\x2', '\x9F', '\xA0', '\a', 'u', '\x2', '\x2', '\xA0', '.', '\x3', '\x2', 
		'\x2', '\x2', '\xA1', '\xA2', '\a', 'n', '\x2', '\x2', '\xA2', '\xA3', 
		'\a', 'g', '\x2', '\x2', '\xA3', '\xA4', '\a', 'v', '\x2', '\x2', '\xA4', 
		'\x30', '\x3', '\x2', '\x2', '\x2', '\xA5', '\xA6', '\a', 'k', '\x2', 
		'\x2', '\xA6', '\xA7', '\a', 'p', '\x2', '\x2', '\xA7', '\x32', '\x3', 
		'\x2', '\x2', '\x2', '\xA8', '\xAA', '\t', '\x3', '\x2', '\x2', '\xA9', 
		'\xA8', '\x3', '\x2', '\x2', '\x2', '\xAA', '\xAB', '\x3', '\x2', '\x2', 
		'\x2', '\xAB', '\xA9', '\x3', '\x2', '\x2', '\x2', '\xAB', '\xAC', '\x3', 
		'\x2', '\x2', '\x2', '\xAC', '\xAD', '\x3', '\x2', '\x2', '\x2', '\xAD', 
		'\xAE', '\b', '\x1A', '\x2', '\x2', '\xAE', '\x34', '\x3', '\x2', '\x2', 
		'\x2', '\xAF', '\xB0', '\a', 'k', '\x2', '\x2', '\xB0', '\xB1', '\a', 
		'h', '\x2', '\x2', '\xB1', '\x36', '\x3', '\x2', '\x2', '\x2', '\xB2', 
		'\xB3', '\a', 'v', '\x2', '\x2', '\xB3', '\xB4', '\a', 'j', '\x2', '\x2', 
		'\xB4', '\xB5', '\a', 'g', '\x2', '\x2', '\xB5', '\xB6', '\a', 'p', '\x2', 
		'\x2', '\xB6', '\x38', '\x3', '\x2', '\x2', '\x2', '\xB7', '\xB8', '\a', 
		'g', '\x2', '\x2', '\xB8', '\xB9', '\a', 'n', '\x2', '\x2', '\xB9', '\xBA', 
		'\a', 'u', '\x2', '\x2', '\xBA', '\xBB', '\a', 'g', '\x2', '\x2', '\xBB', 
		':', '\x3', '\x2', '\x2', '\x2', '\xBC', '\xBD', '\a', 'h', '\x2', '\x2', 
		'\xBD', '\xBE', '\a', 'k', '\x2', '\x2', '\xBE', '<', '\x3', '\x2', '\x2', 
		'\x2', '\xBF', '\xC0', '\a', 'y', '\x2', '\x2', '\xC0', '\xC1', '\a', 
		'j', '\x2', '\x2', '\xC1', '\xC2', '\a', 'k', '\x2', '\x2', '\xC2', '\xC3', 
		'\a', 'n', '\x2', '\x2', '\xC3', '\xC4', '\a', 'g', '\x2', '\x2', '\xC4', 
		'>', '\x3', '\x2', '\x2', '\x2', '\xC5', '\xC6', '\a', 'n', '\x2', '\x2', 
		'\xC6', '\xC7', '\a', 'q', '\x2', '\x2', '\xC7', '\xC8', '\a', 'q', '\x2', 
		'\x2', '\xC8', '\xC9', '\a', 'r', '\x2', '\x2', '\xC9', '@', '\x3', '\x2', 
		'\x2', '\x2', '\xCA', '\xCB', '\a', 'r', '\x2', '\x2', '\xCB', '\xCC', 
		'\a', 'q', '\x2', '\x2', '\xCC', '\xCD', '\a', 'q', '\x2', '\x2', '\xCD', 
		'\xCE', '\a', 'n', '\x2', '\x2', '\xCE', '\x42', '\x3', '\x2', '\x2', 
		'\x2', '\xCF', '\xD0', '\a', 'p', '\x2', '\x2', '\xD0', '\xD1', '\a', 
		'g', '\x2', '\x2', '\xD1', '\xD2', '\a', 'y', '\x2', '\x2', '\xD2', '\x44', 
		'\x3', '\x2', '\x2', '\x2', '\xD3', '\xD7', '\t', '\x4', '\x2', '\x2', 
		'\xD4', '\xD6', '\t', '\x5', '\x2', '\x2', '\xD5', '\xD4', '\x3', '\x2', 
		'\x2', '\x2', '\xD6', '\xD9', '\x3', '\x2', '\x2', '\x2', '\xD7', '\xD5', 
		'\x3', '\x2', '\x2', '\x2', '\xD7', '\xD8', '\x3', '\x2', '\x2', '\x2', 
		'\xD8', '\x46', '\x3', '\x2', '\x2', '\x2', '\xD9', '\xD7', '\x3', '\x2', 
		'\x2', '\x2', '\xDA', '\xDB', '\a', 'k', '\x2', '\x2', '\xDB', '\xDC', 
		'\a', 'u', '\x2', '\x2', '\xDC', '\xDD', '\a', 'x', '\x2', '\x2', '\xDD', 
		'\xDE', '\a', 'q', '\x2', '\x2', '\xDE', '\xDF', '\a', 'k', '\x2', '\x2', 
		'\xDF', '\xE0', '\a', '\x66', '\x2', '\x2', '\xE0', 'H', '\x3', '\x2', 
		'\x2', '\x2', '\xE1', '\xE3', '\t', '\x6', '\x2', '\x2', '\xE2', '\xE1', 
		'\x3', '\x2', '\x2', '\x2', '\xE3', '\xE4', '\x3', '\x2', '\x2', '\x2', 
		'\xE4', '\xE2', '\x3', '\x2', '\x2', '\x2', '\xE4', '\xE5', '\x3', '\x2', 
		'\x2', '\x2', '\xE5', 'J', '\x3', '\x2', '\x2', '\x2', '\xE6', '\xE7', 
		'\a', 'v', '\x2', '\x2', '\xE7', '\xE8', '\a', 't', '\x2', '\x2', '\xE8', 
		'\xE9', '\a', 'w', '\x2', '\x2', '\xE9', '\xEA', '\a', 'g', '\x2', '\x2', 
		'\xEA', 'L', '\x3', '\x2', '\x2', '\x2', '\xEB', '\xEC', '\a', 'h', '\x2', 
		'\x2', '\xEC', '\xED', '\a', '\x63', '\x2', '\x2', '\xED', '\xEE', '\a', 
		'n', '\x2', '\x2', '\xEE', '\xEF', '\a', 'u', '\x2', '\x2', '\xEF', '\xF0', 
		'\a', 'g', '\x2', '\x2', '\xF0', 'N', '\x3', '\x2', '\x2', '\x2', '\xF1', 
		'\xF2', '\a', 'p', '\x2', '\x2', '\xF2', '\xF3', '\a', 'q', '\x2', '\x2', 
		'\xF3', '\xF4', '\a', 'v', '\x2', '\x2', '\xF4', 'P', '\x3', '\x2', '\x2', 
		'\x2', '\xF5', '\xF6', '\a', '\x65', '\x2', '\x2', '\xF6', '\xF7', '\a', 
		'\x63', '\x2', '\x2', '\xF7', '\xF8', '\a', 'u', '\x2', '\x2', '\xF8', 
		'\xF9', '\a', 'g', '\x2', '\x2', '\xF9', 'R', '\x3', '\x2', '\x2', '\x2', 
		'\xFA', '\xFB', '\a', 'q', '\x2', '\x2', '\xFB', '\xFC', '\a', 'h', '\x2', 
		'\x2', '\xFC', 'T', '\x3', '\x2', '\x2', '\x2', '\xFD', '\xFE', '\a', 
		'g', '\x2', '\x2', '\xFE', '\xFF', '\a', 'u', '\x2', '\x2', '\xFF', '\x100', 
		'\a', '\x63', '\x2', '\x2', '\x100', '\x101', '\a', '\x65', '\x2', '\x2', 
		'\x101', 'V', '\x3', '\x2', '\x2', '\x2', '\x102', '\x106', '\t', '\a', 
		'\x2', '\x2', '\x103', '\x105', '\t', '\x5', '\x2', '\x2', '\x104', '\x103', 
		'\x3', '\x2', '\x2', '\x2', '\x105', '\x108', '\x3', '\x2', '\x2', '\x2', 
		'\x106', '\x104', '\x3', '\x2', '\x2', '\x2', '\x106', '\x107', '\x3', 
		'\x2', '\x2', '\x2', '\x107', 'X', '\x3', '\x2', '\x2', '\x2', '\x108', 
		'\x106', '\x3', '\x2', '\x2', '\x2', '\x109', '\x10C', '\a', '^', '\x2', 
		'\x2', '\x10A', '\x10D', '\t', '\b', '\x2', '\x2', '\x10B', '\x10D', '\x5', 
		'[', '.', '\x2', '\x10C', '\x10A', '\x3', '\x2', '\x2', '\x2', '\x10C', 
		'\x10B', '\x3', '\x2', '\x2', '\x2', '\x10D', 'Z', '\x3', '\x2', '\x2', 
		'\x2', '\x10E', '\x10F', '\a', 'w', '\x2', '\x2', '\x10F', '\x110', '\x5', 
		']', '/', '\x2', '\x110', '\x111', '\x5', ']', '/', '\x2', '\x111', '\x112', 
		'\x5', ']', '/', '\x2', '\x112', '\x113', '\x5', ']', '/', '\x2', '\x113', 
		'\\', '\x3', '\x2', '\x2', '\x2', '\x114', '\x115', '\t', '\t', '\x2', 
		'\x2', '\x115', '^', '\x3', '\x2', '\x2', '\x2', '\n', '\x2', '\x8B', 
		'\x8D', '\xAB', '\xD7', '\xE4', '\x106', '\x10C', '\x3', '\b', '\x2', 
		'\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
