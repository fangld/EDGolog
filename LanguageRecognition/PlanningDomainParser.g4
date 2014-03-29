grammar PlanningDomainParser;
import PlanningLexer;

/*
 * Parser Rules
 */

domain: LB DEF LB DOM NAME 
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

listName: NAME+;
listVar: VAR+ ;
