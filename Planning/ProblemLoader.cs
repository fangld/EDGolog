using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class ProblemLoader : PlanningBaseListener
    {
        #region Properties
        public string Name { get; set; }

        public string DomainName { get; set; }

        public DomainLoader DomainLoader { get; set; }

        public string InitState { get; set; }
        
        #endregion

        #region Constructors
        public ProblemLoader()
        {
        }

        public ProblemLoader(DomainLoader domainLoader)
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

        public override void EnterInit(PlanningParser.InitContext context)
        {
            InitState = context.gdName().GetText();
        }

        public void ShowInfo()
        {
            const string barline = "----------------";

            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine(barline);

            Console.WriteLine("Domain name: {0}", DomainName);
            Console.WriteLine(barline);

            Console.WriteLine("Initial state: {0}", InitState);
            Console.WriteLine(barline);
        }

        #endregion
    }
}
