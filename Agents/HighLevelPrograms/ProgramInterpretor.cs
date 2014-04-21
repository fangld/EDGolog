using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Agents.HighLevelPrograms
{
    public  class ProgramInterpretor : HighLevelProgramBaseListener
    {
        public override void EnterProgram(HighLevelProgramParser.ProgramContext context)
        {
            Console.WriteLine(context);
        }
    }
}
