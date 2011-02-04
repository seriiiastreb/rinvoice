using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ManCas
{
    class ServerObject
    {
        DataBridge mDataBridge = null;

        public ServerObject()
        {
            mDataBridge = new DataBridge();
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

    }
}
