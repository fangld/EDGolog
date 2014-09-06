using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using Planning.Clients;

namespace Planning.HighLevelProgramExecution
{
    public class Action: Program
    {
        #region Properties

        public string FullName { get; private set; }

        #endregion

        #region Constructors

        //public Action(string fullName)
        //{
        //    FullName = fullName;
        //}

        public Action(PlanningParser.ProgramContext context)
        {
            FullName = ConstContainer.GetFullName(context.actionSymbol(), context.term());
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return FullName;
        }

        #endregion
    }
}
