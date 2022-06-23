
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
    public partial class Frm_codeValidation : Form
    {
        //Referencia de Frm_SendCodevalidation
        Frm_SendCodevalidation Frm_SendCodevalidation;
        //Instancia de clase   MethodsForgotPassword
        MethodsForgotPassword methodsForgotPassword;

        /// <summary>
        /// Constructor por omisión que recibe la instancia del Frm_SendCodevalidation
        /// </summary>
        /// <param name="frm_SendCode"></param>
        public Frm_codeValidation(Frm_SendCodevalidation frm_SendCode)
        {
            InitializeComponent();
            this.methodsForgotPassword = new MethodsForgotPassword(Frm_Main.getStringConexion());
            this.Frm_SendCodevalidation = frm_SendCode;
        }

       
        /// <summary>
        /// Método encargado de verificar el código de validación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void btnEnviar_Click(object sender, EventArgs e)
        {
          
            try
            {
                string userMail = this.Frm_SendCodevalidation.globalUserMail;
                ForgotPassword forgotPassword = new ForgotPassword();
                forgotPassword.codeValidation = this.txtCodeValidation.Text.Trim().ToString();
                 forgotPassword.userMail = userMail;
                //Verefica si el código de validación es el mismo que el que se encuentra en la BD
                if (this.methodsForgotPassword.compareCodeValidation(forgotPassword))
                {
                    //Instancia de  Frm_Newpassword con paramatros de Frm_codeValidation y Frm_SendCodevalidation
                    //Me va permitir arrastra valores de los demás forms
                    Frm_Newpassword frm = new Frm_Newpassword(this, this.Frm_SendCodevalidation);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Código de validación incorrecto, por favor varefica el código", " Verefica código de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//Fin de métod

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Frm_codeValidation_Load(object sender, EventArgs e)
        {

        }
    }
}
