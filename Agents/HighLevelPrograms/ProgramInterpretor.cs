//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LanguageRecognition;

//namespace Agents.HighLevelPrograms
//{
//    public class ProgramInterpretor
//    {
//        public void EnterProgram(HighLevelProgramParser.ProgramContext context)
//        {
//            //Console.WriteLine(context.GetText());
//            if (context.action() != null)
//            {
//                EnterAction(context.action());
//            }
//            else if (context.SEMICOLON() != null)
//            {
//                EnterProgram(context.program()[0]);
//                EnterProgram(context.program()[1]);
//            }
//            else if (context.IF() != null)
//            {
//                if (context.subjectFormula())
//                throw new NotImplementedException();
//                if (context.ELSE() == null)
//                {
//                    EnterProgram(context.program()[0]);
//                }
//            }
//            else if (context.WHILE() != null)
//            {
//                throw new NotImplementedException();
//            }
//            //Console.WriteLine(context.GetText());
//        }

//        public void EnterAction(HighLevelProgramParser.ActionContext context)
//        {
//            //Console.WriteLine(context.GetText());
//        }
//    }
//}
