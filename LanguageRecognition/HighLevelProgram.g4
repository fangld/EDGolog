grammar HighLevelProgram;

/*
 * Parser Rules
 */

program: action LB listName? RB
       | program SEMICOLON program
	   | IF subjectFormula THEN program ELSE program ENDIF
	   | IF subjectFormula THEN program ENDIF
	   | WHILE subjectFormula DO program ENDWHILE;

subjectFormula: KNOW LB objectFormula RB
              | BEL LB objectFormula RB
			  | LB subjectFormula RB
			  | NOT subjectFormula
			  | subjectFormula AND subjectFormula
			  | subjectFormula OR subjectFormula
			  | LB EXISTS listVariable RB LB subjectFormula RB
			  | LB FORALL listVariable RB LB subjectFormula RB;

objectFormula: predicate LB listName RB
       | NOT objectFormula
       | objectFormula AND objectFormula
	   | objectFormula OR objectFormula
	   | LB EXISTS listVariable RB LB objectFormula RB
	   | LB FORALL listVariable RB LB objectFormula RB
	   | LB objectFormula RB;

predicate: NAME;
action: NAME;
listName: NAME | NAME COMMA listName;
listVariable: VAR* | VAR+ MINUS type listVariable;
type: OBJECT | AGENT | NAME;

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
OBJECT: 'object';
AGENT: 'agent';
LETTER: [a-zA-Z];
DIGIT: [0-9];

NAME: LETTER CHAR*;
CHAR: LETTER | DIGIT | DASH | UL;
NUMBER: DIGIT+ DECIMAL?;
DECIMAL: POINT DIGIT+;
VAR: QM NAME;
FUNSYM : NAME;

WS: [ \t\r\n]+ -> channel(HIDDEN);
