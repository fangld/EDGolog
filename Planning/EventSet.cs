using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class EventSet
    {
        #region Fields

        private List<Event> _eventList;

        #endregion

        #region Properties

        public CUDDNode SuccessorStateAxiom { get; set; }

        #endregion

        #region Constructors

        public EventSet()
        {
            
        }

        #endregion
    }
}
