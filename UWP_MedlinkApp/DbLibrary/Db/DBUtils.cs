using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DbLibrary.DB
{
    public class DBUtils
    {
        public static void crearParametre(DbCommand consulta, string nomParametre, object value, DbType tipus)
        {
            DbParameter parametre = consulta.CreateParameter();
            parametre.ParameterName = nomParametre;
            parametre.Value = value;
            parametre.DbType = tipus;
            consulta.Parameters.Add(parametre);
        }

        public static double ConvertToDouble(string Value)
        {
            if (Value == null)
            {
                return 0;
            }
            else
            {
                double OutVal;
                double.TryParse(Value, out OutVal);

                if (double.IsNaN(OutVal) || double.IsInfinity(OutVal))
                {
                    return 0;
                }
                return OutVal;
            }
        }

        public static T? readDB<T>(DbDataReader reader, string columna)
            where T : struct
        {
            if (!reader.IsDBNull(reader.GetOrdinal(columna)))
            {
                T valor = default(T);
                int numColumna = reader.GetOrdinal(columna);
                if (valor is decimal)
                {
                    valor = (T)(object)reader.GetDecimal(numColumna);
                }
                else if (valor is int)
                {
                    valor = (T)(object)reader.GetInt32(numColumna);
                }
                else if (valor is DateTime)
                {
                    valor = (T)(object)reader.GetDateTime(numColumna);
                }
                else
                {
                    throw new Exception("Tipus no suportat");
                }
                return valor;
            }
            else return null;
        }


        public static T readDBC<T>(DbDataReader reader, string columna)
            where T : class
        {
            if (!reader.IsDBNull(reader.GetOrdinal(columna)))
            {
                T valor = default(T);
                int numColumna = reader.GetOrdinal(columna);

                if (typeof(T) == typeof(String))
                {
                    valor = (T)(object)reader.GetString(numColumna);
                }
                else
                {
                    throw new Exception("Tipus no suportat");
                }
                return valor;
            }
            else return null;
        }

        private const string StringChars = "0123456789abcdef";
        public static string GenerateUniqueHexString(int length)
        {
            Random rand = new Random();
            var charList = StringChars.ToArray();
            string hexString = "";

            for (int i = 0; i < length; i++)
            {
                int randIndex = rand.Next(0, charList.Length);
                hexString += charList[randIndex];
            }

            return hexString;
        }

        public static bool SendEmail(string senderEmail, string senderPassword, string recieverEmail, string subject, string body)
        {
            try
            {
                // Configure the SMTP client with your email provider's settings
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                // Create a new email message
                MailMessage message = new MailMessage();
                message.From = new MailAddress(senderEmail);
                message.To.Add(recieverEmail);
                message.Subject = subject;
                message.Body = body;

                // Send the email
                client.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                // Handle any errors that occurred while trying to send the email
                // Display an error message or take appropriate action
                return false;
            }
        }
    }
}
