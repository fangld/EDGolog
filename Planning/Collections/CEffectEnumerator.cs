using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;

namespace Planning.Collections
{
    public class CEffectEnumerator : MixedRadixEnumeratorWithAssignment<PlanningParser.CEffectContext>
    {
        #region Fields

        private IReadOnlyDictionary<string, Predicate> _predicateDict;

        private List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> _condEffectList;

        private CUDDNode _conditionNode;

        #endregion

        #region Properties

        public List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> CondEffectList
        {
            get { return _condEffectList; }
        }

        #endregion

        #region Constructors

        public CEffectEnumerator(PlanningParser.CEffectContext context, CUDDNode conditionNode,
            IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
            : base(
                context, context.listVariable().GetCollection(), context.listVariable().GetVariableNameList(),
                assignment)
        {
            _condEffectList = new List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>>();
            _conditionNode = conditionNode;
            _predicateDict = predicateDict;
        }

        #endregion

        #region Overriden Methods

        public override void Execute()
        {
            var condEffectList = Event.GetCondEffectList(_context.effect(), _conditionNode, _predicateDict, _assignment);
            _condEffectList.AddRange(condEffectList);
        }

        #endregion
    }
}
