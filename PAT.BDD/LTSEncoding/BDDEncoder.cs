using System.Collections.Generic;
using PAT.Common.Classes.CUDDLib;
using PAT.Common.Classes.Expressions.ExpressionClass;
using PAT.Common.Classes.Expressions;
using PAT.Common.Classes.LTS;
using PAT.Common.Classes.Ultility;

namespace PAT.Common.Classes.SemanticModels.LTS.BDD
{
    public class BDDEncoder
    {
        public Model model;

        /// <summary>
        /// Used to trace back the event name. All events of the system are encode in 1 BDD variable
        /// event name.number of parameters.corresponding index
        /// </summary>
        public List<EventChannelInfo> allEventIndex = new List<EventChannelInfo>();

        /// <summary>
        /// Mapping state to the int index of the current encoded process
        /// </summary>
        public Dictionary<string, int> stateIndexOfCurrentProcess = new Dictionary<string, int>();

        
        /// <summary>
        /// Return the event Index based on eventName & parameter Length. If not exist, add new event to allEventIndex dictionary
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="parameterLength"></param>
        /// <returns></returns>
        public int GetEventIndex(string eventName, int parameterLength)
        {
            for (int i = 0; i < this.allEventIndex.Count; i++)
            {
                EventChannelInfo eventChannel = this.allEventIndex[i];
                if (eventChannel.type == EventChannelInfo.EventType.EVENT && eventChannel.name == eventName && eventChannel.numberOfParameters == parameterLength)
                {
                    return i;
                }
            }

            this.allEventIndex.Add(new EventChannelInfo(eventName, parameterLength, EventChannelInfo.EventType.EVENT));
            //
            return this.allEventIndex.Count - 1;
        }

        /// <summary>
        /// Return the update event expression when the Event is an Event object
        /// </summary>
        /// <param name="Event"></param>
        /// <returns></returns>
        public Expression GetEventExpression(Event Event)
        {
            List<Expression> parameters = this.GetParaExpInEvent(Event);

            int eventIndex = this.GetEventIndex(Event.BaseName, parameters.Count);

            Expression eventUpdateExpression = new Assignment(Model.EVENT_NAME, new IntConstant(eventIndex));

            if (parameters.Count > 0)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    eventUpdateExpression = new Sequence(eventUpdateExpression, new Assignment(this.model.eventParameterVariables[i], parameters[i]));
                }
            }

            return eventUpdateExpression;
        }

        /// <summary>
        /// Get the expression list from event.
        /// Event.a.b return [a, b]
        /// Event.1.2 return [1, 2]
        /// </summary>
        /// <param name="Event"></param>
        /// <returns></returns>
        public List<Expression> GetParaExpInEvent(Event Event)
        {
            List<Expression> parameters = new List<Expression>();

            if (Event.ExpressionList != null)
            {
                parameters = new List<Expression>(Event.ExpressionList);
            }
            else if (Event.EventID != null && Event.EventID.Contains("."))
            {
                string[] temp = Event.EventID.Split('.');

                for (int i = 1; i < temp.Length; i++)
                {
                    parameters.Add(new IntConstant(int.Parse(temp[i])));
                }
            }

            //
            return parameters;
        }

        public int GetChannelIndex(string channelName, EventChannelInfo.EventType type)
        {
            for (int i = 0; i < this.allEventIndex.Count; i++)
            {
                EventChannelInfo eventChannel = this.allEventIndex[i];
                if (eventChannel.type == type && eventChannel.name == channelName)
                {
                    return i;
                }
            }

            EventChannelInfo channel = new EventChannelInfo(channelName, 0, type);
            this.allEventIndex.Add(channel);
            //
            return this.allEventIndex.Count - 1;
        }

        /// <summary>
        /// Add global variables and environment variables
        /// </summary>
        public BDDEncoder()
        {
            model = new Model();

            //4 more events for Tau, Terminate, and tock
            Model.NUMBER_OF_EVENT += 3;
            //each event with global update, add Model.NUMBER_OF_EVENT to its index
            model.AddSingleCopyVar(Model.EVENT_NAME, 0, Model.NUMBER_OF_EVENT - 1);
            
            this.allEventIndex.Add(new EventChannelInfo(Constants.TAU, 0, EventChannelInfo.EventType.EVENT));
            this.allEventIndex.Add(new EventChannelInfo(Constants.TERMINATION, 0, EventChannelInfo.EventType.EVENT));
            this.allEventIndex.Add(new EventChannelInfo(Constants.TOCK, 0, EventChannelInfo.EventType.EVENT));

            for (int i = 0; i < Model.MAX_NUMBER_EVENT_PARAMETERS; i++)
            {
                string varName = Model.EVENT_NAME + Model.NAME_SEPERATOR + i;
                model.eventParameterVariables.Add(varName);
                model.AddSingleCopyVar(varName, Model.MIN_EVENT_INDEX[i], Model.MAX_EVENT_INDEX[i]);
            }
        }

        public class EventChannelInfo : Event
        {
            public enum EventType { EVENT, ASYNC_CHANNEL_INPUT, ASYNC_CHANNEL_OUTPUT, SYNC_CHANNEL_INPUT, SYNC_CHANNEL_OUTPUT };

            /// <summary>
            /// Name of event or channel
            /// </summary>
            public string name;
            /// <summary>
            /// Used for only event, later get parameters value from event#0, event#1...
            /// </summary>
            public int numberOfParameters;

            public EventType type;

            public EventChannelInfo(string name, int numberOfParameters, EventType type)
                : base(name)
            {
                this.name = name;
                this.numberOfParameters = numberOfParameters;
                this.type = type;
            }
        }
    }
}
