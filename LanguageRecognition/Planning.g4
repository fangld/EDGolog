grammar Planning;

/*
 * Parser Rules
 */

// Domain description
domain: LB DEF LB DOM NAME RB 
		   requireDefine?
		   typeDefine?
		   predicatesDefine?
		   structureDefine* 
		RB;

requireDefine: LB COLON REQ requireKey+ RB;
requireKey: strips | typing;
strips: COLON STRIPS;
typing: COLON TYPING;

typeDefine: LB COLON TYPE listName RB;

predicatesDefine: LB COLON PRED atomicFormulaSkeleton+ RB;
atomicFormulaSkeleton: LB predicate listVariable RB;
predicate: NAME;

primitiveType: OBJ | NAME;
type: primitiveType | LB EITHER primitiveType+ RB;

structureDefine: actionDefine;

actionDefine: LB COLON ACT actionSymbol
                 COLON PARM LB listVariable RB
		         actionDefBody
		      RB;
actionSymbol: NAME ;
actionDefBody: (COLON PRE emptyOrPreGD)?
               (COLON EFF emptyOrEffect)?;

emptyOrPreGD: preGD | LB RB;
emptyOrEffect: effect | LB RB;

listName: NAME+;
listVariable: VAR* | VAR+ DASH type listVariable;
preGD: prefGD
     | LB AND preGD* RB
	 | LB FORALL listVariable preGD RB;
prefGD: gd | LB PREF prefName gd RB;
prefName: NAME;

gd: atomicFormula
  | literal
  | LB AND gd* RB
  | LB OR gd* RB
  | LB IMPLY gd gd RB
  | LB EXISTS LB listVariable RB gd RB
  | LB FORALL LB listVariable RB gd RB;

atomicFormula: LB predicate term* RB
             | LB EQ term* RB;
literal: atomicFormula | LB NOT atomicFormula RB;

term: NAME | VAR | functionTerm;

effect: LB AND cEffect* RB
      | cEffect;
cEffect: LB FORALL listVariable effect RB
       | LB WHEN gd condEffect RB
	   | pEffect;
pEffect: LB NOT atomicFormula RB
       | atomicFormula;
condEffect: LB AND pEffect* RB
          | pEffect;

functionTerm: FUNSYM term* ;


// Problem description
problem: LB DEF LB PROM NAME RB 
		   LB COLON DOM NAME RB
		   requireDefine?
		RB;

objectDeclaration: LB COLON OBJS listName RB;
init: LB COLON INIT;

/*
 * Lexer Rules
 */

// Keywords
DOM: 'domain';
PROM: 'problem';
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

OBJS: 'objects';
INIT: 'init';

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

WS: [ \t\r\n]+ -> channel(HIDDEN);