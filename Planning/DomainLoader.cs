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

        private Dictionary<string, Predicate> _predDict;
        
        private Dictionary<string, Action> _actionDict;

        private int _currentCuddIndex;

        #endregion

        #region Properties

        public const string DefaultType = "object";

        public string Name { get; set; }

        public Requirements Requirements { get; set; }

        public List<string> ListType { get; set; }

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
            Requirements = new Requirements();
            ListType = new List<string>();
            ListType.Add(DefaultType);
            _predDict = new Dictionary<string, Predicate>();
            _actionDict = new Dictionary<string, Action>();
            _currentCuddIndex = 0;
        }

        #endregion

        #region Overriden Methods

        public override void EnterDomain(PlanningParser.DomainContext context)
        {
            Name = context.NAME().GetText();
            Console.WriteLine("Name: {0}", Name);
        }

        public override void EnterRequireDefine(PlanningParser.RequireDefineContext context)
        {
            Requirements.Strips = false;

            foreach (var requirment in context.requireKey())
            {
                if (requirment.GetText() == ":typing")
                {
                    Requirements.Typing = true;
                }

                else if (requirment.GetText() == ":strips")
                {
                    Requirements.Strips = true;
                }
            }
        }

        public override void EnterTypeDefine(PlanningParser.TypeDefineContext context)
        {
            foreach (var type in context.listName().NAME())
            {
                ListType.Add(type.GetText());
                Console.WriteLine("type: {0}", type);
            }
        }

        public override void EnterPredicatesDefine(PlanningParser.PredicatesDefineContext context)
        {
            foreach (var atomicFormulaSkeleton in context.atomicFormulaSkeleton())
            {
                Predicate pred = new Predicate();
                pred.Name = atomicFormulaSkeleton.predicate().GetText();
                AddVariablesToContainer(pred, atomicFormulaSkeleton.listVariable());
                _predDict.Add(pred.Name, pred);
            }
        }

        public override void EnterActionDefine(PlanningParser.ActionDefineContext context)
        {
            Action action = new Action(_currentCuddIndex, context);
            _actionDict.Add(action.Name, action);
            _currentCuddIndex = action.CurrentCuddIndex;
        }
        
        private void AddVariablesToContainer(Predicate container, PlanningParser.ListVariableContext context)
        {
            do
            {
                if (context.VAR().Count != 0)
                {
                    string type = context.type() == null ? DefaultType : context.type().GetText();

                    foreach (var varNode in context.VAR())
                    {
                        container.AddVariable(varNode.GetText(), type);
                    }
                }
                context = context.listVariable();
            } while (context != null);
        }

        #endregion

        #region Methods

        public void ShowInfo()
        {
            const string barline = "----------------";

            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine(barline);

            Console.WriteLine("Requirment:");
            Console.WriteLine("  strips: {0}", Requirements.Strips);
            Console.WriteLine("  typing: {0}", Requirements.Typing);
            Console.WriteLine(barline);

            Console.Write("Types: ");
            for (int i = 0; i < ListType.Count - 1; i++)
            {
                Console.Write("{0}, ", ListType[i]);
            }
            Console.WriteLine("{0}", ListType[ListType.Count - 1]);
            Console.WriteLine(barline);

            Console.WriteLine("Predicates:");
            foreach (var pred in _predDict.Values)
            {
                Console.WriteLine("  Name: {0}", pred.Name);
                Console.WriteLine("  Variable: {0}", pred.Count);
                for (int i = 0; i < pred.Count; i++)
                {
                    Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, pred.VariableTypeList[i].Item1,
                        pred.VariableTypeList[i].Item2);
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
                    Console.WriteLine("    Index: {0}, Name: {1}, Type: {2}", i, action.VariableTypeList[i].Item1,
                        action.VariableTypeList[i].Item2);
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
