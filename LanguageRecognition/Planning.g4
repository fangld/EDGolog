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
		   obsDefine*
		RB;

constDefine: LB COLON CONST constSymbol+ RB;
constSymbol: NAME;
typeDefine: LB COLON TYPE typeDeclaration+ RB;

predDefine: LB COLON PRED atomFormSkeleton+ RB;
atomFormSkeleton: LB pred listVariable RB;
pred: NAME;

typeDeclaration: NAME | LB NAME constTerm constTerm RB;

type: OBJ | AGT | NAME;
//interval: LSB constTerm constTerm RSB;


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
                 (COLON PARM LB listVariable RB)?
		         responseDefine+
		      RB;
actionSymbol: NAME;


obsDefine: LB COLON OBS obsSymbol
              (COLON PARM LB listVariable RB)?
              (COLON PRE emptyOrPreGD)?
			  COLON EVTS eventModel
           RB;

obsSymbol: NAME;

eventModel : gdEvent+
           | LB (LB plDeg gdEvent RB)+ RB;

plDeg: INTEGER;


emptyOrPreGD: gd | LB RB;
emptyOrEffect: effect | LB RB;

listName: NAME* | NAME+ MINUS type listName;
listVariable: VAR* | VAR+ MINUS type listVariable;

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
			| LB LT term term RB
		    | LB LEQ term term RB
			| LB GT term term RB
			| LB GEQ term term RB;
termLiteral: termAtomForm | LB NOT termAtomForm RB;

gdEvent: eventFormulaTerm
       | LB NOT gdEvent RB
	   //| LB AND gdEvent+ RB
	   | LB OR gdEvent+ RB
	   | LB EXISTS LB listVariable RB gdEvent RB;
	   //| LB FORALL LB listVariable RB gdEvent RB;
eventFormulaTerm: LB eventSymbol term* RB;

constTerm: NAME
         | INTEGER
		 | LB MINUS constTerm RB
		 | LB MINUS constTerm constTerm RB	 
	     | LB PLUS constTerm constTerm RB;

term: NAME
    | VAR
	| INTEGER
	| LB MINUS term RB
	| LB MINUS term term RB
	| LB PLUS term term RB;

effect: LB AND cEffect+ RB
      | cEffect;
cEffect: LB FORALL LB listVariable RB effect RB
       | LB WHEN gd condEffect RB
	   | termLiteral;
condEffect: LB AND termLiteral+ RB
          | termLiteral;
		  
// Server problem description
serverProblem: LB DEF LB PROM problemName RB
		              LB COLON DOM domainName RB
		              constSetting?
		              objectDeclaration?
		              init
		       RB;

problemName: NAME;
domainName: NAME;
agentDefine: LB COLON AGENTS NAME+ RB;
objectDeclaration: LB COLON OBJS listName RB;
constSetting: LB COLON CONST (LB constSymbol INTEGER RB)+
              RB;


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
MIN: 'min';
MAX: 'max';
EFF: 'effect';
OBJ: 'object';
AGT: 'agent';
EITHER: 'either';
INITKNOWLEDGE: 'initknowledge';
INITBELIEF: 'initbelief';

OBJS: 'objects';

INIT: 'init';
GOAL: 'goal';

// Common used in domain and problem
LB: '(';
RB: ')';
LSB: '[';
RSB: ']';
COLON: ':';
QM: '?';
POINT: '.';
UL: '_';
MINUS: '-';
PLUS: '+';
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

NAME: LETTER CHAR*;
INTEGER: DIGIT+;

fragment LETTER: [a-zA-Z];
fragment DIGIT: [0-9];
fragment CHAR: LETTER | DIGIT | MINUS | UL;

//NUMBER: 
//DECIMAL: POINT DIGIT+;
VAR: QM NAME;
//FUNSYM : NAME;

WS: [ \t\r\n]+ -> channel(HIDDEN);