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

namespace DAL
{
    public class MethodsProjects
    {
        //Objetos para interactuar con la DB
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter dataAdapter;
        private SqlDataReader dataReader;

        //Objeto DataSet para almacenar una tabla de datos de un resultado de búsqueda
        private DataSet dataSet;

        //Variable para almacenar el string de conexión
        private string strConexion;

        /// <summary>
        /// Constructor por omisión recibe el string de conexión
        /// </summary>
        /// <param name="cadenaConexion">Conexión BD</param>
        public MethodsProjects(string cadenaConexion)
        {
            this.strConexion = cadenaConexion;

        }//Fin del constructor


        
        //Procedimiento Almacenado para agregar proyecto
        public void addProject(Project pProyecto)
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
                this.command.CommandText = "[USP_GES_AP_INSERTPROJECTS]";
                //Le indicamos los parámetros
                this.command.Parameters.AddWithValue("@projectCode", pProyecto.projectCode);
                this.command.Parameters.AddWithValue("@projectName", pProyecto.projectName);
                this.command.Parameters.AddWithValue("@description", pProyecto.description);
                this.command.Parameters.AddWithValue("@startDate", pProyecto.startDate);
                this.command.Parameters.AddWithValue("@endDate", pProyecto.endDate);

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


        //Procedimiento Almacenado para Eliminar proyecto
        public void deleteProject(string proCode)
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
                this.command.CommandText = "[USP_GES_AP_DELETEPROJECTS]";
                //Le indicamos los parámetros
                this.command.Parameters.AddWithValue("@projectCode", proCode);

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


        //Procedimiento Almacenado para Editar proyecto
        public void editProject(Project pProyecto)
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
                this.command.CommandText = "[USP_GES_AP_UPDATEPROJECTS]";
                //Le indicamos los parámetros
                this.command.Parameters.AddWithValue("@projectCode", pProyecto.projectCode);
                this.command.Parameters.AddWithValue("@projectName", pProyecto.projectName);
                this.command.Parameters.AddWithValue("@description", pProyecto.description);
                this.command.Parameters.AddWithValue("@startDate", pProyecto.startDate);
                this.command.Parameters.AddWithValue("@endDate", pProyecto.endDate);

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


        //Procedimiento Almacenado para verificar la Existencia de codigo de proyecto
        public bool searchCode(string pCode)
        {
            try
            {
                bool existe = false;

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
                this.command.CommandText = "[USP_GES_AP_SEARCHCODEPROJECT]";

                //Asignación de valores a cada parámetro
                this.command.Parameters.AddWithValue("@code", pCode);

                //Realizar lectura de los datos del cliente
                this.dataReader = this.command.ExecuteReader();

                //Se pregunta si existen datos
                if (this.dataReader.Read())
                {
                    existe = true;
                }

                //Se cierran los recursos
                this.connection.Close();

                //Se liberan los recursos
                this.connection.Dispose();
                this.command.Dispose();

                return existe;

            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch
        }//Fin del método

    }
}
