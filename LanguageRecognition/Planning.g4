grammar Planning;

/*
 * Parser Rules
 */

// Domain description
domain: LB DEF LB DOM NAME RB 
		   //requireDefine?
		   typeDefine?
		   predicatesDefine?
		   structureDefine* 
		RB;

/*requireDefine: LB COLON REQ requireKey+ RB;
requireKey: strips | typing;
strips: COLON STRIPS;
typing: COLON TYPING;*/

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
actionSymbol: NAME;
actionDefBody: (COLON PRE emptyOrPreGD)?
               (COLON EFF emptyOrEffect)?;

emptyOrPreGD: gd | LB RB;
emptyOrEffect: effect | LB RB;

listName: NAME* | NAME+ DASH type listName;
listVariable: VAR* | VAR+ DASH type listVariable;
//preGD: gd//prefGD
     //| LB AND preGD* RB
	 /*| LB FORALL listVariable preGD RB*///;
//prefGD: gd | LB PREF prefName gd RB;
//prefName: NAME;

gd: atomicFormulaTerm
  | literalTerm
  | LB AND gd+ RB
  | LB OR gd+ RB
  | LB NOT gd RB
  | LB IMPLY gd gd RB/*
  | LB EXISTS LB listVariable RB gd RB
  | LB FORALL LB listVariable RB gd RB*/;

atomicFormulaTerm: LB predicate term* RB;
                 //| LB EQ term* RB;
literalTerm: atomicFormulaTerm | LB NOT atomicFormulaTerm RB;

term: NAME | VAR;// | functionTerm;

effect: LB AND cEffect+ RB
      | cEffect;
cEffect: /*LB FORALL listVariable effect RB
       | */LB WHEN gd condEffect RB
	   | literalTerm;
/*pEffect: LB NOT atomicFormulaTerm RB
       | atomicFormulaTerm;*/
condEffect: LB AND literalTerm+ RB
          | literalTerm;

//functionTerm: FUNSYM term* ;

// Problem description
problem: LB DEF LB PROM problemName RB 
		   LB COLON DOM domainName RB
		   agentDefine
		   //requireDefine?
		   objectDeclaration?
		   init
		 RB;

problemName: NAME;
domainName: NAME;

agentDefine: LB COLON AGENTS NAME+ RB;

objectDeclaration: LB COLON OBJS listName RB;

init: LB COLON INIT gdName RB;
gdName: atomicFormulaName
  | literalName
  | LB AND gdName+ RB
  | LB OR gdName+ RB
  | LB IMPLY gdName gdName RB
  | LB EXISTS LB listVariable RB gd RB
  | LB FORALL LB listVariable RB gd RB;

atomicFormulaName: LB predicate NAME* RB;
             //| LB EQ NAME* RB;
literalName: atomicFormulaName | LB NOT atomicFormulaName RB;


/*initEl: literalName
      | LB AT NUMBER literalName RB
	  | LB EQ basicFunctionTerm NUMBER RB
	  | LB EQ basicFunctionTerm NAME RB;
basicFunctionTerm: functionSymbol
                 | LB functionSymbol NAME* RB;
functionSymbol: NAME;*/


//goal: LB COLON GOAL preGD RB;

/*
 * Lexer Rules
 */

// Keywords
DOM: 'domain';
PROM: 'problem';
DEF: 'define';
//REQ: 'requirements';
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
AGENTS: 'agents';
GOAL: 'goal';
AT: 'at';

//STRIPS: 'strips'; // Basic STRIPS-style adds and deletes
//TYPING: 'typing'; // Allow type names in declarations of variables

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
/*PLUS: '+';
MINUS: '-';
MULT: '*';
DIV: '/';
EQ: '=';
LT: '<';
LEQ: '<=';
GT: '>';
GEQ: '>=';*/
AND: 'and';
OR: 'or';
NOT: 'not';
IMPLY: 'imply';
FORALL: 'forall';
EXISTS: 'exists';
WHEN: 'when';
PREF: 'preference';
//BINCOMP: EQ | LT | GT | LEQ | GEQ ;
//BINOP: PLUS | MINUS | MULT | DIV ;
LETTER: [a-zA-Z];
DIGIT: [0-9];

NAME: LETTER CHAR* ;
CHAR: LETTER | DIGIT | DASH | UL ;
NUMBER: DIGIT+ DECIMAL? ;
DECIMAL: COMMA DIGIT+ ;
VAR: QM NAME ;
FUNSYM : NAME ;

WS: [ \t\r\n]+ -> channel(HIDDEN);