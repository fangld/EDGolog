using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agents.Planning
{
    public class GroundPredicate : Ground<Predicate>
    {

        #region Properties

        public int PreviousCuddIndex { get; set; }

        public int SuccessorCuddIndex { get; set; }

        #endregion

        #region Constructors

        public GroundPredicate(Predicate pred, IEnumerable<string> constantList)
            : base(pred, constantList)
        {
        }

        #endregion
    }
}
