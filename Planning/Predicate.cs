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

        public Predicate(PlanningParser.AtomFormSkeletonContext context, string[] constArray, int intialCuddIndex): base(constArray)
        {
            Name = context.predicate().GetText();
            PreviousCuddIndex = intialCuddIndex;
            SuccessiveCuddIndex = intialCuddIndex + 1;
        }

        public bool Satisfy(string name, int[] argumentIndexArray, string[] argumentNameArray)
        {
            bool result = Name == name;
            if (result)
            {
                for (int i = 0; i < argumentIndexArray.Length; i++)
                {
                    if (ConstList[i] != argumentNameArray[i])
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}