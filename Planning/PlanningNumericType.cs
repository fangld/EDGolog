using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class PlanningNumericType : PlanningType
    {
        public int Min { get; set; }

        public int Max { get; set; }

        public override string ToString()
        {
            string reuslt = string.Format("Name: {0}, Min: {1}, Max: {2}", Name, Min, Max);
            return reuslt;
        }
    }
}
