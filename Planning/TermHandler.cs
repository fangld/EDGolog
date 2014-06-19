using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class TermHandler
    {
        #region Fields

        private const string Agent1Id = "a1";
        private const string Agent2Id = "a2";

        private Dictionary<string, PlanningType> _typeDict;

        private Dictionary<string, int> _numericConstValues;

        private Dictionary<string, string> _constTypeMap;

        private Dictionary<string, List<string>> _typeConstListMap;

        #endregion

        #region Properties

        public static Dictionary<string, string> EmtpyAssignment;

        #endregion

        #region Constructors

        public TermHandler(PlanningParser.NumericSettingContext numericSettingContext, PlanningParser.TypeDefineContext typeDefineContext,
            PlanningParser.ObjectDeclarationContext objDecContext)
        {
            BuildNumericConst(numericSettingContext);
            BuildTypeDict(typeDefineContext);
            BuildConstTypeMap(objDecContext);
        }

        static TermHandler()
        {
            EmtpyAssignment = new Dictionary<string, string>();
        }

        #endregion

        #region Methods

        private void BuildNumericConst(PlanningParser.NumericSettingContext context)
        {
            int count = context.INTEGER().Count;
            _numericConstValues = new Dictionary<string, int>(count);
            for (int i = 0; i < count; i++)
            {
                string constName = context.numericSymbol(i).GetText();
                int value = int.Parse(context.INTEGER(i).GetText());
                _numericConstValues.Add(constName, value);
            }
        }

        private void BuildTypeDict(PlanningParser.TypeDefineContext context)
        {
            _typeDict = new Dictionary<string, PlanningType>();

            if (context != null)
            {
                _typeDict.Add(PlanningType.ObjectType.Name, PlanningType.ObjectType);
                _typeDict.Add(PlanningType.AgentType.Name, PlanningType.AgentType);

                foreach (var typeContext in context.typeDeclaration())
                {
                    PlanningType type;
                    string name = typeContext.NAME().GetText();
                    if (typeContext.LB() == null)
                    {
                        type = new PlanningType { Name = name };
                    }
                    else
                    {
                        int min = GetValue(typeContext.constTerm(0));
                        int max = GetValue(typeContext.constTerm(1));
                        type = new PlanningNumericType { Name = name, Min = min, Max = max };
                    }

                    _typeDict.Add(name, type);
                }
            }
        }

        private void BuildConstTypeMap(PlanningParser.ObjectDeclarationContext context)
        {
            _constTypeMap = new Dictionary<string, string>();
            _typeConstListMap = new Dictionary<string, List<string>>();

            _constTypeMap.Add(Agent1Id, PlanningType.AgentType.Name);
            _constTypeMap.Add(Agent2Id, PlanningType.AgentType.Name);
            _typeConstListMap.Add(PlanningType.AgentType.Name, new List<string> { Agent1Id, Agent2Id });
            
            foreach (var pair in _typeDict)
            {
                if (pair.Value is PlanningNumericType)
                {
                    PlanningNumericType type = pair.Value as PlanningNumericType;
                    List<string> constantList = new List<string>(type.Max - type.Min + 1);
                    for (int i = type.Min; i <= type.Max; i++)
                    {
                        constantList.Add(i.ToString());
                    }
                    _typeConstListMap.Add(type.Name, constantList);
                }
            }

            if (context != null)
            {
                var listNameContext = context.listName();
                do
                {
                    string type = listNameContext.type() != null
                        ? listNameContext.type().GetText()
                        : PlanningType.ObjectType.Name;

                    List<string> constantList;

                    if (_typeConstListMap.ContainsKey(type))
                    {
                        constantList = _typeConstListMap[type];
                    }
                    else
                    {
                        constantList = new List<string>(listNameContext.NAME().Count);
                        _typeConstListMap.Add(type, constantList);
                    }

                    foreach (var nameNode in listNameContext.NAME())
                    {
                        _constTypeMap.Add(nameNode.GetText(), type);
                        constantList.Add(nameNode.GetText());
                    }

                    listNameContext = listNameContext.listName();
                } while (listNameContext != null);
            }
        }

        public int GetValue(PlanningParser.ConstTermContext context)
        {
            int result;
            if (context.NAME() != null)
            {
                string constName = context.NAME().GetText();
                result = _numericConstValues[constName];
            }

            else if (context.INTEGER() != null)
            {
                result = int.Parse(context.INTEGER().GetText());
            }

            else if (context.MINUS() != null && context.constTerm().Count == 1)
            {
                result = - GetValue(context.constTerm(0));
            }

            else if (context.MINUS() != null && context.constTerm().Count == 2)
            {
                int firstValue = GetValue(context.constTerm(0));
                int secondValue = GetValue(context.constTerm(1));
                result = firstValue - secondValue;
            }
            else
            {
                int firstValue = GetValue(context.constTerm(0));
                int secondValue = GetValue(context.constTerm(1));
                result = firstValue + secondValue;
            }

            return result;
        }

        public string GetString(PlanningParser.TermContext context, Dictionary<string, string> assignment)
        {
            string result;
            if (context.NAME() != null)
            {
                string name = context.NAME().GetText();
                if (_constTypeMap.ContainsKey(name))
                {
                    result = name;
                }
                else
                {
                    result = _numericConstValues[name].ToString();
                }
            }
            else if (context.VAR() != null)
            {
                string variableName = context.VAR().GetText();
                //Console.WriteLine("TermHandler: variableName: {0}", variableName);
                //foreach (var pair in assignment)
                //{
                //    Console.WriteLine("  Var name{0}, value:{1}", pair.Key, pair.Value);
                //}
                result = assignment[variableName];
            }
            else if (context.INTEGER() != null)
            {
                result = context.INTEGER().GetText();
            }
            else if (context.MINUS() != null && context.term().Count == 1)
            {
                string termString = GetString(context.term(0), assignment);
                int value = int.Parse(termString);
                result = (-value).ToString();
            }
            else if (context.MINUS() != null && context.term().Count == 2)
            {
                string firstTermString = GetString(context.term(0), assignment);
                string secondTermString = GetString(context.term(1), assignment);

                int firstValue = int.Parse(firstTermString);
                int secondValue = int.Parse(secondTermString);

                result = (firstValue - secondValue).ToString();
            }
            else
            {
                string firstTermString = GetString(context.term(0), assignment);
                string secondTermString = GetString(context.term(1), assignment);

                int firstValue = int.Parse(firstTermString);
                int secondValue = int.Parse(secondTermString);

                result = (firstValue + secondValue).ToString();
            }

            return result;
        }

        public List<string> GetConstList(string type)
        {
            Console.WriteLine("Type: {0}", type);
            return _typeConstListMap[type];
        }

        public void ShowInfo()
        {
            Console.WriteLine("Types:");
            foreach (var type in _typeDict)
            {
                Console.WriteLine("  {0}", type.Value);
            }
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Numeric constants:");
            foreach (var pair in _numericConstValues)
            {
                Console.WriteLine("  Name: {0}, Value: {1}", pair.Key, pair.Value);
            }
            Console.WriteLine(Domain.BarLine);

            Console.WriteLine("Object constants:");
            foreach (var pair in _constTypeMap)
            {
                Console.WriteLine("  Name: {0}, Type: {1}", pair.Key, pair.Value);
            }
            Console.WriteLine(Domain.BarLine);
        }

        #endregion
    }
}
