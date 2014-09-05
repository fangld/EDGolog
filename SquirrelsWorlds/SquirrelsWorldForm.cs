using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4.Runtime;
using LanguageRecognition;
using ObjectWorlds.Network;
using PAT.Common.Classes.CUDDLib;
using Planning;
using Planning.Servers;
using SquirrelsWorlds.Properties;
using Action = Planning.Action;

namespace SquirrelsWorlds
{
    public partial class SquirrelsWorldForm : Form
    {
        #region Fields

        private const int LocationX = 20;

        private const int LocationY = 50;

        private const int LineWidth = 1;

        private const int AcornMargin = 5;

        private const int SquirrelWidth = 50;

        private const int SquirrelHeight = 50;

        private const int GridWidth = SquirrelWidth*2;

        private const int GridHeight = SquirrelHeight * 2;

        private const int AcornWidth = 25;

        private const int AcornHeight = 25;

        private int _gridCount;

        private int _port;

        private int _listenBackLog;

        private string _serverProblemFileName;

        private string _domainFileName;

        private ServerProblem _problem;

        private Server _server;

        private Brush _acornBrush;

        private Pen _linePen;

        private Pen _acornPen;

        private Image _edgyRightImage;
        private Image _edgyLeftImage;
        private Image _edgeInitialImage;
        private Image _edgyLastImage;

        private Image _wallyRightImage;
        private Image _wallyLeftImage;
        private Image _wallyInitialImage;
        private Image _wallyLastImage;

        #endregion

        public SquirrelsWorldForm()
        {
            _port = Settings.Default.Port;
            _domainFileName = Settings.Default.DomainFileName;
            _serverProblemFileName = Settings.Default.ServerProblemFileName;
            
            Initial();

            _acornBrush = new SolidBrush(Color.Goldenrod);
            _acornPen = new Pen(Color.Goldenrod, 2);
            _linePen = new Pen(Color.Black, LineWidth);
            _edgyLeftImage = Resources.EdgyLeft;
            _edgyRightImage = Resources.EdgyRight;
            _edgeInitialImage = Resources.EdgyUp;

            _wallyLeftImage = Resources.WallyLeft;
            _wallyRightImage = Resources.WallyRight;
            _wallyInitialImage = Resources.WallyUp;

            InitializeComponent();
        }

        //void _server_ObjectBaseChanged(object sender, Tuple<IReadOnlyDictionary<string, bool>, Event> e)
        //{
        //    Graphics graphics = CreateGraphics();
        //    DrawGrids(graphics);
        //    DrawAcorns(graphics, _server.ObjectBase.PredBooleanMap);
        //    DrawMovingSquirrels(graphics, e.Item1, e.Item2);
        //    graphics.Dispose();
        //    var predBoolMap = _server.ObjectBase.PredBooleanMap;

        //    listviewPredBoolMap.Items.Clear();
        //    ListViewItem predicateItem = new ListViewItem();
        //    ListViewItem valueItem = new ListViewItem();

        //    foreach (var pair in predBoolMap)
        //    {
        //        predicateItem.SubItems.Add(pair.Key);
        //        valueItem.SubItems.Add(pair.Value.ToString());
        //    }

        //    listviewPredBoolMap.Items.AddRange(new[] { predicateItem, valueItem });

        //}

        private void DrawGrids(Graphics graphics)
        {
            graphics.Clear(Color.White);

            graphics.DrawLine(_linePen, LocationX, LocationY, LocationX + (GridWidth + LineWidth) * _gridCount,
                LocationY);
            graphics.DrawLine(_linePen, LocationX, LocationY + GridHeight + LineWidth,
                LocationX + (GridWidth + LineWidth)*_gridCount, LocationY + GridHeight + LineWidth);
            for (int i = 0; i <= _gridCount; i++)
            {
                graphics.DrawLine(_linePen, LocationX + (GridWidth + LineWidth)*i, LocationY,
                    LocationX + (GridWidth + LineWidth)*i, LocationY + GridHeight + LineWidth);
            }
        }

        private void DrawAcorns(Graphics graphics, IReadOnlyDictionary<string, bool> predBooleanMap)
        {
            for (int i = 0; i < _gridCount; i++)
            {
                string predicatePrefix = string.Format("acorn({0}", i);
                foreach (var pair in predBooleanMap)
                {
                    if (pair.Key.Contains(predicatePrefix) && pair.Value)
                    {
                        Predicate predicate = _problem.PredicateDict[pair.Key];
                        string acornCountString = predicate.ConstList[1];
                        int acornCount = int.Parse(acornCountString);
                        for (int j = 0; j < acornCount; j++)
                        {
                            graphics.DrawEllipse(_acornPen,
                                LocationX + (GridWidth + LineWidth)*i + (AcornMargin + AcornWidth)*j + LineWidth +
                                AcornMargin, LocationY + LineWidth + AcornMargin, AcornWidth, AcornHeight);
                            graphics.FillEllipse(_acornBrush,
                                LocationX + (GridWidth + LineWidth)*i + (AcornMargin + AcornWidth)*j + LineWidth +
                                AcornMargin, LocationY + LineWidth + AcornMargin, AcornWidth, AcornHeight);
                        }
                    }
                }
            }
        }

        private void DrawInitialSquirrels(Graphics graphics, IReadOnlyDictionary<string, bool> predBooleanMap)
        {
            _edgyLastImage = _edgeInitialImage;
            _wallyLastImage = _wallyInitialImage;
            DrawSquirrels(graphics, _edgeInitialImage, _wallyInitialImage, predBooleanMap);
        }

        private void DrawMovingSquirrels(Graphics graphics, IReadOnlyDictionary<string, bool> predBooleanMap, Action action)
        {
            Image edgyImage = _edgyLastImage;
            Image wallyImage = _wallyLastImage;
            string actionFullName = action.FullName;
            if (actionFullName == "Left(a1)")
            {
                edgyImage = _edgyLeftImage;
                _edgyLastImage = edgyImage;
            }
            else if (actionFullName == "Right(a1)")
            {
                edgyImage = _edgyRightImage;
                _edgyLastImage = edgyImage;
            }
            else if (actionFullName == "Left(a2)")
            {
                wallyImage = _wallyLeftImage;
                _wallyLastImage = wallyImage;
            }
            else if (actionFullName == "Right(a2)")
            {
                wallyImage = _wallyRightImage;
                _wallyLastImage = wallyImage;
            }

            DrawSquirrels(graphics, edgyImage, wallyImage, predBooleanMap);
        }

        private void DrawSquirrels(Graphics graphics, Image edgyImage, Image wallyImage, IReadOnlyDictionary<string, bool> predBooleanMap)
        {
            foreach (var pair in predBooleanMap)
            {
                if (pair.Key.Contains("loc(a1") && pair.Value)
                {
                    Predicate predicate = _problem.PredicateDict[pair.Key];
                    string locString = predicate.ConstList[1];
                    int location = int.Parse(locString);
                    graphics.DrawImage(edgyImage,
                        LocationX + (GridWidth + LineWidth)*location + LineWidth,
                        LocationY + LineWidth + SquirrelHeight, SquirrelWidth, SquirrelHeight);
                }
                else if (pair.Key.Contains("loc(a2") && pair.Value)
                {
                    Predicate predicate = _problem.PredicateDict[pair.Key];
                    string locString = predicate.ConstList[1];
                    int location = int.Parse(locString);
                    graphics.DrawImage(wallyImage,
                        LocationX + (GridWidth + LineWidth)*location + LineWidth + SquirrelWidth,
                        LocationY + LineWidth + SquirrelHeight, SquirrelWidth, SquirrelHeight);
                }
            }
        }
        
        private void Initial()
        {
            CUDD.InitialiseCUDD(3072, 256, 262144, 0.1);

            // Create a TextReader that reads from a file
            TextReader tr = new StreamReader(_domainFileName);

            // create a CharStream that reads from standard input
            AntlrInputStream input = new AntlrInputStream(tr);
            // create a lexer that feeds off of input CharStream

            PlanningLexer lexer = new PlanningLexer(input);
            // create a buffer of tokens pulled from the lexer
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            PlanningParser parser = new PlanningParser(tokens);

            var domainContext = parser.domain(); // begin parsing at init rule
            //Console.WriteLine(domainContext.ToStringTree(parser));
            tr.Close();
            //var domain = Domain.CreateInstance(domainContext);
            //ShowDomainInfo(domain);

            // Create a TextReader that reads from a file
            tr = new StreamReader(_serverProblemFileName);

            // create a CharStream that reads from standard input
            input = new AntlrInputStream(tr);
            // create a lexer that feeds off of input CharStream

            lexer = new PlanningLexer(input);
            // create a buffer of tokens pulled from the lexer
            tokens = new CommonTokenStream(lexer);
            // create a parser that feeds off the tokens buffer
            parser = new PlanningParser(tokens);

            var serverProblemContext = parser.serverProblem(); // begin parsing at init rule
            tr.Close();
            _problem = ServerProblem.CreateInstance(domainContext, serverProblemContext);
            _gridCount = Globals.TermInterpreter.NumericConstValueDict["maxLoc"] + 1;
            //            problem.ShowInfo();

            _server = new Server(_port, _listenBackLog, _problem);
            
            _server.ObjectBaseChanged += _server_ObjectBaseChanged;

        }

        void _server_ObjectBaseChanged(object sender, Tuple<IReadOnlyDictionary<string, bool>, string, Planning.Action, Response, Observation, Event> e)
        {
            Graphics graphics = CreateGraphics();
            DrawGrids(graphics);
            DrawAcorns(graphics, _server.ObjectBase.PredBooleanMap);
            DrawMovingSquirrels(graphics, e.Item1, e.Item3);
            graphics.Dispose();

            var predBoolMap = _server.ObjectBase.PredBooleanMap;
            foreach (var pair in predBoolMap)
            {
                ListViewItem[] searchItems = listviewPredBoolMap.Items.Find(pair.Key, true);
                Console.WriteLine("Count of listview items: {0}", searchItems.Length);
                Console.WriteLine(pair.Key);
                string newValueString = pair.Value.ToString();
                searchItems[0].ForeColor = searchItems[0].SubItems[1].Text == newValueString ? Color.Black : Color.Red;
                searchItems[0].SubItems[1].Text = newValueString;
            }

            rtbEventList.AppendText(e.Item6.FullName);
            rtbEventList.AppendText("\n");

            if (e.Item2 == "a1")
            {
                string actionText = string.Format("Action: {0}\n", e.Item3);
                rtbEdgy.AppendText(actionText);
                string responseText = string.Format("Response: {0}\n", e.Item4);
                rtbEdgy.AppendText(responseText);
                string observationText = string.Format("Observation: {0}\n", e.Item5);
                rtbWally.AppendText(observationText);
            }
            else
            {
                string actionText = string.Format("Action: {0}\n", e.Item3);
                rtbWally.AppendText(actionText);
                string responseText = string.Format("Response: {0}\n", e.Item4);
                rtbWally.AppendText(responseText);
                string observationText = string.Format("Observation: {0}\n", e.Item5);
                rtbEdgy.AppendText(observationText);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var predBoolMap = _server.ObjectBase.PredBooleanMap;
            Graphics graphics = CreateGraphics();
            DrawGrids(graphics);
            DrawAcorns(graphics, predBoolMap);
            DrawInitialSquirrels(graphics, predBoolMap);
            graphics.Dispose();

            foreach (var pair in predBoolMap)
            {
                ListViewItem predicateItem = new ListViewItem(pair.Key);
                predicateItem.Name = pair.Key;
                predicateItem.SubItems.Add(pair.Value.ToString());
                listviewPredBoolMap.Items.Add(predicateItem);
            }
            
            Thread thread = new Thread(_server.Run);
            thread.Start();
        }
    }
}
