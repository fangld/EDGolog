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

        private Dictionary<string, Predicate> _predicateDict;
        
        private Dictionary<string, Action> _actionDict;

        #endregion

        #region Properties

        public const string DefaultType = "object";

        public string Name { get; set; }

        public Requirements Requirements { get; set; }

        public List<string> ListType { get; set; }

        public IReadOnlyDictionary<string, Predicate> PredicateDict
        {
            get { return _predicateDict; }
        }

        public IReadOnlyDictionary<string, Action> ActionDict
        {
            get { return _actionDict; }
        }

        //public List<Predicate> PredicateDefinitions { get; set; }

        //public List<Action> ActionDefinitions { get; set; }


        #endregion

        #region Constructors
        public DomainLoader()
        {
            Requirements = new Requirements();
            ListType = new List<string>();
            ListType.Add(DefaultType);
            _predicateDict = new Dictionary<string, Predicate>();
            _actionDict = new Dictionary<string, Action>();
            //PredicateDefinitions = new List<Predicate>();
            //AtionDefinitions = new List<Action>();
        }

        #endregion

        #region Overriden Methods

        public override void EnterDomain(PlanningParser.DomainContext context)
        {
            Name = context.NAME().GetText();
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
            }
        }

        public override void EnterPredicatesDefine(PlanningParser.PredicatesDefineContext context)
        {
            foreach (var atomicFormulaSkeleton in context.atomicFormulaSkeleton())
            {
                Predicate pred = new Predicate();
                pred.Name = atomicFormulaSkeleton.predicate().GetText();
                AddVariablesToContainer(pred, atomicFormulaSkeleton.listVariable());
                _predicateDict.Add(pred.Name, pred);
            }
        }

        public override void EnterActionDefine(PlanningParser.ActionDefineContext context)
        {
            Action action = new Action();
            action.Name = context.actionSymbol().GetText();
            AddVariablesToContainer(action, context.listVariable());

            FormulaVistor vistor = new FormulaVistor();

            action.Precondition = CUDD.ONE;
            if (context.actionDefBody().emptyOrPreGD() != null)
            {
                if (context.actionDefBody().emptyOrPreGD().preGD() != null)
                {
                    action.Precondition = vistor.Visit(context.actionDefBody().emptyOrPreGD().preGD());
                }
            }

            action.Effect = "true";
            if (context.actionDefBody().emptyOrEffect() != null)
            {
                if (context.actionDefBody().emptyOrEffect().effect() != null)
                {
                    action.Effect = context.actionDefBody().emptyOrEffect().effect().GetText();
                }
            }

            _actionDict.Add(action.Name, action);
        }
        
        private void AddVariablesToContainer(Predicate container, PlanningParser.ListVariableContext context)
        {
            do
            {
                if (context.VAR().Count != 0)
                {
                    string type = context.type() == null ? DefaultType : context.type().GetText();
                    container.AddVariableTypes(type, context.VAR().Count);
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
            foreach (var pred in _predicateDict.Values)
            {
                Console.WriteLine("  Name: {0}", pred.Name);
                Console.WriteLine("  Variable: {0}", pred.Count);
                for (int i = 0; i < pred.Count; i++)
                {
                    Console.WriteLine("    Index: {0}, Type: {1}", i, pred.ListVariablesType[i]);
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
                    Console.WriteLine("    Index: {0}, Type: {1}", i, action.ListVariablesType[i]);
                }
                Console.WriteLine("  Precondition: {0}", action.Precondition);
                Console.WriteLine("  Effect: {0}", action.Effect);
                Console.WriteLine();
            }
        }

        #endregion
    }
}
