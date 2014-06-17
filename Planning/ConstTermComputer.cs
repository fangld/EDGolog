//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LanguageRecognition;

//namespace Planning
//{
//    public class ConstTermComputer
//    {
//        #region Fields

//        private Dictionary<string, int> _numericConstValues;

//        #endregion

//        #region Constructors

//        public ConstTermComputer(PlanningParser.ConstSettingContext context)
//        {
//            BuildConstValues(context);
//        }

//        #endregion

//        #region Methods

//        private void BuildConstValues(PlanningParser.ConstSettingContext context)
//        {
//            int count = context.INTEGER().Count;
//            _numericConstValues = new Dictionary<string, int>(count);
//            for (int i = 0; i < count; i++)
//            {
//                string constName = context.constSymbol(i).GetText();
//                int value = int.Parse(context.INTEGER(i).GetText());
//                _numericConstValues.Add(constName, value);
//            }
//        }

//        public int GetValue(PlanningParser.ConstTermContext context)
//        {
//            int result;
//            if (context.NAME() != null)
//            {
//                string constName = context.NAME().GetText();
//                result = _numericConstValues[constName];
//            }

//            else if (context.INTEGER() != null)
//            {
//                result = int.Parse(context.INTEGER().GetText());
//            }

//            else if (context.MINUS() != null && context.constTerm().Count == 1)
//            {
//                result = - GetValue(context.constTerm(0));
//            }

//            else if (context.MINUS() != null && context.constTerm().Count == 2)
//            {
//                int firstValue = GetValue(context.constTerm(0));
//                int secondValue = GetValue(context.constTerm(1));
//                result = firstValue - secondValue;
//            }
//            else
//            {
//                int firstValue = GetValue(context.constTerm(0));
//                int secondValue = GetValue(context.constTerm(1));
//                result = firstValue + secondValue;
//            }

//            return result;
//        }

//        public void ShowInfo()
//        {
//            Console.WriteLine("Constants:");
//            foreach (var pair in _numericConstValues)
//            {
//                Console.WriteLine("  Name: {0}, Value: {1}", pair.Key, pair.Value);
//            }

//        }

//        #endregion
//    }
//}
