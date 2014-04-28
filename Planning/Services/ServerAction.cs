using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Services
{
    public class ServerAction : Action<ServerAbstractPredicate>
    {

        #region Methods for creating an instance

        public override void FromContext(int initialCuddIndex, PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            CurrentCuddIndex = initialCuddIndex;
            Name = context.actionSymbol().GetText();
            GenerateVariableList(context.listVariable());
            GenerateAbstractPredicates(context.actionDefBody(), predDict);
            GeneratePrecondition(context, predDict);
            GenerateEffect(context, predDict);
        }

        protected override CUDDNode GetCuddNode(PlanningParser.AtomicFormulaTermContext context)
        {
            ServerAbstractPredicate abstractPredicate = GetAbstractPredicate(context);
            int index = abstractPredicate.CuddIndex;
            CUDDNode result = CUDD.Var(index);
            return result;
        }

        protected override void GenerateAbstractPredicates(PlanningParser.AtomicFormulaTermContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            var abstractPredicate = CreateAbstractPredicate(context, predDict);
            if (!_abstractPredDict.ContainsKey(abstractPredicate.ToString()))
            {
                abstractPredicate.CuddIndex = CurrentCuddIndex;
                CurrentCuddIndex++;
                _abstractPredDict.Add(abstractPredicate.ToString(), abstractPredicate);
            }
        }

        #endregion
    }
}
