using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class GroundPredicate : Ground<Predicate>
    {
        #region Properties

        #endregion

        #region Methods

        public void From(Predicate container, IEnumerable<string> constantList)
        {
            Container = container;
            SetConstantList(constantList);
        }

        #endregion
    }
}
