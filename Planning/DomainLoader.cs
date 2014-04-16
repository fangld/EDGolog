using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class DomainLoader : PlanningBaseListener
    {
        #region Fields

        private List<string> _typeList;

        private Dictionary<string, Predicate> _predDict;
        
        private Dictionary<string, Action> _actionDict;

        #endregion

        #region Properties

        public string Name { get; set; }

        public int CurrentCuddIndex { get; set; }

        //public Requirements Requirements { get; set; }

        public IReadOnlyList<string> TypeList
        {
            get { return _typeList; }
        }

        public IReadOnlyDictionary<string, Predicate> PredicateDict
        {
            get { return _predDict; }
        }

        public IReadOnlyDictionary<string, Action> ActionDict
        {
            get { return _actionDict; }
        }

        #endregion

        #region Constructors
        public DomainLoader()
        {
            _typeList = new List<string> {VariableContainer.DefaultType};
            _predDict = new Dictionary<string, Predicate>();
            _actionDict = new Dictionary<string, Action>();
            CurrentCuddIndex = 0;
        }

        #endregion

        #region Overriden Methods

        public override void EnterDomain(PlanningParser.DomainContext context)
        {
            Name = context.NAME().GetText();
            Console.WriteLine("Name: {0}", Name);
        }

        //public override void EnterRequireDefine(PlanningParser.RequireDefineContext context)
        //{
        //    Requirements.Strips = false;

        //    foreach (var requirment in context.requireKey())
        //    {
        //        if (requirment.GetText() == ":typing")
        //        {
        //            Requirements.Typing = true;
        //        }

        //        else if (requirment.GetText() == ":strips")
        //        {
        //            Requirements.Strips = true;
        //        }
        //    }
        //}

        public override void EnterTypeDefine(PlanningParser.TypeDefineContext context)
        {
            foreach (var type in context.listName().NAME())
            {
                _typeList.Add(type.GetText());
                Console.WriteLine("type: {0}", type);
            }
        }

        public override void EnterPredicatesDefine(PlanningParser.PredicatesDefineContext context)
        {
            foreach (var atomicFormulaSkeleton in context.atomicFormulaSkeleton())
            {
                Predicate pred = Predicate.FromContext(atomicFormulaSkeleton);
                _predDict.Add(pred.Name, pred);
            }
        }

        public override void EnterActionDefine(PlanningParser.ActionDefineContext context)
        {
            Action action = Action.FromContext(CurrentCuddIndex, context, _predDict);
            _actionDict.Add(action.Name, action);
            CurrentCuddIndex = action.CurrentCuddIndex;
        }

        #endregion

        #region Methods

        public void ShowInfo()
        {
            const string barline = "----------------";

            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine(barline);

            //Console.WriteLine("Requirment:");
            //Console.WriteLine("  strips: {0}", Requirements.Strips);
            //Console.WriteLine("  typing: {0}", Requirements.Typing);
            Console.WriteLine(barline);

            Console.Write("Types: ");
            for (int i = 0; i < _typeList.Count - 1; i++)
            {
                Console.Write("{0}, ", _typeList[i]);
            }
            Console.WriteLine("{0}", _typeList[_typeList.Count - 1]);
            Console.WriteLine(barline);

            Console.WriteLine("Predicates:");
            foreach (var pred in _predDict.Values)
            {
                Console.WriteLine("  Name: {0}", pred.Name);
                Console.WriteLine("  Variable: {0}", pred.Count);
                for (int i = 0; i < pred.Count; i++)
                {
                    Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, pred.VariableList[i].Item1,
                        pred.VariableList[i].Item2);
                }
                Console.WriteLine();
            }
            Console.WriteLine(barline);

            Console.WriteLine("Actions:");
            foreach (var action in _actionDict.Values)
            {
                Console.WriteLine("  Name: {0}", action.Name);
                Console.WriteLine("  Variable: {0}", action.Count);
                for (int i = 0; i < action.Count; i++)
                {
                    Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, action.VariableList[i].Item1,
                        action.VariableList[i].Item2);
                }

                Console.WriteLine("    Previous Abstract Predicates: ");
                foreach (var abstractPredicate in action.PreviousAbstractPredicates)
                {
                    Console.WriteLine("      Name: {0}, CuddIndex: {1}", abstractPredicate, abstractPredicate.CuddIndex);
                }

                Console.WriteLine("    Successive Abstract Predicates: ");
                foreach (var abstractPredicate in action.SuccessiveAbstractPredicates)
                {
                    Console.WriteLine("      Name: {0}, CuddIndex: {1}", abstractPredicate, abstractPredicate.CuddIndex);
                }
                Console.WriteLine("  Precondition:");
                CUDD.Print.PrintMinterm(action.Precondition);

                Console.WriteLine("  Effect:");
                CUDD.Print.PrintMinterm(action.Effect);

                Console.WriteLine();
            }
        }

        #endregion
    }
}
