lexer grammar PlanningLexer;

// Keywords in domain
DOM: 'domain';
DEF: 'define';
REQ: 'requirements';
TYPE: 'types';
PRED: 'predicates';
ACT: 'action';
PARM: 'parameters';
PRE: 'precondition';
EFF: 'effect';
OBJ: 'object';
EITHER: 'either';
STRIPS: 'strips'; // Basic STRIPS-style adds and deletes
TYPING: 'typing'; // Allow type names in declarations of variables

// Common used in domain and problem
LB: '(';
RB: ')';
LSB: '[';
RSB: ']';
COLON: ':';
QM: '?';
COMMA: '.';
UL: '_';
DASH: '-';
PLUS: '+';
MINUS: '-';
MULT: '*';
DIV: '/';
EQ: '=';
LT: '<';
LEQ: '<=';
GT: '>';
GEQ: '>=';
AND: 'and';
OR: 'or';
NOT: 'not';
IMPLY: 'imply';
FORALL: 'forall';
EXISTS: 'exists';
WHEN: 'when';
PREF: 'preference';
BINCOMP: EQ | LT | GT | LEQ | GEQ ;
BINOP: PLUS | MINUS | MULT | DIV ;
LETTER: [a-zA-Z];
DIGIT: [0-9];

NAME: LETTER CHAR* ;
CHAR: LETTER | DIGIT | DASH | UL ;
NUMBER: DIGIT+ DECIMAL? ;
DECIMAL: COMMA DIGIT+ ;
VAR: QM NAME ;
FUNSYM : NAME ;

// Keywords in requirements


WS: [ \t\r\n]+ -> channel(HIDDEN);
