grammar HighLevelProgram;

/*
 * Parser Rules
 */

program: action
       | program SEMICOLON program
	   | IF subjectFormula THEN program ELSE program ENDIF
	   | IF subjectFormula THEN program ENDIF
	   | WHILE subjectFormula DO program ENDWHILE;

subjectFormula: KNOW LB objectFormula RB
              | BEL LB objectFormula RB
			  | NOT subjectFormula
			  | subjectFormula AND subjectFormula
			  | subjectFormula OR subjectFormula;

objectFormula: predicate
       | LB objectFormula RB
       | NOT objectFormula
       | objectFormula AND objectFormula
	   | objectFormula OR objectFormula;
predicate: NAME LB listName RB;

action: NAME LB listName? RB;

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
KNOW: 'know';
BEL: 'bel';
LB: '(';
RB: ')';
COLON: ':';
SEMICOLON: ';';
QM: '?';
COMMA: ',';
POINT: '.';
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
DECIMAL: POINT DIGIT+;
VAR: QM NAME;
FUNSYM : NAME;

WS: [ \t\r\n]+ -> channel(HIDDEN);
