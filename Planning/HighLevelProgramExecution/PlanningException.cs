﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.HighLevelProgramExecution
{
    public class PlanningException: Exception
    {
        #region Constructors

        public PlanningException() { }

        public PlanningException(string message) : base(message) { }

        #endregion
    }
}
