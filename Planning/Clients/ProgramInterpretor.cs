//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LanguageRecognition;

//namespace Planning.Clients
//{
//    public class ProgramInterpretor
//    {
//        #region Fields

//        private MentalAttitude _mentalAttitude;

//#endregion

//#region Constructors

//#endregion

//        public void Interpret(PlanningParser.ProgramContext context)
//        {
//            if (context.SEQ() != null)
//            {
//                foreach (var programContext in context.program())
//                {
//                    Interpret(programContext);
//                }
//            }
//            else if (context.IF() != null)
//            {
//                if (_mentalAttitude.Implies(context.subjectGd()))
//                {
//                    Interpret(context.program()[0]);
//                }
//                else if (context.program()[1] != null)
//                {
//                    Interpret(context.program()[1]);
//                }
//            }
//            else if (context.WHILE() != null)
//            {
//                while (_mentalAttitude.Implies(context.subjectGd()))
//                {
//                    Interpret(context.program(0));
//                }
//            }
//            else
//            {
                
//            }
//        }
//    }
//}
