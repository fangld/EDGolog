using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.ContextExtensions
{
    public static class GdContextExtension
    {
        #region Methods

        public static CUDDNode GetCuddNode(this PlanningParser.GdContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, Dictionary<string, string> assignment)
        {
            CUDDNode result;
            if (context.termAtomForm() != null)
            {
                result = GetCuddNode(context.termAtomForm(), predicateDict, assignment);
            }
            else if (context.AND() != null)
            {
                result = CUDD.ONE;
                CUDD.Ref(result);
                for (int i = 0; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i], predicateDict, assignment);
                    if (gdNode.Equals(CUDD.ZERO))
                    {
                        CUDD.Deref(result);
                        result = CUDD.ZERO;
                        break;
                    }
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = CUDD.ZERO;
                CUDD.Ref(result);
                for (int i = 0; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i], predicateDict, assignment);
                    if (gdNode.Equals(CUDD.ONE))
                    {
                        CUDD.Deref(result);
                        result = CUDD.ONE;
                        break;
                    }
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gd()[0], predicateDict, assignment);
                result = CUDD.Function.Not(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = GetCuddNode(context.gd()[0], predicateDict, assignment);
                CUDDNode gdNode1 = GetCuddNode(context.gd()[1], predicateDict, assignment);
                result = CUDD.Function.Implies(gdNode0, gdNode1);
            }
            else
            {
                var listVariableContext = context.listVariable();
                var collection = listVariableContext.GetCollection();
                var varNameList = listVariableContext.GetVariableNameList();

                bool isForall = context.FORALL() != null;
                result = ScanVariableList(context.gd(0), predicateDict, assignment, varNameList, collection, 0, isForall);
            }

            return result;
        }

        private static CUDDNode GetCuddNode(this PlanningParser.TermAtomFormContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, Dictionary<string, string> assignment)
        {
            CUDDNode result;
            if (context.predicate() != null)
            {
                string predicateFullName = ConstContainer.GetFullName(context, assignment);
                Predicate pred = predicateDict[predicateFullName];
                int cuddIndex = pred.PreviousCuddIndex;
                result = CUDD.Var(cuddIndex);
            }
            else
            {
                string firstTermString = Globals.TermInterpreter.GetString(context.term(0), assignment);
                string secondTermString = Globals.TermInterpreter.GetString(context.term(1), assignment);
                if (context.EQ() != null)
                {
                    result = firstTermString == secondTermString ? CUDD.ONE : CUDD.ZERO;
                }
                else if (context.NEQ() != null)
                {
                    result = firstTermString != secondTermString ? CUDD.ONE : CUDD.ZERO;
                }
                else
                {
                    int firstValue = int.Parse(firstTermString);
                    int secondValue = int.Parse(secondTermString);
                    if (context.LT() != null)
                    {
                        result = firstValue < secondValue ? CUDD.ONE : CUDD.ZERO;
                    }

                    else if (context.LEQ() != null)
                    {
                        result = firstValue <= secondValue ? CUDD.ONE : CUDD.ZERO;
                    }
                    else if (context.GT() != null)
                    {
                        result = firstValue > secondValue ? CUDD.ONE : CUDD.ZERO;
                    }
                    else
                    {
                        result = firstValue >= secondValue ? CUDD.ONE : CUDD.ZERO;
                    }
                }
                CUDD.Ref(result);
            }

            return result;
        }

        private static CUDDNode ScanVariableList(PlanningParser.GdContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, Dictionary<string, string> assignment,
            IReadOnlyList<string> variableNameList, IReadOnlyList<List<string>> collection, int currentLevel = 0,
            bool isForall = true)
        {
            CUDDNode result;
            if (currentLevel != variableNameList.Count)
            {
                string variableName = variableNameList[currentLevel];
                result = isForall ? CUDD.ONE : CUDD.ZERO;
                CUDDNode equalNode = isForall ? CUDD.ZERO : CUDD.ONE;
                CUDD.Ref(result);
                Func<CUDDNode, CUDDNode, CUDDNode> boolFunc;
                if (isForall)
                {
                    boolFunc = CUDD.Function.And;
                }
                else
                {
                    boolFunc = CUDD.Function.Or;
                }

                foreach (string value in collection[currentLevel])
                {
                    if (assignment.ContainsKey(variableName))
                    {
                        assignment[variableName] = value;
                    }
                    else
                    {
                        assignment.Add(variableName, value);
                    }

                    CUDDNode gdNode = ScanVariableList(context, predicateDict, assignment, variableNameList, collection,
                        currentLevel + 1);

                    if (gdNode.Equals(equalNode))
                    {
                        CUDD.Deref(result);
                        result = equalNode;
                        break;
                    }

                    CUDDNode boolNode = boolFunc(result, gdNode);
                    result = boolNode;
                }
            }
            else
            {
                result = GetCuddNode(context, predicateDict, assignment);
            }

            return result;
        }

        #endregion
    }
}
