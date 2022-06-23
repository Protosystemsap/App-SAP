using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    //Driver SQL Server
    using System.Data.SqlClient;

    //Importación del BLL
    using BLL;

    //Importación de la libreria System.Data
    using System.Data;
    using System.Net.Mail;


    public class MethodsLogin
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
        public MethodsLogin(string cadenaConexion)
        {
            this.strConexion = cadenaConexion;

        }//Fin del constructor

        public bool autenticacionUsuario(User pUser)
        {
            try
            {
                bool autorizado = false;

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
                this.command.CommandText = "[USP_GES_AUTHENTICATION_USERS]";

                //Asignación de valores a cada parámetro
                this.command.Parameters.AddWithValue("@userMail", pUser.usermail);
                this.command.Parameters.AddWithValue("@password", pUser.password);

                //Realizar lectura de los datos del usuario
                this.dataReader = this.command.ExecuteReader();

                //Se pregunta si existen datos
                if (this.dataReader.Read())
                {
                    autorizado = true;
                }

                //Se cierran los recursos
                this.connection.Close();

                //Se liberan los recursos
                this.connection.Dispose();
                this.command.Dispose();

                return autorizado;

            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch
        }//Fin del método





    }//Fin de la clase
}//Fin del namespace
