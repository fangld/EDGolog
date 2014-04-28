using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Services
{
    public class ServerAction : Action
    {

        #region Constructors

        protected ServerAction(int initialCuddIndex): base(initialCuddIndex)
        {
        }

        #endregion

        #region Methods for creating an instance

        public static ServerAction FromContext(int initialCuddIndex, PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            ServerAction result = new ServerAction(initialCuddIndex);
            result.Name = context.actionSymbol().GetText();
            result.GenerateVariableList(context.listVariable());
            result.GenerateAbstractPredicates(context.actionDefBody(), predDict);
            result.GeneratePrecondition(context, predDict);
            result.GenerateEffect(context, predDict);
            return result;
        }

        #endregion
    }
}
