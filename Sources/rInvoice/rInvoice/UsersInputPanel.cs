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
    public partial class UsersInputPanel : Form
    {
        private ServerObject mServerObject = null;
        int mUserID = 0;
        string mLoghin = string.Empty;
        string mFirstName = string.Empty;
        string mLastName = string.Empty;
        int mRole = 0;
        string mPassword = string.Empty;
        int mRecordStatus = 0;

        public int UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }

        public string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
        }

        public string LastName
        {
            get { return mLastName; }
            set { mLastName = value; }
        }
        
        public string Loghin
        {
            get { return mLoghin; }
            set { mLoghin = value; }
        }

        public int Role
        {
            get { return mRole; }
            set { mRole = value; }
        }

        public int RecordStatus
        {
            get { return mRecordStatus; }
            set { mRecordStatus = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        public UsersInputPanel()
        {
            InitializeComponent();
            mServerObject = new ServerObject();
        }

        private void UsersInputPanel_Load(object sender, EventArgs e)
        {
            fillRoleComboBox();
            userIdTextBox.Text = mUserID.ToString();
            loginTextBox.Text = mLoghin;
            firstNameTextBox.Text = mFirstName;
            lastNameTextBox.Text = mLastName;
            
            rolesComboBox.SelectedValue = mRole;

            passwordTextBox.Text = mPassword;
            passwordConfirmTextBox.Text = mPassword;

            if (mRecordStatus == 1) activeUserCheckBox.Checked = true;
            else
                activeUserCheckBox.Checked = false;
        }

        private void fillRoleComboBox()
        { 
        }
    }
}
