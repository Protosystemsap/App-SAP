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
    public partial class Frm_SendCodevalidation : Form
    {

        //Referenciamos la clase metodos usuarios
        MethodsForgotPassword methodsForgotPassword;
        //Variable para globalUserMail
        public string globalUserMail;

        public Frm_SendCodevalidation()
        {
            InitializeComponent();
            this.methodsForgotPassword = new MethodsForgotPassword(Frm_Main.getStringConexion());

        }


        private void Frm_SendCodevalidation_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// /Método encargado de actualizar el código de validación
        /// </summary>
        public void updateCodeValidation()
        {
            try
            {
                ForgotPassword forgotPassword = new ForgotPassword();
                forgotPassword.userMail = this.txtEmail.Text.Trim().ToString();
                this.methodsForgotPassword.updateCodeValidation(forgotPassword);
                //Evia código de validación al correo
                this.methodsForgotPassword.sendCodeValidationEmail(forgotPassword.userMail);
                this.globalUserMail = forgotPassword.userMail;
            }
            catch (Exception)
            {

                throw;
            }


        }//Fin de método

        /// <summary>
        /// Método encargado de actualizar el código de validación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnviar_Click(object sender, EventArgs e)

        {
            try
            {
                if (this.txtEmail.Text != string.Empty)
                {

                    this.updateCodeValidation();

                    Frm_codeValidation frm = new Frm_codeValidation(this);

                    frm.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Complete los espacios en blanco", "Espacios Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }//Fin de metodo

        /// <summary>
        /// Frm_SendCodevalidation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
