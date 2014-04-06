using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class ProblemLoader : PlanningBaseListener
    {
        #region Fields

        private Dictionary<string, string> _objectNameTypeMap;

        //private List<string> TruePredSet;

        #endregion

        #region Properties

        public string Name { get; set; }

        public string DomainName { get; set; }

        public DomainLoader DomainLoader { get; set; }

        //public string InitState { get; set; }

        public HashSet<string> TruePredSet { get; set; }


        public IReadOnlyDictionary<string, string> ObjectNameTypeMapMap
        {
            get { return _objectNameTypeMap; }
        }

        #endregion

        #region Constructors

        public ProblemLoader()
        {
            _objectNameTypeMap = new Dictionary<string, string>();
            TruePredSet = new HashSet<string>();
        }

        public ProblemLoader(DomainLoader domainLoader):this()
        {
            DomainLoader = domainLoader;
        }

        #endregion

        #region Overriden Methods

        public override void EnterProblem(PlanningParser.ProblemContext context)
        {
            Name = context.problemName().GetText();
            DomainName = context.domainName().GetText();
        }

        public override void EnterObjectDeclaration(PlanningParser.ObjectDeclarationContext context)
        {
            var listNameContext = context.listName();
            do
            {
                string type = listNameContext.type() != null
                    ? listNameContext.type().GetText()
                    : DomainLoader.DefaultType;
                foreach (var nameNode in listNameContext.NAME())
                {
                    _objectNameTypeMap.Add(nameNode.GetText(), type);
                }
                listNameContext = listNameContext.listName();
            } while (listNameContext != null);
        }

        public override void EnterInit(PlanningParser.InitContext context)
        {
            foreach (var gdNameContext in context.gdName().gdName())
            {
                var afnContext = gdNameContext.atomicFormulaName();
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}(", afnContext.predicate().GetText());
                var nameNodes = afnContext.NAME();
                for (int i = 0; i < nameNodes.Count - 1; i++)
                {
                    sb.AppendFormat("{0},", nameNodes[i].GetText());
                }
                sb.AppendFormat("{0})", nameNodes[nameNodes.Count - 1].GetText());
                TruePredSet.Add(sb.ToString());
            }
            
            //InitState = context.gdName().GetText();
        }

        public void ShowInfo()
        {
            const string barline = "----------------";

            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine(barline);

            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(barline);

            Console.WriteLine("Variables:");
            foreach (var pair in _objectNameTypeMap)
            {
                Console.WriteLine("  {0} - {1}", pair.Key, pair.Value);
            }
            Console.WriteLine(barline);

            Console.WriteLine("Initial state");
            foreach (var pred in TruePredSet)
            {
                Console.WriteLine(" {0}", pred);
            }

            Console.WriteLine(barline);
        }

        #endregion
    }
}
