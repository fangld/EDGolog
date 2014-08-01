using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Atn;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;

namespace Planning.Collections
{
    public class ExclusiveAxiomEnumerator : MixedRadixEnumeratroWithIndexArray
    {
        #region Fields
        
        private IDictionary<string, Predicate> _predicateDict;

        private string _predicateName;

        #endregion

        #region Properties

        public CUDDNode ExclusiveAxiom { get; private set; }

        #endregion

        #region Constructors

        public ExclusiveAxiomEnumerator(string predicateName, IDictionary<string, Predicate> predicateDict, IReadOnlyList<IList<string>> collection)
            : base(collection)
        {
            //Console.WriteLine("Enter ExclusiveAxiomEnumerator's constructor");
            //Console.ReadLine();
            _predicateName = predicateName;
            ExclusiveAxiom = CUDD.Constant(1);
            _predicateDict = predicateDict;
        }

        #endregion

        #region Overriden Methods

        public override void Execute()
        {
            List<Predicate> satifiedPredList = new List<Predicate>();
            foreach (var predicate in _predicateDict.Values)
            {
                if (predicate.Satisfy(_predicateName, _index, _scanArray))
                {
                    satifiedPredList.Add(predicate);
                }
            }

            CUDDNode orNode = CUDD.Constant(0);
            for (int i = 0; i < satifiedPredList.Count; i++)
            {
                CUDDNode predicate = CUDD.Var(satifiedPredList[i].PreviousCuddIndex);
                orNode = CUDD.Function.Or(orNode, predicate);
            }

            //ExclusiveAxiom = CUDD.Function.And(ExclusiveAxiom, orNode);

            CUDDNode exclusiveNode = CUDD.Constant(1);

            for (int i = 0; i < satifiedPredList.Count - 1; i++)
            {
                for (int j = i + 1; j < satifiedPredList.Count; j++)
                {
                    CUDDNode firstLiteral = CUDD.Var(satifiedPredList[i].PreviousCuddIndex);
                    CUDDNode secondLiteral = CUDD.Var(satifiedPredList[j].PreviousCuddIndex);
                    CUDDNode bothNode = CUDD.Function.And(firstLiteral, secondLiteral);
                    CUDDNode negBothNode = CUDD.Function.Not(bothNode);
                    exclusiveNode = CUDD.Function.And(exclusiveNode, negBothNode);
                }
            }

            CUDDNode eachExclusiveAxiom = CUDD.Function.And(orNode, exclusiveNode);

            ExclusiveAxiom = CUDD.Function.And(ExclusiveAxiom, eachExclusiveAxiom);

            //Console.WriteLine("Enumerator's exclusive axiom:");
            //CUDD.Print.PrintMinterm(eachExclusiveAxiom);
            //Console.ReadLine();
        }

        #endregion
    }
}
