using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using System.Configuration;

namespace rInvoice
{
    class DataBridge
    {
        private SqlCeConnection mSqlConnection = null;
        private string mLastError = string.Empty;
        private string mConnectionString = string.Empty;

        public DataBridge()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ToString();
            mConnectionString = connectionString;
        }

        public DataBridge(string connectionString)
        {
            mConnectionString = connectionString;
        }

        public string LastError
        {
            get { return mLastError; }
            set { mLastError = value; }
        }

        public bool Connect()
        {
            mLastError = string.Empty;

            bool result = false;

            mSqlConnection = new SqlCeConnection(mConnectionString);

            try
            {
                if (mSqlConnection.State != ConnectionState.Open)
                {
                    mSqlConnection.Open();
                }
                result = true;
            }
            catch (Exception e)
            {
                mLastError = DateTime.Now.ToString() + " : " + e.Message;
                if (mSqlConnection.State == ConnectionState.Open)
                {
                    mSqlConnection.Close();
                }
            }

            return result;
        }

        public void Disconnect()
        {
            if (mSqlConnection.State == ConnectionState.Open)
            {
                mSqlConnection.Close();
            }

            mLastError = string.Empty;
        }

        public System.Data.DataTable ExecuteQuery(string queryString)
        {
            System.Data.DataTable toReturn = new System.Data.DataTable();

            try
            {

                SqlCeCommand command = mSqlConnection.CreateCommand();
                command.CommandText = queryString;
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(command);
                adapter.Fill(toReturn);

            }
            catch (Exception e)
            {
                toReturn = null;
                mLastError = e.Message;
            }

            return toReturn;
        }

        public object ExecuteScalarQuery(string scalarQuery)
        {
            object result = null;
            try
            {
                SqlCeCommand command = mSqlConnection.CreateCommand();
                command.CommandText = scalarQuery;
                result = command.ExecuteScalar();
            }
            catch (Exception e)
            {
                mLastError = e.Message;
            }

            return result;
        }

        public bool ExecuteNonQuery(string nonQuery)
        {
            bool result = false;
            try
            {
                SqlCeCommand command = mSqlConnection.CreateCommand();
                command.CommandText = nonQuery;

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                mLastError = e.Message;
            }

            return result;
        }

        internal bool ExecuteNonQueryBatch(string[] insertQueryBatch)
        {
            bool result = true;
            SqlCeTransaction transaction = mSqlConnection.BeginTransaction();
            try
            {
                SqlCeCommand command = mSqlConnection.CreateCommand();

                command.Transaction = transaction;

                for (int i = 0; i < insertQueryBatch.Length; i++)
                {
                    string nonQuery = insertQueryBatch[i];

                    command.CommandText = nonQuery;
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        result &= true;
                    }
                    else
                    {
                        result &= false;
                        mLastError = "Failed to insert data. Statement:\r\n" + nonQuery + "\r\n";
                    }
                }

                if (result)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            catch (Exception e)
            {
                mLastError = e.Message;
                transaction.Rollback();
            }

            return result;
        }
    }
}
