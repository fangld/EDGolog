grammar PlanningDomain;
import PlanningLexer;

/*
 * Parser Rules
 */

domain: LB DEF LB DOM NAME RB 
		   reqDef 
		   typeDef 
		   predDef
		   structDef* 
		RB;

reqDef: LB COLON REQ reqKey+ RB;
reqKey: STRIPS | TYPING;

typeDef: LB COLON TYPE listName RB;

predDef: LB COLON PRED atomicFormSke+ RB;
atomicFormSke: LB pred listVar RB;
pred: NAME;

primType: NAME | OBJ;
type: primType | LB EITHER primType+ RB;

structDef: actDef;

actDef: LB COLON ACT actSym 
           COLON PARM LB listVar RB
		   actBodyDef 
		RB;
actSym: NAME ;
actBodyDef: LSB COLON PRE preGD RSB
            LSB COLON EFF RSB;

listName: NAME+ ;
listVar: VAR* | VAR+ DASH type listVar;
preGD: prefGD
     | LB AND preGD* RB
	 | LB FORALL listVar preGD RB;
prefGD: gd | LB PREF prefName gd RB;
prefName: NAME;

gd: atomicForm
  | LB NOT atomicForm RB
  | LB AND gd* RB
  | LB OR gd* RB
  | LB IMPLY gd gd RB
  | LB EXISTS LB listVar RB gd RB
  | LB FORALL LB listVar RB gd RB;

atomicForm: LB PRED term* RB
          | LB EQ term* RB;
//literal: atomicForm | LB NOT atomicForm RB;

term: NAME | VAR | funTerm;
funTerm: FUNSYM term* ;
