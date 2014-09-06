using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using Planning.Clients;

namespace Planning.HighLevelProgramExecution
{
    public class ConditionalStructure : Program
    {
        #region Properties

        public PlanningParser.SubjectGdContext Condition { get; private set; }
        public Program SubProgram1 { get; private set; }
        public Program SubProgram2 { get; private set; }

        #endregion

        #region Constructors

        public ConditionalStructure(PlanningParser.ProgramContext context)
        {
            Condition = context.subjectGd();
            SubProgram1 = CreateInstance(context.program(0));
            SubProgram2 = context.program().Count == 2 ? CreateInstance(context.program(1)) : EmptyProgram;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            string result = string.Format("if {0} then {1} else {2} endIf", Condition.GetText(), SubProgram1, SubProgram2);
            return result;
        }

        #endregion
    }
}
