using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using rInvoice.Utils;

namespace rInvoice
{
    class ServerObject
    {
        DataBridge mDataBridge = null;
        private DataTable mTableNames;
        private DataTable mTableFieldNames;

        public ServerObject()
        {
            mDataBridge = new DataBridge();

            string commandTableList = @"SELECT " + UFT.Table_TableList.COLUMN_TableID
                                            + ", " + UFT.Table_TableList.COLUMN_TableNameDB
                                            + " FROM " + UFT.Table_TableList.TABLE_NAME;

            string commandFieldList = @"SELECT " + UFT.Table_FieldsList.COLUMN_FieldID
                                            + ", " + UFT.Table_FieldsList.COLUMN_FieldNameDB
                                            + " FROM " + UFT.Table_FieldsList.TABLE_NAME;

            if (mDataBridge.Connect() == true)
            {
                mTableNames = mDataBridge.ExecuteQuery(commandTableList);
                //mLastError = mDataBridge.LastError;

                mTableFieldNames = mDataBridge.ExecuteQuery(commandFieldList);
                //mLastError = mDataBridge.LastError;
                mDataBridge.Disconnect();
            }
            //else
            //{
            //    throw new Exception("HRPM server application failed to connect to DB. " + mLastError);
            //}

        }


       private string GetTableName(int tableID)
        {
            string tableName = string.Empty;
            if (mTableNames != null && mTableNames.Rows.Count > 0)
            {
                string expression = UFT.Table_TableList.COLUMN_TableID + " = " + tableID;
                DataRow[] selectedRows = mTableNames.Select(expression);
                if (selectedRows != null && selectedRows.Length == 1)
                {
                    tableName = (string)selectedRows[0].ItemArray[1];
                }
                else
                {
                    throw new Exception("Multiple values for one table name code!");
                }

            }
            return tableName;
        }

        private string GetColumnName(int columnID)
        {
            string columnName = string.Empty;
            if (mTableFieldNames != null && mTableFieldNames.Rows.Count > 0)
            {
                string expression = UFT.Table_FieldsList.COLUMN_FieldID + " = " + columnID;
                DataRow[] selectedRows = mTableFieldNames.Select(expression);
                if (selectedRows != null && selectedRows.Length == 1)
                {
                    columnName = (string)selectedRows[0].ItemArray[1];
                }
                else
                {
                    throw new Exception("Multiple values for one table name code!");
                }

            }
            return columnName;
        }

        public bool AutentificateUser(string username, string password)
        {
            bool result = false;
            DataTable returnTable = new DataTable();

            try
            {
                if (mDataBridge.Connect() == true)
                {

                    string commandText = @"SELECT UserID "
                                            + " FROM USERS "
                                            + " WHERE Login ='" + username + "' "
                                            + " AND Password ='" + password + "' ";

                    returnTable = mDataBridge.ExecuteQuery(commandText);
                }
            }
            finally
            {
                mDataBridge.Disconnect();
            }

            if (returnTable != null && returnTable.Rows.Count > 0) result = true;

            return result;
        }

        public DataTable SelectUsers(bool isDelete)
        {
            DataTable resultTable = null;
            try
            {
                if (mDataBridge.Connect() == true)
                {
                    string commandText = @"SELECT UserID "
                        + ", FirstName "
                        + ", LastName "
                        + ", Login "
                        + " FROM USERS "
                        + " WHERE isDelete = " + Convert.ToInt32(isDelete) + " "
                        + " ";

                    resultTable = mDataBridge.ExecuteQuery(commandText);
                }
            }
            finally
            {
                mDataBridge.Disconnect();
            }
            return resultTable;
        }

        public DataTable SelectTypeClassifiers(bool isDelete)
        {
            DataTable resultTable = null;
            try
            {
                if (mDataBridge.Connect() == true)
                {

                    string commandText = @"SELECT TypeID "
                        + ", Name "
                        + " FROM ClassifierTypes "
                        + " WHERE isDelete = " + Convert.ToInt32(isDelete) + " "
                        + " ";

                    resultTable = mDataBridge.ExecuteQuery(commandText);
                }
            }
            finally
            {
                mDataBridge.Disconnect();
            }
            return resultTable;
        }

        public DataTable SelectClassifiers(int typeID, bool isDelete)
        {
            DataTable resultTable = null;
            try
            {
                if (mDataBridge.Connect() == true)
                {

                    string commandText = @"SELECT Classifiers.CodeID "
                        + ", Classifiers.Name "
                        + ", Classifiers.Description "
                        + ", ClassifierTypes.Name AS Grupe "
                        + " FROM Classifiers INNER JOIN ClassifierTypes "
                        + " ON Classifiers.TypeID = ClassifierTypes.TypeID "
                        + " WHERE Classifiers.isDelete = " + Convert.ToInt32(isDelete) + " "
                        + " ";
                    if (typeID > 1)
                    {
                        commandText += " AND Classifiers.TypeID = " + typeID;
                    }

                    resultTable = mDataBridge.ExecuteQuery(commandText);
                }
            }
            finally
            {
                mDataBridge.Disconnect();
            }
            return resultTable;
        }

        public bool AddClassifierType(string name)
        {
            bool result = false;

            try
            {
                if (mDataBridge.Connect() == true)
                {
                    string nonQuery = @"INSERT INTO ClassifierTypes ([Name]) "
                        + "VALUES ( "
                        + " '" + name + "' "
                        + " ) ";

                    result = mDataBridge.ExecuteNonQuery(nonQuery);
                }
            }
            finally
            {
                mDataBridge.Disconnect();
            }

            return result;
        }

        public bool UpdateClassifierType(int typeID, string nameType)
        {
            bool result = false;

            try
            {
                if (mDataBridge.Connect() == true)
                {
                    string nonQuery = @" UPDATE ClassifierTypes "
                            + "SET ClassifierTypes.[Name] = '" + nameType + "' "
                            + " WHERE ClassifierTypes.TypeID = " + typeID + " "
                            + " ";

                    result = mDataBridge.ExecuteNonQuery(nonQuery);
                }
            }
            finally
            {
                mDataBridge.Disconnect();
            }

            return result;
        }

        public bool AddClassifiers(int typeID, string name, string description)
        {
            bool result = false;

            try
            {
                if (mDataBridge.Connect() == true)
                {
                    string nonQuery = @"INSERT INTO Classifiers (TypeID, [Name], Description) "
                        + "VALUES ( "
                        + typeID
                        + ", '" + name + "' "
                        + ", '" + description + "' "
                        + " ) ";

                    result = mDataBridge.ExecuteNonQuery(nonQuery);
                }
            }
            finally
            {
                mDataBridge.Disconnect();
            }

            return result;
        }

        public bool UpdateClassifiers(int codeID, int typeID, string nameClassifiers, string description)
        {
            bool result = false;

            try
            {
                if (mDataBridge.Connect() == true)
                {
                    string nonQuery = @" UPDATE Classifiers "
                            + "SET Classifiers.[TypeID] = " + typeID + " "
                            + ", Classifiers.[Name] = '" + nameClassifiers + "' "
                            + ", Classifiers.[Description] ='" + description + "' "
                            + " WHERE Classifiers.CodeID = " + codeID + " "
                            + " ";

                    result = mDataBridge.ExecuteNonQuery(nonQuery);
                }
            }
            finally
            {
                mDataBridge.Disconnect();
            }

            return result;
        }

        public DataTable GetClassifiersListByTypeID(int typeID)
        {
            DataTable classifierTypesByIDList = new DataTable();

            try
            {
                if (mDataBridge.Connect() == true)
                {

                    string commandText = @"SELECT Classifiers.[" + GetColumnName(UFT.Table_Classifiers.COLUMN_Code) + "] "
                                         + ", Classifiers.[" + GetColumnName(UFT.Table_Classifiers.COLUMN_Name) + "] "
                                         + ", Classifiers.[" + GetColumnName(UFT.Table_Classifiers.COLUMN_Description) + "] "
                                         + " FROM " + GetTableName(UFT.Table_Classifiers.TABLE_NAME) + " as Classifiers "
                                        + " WHERE Classifiers.[" + GetColumnName(UFT.Table_Classifiers.COLUMN_TypeID) + "] = " + typeID + " "
                                        + "OR Classifiers.[" + GetColumnName(UFT.Table_Classifiers.COLUMN_Code) + "] = 0 ";
                    commandText += " ORDER BY Classifiers.[" + GetColumnName(UFT.Table_Classifiers.COLUMN_Name) + "] asc ";
                    classifierTypesByIDList = mDataBridge.ExecuteQuery(commandText);
                   // mLastError = mDataBridge.LastError;
                }
            }
            finally
            {
                mDataBridge.Disconnect();
            }

            return classifierTypesByIDList;
        }

    }
}
