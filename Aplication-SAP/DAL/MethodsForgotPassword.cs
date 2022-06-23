using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Driver SQL Server
using System.Data.SqlClient;

//Importación del BLL
using BLL;

//Importación de la libreria System.Data
using System.Data;
using System.Net.Mail;

namespace DAL
{
    public class MethodsForgotPassword
    {
        //Objetos para interactuar con la DB
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter dataAdapter;
        private SqlDataReader dataReader;

        //variable que almacena codigo de validación
        public string backrestCodeValidation;

        //Objeto DataSet para almacenar una tabla de datos de un resultado de búsqueda
        private DataSet dataSet;

        //Variable para almacenar el string de conexión
        private string strConexion;

        /// <summary>
        /// Constructor por omisión recibe el string de conexión
        /// </summary>
        /// <param name="cadenaConexion">Conexión BD</param>
        public MethodsForgotPassword(string cadenaConexion)
        {
            this.strConexion = cadenaConexion;

        }//Fin del constructor

        public bool compareCodeValidation(ForgotPassword forgotPassword)
        {
            try
            {
                bool valido = false;

                //Se crea una instancia de la conexión
                this.connection = new SqlConnection(this.strConexion);

                // Se intenta abrir la conexión
                this.connection.Open();

                //Se instancia el comando
                this.command = new SqlCommand();

                //Se asigna la conexión
                this.command.Connection = this.connection;

                //Se indica el tipo de comando
                this.command.CommandType = System.Data.CommandType.StoredProcedure;

                //Se asigna el nombre del procedimiento almacenado
                this.command.CommandText = "USP_GES_AP_COMPARECODEVALIDATION";

                ////Asignación de valores a cada parámetro
                this.command.Parameters.AddWithValue("@userMail", forgotPassword.userMail);
                this.command.Parameters.AddWithValue("@codeValidation", forgotPassword.codeValidation);

                //Realizar lectura de los datos del usuario
                this.dataReader = this.command.ExecuteReader();

                //Se pregunta si existen datos
                if (this.dataReader.Read())
                {
                    valido = true;
                }

                //Se cierran los recursos
                this.connection.Close();
                //Limpiamos los parámetros
                this.command.Parameters.Clear();
                //Se liberan los recursos
                this.connection.Dispose();
                this.command.Dispose();

                return valido;

            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch
        }//Fin del método

        /// <summary>
        /// Método encargado de actulizar código de validación
        /// </summary>
        /// <param name="forgotPassword"></param>
        public void updateCodeValidation(ForgotPassword forgotPassword)
        {
            try
            {
                this.backrestCodeValidation = this.codeValidation();
                //Se crea una instancia de la conexión
                this.connection = new SqlConnection(this.strConexion);

                // Se intenta abrir la conexión
                this.connection.Open();

                //Se instancia el comando
                this.command = new SqlCommand();

                //Se asigna la conexión
                this.command.Connection = this.connection;

                //Se indica el tipo de comando
                this.command.CommandType = System.Data.CommandType.StoredProcedure;

                //Se asigna el nombre del procedimiento almacenado
                this.command.CommandText = "USP_GES_AP_UPDATECODEVALIDATION";

                ////Asignación de valores a cada parámetro
                this.command.Parameters.AddWithValue("@userMail", forgotPassword.userMail);
                this.command.Parameters.AddWithValue("@codeValidation", backrestCodeValidation);

                //Ejecutamos el comando
                this.command.ExecuteNonQuery();
                //Limpiamos los parámetros
                this.command.Parameters.Clear();
                //Se cierran los recursos
                this.connection.Close();

                //Se liberan los recursos
                this.connection.Dispose();
                this.command.Dispose();

              

            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch
        }//Fin del método




        /// <summary>
        /// Metodo encargado de genera codigo de validacion ramdon
        /// </summary>
        /// <returns></returns>
        public string codeValidation()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[6];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);
            Console.WriteLine(resultString);

            return resultString;
        }

       
        /// <summary>
        /// Método encargado de enviar código de validación al correo
        /// </summary>
        /// <param name="pUserMail"></param>
        public void sendCodeValidationEmail(string pUserMail)
        {
            try
            {
                String MailUser = "protosystemsap@gmail.com";
                string MailDestiny = pUserMail;
                string MialPassword = "ypxjgxkxrjpbnuyp";
                MailMessage mailMessage = new MailMessage(MailUser, MailDestiny, "Recuperar contraseña", "Código de validación :  "  +this.backrestCodeValidation);

                //Valido que tambiém MailMessage recibe Html
                mailMessage.IsBodyHtml = true;

                //Configura el servidor de correo Gmail
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(MailUser, MialPassword);

                smtp.Send(mailMessage);
                smtp.Dispose();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }//Fin de método


        public void createNewPassword(ForgotPassword forgotPassword)
        {
            try
            {
                this.backrestCodeValidation = this.codeValidation();
                //Se crea una instancia de la conexión
                this.connection = new SqlConnection(this.strConexion);

                // Se intenta abrir la conexión
                this.connection.Open();

                //Se instancia el comando
                this.command = new SqlCommand();

                //Se asigna la conexión
                this.command.Connection = this.connection;

                //Se indica el tipo de comando
                this.command.CommandType = System.Data.CommandType.StoredProcedure;

                //Se asigna el nombre del procedimiento almacenado
                this.command.CommandText = "USP_GES_AP_CREATEENEWPASSWORD";

                ////Asignación de valores a cada parámetro
                this.command.Parameters.AddWithValue("@userMail", forgotPassword.userMail);
                this.command.Parameters.AddWithValue("@password", forgotPassword.password);

                //Ejecutamos el comando
                this.command.ExecuteNonQuery();
                //Limpiamos los parámetros
                this.command.Parameters.Clear();
                //Se cierran los recursos
                this.connection.Close();

                //Se liberan los recursos
                this.connection.Dispose();
                this.command.Dispose();



            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch
        }//Fin del método

    }//Fin de clase

   
}


