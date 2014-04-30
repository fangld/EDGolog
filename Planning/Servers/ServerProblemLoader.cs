using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Servers
{
    public class ServerProblemLoader: ProblemLoader<ServerProblem, ServerDomain, ServerAction, ServerAbstractPredicate, ServerGroundPredicate, ServerGroundAction>
    {
        #region Constructors

        public ServerProblemLoader(ServerDomain domain) : base(domain)
        {
        }

        #endregion
    }
}
