using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class GroundAction
    {
        #region Fields

        private List<string> _listParam;

        #endregion

        #region Properties

        public Action Action { get; set; }

        public IReadOnlyList<string> ListParam
        {
            get { return _listParam; }
        }

        #endregion

        #region Constructors

        public GroundAction(Action action, List<string> listParam)
        {
            Action = action;
            _listParam = listParam;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}(", Action.Name);

            for (int i = 0; i < _listParam.Count - 1; i++)
            {
                sb.AppendFormat("{0},", _listParam[i]);
            }

            sb.AppendFormat("{0})", _listParam[_listParam.Count - 1]);
            return sb.ToString();
        }

        #endregion
    }
}
