using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.Clients;

namespace Agents.HighLevelPrograms
{
    public static class MentalAttitudeExtension
    {
        public static bool Implies(this MentalAttitude mentalAttitude,
            HighLevelProgramParser.SubjectFormulaContext context)
        {
            bool result;
            if (context.KNOW() != null)
            {
                CUDDNode knowledge = mentalAttitude.Knowledge;
                CUDD.Ref(knowledge);
                CUDDNode query = context.objectFormula().GetCuddNode();
                CUDDNode impliesNode = CUDD.Function.Implies(knowledge, query);
                result = impliesNode.Equals(CUDD.ONE);
            }
            else if (context.BEL() != null)
            {
                CUDDNode belief = mentalAttitude.Belief;
                CUDD.Ref(belief);
                CUDDNode query = context.objectFormula().GetCuddNode();
                CUDDNode impliesNode = CUDD.Function.Implies(belief, query);
                result = impliesNode.Equals(CUDD.ONE);
            }
            else if (context.AND() != null)
            {
                bool impliesLeftSubForm = Implies(mentalAttitude, context.subjectFormula(0));
                bool impliesRightSubForm = Implies(mentalAttitude, context.subjectFormula(1));
                result = impliesLeftSubForm && impliesRightSubForm;
            }
            else if (context.OR() != null)
            {
                bool impliesLeftSubForm = Implies(mentalAttitude, context.subjectFormula(0));
                bool impliesRightSubForm = Implies(mentalAttitude, context.subjectFormula(1));
                result = impliesLeftSubForm || impliesRightSubForm;
            }
            else if (context.NOT() != null)
            {
                bool implieSubForm = Implies(mentalAttitude, context.subjectFormula(0));
                result = !implieSubForm;
            }
            else if (context.FORALL() != null)
            {
                
            }
            else if (context.EXISTS() != null)
            {

            }
            else
            {
                bool implieSubForm = Implies(mentalAttitude, context.subjectFormula(0));
                result = implieSubForm;
            }
        }
    }
}
