//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Planning
//{
//    public class Ground<T> where T : VariableContainer
//    {
//        #region Fields

//        private List<string> _constantList;

//        #endregion

//        #region Properties

//        public T Container { get; set; }

//        public IReadOnlyList<string> ConstantList
//        {
//            get { return _constantList; }
//        }

//        #endregion

//        #region Methods

//        protected void SetConstantList(IEnumerable<string> constantList)
//        {
//            _constantList = new List<string>(constantList);
//        }

//        public override string ToString()
//        {
//            return VariableContainer.GetFullName(Container.Name, _constantList);
//        }

//        #endregion
//    }
//}
