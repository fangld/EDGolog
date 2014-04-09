using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class UsedPredicateGenerator : PlanningBaseVisitor<Tuple<List<AbstractPredicate>, List<AbstractPredicate>>>
    {
        //public override Tuple<List<AbstractPredicate>, List<AbstractPredicate>> VisitActionDefBody(PlanningParser.ActionDefBodyContext context)
        //{
        //    List<AbstractPredicate> item1 = new List<AbstractPredicate>();
        //    List<AbstractPredicate> item2 = new List<AbstractPredicate>();
        //    Tuple<List<AbstractPredicate>, List<AbstractPredicate>> result = new Tuple<List<AbstractPredicate>, List<AbstractPredicate>>(item1, item2);

            
        //}
    }
}
