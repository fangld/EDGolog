using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquirrelsWorlds
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SquirrelsWorldForm squirrelsWorldForm = new SquirrelsWorldForm();
            //squirrelsWorldForm.LocationX = 20;
            //squirrelsWorldForm.LocationY = 20;
            //squirrelsWorldForm.LineWidth = 1;
            //squirrelsWorldForm.BitmapWidth = 50;
            //squirrelsWorldForm.BitmapHeight = 50;
            Application.Run(squirrelsWorldForm);
        }
    }
}
