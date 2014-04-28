using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Services
{
    public class ServerAbstractPredicate : AbstractPredicate
    {
        #region Constructors

        public ServerAbstractPredicate(List<string> parameterList): base(parameterList)
        {
        }

        #endregion
    }
}
