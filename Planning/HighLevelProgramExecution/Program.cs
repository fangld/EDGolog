using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using Planning.Clients;

namespace Planning.HighLevelProgramExecution
{
    public abstract class Program
    {
        #region Properties

        public static readonly EmptyProgram EmptyProgram;

        #endregion

        #region Constructors

        static Program()
        {
            EmptyProgram = new EmptyProgram();
        }

        #endregion

        #region Methods

        public static Program CreateInstance(PlanningParser.ProgramContext context)
        {
            Program result;
            if (context.actionSymbol() != null)
            {
                result = new Action(context);
            }
            else if (context.SEQ() != null)
            {
               result = new SequenceStructure(context);
            }
            else if (context.IF() != null)
            {
                result = new ConditionalStructure(context);
            }
            else// if (context.WHILE() != null)
            {
                result = new LoopStructure(context);

            }
            return result;
        }

        #endregion
    }
}
