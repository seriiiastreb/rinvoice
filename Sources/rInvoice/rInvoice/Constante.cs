using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace rInvoice
{
    public partial class Constante : UserControl
    {
        ServerObject mServerObject = null;

        public Constante()
        {
            InitializeComponent();
            mServerObject = new ServerObject();
        }

        private void CloseThisTab()
        {
            TabPage tabpage = (TabPage)this.Parent;
            TabControl tabControl = (TabControl)tabpage.Parent;

            tabControl.TabPages.Remove(tabpage);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            CloseThisTab();
        }
    }
}
