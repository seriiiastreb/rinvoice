using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rInvoice.Utils
{
    public class UFT
    {
        public class Table_TableList
        {
            public static readonly string TABLE_NAME = "TablesList";
            public static readonly string COLUMN_TableID = "TableID";
            public static readonly string COLUMN_TableNameDB = "TableNameDB";
            public static readonly string COLUMN_TableNameForUser = "TableNameForUser";
        }

        public class Table_FieldsList
        {
            public static readonly string TABLE_NAME = "FieldsList";
            public static readonly string COLUMN_TableID = "TableID";
            public static readonly string COLUMN_FieldID = "FieldID";
            public static readonly string COLUMN_FieldNameDB = "FieldNameDB";
            public static readonly string COLUMN_FieldNameForUser = "FieldNameForUser";
            public static readonly string COLUMN_ClassifierType = "ClassifierType";
        }

    }
}
