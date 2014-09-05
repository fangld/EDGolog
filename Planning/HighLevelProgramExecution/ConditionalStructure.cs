using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning.HighLevelProgramExecution
{
    public class ConditionalStructure : Program
    {
        #region Properties

        public PlanningParser.SubjectGdContext Condition { get; private set; }
        public Program SubProgram1 { get; private set; }
        public Program SubProgram2 { get; private set; }

        #endregion
    }
}
