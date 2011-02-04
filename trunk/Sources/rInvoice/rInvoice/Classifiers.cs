using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManCas
{
    public partial class Classifiers : UserControl
    {
        ServerObject mServerObject = null;
        int mTypeID = 0;

        public int TypeID
        {
            get { return mTypeID; }
            set { mTypeID = value; }
        }


        public Classifiers()
        {
            InitializeComponent();
            mServerObject = new ServerObject();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            CloseThisTab();
        }

        private void addGrupButton_Click(object sender, EventArgs e)
        {
            ClassifiersGrupInputPanel AddForm = new ClassifiersGrupInputPanel();
            AddForm.CreateNew = true;
            System.Windows.Forms.DialogResult result = AddForm.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("Data was saved");
            }
            else
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    MessageBox.Show("Sorry, data was not saved");
                }
            FillGrupDatagrid();
        }

        private void addClassifierbutton_Click(object sender, EventArgs e)
        {
            ClassifiersInputPanel AddForm = new ClassifiersInputPanel();
            AddForm.CreateNew = true;
            AddForm.TypeID = mTypeID;
            System.Windows.Forms.DialogResult result = AddForm.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("Data was saved");
            }
            else
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    MessageBox.Show("Sorry, data was not saved");
                }
            FillClassifiersDataGrid(mTypeID);
        }

        private void CloseThisTab()
        {
            TabPage tabpage = (TabPage)this.Parent;
            TabControl tabControl = (TabControl)tabpage.Parent;

            tabControl.TabPages.Remove(tabpage);
        }

        private void Classifiers_Load(object sender, EventArgs e)
        {
            FillGrupDatagrid();
            FillClassifiersDataGrid(mTypeID);
        }

        private void FillGrupDatagrid()
        {
            DataTable grupTable = mServerObject.SelectTypeClassifiers(false);
            dataGridView1.DataSource = grupTable;
        }

        private void FillClassifiersDataGrid(int typeID)
        {
            DataTable classifiersTable = mServerObject.SelectClassifiers(typeID, false);
            dataGridView2.DataSource = classifiersTable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int typeId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                mTypeID = typeId;
                FillClassifiersDataGrid(mTypeID);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassifiersGrupInputPanel typeInputPanel = new ClassifiersGrupInputPanel();
            if (e.RowIndex >= 0)
            {
                int typeId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                string nameType = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

                typeInputPanel.TypeID = typeId;
                typeInputPanel.NameType = nameType;
                typeInputPanel.CreateNew = false;

                try
                {
                    System.Windows.Forms.DialogResult result = typeInputPanel.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {

                        MessageBox.Show(typeInputPanel.TypeID + "' data was saved");
                    }

                    else
                    {
                        MessageBox.Show(typeInputPanel.TypeID + "' data was NOT saved");
                    }
                }
                finally
                {
                    FillGrupDatagrid();
                }


            }
        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassifiersInputPanel updateForm = new ClassifiersInputPanel();
            if (e.RowIndex >= 0)
            {
                int codeId = int.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                string nameClassifier = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                string description = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();

                updateForm.CodeID = codeId;
                updateForm.NameClassifier = nameClassifier;
                updateForm.Description = description;
                updateForm.TypeID = mTypeID;
                updateForm.CreateNew = false;

                try
                {
                    System.Windows.Forms.DialogResult result = updateForm.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {

                        MessageBox.Show(updateForm.CodeID + "' data was saved");
                    }

                    else
                    {
                        MessageBox.Show(updateForm.CodeID + "' data was NOT saved");
                    }
                }
                finally
                {
                    FillClassifiersDataGrid(mTypeID);
                }
            }
        }
    }
}
