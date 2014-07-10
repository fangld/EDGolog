using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
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
                //Console.WriteLine(" isForal: {0}", isForall);
                //Console.WriteLine(" context: {0}", context.GetText());

                result = ScanVariableList(context.gd(0), predicateDict, varNameList, collection, assignment, 0, isForall);
            }

            return result;
        }

        private static CUDDNode GetCuddNode(this PlanningParser.TermAtomFormContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
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
            IReadOnlyDictionary<string, Predicate> predicateDict, IReadOnlyList<string> variableNameList,
            IReadOnlyList<List<string>> collection, StringDictionary assignment, int currentLevel = 0,
            bool isForall = true)
        {
            CUDDNode result;
            if (currentLevel != variableNameList.Count)
            {
                string variableName = variableNameList[currentLevel];
                result = isForall ? CUDD.ONE : CUDD.ZERO;
                CUDD.Ref(result);

                CUDDNode equalNode = isForall ? CUDD.ZERO : CUDD.ONE;
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
                    //Console.WriteLine("  Foreach Key: {0}, value: {1}", variableName, value);
                    assignment[variableName] = value;

                    CUDDNode gdNode = ScanVariableList(context, predicateDict, variableNameList, collection, assignment,
                        currentLevel + 1, isForall);

                    if (gdNode.Equals(equalNode))
                    {
                        //Console.WriteLine("  isForall: {0}", isForall);
                        //CUDD.Print.PrintMinterm(equalNode);

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

            //foreach (DictionaryEntry pair in assignment)
            //{
            //    Console.WriteLine("  Key: {0}, Value: {1}", pair.Key, pair.Value);
            //}

            //Console.WriteLine("  Context: {0}", context.GetText());
            //CUDD.Print.PrintMinterm(result);
            //Console.ReadLine();

            return result;
        }

        #endregion
    }
}