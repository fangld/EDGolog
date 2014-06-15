using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class PlanningType
    {
        /// <summary>
        /// The name of object type
        /// </summary>
        public static readonly PlanningType ObjectType = new PlanningType() { Name = "object" };

        /// <summary>
        /// The name of agent type
        /// </summary>
        public static readonly PlanningType AgentType = new PlanningType() { Name = "agent" };

        public string Name { get; set; }

        public override string ToString()
        {
            string reuslt = string.Format("Name: {0}", Name);
            return reuslt;
        }
    }
}
