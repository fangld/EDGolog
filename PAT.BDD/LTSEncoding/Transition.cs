using System.Collections.Generic;
using PAT.Common.Classes.CUDDLib;
using PAT.Common.Classes.Expressions.ExpressionClass;
using PAT.Common.Classes.LTS;
using PAT.Common.Classes.Ultility;

namespace PAT.Common.Classes.SemanticModels.LTS.BDD
{
    public class Transition
    {
        public Expression GuardCondition;

        public Event Event;

        public Expression ProgramBlock;

        public bool HasLocalVariable;

        public State FromState;
        public State ToState;

        public Transition(Event e, Expression Guard, Expression assignment, State from, State to)
        {
            Event = e;
            ProgramBlock = assignment;
            //LocalVariables = localvar;
            GuardCondition = Guard;
            FromState = from;
            ToState = to;
        }

        public override string ToString()
        {
            return "\"" + FromState + "\"--" + GetTransitionLabel() + "-->\"" + ToState + "\"";
        }

        private string label = null;
        public string GetTransitionLabel()
        {
            if (label == null)
            {
                string program = "";

                if (ProgramBlock != null)
                {
                    program += "{" + ProgramBlock + "}";
                }

                string guard = "";

                if (GuardCondition != null)
                {
                    guard += "[" + GuardCondition + "]";
                }

                label = guard + Event + program;
            }

            return label;
        }

        public List<Transition> ClearConstantExtended(List<State> newStates, Dictionary<string, Expression> constMapping)
        {
            return new List<Transition>() { ClearConstant(newStates, constMapping, false) };
        }


        public Transition ClearConstant(List<State> newStates, Dictionary<string, Expression> constMapping, bool checkSelect)
        {
            State newFrom = null, newTo = null;
            foreach (State state in newStates)
            {
                if (state.Name == FromState.Name)
                {
                    newFrom = state;
                }

                if (state.Name == ToState.Name)
                {
                    newTo = state;
                }
            }

            return new Transition(Event.ClearConstant(constMapping), GuardCondition == null ? GuardCondition : GuardCondition.ClearConstant(constMapping), ProgramBlock == null ? ProgramBlock : ProgramBlock.ClearConstant(constMapping), newFrom, newTo);
        }

        
        /// <summary>
        /// Encode transition, if it is synchronized, then we don't add constraint of unchanged global variables
        /// Parallel process only synchorinize event which does not change global variable or each transition changes same to global variables
        /// 3 kinds of transition: normal event, async channel input and async channel output
        /// </summary>
        /// <param name="encoder"></param>
        /// <param name="processVariableName"></param>
        /// <param name="localVars">Local of the current SymbolicLTS is unchanged</param>
        /// <param name="isSynchronized"></param>
        /// <returns></returns>
        public List<CUDDNode> Encode(BDDEncoder encoder, string processVariableName, List<int> localVars, bool isSynchronized)
        {
            Expression guardExpressions = new PrimitiveApplication(PrimitiveApplication.EQUAL,
                                                        new Variable(processVariableName), new IntConstant(encoder.stateIndexOfCurrentProcess[this.FromState.Name]));

            guardExpressions = Expression.CombineGuard(guardExpressions, GuardCondition);


            Expression eventUpdateExpression;
            if (this.Event is BDDEncoder.EventChannelInfo)
            {
                int channelIndex = encoder.GetChannelIndex(this.Event.BaseName, (this.Event as BDDEncoder.EventChannelInfo).type);
                eventUpdateExpression = new Assignment(Model.EVENT_NAME, new IntConstant(channelIndex));
            }
            else
            {
                eventUpdateExpression = encoder.GetEventExpression(this.Event);
            }

            Assignment stateUpdateExpression = new Assignment(processVariableName, new IntConstant(encoder.stateIndexOfCurrentProcess[this.ToState.Name]));
            Sequence updateExpressions = new Sequence(eventUpdateExpression, stateUpdateExpression);

            if (this.ProgramBlock != null)
            {
                updateExpressions = new Sequence(updateExpressions, this.ProgramBlock);
            }

            List<int> unchangedVars = new List<int>(localVars);
            if (!isSynchronized)
            {
                unchangedVars.AddRange(encoder.model.GlobalVarIndex);
            }

            return encoder.model.EncodeTransition(guardExpressions, updateExpressions, unchangedVars);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoder"></param>
        /// <param name="processVariableName"></param>
        /// <param name="localVars">Local of the current SymbolicLTS is unchanged</param>
        /// <returns></returns>
        public List<CUDDNode> EncodeSyncChannelInTransition(BDDEncoder encoder, string processVariableName, List<int> localVars)
        {
            Expression guard = new PrimitiveApplication(PrimitiveApplication.EQUAL,
                                                        new Variable(processVariableName), new IntConstant(encoder.stateIndexOfCurrentProcess[this.FromState.Name]));

            guard = Expression.CombineGuard(guard, GuardCondition);

            List<Expression> parameters = encoder.GetParaExpInEvent(this.Event);
            int channelEventIndex = encoder.GetEventIndex(this.Event.BaseName, parameters.Count);

            guard = new PrimitiveApplication(PrimitiveApplication.AND, guard, new Assignment(Model.EVENT_NAME, new IntConstant(channelEventIndex)));
            guard = new PrimitiveApplication(PrimitiveApplication.AND, guard, new Assignment(processVariableName, new IntConstant(encoder.stateIndexOfCurrentProcess[this.ToState.Name])));

            //channel input has not program block
            for (int i = 0; i < parameters.Count; i++)
            {
                if (parameters[i] is IntConstant)
                {
                    //eventParameterVariables[i]' = exps[i]
                    guard = new PrimitiveApplication(PrimitiveApplication.AND, guard, new Assignment(encoder.model.eventParameterVariables[i], parameters[i]));
                }
                else
                {
                    //eventParameterVariables[i]' = exps[i]'
                    guard = new PrimitiveApplication(PrimitiveApplication.AND, guard,
                                 new PrimitiveApplication(PrimitiveApplication.EQUAL, new VariablePrime(encoder.model.eventParameterVariables[i]),
                                                          new VariablePrime(parameters[i].expressionID)));

                }
            }

            List<CUDDNode> transitions = guard.TranslateBoolExpToBDD(encoder.model).GuardDDs;
            return encoder.model.AddVarUnchangedConstraint(transitions, localVars);
        }

        /// <summary>
        /// Encode sync channel out transition as event!a.b.c
        /// </summary>
        /// <param name="encoder"></param>
        /// <param name="processVariableName"></param>
        /// <param name="localVars">Local of the current SymbolicLTS is unchanged</param>
        /// <returns></returns>
        public List<CUDDNode> EncodeSyncChannelOutTransition(BDDEncoder encoder, string processVariableName, List<int> localVars)
        {
            Expression guard = new PrimitiveApplication(PrimitiveApplication.EQUAL,
                                                       new Variable(processVariableName), new IntConstant(encoder.stateIndexOfCurrentProcess[this.FromState.Name]));

            guard = Expression.CombineGuard(guard, GuardCondition);

            List<Expression> parameters = encoder.GetParaExpInEvent(this.Event);
            int channelEventIndex = encoder.GetEventIndex(this.Event.BaseName, parameters.Count);

            guard = new PrimitiveApplication(PrimitiveApplication.AND, guard, new Assignment(Model.EVENT_NAME, new IntConstant(channelEventIndex)));
            for (int i = 0; i < parameters.Count; i++)
            {
                //assign event parameter to the values in the event expression
                guard = new PrimitiveApplication(PrimitiveApplication.AND, guard, new Assignment(encoder.model.eventParameterVariables[i], parameters[i]));
            }

            guard = new PrimitiveApplication(PrimitiveApplication.AND, guard, new Assignment(processVariableName, new IntConstant(encoder.stateIndexOfCurrentProcess[this.ToState.Name])));

            List<CUDDNode> transitions = guard.TranslateBoolExpToBDD(encoder.model).GuardDDs;
            return encoder.model.AddVarUnchangedConstraint(transitions, localVars);
        }

        public bool IsTau()
        {
            return Event.BaseName == Constants.TAU;
        }

        public bool IsTick()
        {
            return Event.BaseName == Constants.TOCK;
        }

        public bool IsTermination()
        {
            return Event.BaseName == Constants.TERMINATION;
        }
    }
}
