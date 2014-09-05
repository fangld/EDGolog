using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.HighLevelProgramExecution
{
    public class Sequence : Program
    {
        #region Properties

        public Program SubProgram1 { get; private set; }
        public Program SubProgram2 { get; private set; }

        #endregion
    }
}
