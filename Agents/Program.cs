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
            agent.SendMessage();
            Console.ReadLine();
        }
    }
}
