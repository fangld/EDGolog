using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4.Runtime;
using LanguageRecognition;
using ObjectWorlds.Network;
using PAT.Common.Classes.CUDDLib;
using Planning;
using Planning.Servers;
using SquirrelsWorlds.Properties;

namespace SquirrelsWorlds
{
    public partial class SquirrelsWorldForm : Form
    {
        #region Fields

        private const int LocationX = 20;

        private const int LocationY = 20;

        private const int LineWidth = 1;

        private const int BitmapWidth = 50;

        private const int BitmapHeight = 50;

        private int _gridCount;

        private int _port;

        private int _listenBackLog;

        private string _serverProblemFileName;

        private string _domainFileName;

        private ServerProblem _problem;

        private Server _server;

        #endregion

        public SquirrelsWorldForm()
        {
            //_port = Settings.Default.Port;
            //_domainFileName = Settings.Default.DomainFileName;
            //_serverProblemFileName = Settings.Default.ServerProblemFileName;
            //Initial();
            //Draw();

            InitializeComponent();
            Draw();
        }

        private void Draw()
        {
            System.Drawing.Graphics formGraphics = CreateGraphics();
            string drawString = "Sample Text";
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            float x = 150.0F;
            float y = 50.0F;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            formGraphics.Dispose();

            //Console.WriteLine("Draw background");
            //Graphics graphics = CreateGraphics();
            ////graphics.Clear(Color.White);
            //graphics.DrawString("0123456789", new Font("Fixedsys", 18), new SolidBrush(Color.Blue), 100, 100);

            //Pen pen = new Pen(Color.Black, LineWidth);
            //graphics.DrawLine(pen, LocationX, LocationY, LocationX + (BitmapWidth + LineWidth) * _gridCount,
            //    LocationY);
            //graphics.DrawLine(pen, LocationX, LocationY + BitmapHeight + LineWidth,
            //    LocationX + (BitmapWidth + LineWidth) * _gridCount, LocationY + BitmapHeight + LineWidth);
            //for (int i = 0; i <= _gridCount; i++)
            //{
            //    graphics.DrawLine(pen, LocationX + (BitmapWidth + LineWidth) * i, LocationY,
            //        LocationX + (BitmapWidth + LineWidth) * i, LocationY + BitmapHeight + LineWidth);
            //}

            //Image edgyRightImage = Resources.EdgyRight;
            //graphics.DrawImage(edgyRightImage, LocationX + LineWidth, LocationY + LineWidth, BitmapWidth, BitmapHeight);

            //Image wallyLeftImage = Resources.WallyLeft;
            //graphics.DrawImage(wallyLeftImage, LocationX + (BitmapWidth + LineWidth) * 7 + LineWidth, LocationY + LineWidth, BitmapWidth, BitmapHeight);
        }

        #region Methods

        private void Initial()
        {
            CUDD.InitialiseCUDD(256, 256, 262144, 0.1);

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
            //_server.Run();
        }

        #endregion
    }
}
