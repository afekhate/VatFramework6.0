using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.OleDb;

namespace VatFramework.Utilities
{
    public class Common
    {
        public static string GenerateReferenceNo()
        {

            return string.Format("{0}", DateTime.Now.ToString("yyyyMMddHHmmssfffff"));
        }
        public static string ValidateMobile(string mobile)
        {
            string Val = mobile.Substring(0, 1);
            string mobileNo;
            if (Val == "0")
            {
                mobileNo = mobile.Substring(mobile.Length - 10);
                mobileNo = "234" + mobileNo;
            }
            else
            {
                mobileNo = "234" + mobile;
            }
            return mobileNo;
        }
        public static string GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType()?
           .GetMember(enumValue.ToString())?[0]?
           .GetCustomAttribute<DisplayAttribute>()?
           .Name;
        }
        
        public static bool IsImageExtension(string extension)
        {
            var allowedExtensions = new List<string> { "jpeg", "jpg", "png"};
            foreach(var item in allowedExtensions)
            {
                if (item.ToLower() == extension.ToLower())
                    return true;
            }
            return false;
        }

        public static bool IsVideoExtension(string extension)
        {
            var allowedExtensions = new List<string> { "mp4", "webm", "ogg" };
            foreach (var item in allowedExtensions)
            {
                if (item.ToLower() == extension.ToLower())
                    return true;
            }
            return false;
        }


        #region Message Test Cases
        public enum MessageTestCases
        {
            [Display(Name = ".")]
            caseOne = 1,
            [Display(Name = "Ade")]
            caseTwo = 2,
            [Display(Name = "Kafanchan @ There is a political conflict in Kafachan today")]
            caseThree = 3,
            [Display(Name = "Kayode @ Rogbodiyan Aala pẹlu awọn adugbo agbegbe")]
            caseFour = 4,
            [Display(Name = "Kayode | Birnin Gwari   | Cross/inter Border Conflict with neighbouring community")]
            caseFive = 5,
            [Display(Name = "Ayomide @ Sabon Gari @ The youths were engaged in a fight and there was destruction of properties")]
            caseSix = 6,
            [Display(Name = "Barka dai, yaya kake?")]
            caseSeven = 7,
            [Display(Name = "Ana ruwan sama sosai a yau")]
            caseEight = 8,
            [Display(Name = "Kafanchan @ Akwai rikicin siyasa a Kafachan yau")]
            caseNine = 9,
            [Display(Name = "Adeyemi @ Kaura @ Ana cigaba da zanga-zanga a karamar hukumar Kaura")]
            caseTen = 10,
            [Display(Name = "Adeyemi @ Kaura @ There is protest ongoing in Kaura local government")]
            caseEleven = 11,
            [Display(Name = "Bolaji @ Kafanchan @ There is a political conflict in Kafachan today")]
            caseTwelve = 12,
            [Display(Name = "Adeyemi @ Kaura @ Nibẹ ni ifihan ti nlọ lọwọ ni ijọba agbegbe Kaura")]
            caseThirteen = 13,
            [Display(Name = "Wọn ti mu nitori awọn iṣe ti ipanilaya")]
            caseFourteen = 14,
            [Display(Name = "Hello, how are you?")]
            caseFifteen = 15,
            [Display(Name = "It is raining heavily today")]
            casesixteen = 16,
        }

        public static string GetMessageTestCases(int testCaseValue)
        {
            string testCase = "";
            if (testCaseValue == 1)
            {

                testCase = ".";
            }
            else if (testCaseValue == 2)

            {
                testCase = "Ade";
            }

            else if (testCaseValue == 3)

            {
                testCase = "Kafanchan @ There is a political conflict in Kafachan today";
            }
            else if (testCaseValue == 4)

            {
                testCase = "Kayode @ Rogbodiyan Aala pẹlu awọn adugbo agbegbe";
            }
            else if (testCaseValue == 5)

            {
                testCase = "Kayode | Birnin Gwari   | Cross/inter Border Conflict with neighbouring community";
            }
            else if (testCaseValue == 6)
            {

                testCase = "Ayomide @ Sabon Gari @ The youths were engaged in a fight and destruction of properties";
            }
            else if (testCaseValue == 7)

            {
                testCase = "Barka dai, yaya kake?";
            }

            else if (testCaseValue == 8)

            {
                testCase = "Ana ruwan sama sosai a yau";
            }
            else if (testCaseValue == 9)

            {
                testCase = "Kafanchan @ Akwai rikicin siyasa a Kafachan yau";
            }
            else if (testCaseValue == 10)

            {
                testCase = "Adeyemi @ Kaura @ Ana cigaba da zanga-zanga a karamar hukumar Kaura";
            }
            else if (testCaseValue == 11)
            {

                testCase = "Adeyemi @ Kaura @ There is protest ongoing in Kaura local government";
            }
            else if (testCaseValue == 12)

            {
                testCase = "Bolaji @ Kafanchan @ There is a political conflict in Kafachan today";
            }

            else if (testCaseValue == 13)

            {
                testCase = "Adeyemi @ Kaura @ Nibẹ ni ifihan ti nlọ lọwọ ni ijọba agbegbe Kaura";
            }
            else if (testCaseValue == 14)

            {
                testCase = "Wọn ti mu nitori awọn iṣe ti ipanilaya";
            }
            else if (testCaseValue == 15)

            {
                testCase = "Hello, how are you?";
            }
            else if (testCaseValue == 16)

            {
                testCase = "It is raining heavily today";
            }
            return testCase;
        }

        #endregion


        #region Various Incident Status At Different Levels

        public enum AuthorizerStatus
        {
            [Display(Name = "Pending Case")]
            Pending_Case = 0,
            [Display(Name = "Accepted Case")]
            Accept = 1,
            [Display(Name = "Rejected Case")]
            Reject = 2
        }

        public static string GetAuthorizerStatusDescription(int Status)
        {
            string myStatus = "";
            if (Status == 0)
            {

                myStatus = "Pending Case";
            }
            else if (Status == 1)

            {
                myStatus = "Accepted Case";
            }

            else if (Status == 2)

            {
                myStatus = "Rejected Case";
            }

            return myStatus;
        }

        public enum IncidentResponderStatus
        {
            [Display(Name= "Pending Case")]
            Pending_Pickup = 0,
            [Display(Name = "Accepted Case")]
            Picked = 1,
            [Display(Name = "Rejected Case")]
            Rejected = 2,
            [Display(Name = "Case Unavailable")]
            Unavailable = 3,
            [Display(Name = "Resolved Case")]
            Resolved = 4,  
                              // 0 - Pending_Pickup, 1 - Picked, 2 - Rejected, 3 - Unavailable  4 - Resolved  
        }

        public static string GetResponderStatusDescription(int Status)
        {
            string myStatus = "";
            if (Status == 0)
            {

                myStatus = "Pending Case";
            }
            else if (Status == 1)

            {
                myStatus = "Accepted Case";
            }

            else if (Status == 2)

            {
                myStatus = "Rejected Case";
            }
            else if (Status == 3)

            {
                myStatus = "Unavailable";
            }
            else if (Status == 4)

            {
                myStatus = "Resolved Case";
            }
            return myStatus;
        }

        public enum IncidentStatus
        {
            [Display(Name = "New Incident")] 
            Created = 0,  //incident creation  > red
            [Display(Name = "Escalated To Responder(s)")] 
            Assigned = 1,  // when incident is assigned > yellow
            [Display(Name = "Incident Rejected")] 
            Rejected = 2,  // when incident is rejected  => red
            [Display(Name = "Incident Reassigned")] 
            ReAssigned = 3,  // when incident is reassigned after being rejected => pink
            [Display(Name = "Task In Progress")] 
            Pickup = 4,   // when responder pickup the incident for execution => orange
            [Display(Name = "Incident Resolved")] 
            Resolved = 5,   // when the responder resolve the incident => blue
            [Display(Name = "Pending")] 
            Pending = 6,   // when the incident has not been pick up within a particular time >red
            [Display(Name = "Closed")] 
            Closed = 7,   // the incident is confirm resolved. => green
            
        }

        public static string clearText(string dirtyText)
        {
            string message = "";
            try
            {
                if (dirtyText !="")
                {


                    message = dirtyText.Replace("&", " ");
                    message = message.Replace("<", " ");
                    message = message.Replace("/", " ");
                    message = message.Replace("'", " ");
                }

                return message;
            }
            catch (Exception ex)
            {

                return message;
            }
        }
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            bool result = false;
            try
            {

                string pattern = @"^[0]\d{10}$";

                RegexOptions options = RegexOptions.Multiline;

                foreach (Match m in Regex.Matches(phoneNumber, pattern, options))
                {
                    result = true;
                }

                return result;
            }
            catch (Exception)
            {
                return result;

            }
        }


        public static string FetchStatusDescription(int Status)
        {
            string myStatus = "";
            if (Status == 0)
            {

                myStatus = "New Incident";
            }
            else if (Status == 1)

            {
                myStatus = "Escalated To Responder(s)";
            }

            else if (Status == 2)

            {
                myStatus = "Incident Rejected";
            }
            else if (Status == 3)

            {
                myStatus = "Incident Reassigned";
            }
            else if (Status == 4)

            {
                myStatus = "Task In Progress";
            }
            else if (Status == 5)

            {
                myStatus = "Incident Resolved";
            }
            else if (Status == 6)

            {
                myStatus = "Pending";
            }

            else if (Status == 7)

            {
                myStatus = "Closed";
            }
            return myStatus;
        }

        #endregion


        #region general
        public static string ConvertDate(string DataValue)
        {
            try
            {
                string[] BreakStartDate = DataValue.Split("/");
                string data = BreakStartDate[2] + "-" + BreakStartDate[0] + "-" + BreakStartDate[1];

                return data;
            }
            catch (Exception)
            {

                return DataValue;
            }
        }

        public static string FormatDate(string date)
        {
            string formatDate = date.Replace(" 12:00:00 AM", "");
            string[] breakDate = formatDate.Split('/');
            string dd = "";
            string mm = "";
            string newdate = "";
            if (breakDate[0].Length == 1)
            {
                dd = "0" + breakDate[0];
            }
            else
            {
                dd = breakDate[0];
            }
            if (breakDate[1].Length == 1)
            {
                mm = "0" + breakDate[1];
            }
            else
            {
                mm = breakDate[1];
            }
            newdate = breakDate[2] + "-" + dd + "-" + mm;
            return newdate.ToString().Replace(" 12:00:00 AM", "");
        }

        public static string ConvertDate_Date(string DataValue)
        {
            try
            {
                string[] BreakStartDate = DataValue.Split("/");
                string data = BreakStartDate[2] + "-" + BreakStartDate[0] + "-" + BreakStartDate[1];

                return data;
            }
            catch (Exception)
            {

                return DataValue;
            }
        }


        public static int FetchMonthDigit(string Month)
        {
            int digit = 0;
            if (Month == "January")
            {

                digit = 1;
            }
            else if (Month == "February")

            {
                digit = 2;
            }

            else if (Month == "March")

            {
                digit = 3;
            }
            else if (Month == "April")

            {
                digit = 4;
            }
            else if (Month == "May")

            {
                digit = 5;
            }
            else if (Month == "June")

            {
                digit = 6;
            }
            else if (Month == "July")

            {
                digit = 7;
            }

            else if (Month == "August")

            {
                digit = 8;
            }
            else if (Month == "September")

            {
                digit = 9;
            }
            else if (Month == "October")

            {
                digit = 10;
            }
            else if (Month == "November")

            {
                digit = 11;
            }
            else if (Month == "December")

            {
                digit = 12;
            }
            return digit;
        }









        //http://maps.google.com/mapfiles/ms/micons/pink.png
        public static string GoogleMapIncidentIcon(int Status)
        {
            string myStatus = "red.png";
            if (Status == 0)
            {

                myStatus = "yellow.png";
            }
            else if (Status == 1)

            {
                myStatus = "orange.png";
            }

            else if (Status == 2)

            {
                myStatus = "red.png";
            }
            else if (Status == 3)

            {
                myStatus = "pink.png";
            }
            else if (Status == 4)

            {
                myStatus = "purple.png";
            }
            else if (Status == 5)

            {
                myStatus = "blue.png";
            }
            else if (Status == 6)

            {
                myStatus = "lightblue.png";
            }

            else if (Status == 7)

            {
                myStatus = "green.png";
            }
            return myStatus;
        }

        public static int FetchStatusDescriptionValue(string Status)
        {
            int myStatus = 0;

            if (Status == "Created")
            {

                myStatus = 0;
            }
            else if (Status == "Assigned")

            {
                myStatus = 1;
            }

            else if (Status == "Rejected")

            {
                myStatus = 2;
            }
            else if (Status == "ReAssigned")

            {
                myStatus = 3;
            }
            else if (Status == "Pickup")

            {
                myStatus =4;
            }
            else if (Status == "Resolved")

            {
                myStatus = 5;
            }
            else if (Status == "Pending")

            {
                myStatus = 6;
            }

            else if (Status == "Closed")

            {
                myStatus = 7;
            }

            return myStatus;
        }
        private readonly Random _random = new Random();

        public static DataSet ExcelToDataSet(string SourceFilename, bool p)
        {
            DataSet ds = new DataSet();
            try
            {
                string connStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;\"", SourceFilename);
                // string connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;\"", SourceFilename);

                OleDbConnection conn = new OleDbConnection(connStr);
                conn.Open();
                DataTable schemaDT = conn.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
                conn.Close();

                string tableName = schemaDT.Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}]", tableName), conn);

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
                
            }

            return ds;
        }




        public static DataSet ExcelToDataSet(string SourceFilename)
        {
            DataSet ds = new DataSet();
            try
            {
                string connStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;\"", SourceFilename);

                OleDbConnection conn = new OleDbConnection(connStr);
                conn.Open();
                DataTable schemaDT = conn.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
                conn.Close();


                string tableName = schemaDT.Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}]", tableName), conn);

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {
               
            }

            return ds;
        }


        // Instantiate random number generator.  
      
        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                { return ip.ToString(); }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

      

        public static String GenerateFileName(String PatientNo)
        {
            String FileName = String.Format("{0}_{1}_{2}", PatientNo.Replace("/", "#"), DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid().ToString());
            return FileName;
        }


        public static decimal AmountToKobo(decimal amount)
        {
            return (decimal)(amount * 100);
        }


        public static decimal KoboToAmount(decimal amount)
        {
            return (decimal)(amount / 100);
        }

        public static string GenerateGTBReferenceNumber()
        {
            return "GTB" + DateTime.Now.Ticks;
        }


        public static string GTBSHA512(string hash_string)
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            string hashed = BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
            return hashed;
        }


        public static string GetSHA512(string text)
        {
            ASCIIEncoding UE = new ASCIIEncoding();
            byte[] hashValue;
            byte[] data = UE.GetBytes(text);
            SHA512 hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(data);
            hex = ByteToString(hashValue);
            return hex;
        }
        public static string EncryptID(int id)
        {
            return HttpUtility.UrlEncode(PasswordSecurity.Encrypt(id.ToString(), "siswiusj34938322*"));
        }

        public static int DecryptID(string encryptedID)
        {
            try
            {
                return Convert.ToInt32(PasswordSecurity.Decrypt(HttpUtility.UrlDecode(encryptedID), "siswiusj34938322*"));
            }
            catch (Exception ex)
            {
                return Convert.ToInt32(PasswordSecurity.Decrypt(encryptedID, "siswiusj34938322*"));
            }

        }

        public class PasswordSecurity
        {
            private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

            // This constant is used to determine the keysize of the encryption algorithm.
            private const int keysize = 256;


            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
            public static string Encrypt(string plainText, string passPhrase)
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
                {
                    byte[] keyBytes = password.GetBytes(keysize / 8);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                    cryptoStream.FlushFinalBlock();
                                    byte[] cipherTextBytes = memoryStream.ToArray();
                                    return Convert.ToBase64String(cipherTextBytes);
                                }
                            }
                        }
                    }
                }
            }


            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
            public static string Decrypt(string cipherText, string passPhrase)
            {
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
                {
                    byte[] keyBytes = password.GetBytes(keysize / 8);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                        {
                            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }
        }
        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }


        public static string Tokenize(string amount)
        {
            return (amount.Split('.')[0].Replace(",", ""));
        }

        public static string xTokenize(string amount)
        {
            return (amount.Replace(",", ""));
        }

        public static string Randomize()
        {
            string number = "";
            for (int i = 0; i < 16; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }

        public static string Randomize(bool isGuid)
        {
            string guid = Guid.NewGuid().ToString();
            guid = guid.Replace("-", "").Substring(0, 10);
            return guid;
        }

        private static Random Rnd = new Random();
        public static string RandomizeNumber()
        {
            string number = "";
            for (int i = 0; i < 10; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }

        public static string RandomizePassword()
        {
            string number = "";
            for (int i = 0; i < 5; i++)
            {
                number = number + Convert.ToString(Rnd.Next(0, 9));
            }
            return number;
        }

        public static string SpellNumber(string MyNumber)
        {
            string Naria = "";
            string Kobo = "";
            string Temp = "";
            int DecimalPlace = 0;
            int Count = 0;
            string[] Place = new string[10];
            Place[2] = " Thousand ";
            Place[3] = " Million ";
            Place[4] = " Billion ";
            Place[5] = " Trillion ";
            // String representation of amount.
            //[Obsolete]
            //MyNumber = Strings.Trim(Conversion.Str(MyNumber));
            MyNumber = MyNumber.ToString().Trim();
            // Position of decimal place 0 if none.
            DecimalPlace = Strings.InStr(MyNumber, ".");
            // Convert Kobo and set MyNumber to Naria amount.
            if (DecimalPlace > 0)
            {
                //New Code
                var decimaPlaces = DecimalPlace + 1;
                var stringMid = MyNumber.Substring(decimaPlaces) + "00";
                var stringLeft = stringMid.Substring(0, 2);
                Kobo = GetTens(stringLeft);

                // Old Code
                // Kobo = GetTens(Strings.Left(Strings.Mid(MyNumber, DecimalPlace + 1) + "00", 2));
                var decimalPlaces2 = DecimalPlace - 1;
                var getLeft2 = MyNumber.Substring(decimalPlaces2);
                MyNumber = getLeft2.Trim();
            }
            Count = 1;
            while (!string.IsNullOrEmpty(MyNumber))
            {
                Temp = GetHundreds(Strings.Right(MyNumber, 3));
                if (!string.IsNullOrEmpty(Temp))
                    Naria = Temp + Place[Count] + Naria;
                if (Strings.Len(MyNumber) > 3)
                {
                    MyNumber = Strings.Left(MyNumber, Strings.Len(MyNumber) - 3);
                }
                else
                {
                    MyNumber = "";
                }
                Count = Count + 1;
            }

            if (Naria.Trim().EndsWith(" AND"))
            {
                Naria = Naria.Substring(0, Naria.Length - 1 - 4);
            }

            switch (Naria)
            {
                case "":
                    Naria = "";
                    break;
                case "One":
                    Naria = "One Naira";
                    break;
                default:
                    Naria = Naria + " Naira";
                    break;
            }
            switch (Kobo)
            {
                case "":
                    Kobo = "";
                    break;
                case "One":
                    Kobo = " and One Kobo";
                    break;
                default:
                    Kobo = " and " + Kobo + " Kobo";
                    break;
            }
            return Naria + Kobo + " only";
        }

        // Converts a number from 100-999 into text 
        public static string GetHundreds(string MyNumber)
        {
            string Result = "";
            if (Convert.ToInt32(MyNumber) == 0)
            {
                return string.Empty;
            }
            MyNumber = Strings.Right("000" + MyNumber, 3);
            // Convert the hundreds place.
            if (Strings.Mid(MyNumber, 1, 1) != "0")
            {
                Result = GetDigit(Strings.Mid(MyNumber, 1, 1)) + " Hundred ";
            }
            // Convert the tens and ones place.
            if (Strings.Mid(MyNumber, 2, 1) != "0")
            {
                if (string.IsNullOrEmpty(Result))
                {
                    Result = Result + GetTens(Strings.Mid(MyNumber, 2));
                }
                else
                {
                    Result = Result + "AND " + GetTens(Strings.Mid(MyNumber, 2));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Result))
                {
                    Result = Result + GetDigit(Strings.Mid(MyNumber, 3));
                }
                else
                {
                    Result = Result + "AND " + GetDigit(Strings.Mid(MyNumber, 3));
                }

            }
            return Result;
        }

        // Converts a number from 10 to 99 into text. 
        public static string GetTens(string TensText)
        {
            string Result = null;
            Result = "";
            // Null out the temporary function value.
            // If value between 10-19...
            if (Convert.ToInt32(Strings.Left(TensText, 1)) == 1)
            {
                switch (Convert.ToInt32(TensText))
                {
                    case 10:
                        Result = "Ten";
                        break;
                    case 11:
                        Result = "Eleven";
                        break;
                    case 12:
                        Result = "Twelve";
                        break;
                    case 13:
                        Result = "Thirteen";
                        break;
                    case 14:
                        Result = "Fourteen";
                        break;
                    case 15:
                        Result = "Fifteen";
                        break;
                    case 16:
                        Result = "Sixteen";
                        break;
                    case 17:
                        Result = "Seventeen";
                        break;
                    case 18:
                        Result = "Eighteen";
                        break;
                    case 19:
                        Result = "Nineteen";
                        break;
                    default:
                        break;
                }
                // If value between 20-99...
            }
            else
            {
                switch (Convert.ToInt32(Strings.Left(TensText, 1)))
                {
                    case 2:
                        Result = "Twenty ";
                        break;
                    case 3:
                        Result = "Thirty ";
                        break;
                    case 4:
                        Result = "Forty ";
                        break;
                    case 5:
                        Result = "Fifty ";
                        break;
                    case 6:
                        Result = "Sixty ";
                        break;
                    case 7:
                        Result = "Seventy ";
                        break;
                    case 8:
                        Result = "Eighty ";
                        break;
                    case 9:
                        Result = "Ninety ";
                        break;
                    default:
                        break;
                }
                Result = Result + GetDigit(Strings.Right(TensText, 1));
                // Retrieve ones place.
            }
            return Result;
        }

        // Converts a number from 1 to 9 into text. 
        public static string GetDigit(string Digit)
        {
            string functionReturnValue = null;
            switch (Convert.ToInt32(Digit))
            {
                case 1:
                    functionReturnValue = "One";
                    break;
                case 2:
                    functionReturnValue = "Two";
                    break;
                case 3:
                    functionReturnValue = "Three";
                    break;
                case 4:
                    functionReturnValue = "Four";
                    break;
                case 5:
                    functionReturnValue = "Five";
                    break;
                case 6:
                    functionReturnValue = "Six";
                    break;
                case 7:
                    functionReturnValue = "Seven";
                    break;
                case 8:
                    functionReturnValue = "Eight";
                    break;
                case 9:
                    functionReturnValue = "Nine";
                    break;
                default:
                    functionReturnValue = "";
                    break;
            }
            return functionReturnValue;
        }

        public static string ConvertObjectToJson(object myObject)
        {
            return JsonConvert.SerializeObject(myObject, Newtonsoft.Json.Formatting.Indented);
        }

    }


    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }

        public static T Peek<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o = tempData.Peek(key);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
    #endregion

}
