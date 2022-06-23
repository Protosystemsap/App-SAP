using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BLL;
using DAL;

namespace Aplication_SAP
{
    public partial class Frm_Login : Form
    {

        //Referenciamos la clase metodos usuarios
        MethodsLogin methodsLogin;


        public Frm_Login()
        {
            InitializeComponent();

            this.methodsLogin = new MethodsLogin(Frm_Main.getStringConexion());
        }

       
       

        private void Frm_Login_Load(object sender, EventArgs e)
        {

        }

        private void labelForgotPassword_Click(object sender, EventArgs e)
        {
            try
            {
                //Variable de tipo formulario se crea una instancia
                Frm_SendCodevalidation frm = new Frm_SendCodevalidation();

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtEmail.Text != string.Empty && this.txtContrasena.Text != string.Empty)
            {
                try
                {
                    //Objeto usuario temporal
                    User temp = new User();

                    //Se extraen los datos de los campos de texto
                    temp.usermail = this.txtEmail.Text.Trim();
                    temp.password = this.txtContrasena.Text.Trim();

                    //Se realiza un intento de autenticación
                    if (this.intentoAutenticacion(temp))
                    {
                        //Cargamos el usuario en sesión
                       // Frm_Main.sesionUsuario = temp;

                        //Cerramos la ventana
                        this.Close();
                    }
                    else
                    {
                        throw new Exception("Login o contraseña incorrecta");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Complete los espacios en blanco", "Espacios Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento para verificar la autenticación del usuario
        /// </summary>
        /// <param name="temp">Usuario en el textbox</param>
        /// <returns>Si se autentica o no el usuario en la BD</returns>
        private bool intentoAutenticacion(User temp)
        {
            bool autenticado = false;

            //Autenticación BD
            if (this.methodsLogin.autenticacionUsuario(temp))
            {
                autenticado = true;
            }


            return autenticado;
        }//Fin del método


    }//Fin de la clase
}//Fin del namespace
