using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning.ContextExtensions
{
    public static class ListVariableContextExtension
    {
        public static IReadOnlyList<IList<string>> GetCollection(this PlanningParser.ListVariableContext context)
        {
            List<IList<string>> result = new List<IList<string>>();

            do
            {
                int count = context.VAR().Count;
                if (count != 0)
                {
                    string type = context.type() == null ? PlanningType.ObjectType.Name : context.type().GetText();

                    for (int i = 0; i < count; i++)
                    {
                        var constList = Globals.TermInterpreter.GetConstList(type);
                        result.Add(constList);
                    }
                }
                context = context.listVariable();
            } while (context != null);

            return result;
        }

        public static IReadOnlyList<string> GetVariableNameList(this PlanningParser.ListVariableContext context)
        {
            List<string> result = new List<string>();

            do
            {
                int count = context.VAR().Count;
                if (count != 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        result.Add(context.VAR(i).GetText());
                    }
                }
                context = context.listVariable();
            } while (context != null);

            return result;
        }
    }
}
