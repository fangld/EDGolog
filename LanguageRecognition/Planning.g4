grammar Planning;

/*
 * Parser Rules
 */

// Domain description
domain: LB DEFINE LB DOMAIN NAME RB
           numericDefine?
		   typeDefine?
		   predicateDefine?
		   eventDefine*
		   actionDefine*
		   observationDefine*
		RB;

numericDefine: LB COLON NUMS numericSymbol+ RB;
numericSymbol: NAME;
typeDefine: LB COLON TYPE typeDeclaration+ RB;

predicateDefine: LB COLON PREDICATE atomFormSkeleton+ RB;
atomFormSkeleton: LB predicate listVariable RB;
predicate: NAME;

typeDeclaration: NAME | LB NAME constTerm constTerm RB;

type: OBJECT | AGENT | NAME;
//interval: LSB constTerm constTerm RSB;


eventDefine: LB COLON EVENT eventSymbol
                (COLON PARAMETER LB listVariable RB)?
                (COLON PRECONDITION emptyOrPreGD)?
                (COLON EFFECT emptyOrEffect)?
			 RB;
eventSymbol: NAME;


responseDefine: LB COLON RESPONSE responseSymbol
                   (COLON PARAMETER LB listVariable RB)?
                   COLON EVENTMODEL eventModel
                RB;
responseSymbol: NAME;

actionDefine: LB COLON ACTION actionSymbol
                 (COLON PARAMETER LB listVariable RB)?
		         responseDefine+
		      RB;
actionSymbol: NAME;


observationDefine: LB COLON OBSERVATION observationSymbol
              (COLON PARAMETER LB listVariable RB)?
              (COLON PRECONDITION emptyOrPreGD)?
			  COLON EVENTMODEL eventModel
           RB;

observationSymbol: NAME;

eventModel : gdEvent
           | LB (plGdEvent)+ RB;
plGdEvent : LB COLON PLDEGREE plDeg
               COLON EVENTS gdEvent 
			RB;
plDeg: INTEGER;


emptyOrPreGD: gd | LB RB;
emptyOrEffect: effect | LB RB;

listName: NAME* | NAME+ MINUS type listName;
listVariable: VAR* | VAR+ MINUS type listVariable;

gd: termAtomForm
  | LB AND gd+ RB
  | LB OR gd+ RB
  | LB NOT gd RB
  | LB IMPLY gd gd RB
  | LB EXISTS LB listVariable RB gd RB
  | LB FORALL LB listVariable RB gd RB;

termAtomForm: LB predicate term* RB
            | LB EQ term term RB
            | LB NEQ term term RB
			| LB LT term term RB
		    | LB LEQ term term RB
			| LB GT term term RB
			| LB GEQ term term RB;
termLiteral: termAtomForm | LB NOT termAtomForm RB;

gdEvent: termEventForm
       | LB NOT gdEvent RB
	   | LB AND gdEvent+ RB
	   | LB OR gdEvent+ RB
	   | LB IMPLY gdEvent gdEvent RB
	   | LB EXISTS LB listVariable RB gdEvent RB
	   | LB FORALL LB listVariable RB gdEvent RB;
termEventForm: LB eventSymbol term* RB
             | LB EQ term term RB
             | LB NEQ term term RB
     		 | LB LT term term RB 
			 | LB LEQ term term RB
			 | LB GT term term RB
			 | LB GEQ term term RB;

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
serverProblem: LB DEFINE LB PROBLEM problemName RB
		                 LB COLON DOMAIN domainName RB
		                 numericSetting?
		                 objectDeclaration?
		                 init
		       RB;

problemName: NAME;
domainName: NAME;
agentDefine: LB COLON AGENT NAME+ RB;
objectDeclaration: LB COLON OBJS listName RB;
numericSetting: LB COLON NUMS (LB numericSymbol INTEGER RB)+
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

constTermAtomForm: LB predicate constTerm* RB
                 | LB EQ constTerm constTerm RB
				 | LB LT constTerm constTerm RB
				 | LB LEQ constTerm constTerm RB
				 | LB GT constTerm constTerm RB
				 | LB GEQ constTerm constTerm RB;
constTermLiteral: constTermAtomForm | LB NOT constTermAtomForm RB;

// Client problem description
clientProblem: LB DEFINE LB PROBLEM problemName RB
                         LB COLON DOMAIN domainName RB
	     		         agentDefine
			             LB COLON AGENTID agentId RB
			             objectDeclaration?
			             numericSetting?
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
DOMAIN: 'domain';
PROBLEM: 'problem';
DEFINE: 'define';
AGENTID: 'agentid';
CONST: 'constants';
TYPE: 'types';
PREDICATE: 'predicates';
ACTION: 'action';
EVENT: 'event';
EVENTS: 'events';
PLDEGREE: 'pldegree';
EVENTMODEL: 'eventmodel';
PARAMETER: 'parameters';
PRECONDITION: 'precondition';
RESPONSE: 'response';
OBSERVATION: 'observation';
MIN: 'min';
MAX: 'max';
NUMS: 'numbers';
EFFECT: 'effect';
OBJECT: 'object';
AGENT: 'agent';
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
NEQ: '!=';
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