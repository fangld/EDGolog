using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Domain
    {
        #region Fields

        private List<string> _typeList;

        private Dictionary<string, Predicate> _predDict;

        private Dictionary<string, Action> _actionDict;

        #endregion

        #region Properties

        public string Name { get; set; }

        public int CurrentCuddIndex { get; set; }

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

        public Domain()
        {
            _typeList = new List<string> { VariableContainer.DefaultType };
            _predDict = new Dictionary<string, Predicate>();
            _actionDict = new Dictionary<string, Action>();
            CurrentCuddIndex = 0;
        }

        #endregion

        #region Methods

        public void AddToTypeList(string type)
        {
            _typeList.Add(type);
        }

        public void AddToPredicateDict(Predicate predicate)
        {
            _predDict.Add(predicate.Name, predicate);
        }

        public void AddToActionDict(Action action)
        {
            _actionDict.Add(action.Name, action);
            CurrentCuddIndex = action.CurrentCuddIndex;
        }

        public void ShowInfo()
        {
            const string barline = "----------------";

            Console.WriteLine("Name: {0}", Name);
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

                Console.WriteLine("    Abstract Predicates: ");
                foreach (var pair in action.AbstractPredicateDict)
                {
                    Console.WriteLine("      Name: {0}, CuddIndex: {1}", pair.Key, pair.Value.CuddIndex);
                }
                Console.WriteLine("  Precondition:");
                CUDD.Print.PrintMinterm(action.Precondition);

                Console.WriteLine("  Effect:");
                for (int i = 0; i < action.Effect.Count; i++)
                {
                    Console.WriteLine("      Index:{0}", i);
                    Console.WriteLine("      Condition:");
                    CUDD.Print.PrintMinterm(action.Effect[i].Item1);

                    Console.Write("      Literals: { ");
                    var literal = action.Effect[i].Item2[0];
                    if (literal.Item2)
                    {
                        Console.Write("{0}", literal.Item1);
                    }
                    else
                    {
                        Console.Write("not {0}", literal.Item1);
                    }

                    for (int j = 1; j < action.Effect[i].Item2.Count; j++)
                    {
                        if (literal.Item2)
                        {
                            Console.Write(", {0}", literal.Item1);
                        }
                        else
                        {
                            Console.Write(", not {0}", literal.Item1);
                        }
                    }

                    Console.WriteLine(" }");
                }

                Console.WriteLine();
            }
        }

        #endregion
    }
}
