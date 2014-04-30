using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Clients
{
    public class ClientGroundAction : GroundAction<ClientAction>
    {
        //public override void From(ClientAction action, IEnumerable<string> constantList, Dictionary<string, ClientGroundPredicate> gndPredDict)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override int GetPreconditionCuddIndex(ClientGroundPredicate gndPred)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override int GetPreconditionCuddIndex(AbstractPredicate abstractPred)
        //{
        //    throw new NotImplementedException();
        //}

        public override void From(ClientAction action, IEnumerable<string> constantList, Dictionary<string, GroundPredicate> gndPredDict)
        {
            throw new NotImplementedException();
        }
    }
}
