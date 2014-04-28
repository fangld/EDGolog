//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Agents.Planning;
//using LanguageRecognition;
//using PAT.Common.Classes.CUDDLib;

//namespace Agents.HighLevelPrograms
//{
//    public class MentalAttitude
//    {
//        #region Fields

//        private Problem _problem;

//        #endregion

//        #region Properties

//        private CUDDNode Knowledge;

//        private CUDDNode Belief;

//        #endregion

//        #region Constructors

//        public MentalAttitude(Problem problem)
//        {
//            _problem = problem;
//            Knowledge = _problem.Knowledge;
//            Belief = _problem.Belief;
//        }

//        #endregion

//        #region Methods

//        public void Update(GroundAction gndAction)
//        {
//            //CUDDNode preconditionNode = CUDD.Function.Implies(Knowledge, gndAction.Precondition);

//            //CUDD.Print.PrintMinterm(Knowledge);

//            CUDDNode knowledgeAndPre = CUDD.Function.And(Knowledge, gndAction.Precondition);
//            CUDD.Ref(knowledgeAndPre);
//            CUDD.Deref(Knowledge);

//            CUDDNode combinationNode = CUDD.Function.And(knowledgeAndPre, gndAction.SuccessorStateAxiom);
//            CUDD.Ref(combinationNode);
//            CUDD.Deref(knowledgeAndPre);

//            foreach (var pair in gndAction.GroundPredicateList)
//            {
//                CUDDNode intermediateNode = combinationNode;

//                CUDDNode trueRestrictBy = CUDD.Var(pair.PreviousCuddIndex);
//                CUDDNode trueNode = CUDD.Function.Restrict(intermediateNode, trueRestrictBy);
//                CUDD.Ref(trueNode);

//                CUDDNode falseRestrictBy = CUDD.Function.Not(trueRestrictBy);
//                CUDD.Ref(falseRestrictBy);
//                CUDDNode falseNode = CUDD.Function.Restrict(intermediateNode, falseRestrictBy);
//                CUDD.Ref(falseNode);

//                CUDD.Deref(falseRestrictBy);
//                CUDD.Deref(intermediateNode);

//                combinationNode = CUDD.Function.Or(trueNode, falseNode);
//                CUDD.Deref(trueNode);
//                CUDD.Deref(falseNode);
//                CUDD.Ref(combinationNode);
//            }

//            CUDDVars oldVars = new CUDDVars();
//            CUDDVars newVars = new CUDDVars();

//            foreach (var gndPred in gndAction.GroundPredicateList)
//            {
//                oldVars.AddVar(CUDD.Var(gndPred.SuccessorCuddIndex));
//                newVars.AddVar(CUDD.Var(gndPred.PreviousCuddIndex));
//            }

//            Knowledge = CUDD.Variable.SwapVariables(combinationNode, oldVars, newVars);
//            CUDD.Ref(Knowledge);
//            CUDD.Deref(combinationNode);


//            //if (preconditionNode.GetValue() > 0.5)
//            //{

//            //}
//            //else
//            //{
//            //    Console.WriteLine("    Action {0} is not executable now!", gndAction);
//            //}
//            //CUDD.Deref(kbNode);
//        }

//        public bool Implies(HighLevelProgramParser.ObjectFormulaContext context)
//        {
//            CUDDNode query = GetCuddNode(context);
//            CUDDNode impliesNode = CUDD.Function.Implies(Knowledge, query);
//            CUDD.Ref(impliesNode);
//            CUDD.Deref(query);
//            return impliesNode.GetValue() > 0.5;
//        }

//        public void ShowInfo()
//        {
//            Console.WriteLine("Knowledge:");
//            CUDD.Print.PrintMinterm(Knowledge);
//        }

//        private CUDDNode GetCuddNode(HighLevelProgramParser.ObjectFormulaContext context)
//        {
//            CUDDNode result = null;
//            if (context.AND() != null)
//            {
//                CUDDNode leftNode = GetCuddNode(context.objectFormula()[0]);
//                CUDDNode rightNode = GetCuddNode(context.objectFormula()[0]);
//                result = CUDD.Function.And(leftNode, rightNode);
//                CUDD.Deref(leftNode);
//                CUDD.Deref(rightNode);
//            }
//            else if (context.OR() != null)
//            {
//                CUDDNode leftNode = GetCuddNode(context.objectFormula()[0]);
//                CUDDNode rightNode = GetCuddNode(context.objectFormula()[0]);
//                result = CUDD.Function.Or(leftNode, rightNode);
//                CUDD.Deref(leftNode);
//                CUDD.Deref(rightNode);
//            }
//            else if (context.NOT() != null)
//            {
//                CUDDNode node = GetCuddNode(context.objectFormula()[0]);
//                result = CUDD.Function.Not(node);
//                CUDD.Deref(node);
//            }
//            else if (context.LB() != null)
//            {
//                result = GetCuddNode(context.objectFormula()[0]);
//            }
//            else if (context.predicate() != null)
//            {
//                result = GetCuddNode(context.predicate());
//            }

//            CUDD.Ref(result);
//            return result;
//        }

//        private CUDDNode GetCuddNode(HighLevelProgramParser.PredicateContext context)
//        {
//            string fullName = GetFullName(context);
//            int index = _problem.GroundPredicateDict[fullName].PreviousCuddIndex;
//            CUDDNode result = CUDD.Var(index);
//            return result;
//        }

//        private string GetFullName(HighLevelProgramParser.PredicateContext context)
//        {
//            string name = context.NAME().GetText();
//            List<string> constantList = new List<string>();
//            HighLevelProgramParser.ListNameContext listNameContext = context.listName();
//            while (listNameContext != null)
//            {
//                string constant = context.listName().NAME().GetText();
//                constantList.Add(constant);
//                listNameContext = listNameContext.listName();
//            }
//            string result = VariableContainer.GetFullName(name, constantList);
//            return result;
            
//        }

//        #endregion
//    }
//}
