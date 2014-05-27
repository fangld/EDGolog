grammar Planning;

/*
 * Parser Rules
 */

// Domain description
domain: LB DEF LB DOM NAME RB
		   typeDefine?
		   predicatesDefine?
		   actionDefine* 
		RB;

typeDefine: LB COLON TYPE listName RB;

predicatesDefine: LB COLON PRED atomicFormulaSkeleton+ RB;
atomicFormulaSkeleton: LB predicate listVariable RB;
predicate: NAME;

primitiveType: OBJ | AGT | NAME;
type: primitiveType | LB EITHER primitiveType+ RB;

actionDefine: LB COLON ACT actionSymbol
                 COLON PARM LB listVariable RB
		         eventSetDefine+
		      RB;
actionSymbol: NAME;

eventSetDefine: LB eventDefine+ RB;

eventDefine: LB (COLON PLD INTEGER)?
                (COLON PRE emptyOrPreGD)?
                (COLON EFF emptyOrEffect)?
			 RB;

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

// Server problem description
serverProblem: LB DEF LB PROM problemName RB
		   LB COLON DOM domainName RB
		   agentDefine
		   objectDeclaration?
		   init
		 RB;

problemName: NAME;
domainName: NAME;
agentDefine: LB COLON AGENTS NAME+ RB;
objectDeclaration: LB COLON OBJS listName RB;

init: LB COLON INIT atomicFormulaName* RB;
gdName: atomicFormulaName
  | literalName
  | LB AND gdName+ RB
  | LB OR gdName+ RB
  | LB NOT gdName RB
  | LB IMPLY gdName gdName RB
  /*| LB EXISTS LB listVariable RB gd RB
  | LB FORALL LB listVariable RB gd RB*/;

atomicFormulaName: LB predicate NAME* RB;
             //| LB EQ NAME* RB;
literalName: atomicFormulaName | LB NOT atomicFormulaName RB;

// Client problem description
clientProblem: LB DEF LB PROM problemName RB
		   LB COLON DOM domainName RB
		   agentDefine
		   LB COLON AGENTID agentId RB		   
		   objectDeclaration?
		   initKnowledge?
		   initBelief?
		 RB;
initKnowledge: LB COLON INITKNOWLEDGE gdName RB;
initBelief: LB COLON INITBELIEF gdName RB;
agentId: NAME;

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
AGENTID: 'agentid';
//REQ: 'requirements';
TYPE: 'types';
PRED: 'predicates';
ACT: 'action';
PARM: 'parameters';
PRE: 'precondition';
EFF: 'effect';
OBJ: 'object';
AGT: 'agent';
EITHER: 'either';
INITKNOWLEDGE: 'initknowledge';
INITBELIEF: 'initbelief';

OBJS: 'objects';
INIT: 'init';
AGENTS: 'agents';
GOAL: 'goal';
//AT: 'at';

//STRIPS: 'strips'; // Basic STRIPS-style adds and deletes
//TYPING: 'typing'; // Allow type names in declarations of variables

// Common used in domain and problem
LB: '(';
RB: ')';
LSB: '[';
RSB: ']';
COLON: ':';
QM: '?';
POINT: '.';
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
//PREF: 'preference';
//BINCOMP: EQ | LT | GT | LEQ | GEQ ;
//BINOP: PLUS | MINUS | MULT | DIV ;
LETTER: [a-zA-Z];
DIGIT: [0-9];

NAME: LETTER CHAR*;
CHAR: LETTER | DIGIT | DASH | UL;
INTEGER: [1-9] DIGIT*;
NUMBER: DIGIT+ DECIMAL?;
DECIMAL: POINT DIGIT+;
VAR: QM NAME;
FUNSYM : NAME;

WS: [ \t\r\n]+ -> channel(HIDDEN);