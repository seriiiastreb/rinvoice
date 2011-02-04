using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace rInvoice
{
    public partial class LoghinForm : Form
    {
        ServerObject mServerObject = null;

        public LoghinForm()
        {
            InitializeComponent();
            statusLabel.Visible = false;
            mServerObject = new ServerObject();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            bool autentificate = mServerObject.AutentificateUser(userTextBox.Text, passwordTextBox.Text);

            if (autentificate)
            {
                this.Visible = false;
                MainForm f = new MainForm();
                f.ShowDialog();

                this.Close();
            }
            else
            {
                statusLabel.Visible = true;
            }
        }
    }
}
