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
            Client agent = new Client();
            agent.Connect();
            do
            {
                string action = Console.ReadLine();
                if (action[0] == 'd')
                {
                    string bombName = string.Format("bomb{0}", action[1]);
                    string toiletName = string.Format("toilet{0}", action[2]);

                    agent.ExecutionAction("dunk", bombName, toiletName);}
                else if (action[0] == 'f')
                {
                    string toiletName = string.Format("toilet{0}", action[1]);
                    agent.ExecutionAction("flush", toiletName);
                }
            } while (true);
        }
    }
}
