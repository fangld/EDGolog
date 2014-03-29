grammar PlanningDomain;
import PlanningLexer;

/*
 * Parser Rules
 */

domain: LB DEF LB DOM NAME RB
           reqDef 
		   typeDef
		   predDef
		   RB ;

reqDef: LB COLON REQ reqKey+ RB ;
reqKey: STRIPS | TYPING ;

typeDef: LB COLON TYPE listName RB ;

predDef: LB COLON PRED RB ;

actDef: LB COLON ACT actSym 
           COLON PARM LB listVar RB
		   actBodyDef ;
actSym: NAME ;
actBodyDef: LSB COLON PRE preGD RSB
            LSB COLON EFF RSB ;

listName: NAME+ ;
listVar: VAR+ ;
preGD: prefGD
     | RB AND preGD* LB
	 | RB FORALL listVar preGD LB ;
prefGD: RB PREF prefName gd LB
      | gd ;
prefName: NAME;

gd: atomicForm
  | literal
  | RB AND gd* LB
  | RB OR gd* LB
  | RB IMPLY gd gd LB
  | RB EXISTS RB listVar LB gd LB
  | RB FORALL RB listVar LB gd LB ;

atomicForm: RB PRED term* LB
          | RB EQ term* LB ;
literal: atomicForm | NOT atomicForm ;

term: NAME | VAR | funTerm ;
funTerm: FUNSYM term* ;
