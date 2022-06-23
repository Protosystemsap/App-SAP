using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Se referencia el DAL
using DAL;

//Se referencia el BLL
using BLL;


namespace Aplication_SAP
{
    public partial class Frm_Newpassword : Form
    {
        //Instancias de de Forms
        Frm_codeValidation Frm_codeValidation;
        Frm_SendCodevalidation Frm_SendCodevalidation;
        //Instancia de clase   MethodsForgotPassword
        MethodsForgotPassword methodsForgotPassword;

        /// <summary>
        /// Constructor por omisión que recibe la instancia del Frm_SendCodevalidation y Frm_codeValidation
        /// </summary>
        /// <param name="frm_CodeV"></param>
        /// <param name="frm_SendCode"></param>
        public Frm_Newpassword(Frm_codeValidation frm_CodeV, Frm_SendCodevalidation frm_SendCode)
        {
            InitializeComponent();
            this.Frm_codeValidation = frm_CodeV;
            this.Frm_SendCodevalidation = frm_SendCode;
            this.methodsForgotPassword = new MethodsForgotPassword(Frm_Main.getStringConexion());
        }//Fin de método

       

        private void Frm_Newpassword_Load(object sender, EventArgs e)
        {

        }

        private void pictureCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        /// <summary>
        /// Método encargado de crear una nueva contraseña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCrear_Click(object sender, EventArgs e)
        {
            string userMail = Frm_SendCodevalidation.globalUserMail;


            try
            {
                if (this.txtContrasenaNueva.Text.Trim().ToString().Equals(this.txtConfirmarcontrasena.Text.Trim().ToString()))
                {
                    ForgotPassword forgotPassword = new ForgotPassword();
                    forgotPassword.userMail = userMail;
                    forgotPassword.password = this.txtContrasenaNueva.Text.Trim().ToString();
                    methodsForgotPassword.createNewPassword(forgotPassword);
                    MessageBox.Show("Exitoso", "Su contraseña ha sido cambiada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Frm_codeValidation.Dispose();
                    Frm_SendCodevalidation.Dispose();
                }
                else if(this.txtContrasenaNueva.Text ==string.Empty || this.txtConfirmarcontrasena.Text == string.Empty)
                {
                    MessageBox.Show("Complete los espacios en blanco", "Espacios Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("La contraseñas no son iguales, por favor verefica las contraseñas", "Verefica contraseñas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

              
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
           


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.txtContrasenaNueva.Clear();
            this.txtConfirmarcontrasena.Clear();
          
        }
    }
}
