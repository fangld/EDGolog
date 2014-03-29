lexer grammar PlanningLexer;

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
MULTIPLE: '*';
DIVIDE: '/';
EQUAL: '=';
LESSTHEN: '<';
LEQ: '<=';
GREATERTHEN: '>';
GEQ: '>=';
BINARYCOMP: EQUAL | LESSTHEN | GREATERTHEN | LEQ | GEQ ;
BINARYOP: PLUS | MINUS | MULTIPLE | DIVIDE ;
LETTER: [a..z|A..Z];
DIGIT: [0..9];

NAME: LETTER CHAR* ;
CHAR: LETTER | DIGIT | DASH | UNDERLINE ;
NUMBER: DIGIT+ DECIMAL? ;
DECIMAL: COMMA DIGIT+ ;
VAR: QM NAME ;

// Keywords in domain
DOM: 'domain';
DEF: 'define';
REQ: 'requirements';
TYPE: 'types';
PRED: 'predicates';
ACT: 'actions';
PARM: 'parameters';
PARM: 'parameters';
PRE: 'precondition';
EFF: 'effect';
FUNSYM : NAME ;

// Keywords in requirements
STRIPS: COLON 'strips'; // Basic STRIPS-style adds and deletes
TYPING: COLON 'typing'; // Allow type names in declarations of variables

WS: [ \t\r\n]+ -> channel(HIDDEN);
