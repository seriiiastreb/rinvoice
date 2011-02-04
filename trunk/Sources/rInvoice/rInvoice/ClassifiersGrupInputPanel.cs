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
    public partial class ClassifiersGrupInputPanel : Form
    {
        private ServerObject mServerObject = null;
        private int mTypeID = 0;
        private string mName = string.Empty;
        bool mCreatingNew = false;

        public int TypeID
        {
            get { return mTypeID; }
            set { mTypeID = value; }
        }

        public string NameType
        {
            get { return mName; }
            set { mName = value; }
        }

        public bool CreateNew
        {
            get
            {
                return mCreatingNew;
            }

            set
            {
                mCreatingNew = value;
            }
        }

        public ClassifiersGrupInputPanel()
        {
            InitializeComponent();
            mServerObject = new ServerObject();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                if (mCreatingNew)
                {
                    result = mServerObject.AddClassifierType(mName);
                }
                else
                {
                    result = mServerObject.UpdateClassifierType(mTypeID, mName);
                }

                if (result)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failure saving data.\r");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failure saving data.\r" + ex.Message);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mName = textBox1.Text.Trim();
        }

        private void ClassifiersGrupInputPanel_Load(object sender, EventArgs e)
        {
            textBox1.Text = mName;
        }
    }
}
