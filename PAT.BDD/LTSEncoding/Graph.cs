using System.Collections.Generic;
using System.Diagnostics;
using PAT.Common.Classes.CUDDLib;
using PAT.Common.Classes.Ultility;

namespace PAT.Common.Classes.SemanticModels.LTS.BDD
{
    public partial class Model
    {
        public int numberOfBDDOperation = 0;


        /// <summary>
        /// P ◦ R is the set of all successors of states in the set P
        /// [ REFS: 'result', DEREFS:states]
        /// </summary>
        /// <param name="states"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <returns></returns>
        public CUDDNode Successors(CUDDNode states, List<List<CUDDNode>> transitions)
        {
            Debug.WriteLine("Successors: " + numberOfBDDOperation);

            numberOfBDDOperation++;

            CUDDNode successors = TransitionsBySourceStates(states, transitions);
            successors = SwapRowColVars(successors);
            successors = CUDD.Abstract.ThereExists(successors, AllColVars);

            return successors;
        }


        /// <summary>
        /// Return transition where source states are in states
        /// [ REFS: 'result', DEREFS:states]
        /// </summary>
        /// <param name="states">source state of transition</param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <returns></returns>
        private CUDDNode TransitionsBySourceStates(CUDDNode states, List<List<CUDDNode>> transitions)
        {
            CUDDNode successors;
            if (transitions.Count > 1)
            {
                CUDD.Ref(states); CUDD.Ref(transitions[1]);
                CUDDNode temp = CUDD.Function.And(states, transitions[1]);

                if (!temp.Equals(CUDD.ZERO))
                {
                    CUDD.Deref(states);
                    successors = temp;
                }
                else
                {
                    CUDD.Deref(temp);
                    CUDD.Ref(transitions[0]);
                    successors = CUDD.Function.And(states, transitions[0]);
                }
            }
            else
            {
                CUDD.Ref(transitions[0]);
                successors = CUDD.Function.And(states, transitions[0]);
            }

            return successors;
        }


        /// <summary>
        /// return all states reachable from start and Start
        /// P ◦ R*
        /// [ REFS: 'result', DEREFS:start]
        /// </summary>
        /// <param name="start"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <returns></returns>
        public CUDDNode SuccessorsStart(CUDDNode start, List<List<CUDDNode>> transition)
        {
            CUDD.Ref(start);
            return CUDD.Function.Or(start, SuccessorsPlus(start, transition));
        }

        /// <summary>
        /// return all states reachable from start
        /// P ◦ R+
        /// [ REFS: 'result', DEREFS:start]
        /// </summary>
        /// <param name="start"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <returns></returns>
        public CUDDNode SuccessorsPlus(CUDDNode start, List<List<CUDDNode>> transition)
        {
            CUDDNode allReachableStates = CUDD.Constant(0);

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
                    CUDD.Deref(allReachableStates);
                    allReachableStates = allReachableTemp;
                }
            }
        }

        /// <summary>
        /// R ◦ P is the set of all predecessors of states in the set P
        /// [ REFS: 'result', DEREFS:states]
        /// </summary>
        /// <param name="states"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <returns></returns>
        public CUDDNode Predecessors(CUDDNode states, List<List<CUDDNode>> transitions)
        {
            Debug.WriteLine("Predecessors: " + numberOfBDDOperation);

            numberOfBDDOperation++;

            states = SwapRowColVars(states);
            CUDDNode predecessors = TransitionsByTargetStates(states, transitions);
            predecessors = CUDD.Abstract.ThereExists(predecessors, AllColVars);

            return predecessors;
        }

        /// <summary>
        /// Return transition whose target states in states
        /// [ REFS: 'result', DEREFS:states]
        /// </summary>
        /// <param name="states">In column form</param>
        /// <param name="transitions"></param>
        /// <returns></returns>
        private CUDDNode TransitionsByTargetStates(CUDDNode states, List<List<CUDDNode>> transitions)
        {
            CUDDNode predecessors;
            if (transitions.Count > 1)
            {
                CUDD.Ref(states);
                CUDD.Ref(transitions[1]);
                CUDDNode temp = CUDD.Function.And(states, transitions[1]);

                if (!temp.Equals(CUDD.ZERO))
                {
                    CUDD.Deref(states);
                    predecessors = temp;
                }
                else
                {
                    CUDD.Deref(temp);
                    CUDD.Ref(transitions[0]);
                    predecessors = CUDD.Function.And(states, transitions[0]);
                }
            }
            else
            {
                CUDD.Ref(transitions[0]);
                predecessors = CUDD.Function.And(states, transitions[0]);
            }
            return predecessors;
        }

        /// <summary>
        /// return all states reachable from start and start
        /// P ◦ R*
        /// [ REFS: 'result', DEREFS:start]
        /// </summary>
        /// <param name="start"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <returns></returns>
        public CUDDNode PredecessorsStart(CUDDNode start, List<List<CUDDNode>> transition)
        {
            CUDD.Ref(start);
            return CUDD.Function.Or(start, PredecessorsPlus(start, transition));
        }

        /// <summary>
        /// return all states reachable to start
        /// P ◦ R+
        /// [ REFS: 'result', DEREFS:start]
        /// </summary>
        /// <param name="start"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <returns></returns>
        public CUDDNode PredecessorsPlus(CUDDNode start, List<List<CUDDNode>> transition)
        {
            CUDDNode allReachableStates = CUDD.Constant(0);

            CUDDNode currentStep = start;

            while (true)
            {
                currentStep = Predecessors(currentStep, transition);

                CUDD.Ref(allReachableStates, currentStep);
                CUDDNode allReachableTemp = CUDD.Function.Or(allReachableStates, currentStep);

                if (allReachableTemp.Equals(allReachableStates))
                {
                    CUDD.Deref(currentStep, allReachableTemp);

                    return allReachableStates;
                }
                else
                {
                    CUDD.Deref(allReachableStates);
                    allReachableStates = allReachableTemp;
                }
            }
        }

        /// <summary>
        /// Return the path from source to destination. 
        /// If reachable, return true and path contains the path from source to destination
        /// Serach from two direction: forward and backward
        /// [ REFS: '', DEREFS:]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <param name="model"></param>
        /// <param name="path">not include the init state</param>
        /// <param name="isEmptyPathAllowed">if false, then the path must be not empty though the source satisfies the destination</param>
        /// <returns></returns>
        public bool Path(CUDDNode source, CUDDNode destination, List<List<CUDDNode>> transitions, List<CUDDNode> path, bool isEmptyPathAllowed)
        {
            if (isEmptyPathAllowed)
            {
                CUDD.Ref(source, destination);
                CUDDNode temp = CUDD.Function.And(source, destination);

                //In case source already satisfies destination
                if (!temp.Equals(CUDD.ZERO))
                {
                    CUDD.Deref(temp);
                    return true;
                }
            }

            //
            bool reachable = false;
            List<CUDDNode> backtrackingReachability = new List<CUDDNode>();

            List<CUDDNode> forwardReachability = new List<CUDDNode>();

            CUDDNode allReachabeToGoal, currentReachableToGoal;
            CUDD.Ref(destination, destination);
            allReachabeToGoal = currentReachableToGoal = destination;

            CUDDNode allReachableFromInit, currentReachableFromInit;
            CUDD.Ref(source, source);
            allReachableFromInit = currentReachableFromInit = source;

            int nextStep = 0;
            int forwardStep = 0;
            int numberOfForwardNodes = 0;
            int backwardSteps = 0;
            int numberOfBackwardNodes = 0;

            CUDDNode commonNode = CUDD.Constant(0);
            do
            {
                if (nextStep != GoForward)
                {
                    //backward
                    Debug.WriteLine("Backward: " + numberOfBDDOperation);
                    numberOfBDDOperation++;

                    //to find source state, now the currentReachableToGoal must be in column form (target state)
                    currentReachableToGoal = this.SwapRowColVars(currentReachableToGoal);
                    currentReachableToGoal = TransitionsByTargetStates(currentReachableToGoal, transitions);

                    CUDD.Ref(currentReachableToGoal);
                    backtrackingReachability.Add(currentReachableToGoal);

                    //get source state, but in row-form
                    currentReachableToGoal = CUDD.Abstract.ThereExists(currentReachableToGoal, AllColVars);

                    //Check 2 directions have intersection
                    CUDD.Ref(currentReachableToGoal, currentReachableFromInit);
                    CUDD.Deref(commonNode);
                    commonNode = CUDD.Function.And(currentReachableToGoal, currentReachableFromInit);

                    if (!commonNode.Equals(CUDD.ZERO))
                    {
                        reachable = true;
                        break;
                    }

                    //find fixpoint
                    CUDD.Ref(currentReachableToGoal, allReachabeToGoal);
                    CUDDNode allReachabeToGoalTemp = CUDD.Function.Or(allReachabeToGoal, currentReachableToGoal);

                    backwardSteps++;
                    numberOfBackwardNodes = CUDD.GetNumNodes(allReachabeToGoalTemp);

                    if (allReachabeToGoalTemp.Equals(allReachabeToGoal))
                    {
                        reachable = false;
                        CUDD.Deref(allReachabeToGoalTemp);
                        break;
                    }
                    else
                    {
                        currentReachableToGoal = CUDD.Function.Different(currentReachableToGoal, allReachabeToGoal);
                        allReachabeToGoal = allReachabeToGoalTemp;
                    }
                }

                if (nextStep != GoBackward)
                {
                    //forward
                    Debug.WriteLine("Forward: " + numberOfBDDOperation);
                    numberOfBDDOperation++;

                    currentReachableFromInit = TransitionsBySourceStates(currentReachableFromInit, transitions);

                    //Must reference because current value of currentReachableDD belonging to backtrackingReachability
                    CUDD.Ref(currentReachableFromInit);
                    forwardReachability.Add(currentReachableFromInit);

                    currentReachableFromInit = this.SwapRowColVars(currentReachableFromInit);

                    //get target state, in row-form
                    currentReachableFromInit = CUDD.Abstract.ThereExists(currentReachableFromInit, AllColVars);

                    //Check 2 directions have intersection
                    CUDD.Ref(currentReachableToGoal, currentReachableFromInit);
                    CUDD.Deref(commonNode);
                    commonNode = CUDD.Function.And(currentReachableToGoal, currentReachableFromInit);

                    if (!commonNode.Equals(CUDD.ZERO))
                    {
                        reachable = true;
                        break;
                    }

                    //find fixpoint
                    CUDD.Ref(currentReachableFromInit, allReachableFromInit);
                    CUDDNode allReachabeFromInitTemp = CUDD.Function.Or(currentReachableFromInit, allReachableFromInit);

                    forwardStep++;
                    numberOfForwardNodes = CUDD.GetNumNodes(allReachabeFromInitTemp);

                    if (allReachabeFromInitTemp.Equals(allReachableFromInit))
                    {
                        reachable = false;
                        CUDD.Deref(allReachabeFromInitTemp);
                        break;
                    }
                    else
                    {
                        currentReachableFromInit = CUDD.Function.Different(currentReachableFromInit, allReachableFromInit);
                        allReachableFromInit = allReachabeFromInitTemp;
                    }
                }

                nextStep = GetNextStep(forwardStep, numberOfForwardNodes, backwardSteps, numberOfBackwardNodes);
            } while (true);

            if (!reachable)
            {
                CUDD.Deref(currentReachableToGoal, allReachabeToGoal, currentReachableFromInit, allReachableFromInit, commonNode);
                CUDD.Deref(backtrackingReachability);
                CUDD.Deref(forwardReachability);

                //
                backtrackingReachability.Clear();
                forwardReachability.Clear();
            }
            else
            {
                //
                CUDD.Deref(currentReachableToGoal, allReachabeToGoal, currentReachableFromInit, allReachableFromInit);

                //in column form
                commonNode = this.SwapRowColVars(commonNode);
                for (int i = forwardReachability.Count - 1; i >= 0; i--)
                {
                    //Kill commonNode
                    CUDDNode correctTransition = CUDD.Function.And(forwardReachability[i], commonNode);

                    CUDD.Ref(correctTransition);
                    backtrackingReachability.Add(correctTransition);


                    //in column form
                    commonNode = CUDD.Abstract.ThereExists(correctTransition, AllColVars);
                    commonNode = this.SwapRowColVars(commonNode);
                }

                CUDD.Deref(commonNode);

                //backtrackingReachability contains transitions from source to destination
                if (backtrackingReachability.Count > 0)
                {
                    CUDDNode currentStateDD = source;
                    CUDD.Ref(currentStateDD);

                    for (int i = backtrackingReachability.Count - 1; i >= 0; i--)
                    {
                        //find the intersection
                        currentStateDD = CUDD.Function.And(backtrackingReachability[i], currentStateDD);

                        //remove all boolean variables in the row-form, it is now set of all reachable backward state
                        currentStateDD = CUDD.Abstract.ThereExists(currentStateDD, AllRowVars);

                        currentStateDD = CUDD.RestrictToFirst(currentStateDD, this.AllColVars);
                        currentStateDD = CUDD.Abstract.ThereExists(currentStateDD, this.GetAllEventVars());

                        //swap to row-variable form
                        currentStateDD = this.SwapRowColVars(currentStateDD);

                        CUDD.Ref(currentStateDD);
                        path.Add(currentStateDD);
                    }

                    //
                    CUDD.Deref(currentStateDD);
                }

            }
            //
            return reachable;
        }


        private const int GoForward = 1;
        private const int GoBackward = -1;

        /// <summary>
        /// Return 1 if the next step is forward
        /// Return -1 if the next step is forward
        /// Return 0 if the next step is both forward and backward
        /// </summary>
        /// <param name="forwardSteps"></param>
        /// <param name="numberOfForwardNodes"></param>
        /// <param name="backwardSteps"></param>
        /// <param name="numberOfBackwardNodes"></param>
        /// <returns></returns>
        private int GetNextStep(int forwardSteps, int numberOfForwardNodes, int backwardSteps, int numberOfBackwardNodes)
        {
            return (numberOfForwardNodes / forwardSteps < numberOfBackwardNodes / backwardSteps) ? GoForward : GoBackward;
        }

        /// <summary>
        /// Check whether destination is reachable from source
        /// Serach from two direction: forward and backward
        /// [ REFS: '', DEREFS:]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <returns></returns>
        public bool Path(CUDDNode source, CUDDNode destination, List<List<CUDDNode>> transitions)
        {
            CUDD.Ref(source, destination);
            CUDDNode temp = CUDD.Function.And(source, destination);

            //In case source already satisfies destination
            if (!temp.Equals(CUDD.ZERO))
            {
                CUDD.Deref(temp);
                return true;
            }

            //
            bool reachable = false;

            CUDDNode allReachabeToGoal, currentReachableToGoal;
            CUDD.Ref(destination, destination);
            allReachabeToGoal = currentReachableToGoal = destination;

            CUDDNode allReachableFromInit, currentReachableFromInit;
            CUDD.Ref(source, source);
            allReachableFromInit = currentReachableFromInit = source;

            int nextStep = 0;
            int forwardStep = 0;
            int numberOfForwardNodes = 0;
            int backwardSteps = 0;
            int numberOfBackwardNodes = 0;

            CUDDNode commonNode = CUDD.Constant(0);
            do
            {
                if (nextStep != GoForward)
                {
                    //backward
                    Debug.WriteLine("Backward: " + numberOfBDDOperation);

                    //to find source state, now the currentReachableToGoal must be in column form (target state)
                    currentReachableToGoal = this.Predecessors(currentReachableToGoal, transitions);

                    //Check 2 directions have intersection
                    CUDD.Ref(currentReachableToGoal, currentReachableFromInit);
                    CUDD.Deref(commonNode);
                    commonNode = CUDD.Function.And(currentReachableToGoal, currentReachableFromInit);

                    if (!commonNode.Equals(CUDD.ZERO))
                    {
                        reachable = true;
                        break;
                    }

                    //find fixpoint
                    CUDD.Ref(currentReachableToGoal, allReachabeToGoal);
                    CUDDNode allReachabeToGoalTemp = CUDD.Function.Or(allReachabeToGoal, currentReachableToGoal);

                    backwardSteps++;
                    numberOfBackwardNodes = CUDD.GetNumNodes(allReachabeToGoalTemp);

                    if (allReachabeToGoalTemp.Equals(allReachabeToGoal))
                    {
                        reachable = false;
                        CUDD.Deref(allReachabeToGoalTemp);
                        break;
                    }
                    else
                    {
                        currentReachableToGoal = CUDD.Function.Different(currentReachableToGoal, allReachabeToGoal);
                        allReachabeToGoal = allReachabeToGoalTemp;
                    }
                }

                if (nextStep != GoBackward)
                {
                    //forward
                    Debug.WriteLine("Forward: " + numberOfBDDOperation);

                    currentReachableFromInit = this.Successors(currentReachableFromInit, transitions);

                    //Check 2 directions have intersection
                    CUDD.Ref(currentReachableToGoal, currentReachableFromInit);
                    CUDD.Deref(commonNode);
                    commonNode = CUDD.Function.And(currentReachableToGoal, currentReachableFromInit);

                    if (!commonNode.Equals(CUDD.ZERO))
                    {
                        reachable = true;
                        break;
                    }

                    //find fixpoint
                    CUDD.Ref(currentReachableFromInit, allReachableFromInit);
                    CUDDNode allReachabeFromInitTemp = CUDD.Function.Or(currentReachableFromInit, allReachableFromInit);

                    forwardStep++;
                    numberOfForwardNodes = CUDD.GetNumNodes(allReachabeFromInitTemp);

                    if (allReachabeFromInitTemp.Equals(allReachableFromInit))
                    {
                        reachable = false;
                        CUDD.Deref(allReachabeFromInitTemp);
                        break;
                    }
                    else
                    {
                        currentReachableFromInit = CUDD.Function.Different(currentReachableFromInit, allReachableFromInit);
                        allReachableFromInit = allReachabeFromInitTemp;
                    }
                }

                nextStep = GetNextStep(forwardStep, numberOfForwardNodes, backwardSteps, numberOfBackwardNodes);
            } while (true);

            CUDD.Deref(allReachabeToGoal, currentReachableToGoal, allReachableFromInit, currentReachableFromInit, commonNode);

            //
            return reachable;
        }

        /// <summary>
        /// Return the path from source to destination. If reachable, return true and path contains the path from source to destination
        /// [ REFS: '', DEREFS:]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <param name="model"></param>
        /// <param name="path">not include the init state</param>
        /// <param name="isEmptyPathAllowed">if false, then the path must be not empty though the source satisfies the destination</param>
        /// <returns></returns>
        public bool PathBackWard(CUDDNode source, CUDDNode destination, List<List<CUDDNode>> transitions, List<CUDDNode> path, bool isEmptyPathAllowed)
        {
            if (isEmptyPathAllowed)
            {
                //
                CUDD.Ref(source, destination);
                CUDDNode temp = CUDD.Function.And(source, destination);

                //In case source already satisfies destination
                if (!temp.Equals(CUDD.ZERO))
                {
                    CUDD.Deref(temp);
                    return true;
                }
            }

            //
            bool reachable = false;
            List<CUDDNode> backtrackingReachability = new List<CUDDNode>();

            CUDDNode allReachabeToGoal, currentReachableToGoal;
            CUDD.Ref(destination, destination);
            allReachabeToGoal = currentReachableToGoal = destination;

            CUDDNode commonNode = CUDD.Constant(0);
            do
            {
                //backward
                Debug.WriteLine("Backward: " + numberOfBDDOperation);
                numberOfBDDOperation++;

                //to find source state, now the currentReachableToGoal must be in column form (target state)
                currentReachableToGoal = this.SwapRowColVars(currentReachableToGoal);
                currentReachableToGoal = TransitionsByTargetStates(currentReachableToGoal, transitions);

                CUDD.Ref(currentReachableToGoal);
                backtrackingReachability.Add(currentReachableToGoal);

                //get source state, but in row-form
                currentReachableToGoal = CUDD.Abstract.ThereExists(currentReachableToGoal, AllColVars);

                //Check 2 directions have intersection
                CUDD.Ref(currentReachableToGoal, source);
                CUDD.Deref(commonNode);
                commonNode = CUDD.Function.And(currentReachableToGoal, source);

                if (!commonNode.Equals(CUDD.ZERO))
                {
                    reachable = true;
                    break;
                }

                //find fixpoint
                CUDD.Ref(currentReachableToGoal, allReachabeToGoal);
                CUDDNode allReachabeToGoalTemp = CUDD.Function.Or(allReachabeToGoal, currentReachableToGoal);

                if (allReachabeToGoalTemp.Equals(allReachabeToGoal))
                {
                    reachable = false;
                    CUDD.Deref(allReachabeToGoalTemp);
                    break;
                }
                else
                {
                    currentReachableToGoal = CUDD.Function.Different(currentReachableToGoal, allReachabeToGoal);
                    allReachabeToGoal = allReachabeToGoalTemp;
                }
            } while (true);

            if (!reachable)
            {
                CUDD.Deref(currentReachableToGoal, allReachabeToGoal, commonNode);
                CUDD.Deref(backtrackingReachability);

                //
                backtrackingReachability.Clear();
            }
            else
            {
                //
                CUDD.Deref(currentReachableToGoal, allReachabeToGoal, commonNode);

                //backtrackingReachability contains transitions from source to destination
                if (backtrackingReachability.Count > 0)
                {
                    CUDDNode currentStateDD = source;
                    CUDD.Ref(currentStateDD);

                    for (int i = backtrackingReachability.Count - 1; i >= 0; i--)
                    {
                        //find the intersection
                        currentStateDD = CUDD.Function.And(backtrackingReachability[i], currentStateDD);

                        //remove all boolean variables in the row-form, it is now set of all reachable backward state
                        currentStateDD = CUDD.Abstract.ThereExists(currentStateDD, AllRowVars);

                        currentStateDD = CUDD.RestrictToFirst(currentStateDD, this.AllColVars);
                        currentStateDD = CUDD.Abstract.ThereExists(currentStateDD, this.GetAllEventVars());

                        //swap to row-variable form
                        currentStateDD = this.SwapRowColVars(currentStateDD);

                        CUDD.Ref(currentStateDD);
                        path.Add(currentStateDD);
                    }

                    //
                    CUDD.Deref(currentStateDD);
                }

            }
            //
            return reachable;
        }

        /// <summary>
        /// Return the path from source to destination. If reachable, return true and path contains the path from source to destination
        /// [ REFS: '', DEREFS:]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <returns></returns>
        public bool PathBackWard(CUDDNode source, CUDDNode destination, List<List<CUDDNode>> transitions)
        {
            CUDD.Ref(source, destination);
            CUDDNode temp = CUDD.Function.And(source, destination);

            //In case source already satisfies destination
            if (!temp.Equals(CUDD.ZERO))
            {
                CUDD.Deref(temp);
                return true;
            }

            bool reachable = false;

            CUDDNode allReachabeToGoal, currentReachableToGoal;
            CUDD.Ref(destination, destination);
            allReachabeToGoal = currentReachableToGoal = destination;

            CUDDNode commonNode = CUDD.Constant(0);
            do
            {
                //backward
                currentReachableToGoal = this.Predecessors(currentReachableToGoal, transitions);

                //Check 2 directions have intersection
                CUDD.Ref(currentReachableToGoal, source);
                CUDD.Deref(commonNode);
                commonNode = CUDD.Function.And(currentReachableToGoal, source);

                if (!commonNode.Equals(CUDD.ZERO))
                {
                    reachable = true;
                    break;
                }

                //find fixpoint
                CUDD.Ref(currentReachableToGoal, allReachabeToGoal);
                CUDDNode allReachabeToGoalTemp = CUDD.Function.Or(allReachabeToGoal, currentReachableToGoal);

                if (allReachabeToGoalTemp.Equals(allReachabeToGoal))
                {
                    reachable = false;
                    CUDD.Deref(allReachabeToGoalTemp);
                    break;
                }
                else
                {
                    currentReachableToGoal = CUDD.Function.Different(currentReachableToGoal, allReachabeToGoal);
                    allReachabeToGoal = allReachabeToGoalTemp;
                }
            } while (true);

            CUDD.Deref(allReachabeToGoal, currentReachableToGoal, commonNode);

            //
            return reachable;
        }

        /// <summary>
        /// Return the path from source to destination. If reachable, return true and path contains the path from source to destination
        /// [ REFS: '', DEREFS:]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <param name="model"></param>
        /// <param name="path">not include the init state</param>
        /// <param name="isEmptyPathAllowed">if false, then the path must be not empty though the source satisfies the destination</param>
        /// <returns></returns>
        public bool PathForward(CUDDNode source, CUDDNode destination, List<List<CUDDNode>> transitions, List<CUDDNode> path, bool isEmptyPathAllowed)
        {
            if (isEmptyPathAllowed)
            {
                CUDD.Ref(source, destination);
                CUDDNode temp = CUDD.Function.And(source, destination);

                //In case source already satisfies destination
                if (!temp.Equals(CUDD.ZERO))
                {
                    CUDD.Deref(temp);
                    return true;
                }
            }

            //
            bool reachable = false;
            List<CUDDNode> forwardReachability = new List<CUDDNode>();

            CUDDNode allReachableFromInit, currentReachableFromInit;
            CUDD.Ref(source, source);
            allReachableFromInit = currentReachableFromInit = source;

            CUDDNode commonNode = CUDD.Constant(0);
            do
            {
                //forward
                Debug.WriteLine("Forward: " + numberOfBDDOperation);

                numberOfBDDOperation++;

                currentReachableFromInit = TransitionsBySourceStates(currentReachableFromInit, transitions);

                //Must reference because current value of currentReachableDD belonging to backtrackingReachability
                CUDD.Ref(currentReachableFromInit);
                forwardReachability.Add(currentReachableFromInit);

                currentReachableFromInit = this.SwapRowColVars(currentReachableFromInit);

                //get target state, in row-form
                currentReachableFromInit = CUDD.Abstract.ThereExists(currentReachableFromInit, AllColVars);

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

            if (!reachable)
            {
                CUDD.Deref(currentReachableFromInit, allReachableFromInit, commonNode);
                CUDD.Deref(forwardReachability);

                //
                forwardReachability.Clear();
            }
            else
            {
                //
                CUDD.Deref(currentReachableFromInit, allReachableFromInit);

                //in column form
                commonNode = this.SwapRowColVars(commonNode);
                for (int i = forwardReachability.Count - 1; i >= 0; i--)
                {
                    forwardReachability[i] = CUDD.Function.And(forwardReachability[i], commonNode);

                    //in column form
                    CUDD.Ref(forwardReachability[i]);
                    commonNode = CUDD.Abstract.ThereExists(forwardReachability[i], AllColVars);
                    commonNode = this.SwapRowColVars(commonNode);
                }

                CUDD.Deref(commonNode);

                //backtrackingReachability contains transitions from source to destination
                if (forwardReachability.Count > 0)
                {
                    CUDDNode currentStateDD = source;
                    CUDD.Ref(currentStateDD);

                    for (int i = 0; i < forwardReachability.Count; i++)
                    {
                        //find the intersection
                        currentStateDD = CUDD.Function.And(forwardReachability[i], currentStateDD);

                        //remove all boolean variables in the row-form, it is now set of all reachable backward state
                        currentStateDD = CUDD.Abstract.ThereExists(currentStateDD, AllRowVars);

                        currentStateDD = CUDD.RestrictToFirst(currentStateDD, this.AllColVars);
                        currentStateDD = CUDD.Abstract.ThereExists(currentStateDD, this.GetAllEventVars());

                        //swap to row-variable form
                        currentStateDD = this.SwapRowColVars(currentStateDD);

                        CUDD.Ref(currentStateDD);
                        path.Add(currentStateDD);
                    }

                    //
                    CUDD.Deref(currentStateDD);
                }

            }
            //
            return reachable;
        }

        /// <summary>
        /// Forward search, check destination is reachable from source
        /// [ REFS: '', DEREFS:]
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <returns></returns>
        public bool PathForward(CUDDNode source, CUDDNode destination, List<List<CUDDNode>> transitions)
        {
            CUDD.Ref(source, destination);
            CUDDNode temp = CUDD.Function.And(source, destination);

            //In case source already satisfies destination
            if (!temp.Equals(CUDD.ZERO))
            {
                CUDD.Deref(temp);
                return true;
            }

            //
            bool reachable = false;

            CUDDNode allReachableFromInit, currentReachableFromInit;
            CUDD.Ref(source, source);
            allReachableFromInit = currentReachableFromInit = source;

            CUDDNode commonNode = CUDD.Constant(0);

            do
            {
                //forward
                currentReachableFromInit = this.Successors(currentReachableFromInit, transitions);

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
        /// Check whether destination is reachable from source. If it is reachable, the path to destination is added at the end of path variable
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="transitions">transitions[0]: normal transitions, transitions[1]: priority transition</param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Path(CUDDNode source, CUDDNode destination, List<List<CUDDNode>> transitions, List<CUDDNode> path, string SelectedEngineName, bool GenerateCounterExample)
        {
            if (SelectedEngineName == Constants.ENGINE_FORWARD_SEARCH_BDD)
            {
                if (GenerateCounterExample)
                {
                    return PathForward(source, destination, transitions, path, true);
                }
                else
                {
                    if (isTAModel)
                    {
                        return PathForward1(source, destination, transitions[0].GetRange(0, transitions[0].Count - 1),
                                           new List<CUDDNode>() { transitions[0][transitions[0].Count - 1] });
                    }
                    else
                    {
                        return PathForward(source, destination, transitions);
                    }
                }
            }
            else if (SelectedEngineName == Constants.ENGINE_BACKWARD_SEARCH_BDD)
            {
                if (GenerateCounterExample)
                {
                    return PathBackWard(source, destination, transitions, path, true);
                }
                else
                {
                    return PathBackWard(source, destination, transitions);
                }
            }
            if (SelectedEngineName == Constants.ENGINE_FORWARD_BACKWARD_SEARCH_BDD)
            {
                if (GenerateCounterExample)
                {
                    return Path(source, destination, transitions, path, true);
                }
                else
                {
                    return Path(source, destination, transitions);
                }
            }

            return false;
        }
    }
}