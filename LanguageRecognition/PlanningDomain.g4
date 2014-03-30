grammar PlanningDomain;
import PlanningLexer;

/*
 * Parser Rules
 */

domain: LB DEF LB DOM NAME RB 
		   requireDefine?
		   typeDefine?
		   predicatesDefine?
		   structureDefine* 
		RB;

requireDefine: LB COLON REQ requireKey+ RB;
requireKey: STRIPS | TYPING;

typeDefine: LB COLON TYPE listName RB;

predicatesDefine: LB COLON PRED atomicFormulaSkeleton+ RB;
atomicFormulaSkeleton: LB predicate listVariable RB;
predicate: NAME;

primitiveType: NAME | OBJ;
type: primitiveType | LB EITHER primitiveType+ RB;

structureDefine: actionDefine;

actionDefine: LB COLON ACT actionSymbol
                 COLON PARM LB listVariable RB
		         actionDefBody
		      RB;
actionSymbol: NAME ;
actionDefBody: (COLON PRE (preGD | (LB RB)))?
               (COLON EFF (effect | (LB RB)))?;

listName: NAME+ ;
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
