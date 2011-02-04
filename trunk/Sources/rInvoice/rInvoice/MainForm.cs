using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManCas
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void AddUserControlInTabs(UserControl userControl)
        {
            // If not exist
            if (!mainTabControl.TabPages.ContainsKey(userControl.Name))
            {
                // Add it
                mainTabControl.TabPages.Add(userControl.Name, userControl.Name);

                TabPage tabPage = mainTabControl.TabPages[mainTabControl.TabPages.IndexOfKey(userControl.Name)];

                tabPage.ImageIndex = 2;
                tabPage.Controls.Add(userControl);
                userControl.Dock = DockStyle.Fill;
                mainTabControl.SelectedTab = tabPage;
                tabPage.Focus();
            }
            else
            {
                // Make it active
                TabPage tabPage = mainTabControl.TabPages[mainTabControl.TabPages.IndexOfKey(userControl.Name)];
                mainTabControl.SelectedTab = tabPage;
                tabPage.Focus();
            }

            Update();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersAdministration usersPanel = new UsersAdministration();
            usersPanel.Name = "Users list";
            AddUserControlInTabs((UserControl)usersPanel);
        }

        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Classifiers classifiersPanel = new Classifiers();
            classifiersPanel.Name = "Classifiers";
            AddUserControlInTabs((UserControl)classifiersPanel);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
