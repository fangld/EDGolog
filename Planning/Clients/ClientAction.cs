using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Clients
{
    public class ClientAction : Action<ClientAbstractPredicate>
    {
        #region Properties

        public CUDDNode SuccessorStateAxiom { get; set; }

        #endregion

        #region Methods for creating an instance

        public override void FromContext(int initialCuddIndex, PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            CurrentCuddIndex = initialCuddIndex;
            Name = context.actionSymbol().GetText();
            GenerateVariableList(context.listVariable());
            GenerateAbstractPredicates(context.actionDefBody(), predDict);
            GeneratePrecondition(context, predDict);
            GenerateEffect(context, predDict);
            GenerateSuccessorStateAxiom();
        }

        protected override void GenerateAbstractPredicates(PlanningParser.AtomicFormulaTermContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            var abstractPredicate = CreateAbstractPredicate(context, predDict);
            if (!_abstractPredDict.ContainsKey(abstractPredicate.ToString()))
            {
                abstractPredicate.PreviousCuddIndex = CurrentCuddIndex;
                CurrentCuddIndex++;
                abstractPredicate.SuccessorCuddIndex = CurrentCuddIndex;
                CurrentCuddIndex++;
                _abstractPredDict.Add(abstractPredicate.ToString(), abstractPredicate);
            }
        }

        #endregion

        #region Methods for generating precondition

        private void GeneratePrecondition(PlanningParser.ActionDefineContext context)
        {
            Precondition = CUDD.ONE;

            if (context.actionDefBody().emptyOrPreGD() != null)
            {
                if (context.actionDefBody().emptyOrPreGD().gd() != null)
                {
                    Precondition = GetCuddNode(context.actionDefBody().emptyOrPreGD().gd(), true);
                }
            }
        }

        #endregion

        #region Methods for getting cudd nodes

        private CUDDNode GetCuddNode(PlanningParser.AtomicFormulaTermContext context, bool isPrevious = true)
        {
            ClientAbstractPredicate abstractPredicate = GetAbstractPredicate(context);
            int index = isPrevious ? abstractPredicate.PreviousCuddIndex : abstractPredicate.SuccessorCuddIndex;
            CUDDNode result = CUDD.Var(index);
            return result;
        }

        #endregion
    }
}
