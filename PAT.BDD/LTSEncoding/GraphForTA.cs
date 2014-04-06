using System.Collections.Generic;
using System.Diagnostics;
using PAT.Common.Classes.CUDDLib;


namespace PAT.Common.Classes.SemanticModels.LTS.BDD
{
    public partial class Model
    {
        public bool isTAModel = false;
        /// <summary>
        /// P ◦ R is the set of all successors of states in the set P
        /// [ REFS: 'result', DEREFS:states]
        /// </summary>
        /// <param name="states"></param>
        /// <param name="transitions"></param>
        /// <returns></returns>
        public CUDDNode Successors(CUDDNode states, List<CUDDNode> transitions)
        {
            CUDD.Ref(transitions);
            CUDDNode temp = CUDD.Function.And(states, transitions);

            CUDDNode successors = SwapRowColVars(temp);

            successors = CUDD.Abstract.ThereExists(successors, AllColVars);

            return successors;
        }


        /// <summary>
        /// return all states reachable from start and Start
        /// P ◦ R*
        /// [ REFS: 'result', DEREFS:start]
        /// </summary>
        /// <param name="start"></param>
        /// <param name="transition"></param>
        /// <returns></returns>
        public CUDDNode SuccessorsStart(CUDDNode start, List<CUDDNode> transition)
        {
            CUDD.Ref(start);
            CUDDNode allReachableStates = start;

            CUDDNode currentStep = start;

            while (true)
            {
                currentStep = Successors(currentStep, transition);

                CUDD.Ref(allReachableStates, currentStep);
                CUDDNode allReachableTemp = CUDD.Function.Or(allReachableStates, currentStep);

                if (allReachableTemp.Equals(allReachableStates))
                {
                    CUDD.Deref(currentStep, allReachableTemp);

                    return allReachableStates;
                }
                else
                {
                    currentStep = CUDD.Function.Different(currentStep, allReachableStates);
                    allReachableStates = allReachableTemp;
                }
            }
        }


        /// <summary>
        /// Forward search, check destination is reachable from source
        /// Store all reachable states and check fix point
        /// At step t, find all reachable states after t time units
        /// Fix point: rechable(0, t) == rechable(0, t + 1)
        /// [ REFS: '', DEREFS:]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="discreteTransitions"></param>
        /// <returns></returns>
        public bool PathForward1(CUDDNode source, CUDDNode destination, List<CUDDNode> discreteTransitions, List<CUDDNode> tickTransitions)
        {
            //
            bool reachable = false;

            CUDD.Ref(source);
            CUDDNode allReachableFromInit = SuccessorsStart(source, discreteTransitions);

            CUDD.Ref(allReachableFromInit);
            CUDDNode currentReachableFromInit = allReachableFromInit;

            CUDDNode commonNode = CUDD.Constant(0);

            do
            {
                Debug.WriteLine("Successors: " + numberOfBDDOperation++);

                currentReachableFromInit = Successors(currentReachableFromInit, tickTransitions);
                currentReachableFromInit = SuccessorsStart(currentReachableFromInit, discreteTransitions);

                //Check 2 directions have intersection
                CUDD.Ref(destination, currentReachableFromInit);
                CUDD.Deref(commonNode);
                commonNode = CUDD.Function.And(destination, currentReachableFromInit);

                if (!commonNode.Equals(CUDD.ZERO))
                {
                    reachable = true;
                    break;
                }

                //find fixpoint
                CUDD.Ref(currentReachableFromInit, allReachableFromInit);
                CUDDNode allReachabeFromInitTemp = CUDD.Function.Or(currentReachableFromInit, allReachableFromInit);

                if (allReachabeFromInitTemp.Equals(allReachableFromInit))
                {
                    //reachable = false;
                    CUDD.Deref(allReachabeFromInitTemp);
                    break;
                }
                else
                {
                    currentReachableFromInit = CUDD.Function.Different(currentReachableFromInit, allReachableFromInit);
                    allReachableFromInit = allReachabeFromInitTemp;
                }
            } while (true);

            CUDD.Deref(allReachableFromInit, currentReachableFromInit, commonNode);

            //
            return reachable;
        }

        /// <summary>
        /// Forward search, check destination is reachable from source
        /// NOT store all reachable states, use some special condition to terminate
        /// At step t, find all reachable states after t time units
        /// Fix point when rechable(t1 + a, t1 + a) is a subset of rechable(t1, t1)
        /// [ REFS: '', DEREFS:]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="discreteTransitions"></param>
        /// <returns></returns>
        public bool PathForward2(CUDDNode source, CUDDNode destination, List<CUDDNode> discreteTransitions, List<CUDDNode> tickTransitions)
        {
            //
            bool reachable = false;

            CUDD.Ref(source);
            CUDDNode currentReachableFromInit = SuccessorsStart(source, discreteTransitions);
            CUDDNode previousReachableFromInit = CUDD.Constant(0);

            int s, p;
            s = p = 0;
            CUDDNode commonNode = CUDD.Constant(0);

            while (!CUDD.IsSubSet(previousReachableFromInit, currentReachableFromInit))
            {
                Debug.WriteLine("Successors: " + numberOfBDDOperation++);

                if(p <= s/2)
                {
                    p = s;
                    CUDD.Deref(previousReachableFromInit);
                    CUDD.Ref(currentReachableFromInit);
                    previousReachableFromInit = currentReachableFromInit;
                }
                currentReachableFromInit = Successors(currentReachableFromInit, tickTransitions);
                currentReachableFromInit = SuccessorsStart(currentReachableFromInit, discreteTransitions);

                s++;

                //Check 2 directions have intersection
                CUDD.Ref(destination, currentReachableFromInit);
                CUDD.Deref(commonNode);
                commonNode = CUDD.Function.And(destination, currentReachableFromInit);

                if (!commonNode.Equals(CUDD.ZERO))
                {
                    reachable = true;
                    break;
                }
            }

            CUDD.Deref(currentReachableFromInit, previousReachableFromInit, commonNode);

            //
            return reachable;
        }

        /// <summary>
        /// Forward search, check destination is reachable from source
        /// [ REFS: '', DEREFS:]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="transitions"></param>
        /// <returns></returns>
        public bool PathForward3(CUDDNode source, CUDDNode destination, List<CUDDNode> transitions)
        {
            //
            bool reachable = false;

            CUDDNode allReachableFromInit, currentReachableFromInit;
            CUDD.Ref(source, source);
            allReachableFromInit = currentReachableFromInit = source;

            CUDDNode commonNode = CUDD.Constant(0);

            do
            {
                Debug.WriteLine("Successors: " + numberOfBDDOperation++);

                currentReachableFromInit = Successors(currentReachableFromInit, transitions);

                //Check 2 directions have intersection
                CUDD.Ref(destination, currentReachableFromInit);
                CUDD.Deref(commonNode);
                commonNode = CUDD.Function.And(destination, currentReachableFromInit);

                if (!commonNode.Equals(CUDD.ZERO))
                {
                    reachable = true;
                    break;
                }

                //find fixpoint
                CUDD.Ref(currentReachableFromInit, allReachableFromInit);
                CUDDNode allReachabeFromInitTemp = CUDD.Function.Or(currentReachableFromInit, allReachableFromInit);

                if (allReachabeFromInitTemp.Equals(allReachableFromInit))
                {
                    //reachable = false;
                    CUDD.Deref(allReachabeFromInitTemp);
                    break;
                }
                else
                {
                    currentReachableFromInit = CUDD.Function.Different(currentReachableFromInit, allReachableFromInit);
                    allReachableFromInit = allReachabeFromInitTemp;
                }
            } while (true);

            CUDD.Deref(allReachableFromInit, currentReachableFromInit, commonNode);

            //
            return reachable;
        }

        /// <summary>
        /// Forward search, check destination is reachable from source
        /// Store all reachable states and check fix point
        /// At step t, get rechable(1, k)
        /// check reachable(0, t) == rechable(0, t + 1) where reachable(a, b) is the rechable states from time a to time b
        /// [ REFS: '', DEREFS:]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="discreteTransitions"></param>
        /// <returns></returns>
        public bool PathForward4(CUDDNode source, CUDDNode destination, List<CUDDNode> discreteTransitions, List<CUDDNode> tickTransitions)
        {
            //
            bool reachable = false;

            CUDD.Ref(source);
            CUDDNode allReachableFromInit = SuccessorsStart(source, discreteTransitions);

            do
            {
                Debug.WriteLine("Successors: " + numberOfBDDOperation++);

                CUDD.Ref(allReachableFromInit);
                CUDDNode currentReachableFromInit = Successors(allReachableFromInit, tickTransitions);

                currentReachableFromInit = SuccessorsStart(currentReachableFromInit, discreteTransitions);

                //Check 2 directions have intersection
                CUDD.Ref(destination, currentReachableFromInit);
                CUDDNode commonNode = CUDD.Function.And(destination, currentReachableFromInit);

                if (!commonNode.Equals(CUDD.ZERO))
                {
                    CUDD.Deref(commonNode);
                    reachable = true;
                    break;
                }

                CUDD.Deref(commonNode);

                //find fixpoint
                CUDD.Ref(currentReachableFromInit, allReachableFromInit);
                CUDDNode allReachabeFromInitTemp = CUDD.Function.Or(currentReachableFromInit, allReachableFromInit);

                if (allReachabeFromInitTemp.Equals(allReachableFromInit))
                {
                    //reachable = false;
                    CUDD.Deref(allReachabeFromInitTemp);
                    break;
                }
                else
                {
                    CUDD.Deref(allReachableFromInit);

                    allReachableFromInit = allReachabeFromInitTemp;
                }
            } while (true);

            CUDD.Deref(allReachableFromInit);

            //
            return reachable;
        }
    }
}