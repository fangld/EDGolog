using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agents.Planning;
using PAT.Common.Classes.CUDDLib;

namespace Agents.HighLevelPrograms
{
    public class MentalAttitude
    {
        #region Fields

        private Problem _problem;

        #endregion

        #region Properties

        private CUDDNode Knowledge;

        private CUDDNode Belief;

        #endregion

        #region Constructors

        public MentalAttitude(Problem problem)
        {
            _problem = problem;
        }

        #endregion

        #region Methods

        public void Update(GroundAction gndAction)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
