using System;
using System.Collections.Generic;
using PAT.Common.Classes.Expressions;
using PAT.Common.Classes.Expressions.ExpressionClass;

namespace PAT.Common.Classes.LTS
{
    /// <summary>
    /// Event class for all event prefixing and Data operations constructs
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Basic event name
        /// </summary>
        public string BaseName;

        /// <summary>
        /// Expression List in the compound event
        /// </summary>
        public Expression[] ExpressionList;

        //if FullName is not null, means the expression list is null, and there is no varaible inside the event.
        public string EventID;
        public string EventName;

        public Event(string name)
        {
            BaseName = name;
        }

        
        public override string ToString()
        {
            //string toReturn;
            if (EventID == null)
            {
                return BaseName + Ultility.Ultility.PPStringListDot(ExpressionList);
            }

            return EventID;
        }

        
        public string GetID()
        {
            if (EventID == null)
            {
                return BaseName + Ultility.Ultility.PPIDListDot(ExpressionList);
            }

            return EventID;
        }

        
        public override bool Equals(object obj)
        {
            return GetID() == (obj as Event).GetID();
        }

        public override int GetHashCode()
        {
            return GetID().GetHashCode();
        }

        //Assumption: the expression list of event can only contain process parameters, which mean after constant clearance, the expression must be a constant.
        public virtual Event ClearConstant(Dictionary<string, Expression> constMapping)
        {
            if (EventID == null)
            {
                string newID = BaseName;
                string newName = BaseName;
                int size = (ExpressionList == null) ? 0 : ExpressionList.Length;
                List<Expression> newExpressionList = new List<Expression>(size);
                bool hasVar = false;

                for (int i = 0; i < size; i++)
                {
                    Expression tempExp = ExpressionList[i].ClearConstant(constMapping);

                    //if there is no variables inside tempExp, then evaluate expression
                    if (!tempExp.HasVar)
                    {
                        throw new Exception("Expression " + tempExp + " has no value! Please make sure it has a value when used in event!");
                    }
                    //otherwise simplely display the variable names.
                    else
                    {
                        hasVar = true;
                        newExpressionList.Add(tempExp);
                    }
                }

                if (hasVar)
                {
                    Event newEvt = new Event(BaseName);
                    newEvt.ExpressionList = newExpressionList.ToArray();
                    return newEvt;
                }
                else
                {
                    Event newEvt = new Event(BaseName);
                    //no need to set since it is null anyway
                    //newEvt.ExpressionList = null;
                    newEvt.EventID = newID;
                    newEvt.EventName = newName;
                    return newEvt;
                }
            }
            //if full name is not null, then there is nothing to be changed, so we can safely return the event itself.

            return this;
        }
    }
}