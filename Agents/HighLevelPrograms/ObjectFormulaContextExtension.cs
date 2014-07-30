//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LanguageRecognition;
//using PAT.Common.Classes.CUDDLib;

//namespace Agents.HighLevelPrograms
//{
//    public static class ObjectFormulaContextExtension
//    {
//        public static CUDDNode GetCuddNode(this HighLevelProgramParser.ObjectFormulaContext context)
//        {
//            CUDDNode result;
//            if (context.AND() != null)
//            {
//                bool impliesLeftSubForm = Implies(mentalAttitude, context.subjectFormula(0));
//                bool impliesRightSubForm = Implies(mentalAttitude, context.subjectFormula(1));
//                result = impliesLeftSubForm && impliesRightSubForm;
//            }
//            else if (context.OR() != null)
//            {
//                bool impliesLeftSubForm = Implies(mentalAttitude, context.subjectFormula(0));
//                bool impliesRightSubForm = Implies(mentalAttitude, context.subjectFormula(1));
//                result = impliesLeftSubForm || impliesRightSubForm;
//            }
//            else if (context.NOT() != null)
//            {
//                bool implieSubForm = Implies(mentalAttitude, context.subjectFormula(0));
//                result = !implieSubForm;
//            }
//            else if (context.FORALL() != null)
//            {

//            }
//            else if (context.EXISTS() != null)
//            {

//            }
//            else
//            {
//                bool implieSubForm = Implies(mentalAttitude, context.subjectFormula(0));
//                result = implieSubForm;
//            }
//        }
//    }
//}
