using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class Predicate : ConstContainer
    {
        #region Properties

        public int PreviousCuddIndex { get; set; }

        public int SuccessiveCuddIndex { get; set; }

        #endregion

        #region Constructors

        public Predicate(PlanningParser.AtomFormSkeletonContext context, string[] constArray, ref int intialCuddIndex): base(constArray)
        {
            Name = context.pred().GetText();
            PreviousCuddIndex = intialCuddIndex;
            intialCuddIndex++;
            SuccessiveCuddIndex = intialCuddIndex;
            intialCuddIndex++;
        }

        #endregion
    }
}