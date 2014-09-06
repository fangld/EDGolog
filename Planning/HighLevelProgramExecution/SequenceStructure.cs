using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using Planning.Clients;

namespace Planning.HighLevelProgramExecution
{
    public class SequenceStructure : Program
    {
        #region Fields

        private Program[] _subProgramArray;

        #endregion

        #region Properties

        public IReadOnlyList<Program> SubPrograms
        {
            get { return _subProgramArray; }
        }

        public int SubProgramLength
        {
            get { return _subProgramArray.Length; }
        }

        #endregion

        #region Constructors

        public SequenceStructure(PlanningParser.ProgramContext context)
        {
            int count = context.program().Count;
            _subProgramArray = new Program[count];
            Parallel.For(0, count, i => _subProgramArray[i] = CreateInstance(context.program(i)));

        }

        public SequenceStructure(Program[] subProgramArray)
        {
            _subProgramArray = subProgramArray;
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _subProgramArray.Length - 1; i++)
            {
                sb.AppendFormat("{0}; ", _subProgramArray[i]);
            }

            sb.AppendFormat("{0}", _subProgramArray[_subProgramArray.Length - 1]);
            
            return sb.ToString();
        }

        #endregion
    }
}
