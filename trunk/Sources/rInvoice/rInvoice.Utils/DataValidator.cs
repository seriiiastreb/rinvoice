using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace rInvoice.Utils
{
    class dataValidator
    {
        string mfilename = string.Empty;

        public string fileName
        {
            get { return mfilename; }
            set { mfilename = value; }
        }

        public dataValidator()
        {
        }

        public bool isFieldNumber(string fieldName)
        {
            bool result = false;
            string pattern = @"^[0-9]$";
            if (fieldName == string.Empty)
            {
                result = true;
            }
            else if (Regex.IsMatch(fieldName, pattern)) { result = true; }

            return result;
        }

        public bool isEmail(string inputEmail)
        {
            bool result = false;

            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (rEMail.IsMatch(inputEmail) && inputEmail.Length > 0)
            { result = true; }

            return result;
        }

        public bool IsNumber(string strNumber)
        {
            // TO DO My bi use --- TRYPARCE

            bool result = false;
            string exp1 = @"^[-+]?[0-9]*\.?[0-9]+$";
            string exp2 = @"^[-+]?[0-9]*\,?[0-9]+$";
            Regex regex = new Regex("(" + exp1 + ")|(" + exp2 + ")");
            if (strNumber == string.Empty) { result = true; }
            else if (regex.IsMatch(strNumber) && strNumber.Length > 0) { result = true; }

            return result;
        }
    }
}
