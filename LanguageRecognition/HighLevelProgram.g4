grammar HighLevelProgram;

/*
 * Parser Rules
 */

program: action
       | program SEMICOLON program
	   | IF formula THEN program ELSE program ENDIF
	   | IF formula THEN program ENDIF
	   | WHILE formula DO program ENDWHILE;

formula: predicate
       | LB formula RB
       | NOT formula
       | formula AND formula
	   | formula OR formula;
predicate: NAME LB listName RB;

action: NAME LB listName RB;

listName: NAME | NAME COMMA listName;

/*
 * Lexer Rules
 */

 // Keywords
IF: 'if';
THEN: 'then';
ELSE: 'else';
ENDIF: 'endif';
WHILE: 'while';
DO: 'do';
ENDWHILE: 'endwhile';
LB: '(';
RB: ')';
COLON: ':';
SEMICOLON: ';';
QM: '?';
COMMA: ',';
UL: '_';
DASH: '-';
AND: 'and';
OR: 'or';
NOT: 'not';
IMPLY: 'imply';
FORALL: 'forall';
EXISTS: 'exists';
LETTER: [a-zA-Z];
DIGIT: [0-9];

NAME: LETTER CHAR*;
CHAR: LETTER | DIGIT | DASH | UL;
NUMBER: DIGIT+ DECIMAL?;
DECIMAL: COMMA DIGIT+;
VAR: QM NAME;
FUNSYM : NAME;

WS: [ \t\r\n]+ -> channel(HIDDEN);
