using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Clients
{
    public class ClientGroundAction : GroundAction<ClientAction, ClientAbstractPredicate, ClientGroundPredicate>
    {
        public override void From(ClientAction action, IEnumerable<string> constantList, Dictionary<string, ClientGroundPredicate> gndPredDict)
        {
            throw new NotImplementedException();
        }

        protected override int GetPreconditionCuddIndex(ClientAbstractPredicate abstractPred)
        {
            throw new NotImplementedException();
        }

        protected override int GetPreconditionCuddIndex(ClientGroundPredicate gndPred)
        {
            throw new NotImplementedException();
        }
    }
}
