using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using Planning.Clients;

namespace Planning.HighLevelProgramExecution
{
    public class LoopStructure : Program
    {
        #region Properties

        public PlanningParser.SubjectGdContext Condition { get; private set; }
        public Program SubProgram { get; private set; }

        #endregion

        #region Constructors

        public LoopStructure(PlanningParser.ProgramContext context)
        {
            Condition = context.subjectGd();
            SubProgram = CreateInstance(context.program(0));
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            string result = string.Format("while {0} do {1} endWhile", Condition.GetText(), SubProgram);
            return result;
        }

        #endregion
    }
}
