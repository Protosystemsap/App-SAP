using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Se referencia el DAL y BLL
using DAL;
using BLL;

//Importación de la libreria para utilizar el archivo de configuración
using Aplication_SAP.Properties;

namespace Aplication_SAP
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        //STRING DE CONEXION
        public static string getStringConexion()
        {
            return Settings.Default.stringconexion;

        }//Fin del método

        private void administrarProyectosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Variable de tipo formulario se crea una instancia
                Frm_Proyects frm = new Frm_Proyects();

                //Por medio del ShowDialog() mostramos el formulario de forma exclusiva en la pantalla
                frm.ShowDialog();

                //Liberamos los recursos del formulario
                frm.Dispose();

            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch 
        }

        /// <summary>
        /// Metódo encargado de mostrar la pantalla login de nuestro sistema
        /// </summary>
        private void mostrarPantallaLogin()
        {
            try
            {
                //Variable de tipo formulario se crea una instancia
                Frm_Login frm = new Frm_Login();

                //Por medio del ShowDialog() mostramos el formulario de forma exclusiva en la pantalla
                frm.ShowDialog();

                //Liberamos los recursos del formulario
                frm.Dispose();

            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch
        }//Fin del evento


        //-----------------------------------------------------------------//
        private void Frm_Main_Load(object sender, EventArgs e)
        {
            this.mostrarPantallaLogin();
        }

        private void listaProyectosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void agregarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Variable de tipo formulario se crea una instancia
                Frm_Users frm = new Frm_Users();

                //Por medio del ShowDialog() mostramos el formulario de forma exclusiva en la pantalla
                frm.ShowDialog();

                //Liberamos los recursos del formulario
                frm.Dispose();

            }//Fin del try
            catch (Exception ex)
            {
                throw ex;
            }//Fin del catch 
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
