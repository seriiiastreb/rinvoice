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
    public partial class ClassifiersInputPanel : Form
    {
        private ServerObject mServerObject = null;
        private int mCodeID = 0;
        private int mTypeID = 0;
        private string mName = string.Empty;
        private string mDescription = string.Empty;
        bool mCreatingNew = false;


        public int CodeID
        {
            get { return mCodeID; }
            set { mCodeID = value; }
        }

        public int TypeID
        {
            get { return mTypeID; }
            set { mTypeID = value; }
        }

        public string NameClassifier
        {
            get { return mName; }
            set { mName = value; }
        }

        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
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


        public ClassifiersInputPanel()
        {
            InitializeComponent();
            mServerObject = new ServerObject();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mName = textBox1.Text.Trim();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            mDescription = textBox2.Text.Trim();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                if (mCreatingNew)
                {
                    result = mServerObject.AddClassifiers(mTypeID, mName, mDescription);
                }
                else
                {
                    result = mServerObject.UpdateClassifiers(mCodeID, mTypeID, mName, mDescription);
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

        private void ClassifiersInputPanel_Load(object sender, EventArgs e)
        {
            textBox1.Text = mName;
            textBox2.Text = mDescription;
        }
    }
}
