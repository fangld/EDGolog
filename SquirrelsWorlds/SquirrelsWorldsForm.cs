using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Planning.Servers;

namespace SquirrelsWorlds
{
    public partial class SquirrelsWorldsForm : Form
    {
        #region Fields

        public ObjectBase _objectBase;

        #endregion

        #region Constructors

        public SquirrelsWorldsForm(ObjectBase objectBase)
        {
            InitializeComponent();
            _objectBase = objectBase;
            _objectBase.ObjectBaseChanged += ShowObjectBase;
        }

        #endregion

        private void SquirrelsWorldsForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Orange, 2);
            graphics.DrawLine(pen, 10, 10, 100, 100);
            graphics.DrawRectangle(pen, 10, 10, 100, 100);
            graphics.DrawEllipse(pen, 10, 10, 100, 100);
            graphics.Dispose();
        }

        private void ShowObjectBase(object sender, Dictionary<string, bool> predBooleanMap)
        {


            Console.WriteLine("Object base:");
            foreach (var pair in predBooleanMap)
            {
                Console.WriteLine("  Predicate: {0}, Value: {1}", pair.Key, pair.Value);
            }
        }
    }
}
