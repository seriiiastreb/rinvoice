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
    public partial class UsersAdministration : UserControl
    {
        ServerObject mServerObject = null;

        public UsersAdministration()
        {
            InitializeComponent();
            mServerObject = new ServerObject();
        }
// text

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

        private void UsersAdministration_Load(object sender, EventArgs e)
        {
            FillGridView();
        }

        private void FillGridView()
        {
            DataTable gvTable = mServerObject.SelectUsers(false);
            dataGridView1.DataSource = gvTable;
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            UsersInputPanel usrp = new UsersInputPanel();
            usrp.ShowDialog();
        }
    }
}
