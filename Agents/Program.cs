using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agents
{
    class Program
    {
        static void Main(string[] args)
        {
            Agent agent = new Agent();
            agent.Connect();
            string action;
            do
            {
                action = Console.ReadLine();
                if (action == "d11")
                {
                    agent.ExecutionAction("dunk", "bomb1", "toilet1");
                }
                else if (action == "d22")
                {
                    agent.ExecutionAction("dunk", "bomb2", "toilet2");
                }
                else
                {
                    agent.ExecutionAction("flush", "toilet1");
                }
            } while (true);
        }
    }
}
