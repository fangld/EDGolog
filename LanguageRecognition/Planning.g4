grammar Planning;

/*
 * Parser Rules
 */

// Domain description
domain: LB DEF LB DOM NAME RB
           constDefine?
		   typeDefine?
		   predDefine?
		   eventDefine*
		   actionDefine*
		   observationDefine*
		RB;

constDefine: LB COLON CONST constSymbol+ RB;
constSymbol: NAME;
typeDefine: LB COLON TYPE typeDeclaration+ RB;

predDefine: LB COLON PRED atomFormSkeleton+ RB;
atomFormSkeleton: LB pred listVariable RB;
pred: NAME;

typeDeclaration: NAME | NAME interval;

type: OBJ | AGT | NAME;
interval: LSB constTerm COMMA constTerm RSB;


eventDefine: LB COLON EVT eventSymbol
                (COLON PARM LB listVariable RB)?
                (COLON PRE emptyOrPreGD)?
                (COLON EFF emptyOrEffect)?
			 RB;
eventSymbol: NAME;


responseDefine: LB COLON RESP responseSymbol
                   (COLON PARM LB listVariable RB)?
                   COLON EVTS gdEvent
                RB;
responseSymbol: NAME;

actionDefine: LB COLON ACT actionSymbol
                 COLON PARM LB listVariable RB
		         responseDefine+
		      RB;
actionSymbol: NAME;


eventModel : gdEvent+
           | (LB plausibilityDegree gdEvent RB)+;

plausibilityDegree: INTEGER;


observationDefine: LB COLON OBS observationSymbol
                      (COLON PRE emptyOrPreGD)?
					  COLON EVTS eventModel
                   RB;

observationSymbol: NAME;


emptyOrPreGD: gd | LB RB;
emptyOrEffect: effect | LB RB;

listName: NAME* | NAME+ DASH type listName;
listVariable: VAR* | VAR+ DASH type listVariable;

gd: termAtomForm
  | termLiteral
  | LB AND gd+ RB
  | LB OR gd+ RB
  | LB NOT gd RB
  | LB IMPLY gd gd RB
  | LB EXISTS LB listVariable RB gd RB
  | LB FORALL LB listVariable RB gd RB;

termAtomForm: LB pred term* RB
            | LB EQ term term RB
			|  LB LT term term RB
		    | LB LEQ term term RB
			| LB GT term term RB
			| LB GEQ term term RB;
termLiteral: termAtomForm | LB NOT termAtomForm RB;

gdEvent: eventFormulaTerm
       | LB NOT gdEvent RB
	   | LB AND gdEvent+ RB
	   | LB OR gdEvent+ RB
	   | LB EXISTS LB listVariable RB gdEvent RB
	   | LB FORALL LB listVariable RB gdEvent RB;
eventFormulaTerm: LB eventSymbol term* RB;

term: NAME
    | VAR
	| INTEGER
	| LB MINUS term term RB
	| LB PLUS term term RB;

constTerm: NAME
         | INTEGER
		 | LB MINUS constTerm constTerm RB
		 | LB MINUS constTerm RB
	     | LB PLUS constTerm constTerm RB;

effect: LB AND cEffect+ RB
      | cEffect;
cEffect: LB FORALL listVariable effect RB
       | LB WHEN gd condEffect RB
	   | termLiteral;
condEffect: LB AND termLiteral+ RB
          | termLiteral;
		  
// Server problem description
serverProblem: LB DEF LB PROM problemName RB
		   LB COLON DOM domainName RB
		   agentDefine
		   objectDeclaration?
		   constSetting?
		   init
		 RB;

problemName: NAME;
domainName: NAME;
agentDefine: LB COLON AGENTS NAME+ RB;
objectDeclaration: LB COLON OBJS listName RB;
constSetting: LB constSymbol INTEGER RB;

init: LB COLON INIT constTermAtomForm* RB;
constTermGd: constTermAtomForm
           | constTermLiteral
           | LB AND constTermGd+ RB
           | LB OR constTermGd+ RB
           | LB NOT constTermGd RB
           | LB IMPLY constTermGd constTermGd RB
           | LB EXISTS LB listVariable RB gd RB
           | LB FORALL LB listVariable RB gd RB;

constTermAtomForm: LB pred constTerm* RB
                 | LB EQ constTerm constTerm RB
				 | LB LT constTerm constTerm RB
				 | LB LEQ constTerm constTerm RB
				 | LB GT constTerm constTerm RB
				 | LB GEQ constTerm constTerm RB;
constTermLiteral: constTermAtomForm | LB NOT constTermAtomForm RB;

// Client problem description
clientProblem: LB DEF LB PROM problemName RB
                      LB COLON DOM domainName RB
			          agentDefine
			          LB COLON AGENTID agentId RB
			          objectDeclaration?
			          constSetting?
			          initKnowledge?
			          initBelief?
			   RB;

initKnowledge: LB COLON INITKNOWLEDGE constTermGd RB;
initBelief: LB COLON INITBELIEF constTermGd RB;
agentId: NAME;

/*
 * Lexer Rules
 */

// Keywords
DOM: 'domain';
PROM: 'problem';
DEF: 'define';
AGENTID: 'agentid';
CONST: 'constants';
TYPE: 'types';
PRED: 'predicates';
ACT: 'action';
EVT: 'event';
EVTS: 'events';
PARM: 'parameters';
PRE: 'precondition';
RESP: 'response';
OBS: 'observation';
EFF: 'effect';
OBJ: 'object';
AGT: 'agent';
EITHER: 'either';
INITKNOWLEDGE: 'initknowledge';
INITBELIEF: 'initbelief';

//NUMBERTYPE: 'numbers';
//OBJTYPE: 'objects';
//AGENTTYPE: 'agents';

INIT: 'init';
GOAL: 'goal';

// Common used in domain and problem
LB: '(';
RB: ')';
LSB: '[';
RSB: ']';
COMMA: ',';
COLON: ':';
QM: '?';
POINT: '.';
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