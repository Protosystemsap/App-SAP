using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.Net.Mail;

namespace DAL
{
     public class UserMethods
    {

        //Objetos para interactual con la base de datos
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter dataAdapter;
        private SqlDataReader dataReader;
        private DataSet dataSet;
        private string strConexion;

        private string backrestPassword;


        public UserMethods(string cadenaConexion)
        {
            this.strConexion = cadenaConexion;
        }//fin del contructor

        //Metodo para hacer la autentificacion

        public bool autentificarCorreo(String pCorreo)

        {
            try
            {
                bool autorizado = false;

                this.connection = new SqlConnection(this.strConexion);

                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;

                this.command.CommandType = System.Data.CommandType.StoredProcedure;

                this.command.CommandText = "[USP_GES_AP_VALIDATEMAIL]";

                this.command.Parameters.AddWithValue("@userMail", pCorreo);

                this.dataReader = this.command.ExecuteReader();

                if (this.dataReader.Read())
                {
                    autorizado = true;
                }

                this.connection.Close();
                this.connection.Dispose();
                this.command.Dispose();

                return autorizado;
            }
            catch (Exception ex)
            {

                throw ex;
            }//fin del try



        }//fin del metodo 

        public void userModification(User pUser)
        {
            try
            {
                //Iniciamos una conexión y le pasamos el string de conexión
                this.connection = new SqlConnection(this.strConexion);
                //Abrimos la conexión
                this.connection.Open();

                //Iniciamos un comando
                this.command = new SqlCommand();
                //Le pasamos la conexión al comando
                this.command.Connection = this.connection;
                //Le indicamos el tipo de comando de procedimiento almacenado
                this.command.CommandType = CommandType.StoredProcedure;
                //Le indicamos el nombre del procedimiento 
                this.command.CommandText = "[USP_GES_AP_ALTERUSER]";
                //Le indicamos los parámetros              
                this.command.Parameters.AddWithValue("@rolUser", pUser.rolUser);
                this.command.Parameters.AddWithValue("@phoneNumber", pUser.phoneNumber);
                //Ejecutamos el comando
                this.command.ExecuteNonQuery();

                //Limpiamos los parámetros
                this.command.Parameters.Clear();
                //Cerramos la conexión
                this.connection.Close();
                //Liberamos los recursos
                this.connection.Dispose();
                this.command.Dispose();
            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch
        }//Fin del método



        //metodo que borra al usuario
        public void deleteUser(User pUser)
        {
            try
            {
                //Iniciamos una conexión y le pasamos el string de conexión
                this.connection = new SqlConnection(this.strConexion);
                //Abrimos la conexión
                this.connection.Open();

                //Iniciamos un comando
                this.command = new SqlCommand();
                //Le pasamos la conexión al comando
                this.command.Connection = this.connection;
                //Le indicamos el tipo de comando de procedimiento almacenado
                this.command.CommandType = CommandType.StoredProcedure;
                //Le indicamos el nombre del procedimiento 
                this.command.CommandText = "[USP_GES_AP_DELETEUSER]";//hacer en bd
                //Le indicamos los parámetros
                this.command.Parameters.AddWithValue("@userMail", pUser.usermail);

                //Ejecutamos el comando
                this.command.ExecuteNonQuery();

                //Limpiamos los parámetros
                this.command.Parameters.Clear();
                //Cerramos la conexión
                this.connection.Close();
                //Liberamos los recursos
                this.connection.Dispose();
                this.command.Dispose();
            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch
        }//Fin del método

        //metodo que busca al usuario
        public User userSearch(string pMailUser)
        {
            try
            {
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
                this.command.CommandText = "[USP_GES_AP_USERSEARCH]";
                //Se asigna el parámetro
                this.command.Parameters.AddWithValue("@userMail",pMailUser);

                //Realizar la lectura del dato retornado
                this.dataReader = this.command.ExecuteReader();

                //Referencia para guardar el producto
                User user = new User();

                //Verificamos que existan filas
                if (this.dataReader.Read())
                {
                    //Se carga el objeto Producto

                    user.rolUser = this.dataReader["rolUser"].ToString();
                    user.phoneNumber = Convert.ToInt32(this.dataReader["phoneNumber"]);

                }
                //Se cierran los recursos
                this.connection.Close();

                //Se liberan los recursos
                this.connection.Dispose();
                this.command.Dispose();

                return user;

            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch

        }//fin del metodo

        //metodo que agrega al usuario
        public void addUser(User pUser)
        {
            try
            {
                this.backrestPassword = this.defaultPassword();

                this.connection = new SqlConnection(this.strConexion);

                this.connection.Open();

                this.command = new SqlCommand();

                this.command.Connection = this.connection;

                this.command.CommandType = CommandType.StoredProcedure;

                this.command.CommandText = "[USP_GES_AP_INSERTUSERS]";

                this.command.Parameters.AddWithValue("@userMail", pUser.usermail);
                this.command.Parameters.AddWithValue("@password", this.backrestPassword);
                this.command.Parameters.AddWithValue("@phoneNumber", pUser.phoneNumber);
                this.command.Parameters.AddWithValue("@rolUser", pUser.rolUser);

                this.command.ExecuteNonQuery();

                this.command.Parameters.Clear();

                this.connection.Close();

                this.connection.Dispose();

                this.command.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }//fin del catch


        }//fin del metodo

        //Crea la contraseña random

        public string defaultPassword()
        {
            //buscar la forma de hacerlo /\w/
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);
            Console.WriteLine(resultString);

            return resultString;
        }

        //Metodo que envia la contrasena al email del usuario
        public void sendPasswordEmail(string pUserMail)
        {
            try
            {
                String MailUser = "protosystemsap@gmail.com";
                string MailDestiny = pUserMail;
                string MialPassword = "ypxjgxkxrjpbnuyp";
                MailMessage mailMessage = new MailMessage(MailUser, MailDestiny, "Recuperar contraseña", "Código de validación :  " + this.backrestPassword
);

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
        }//fin del metodo


    }//Fin de clase
  }//Fin del namespace
