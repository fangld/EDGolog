//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LanguageRecognition;
//using PAT.Common.Classes.CUDDLib;

//namespace Planning
//{
//    public class Event : VariableContainer 
//    {
//        #region Fields

//        protected Dictionary<string, AbstractPredicate> _abstractPredDict;

//        private List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> _effect;

//        #endregion

//        #region Properties

//        public int PlausibilityDegree { get; set; }

//        public CUDDNode Precondition { get; set; }

//        public IReadOnlyList<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> Effect
//        {
//            get { return _effect; }
//        }

//        #endregion

//        #region Constructors

//        private Event()
//        {
//            _effect = new List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>>();
//        }

//        #endregion

//        #region Methods

//        public static Event From()
//        {
//            Event result = new Event();
//            return result;
//        }

//        #region Methods for generating precondition

//        protected void GeneratePrecondition(PlanningParser.EventDefineContext context, IReadOnlyDictionary<string, Predicate> predDict)
//        {
//            Precondition = CUDD.ONE;

//            if (context.PRE() != null)
//            {
//                if (context.emptyOrPreGD().gd() != null)
//                {
//                    Precondition = GetCuddNode(context.emptyOrPreGD().gd());
//                }
//            }
//        }

//        protected AbstractPredicate GetAbstractPredicate(PlanningParser.TermAtomFormContext context)
//        {
//            string abstractPredName = VariableContainer.GetFullName(context);
//            AbstractPredicate result = _abstractPredDict[abstractPredName];
//            return result;
//        }

//        #endregion

//        #endregion
//    }
//}
