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
    public partial class Frm_Users : Form
    {

        UserMethods usersMethods;

        public Frm_Users()
        {
            InitializeComponent();

            this.usersMethods = new UserMethods(Frm_Main.getStringConexion());
            this.cmbRol.SelectedIndex = 0;
        }

        //metodo que carga los datos del usuario
        public void cargarDatosUsuario(User pUser)
        {
            this.txtEmail.Text = pUser.usermail.ToString();
            this.txtTelefono.Text = pUser.phoneNumber.ToString();
            this.cmbRol.Text = pUser.rolUser.ToString();

        }


        private void Frm_Users_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //puede que tencha error en la parte de rol
            if (this.txtEmail.Text != string.Empty && this.txtTelefono.Text != String.Empty)
            {

                try
                {
                    if (this.usersMethods.autentificarCorreo(this.txtEmail.Text.Trim()) == false)
                    {
                        User user = new User();

                        user.usermail = this.txtEmail.Text.Trim();
                        user.phoneNumber = Int32.Parse(this.txtTelefono.Text.Trim());
                        user.rolUser = this.cmbRol.SelectedItem.ToString();

                        this.usersMethods.addUser(user);
                        this.usersMethods.sendPasswordEmail(user.usermail);
                        MessageBox.Show("se agrego");

                        this.limpiar();

                    }//fin del if que autentifica el correo
                    else
                    {
                        MessageBox.Show("ya existe");
                    }//fin del esle
                }//fin del try
                catch (Exception ex)
                {
                    throw ex;
                }//fin del catch
            }//fin del if que verifica que no este vacio el campo del email
        }//fin del metodo



        public void limpiar()
        {
            this.txtEmail.Clear();
            this.txtTelefono.Clear();
        }

        private void pictureCerra_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (this.txtEmail.Text != string.Empty)
            {
                try
                {
                    if (this.usersMethods.autentificarCorreo(this.txtEmail.Text.Trim()) == true)
                    {
                        User user = new User();

                        user.usermail = this.txtEmail.Text.Trim();
                        this.usersMethods.deleteUser(user);
                        MessageBox.Show("se elimino");

                        this.limpiar();

                    }
                    else
                    {
                        MessageBox.Show("no existe ese usuario");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }//fin del catch
            }//fin del if
        }//Fin de metodo

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (this.txtEmail.Text != string.Empty)
            {
                try
                {
                    if (this.txtTelefono.Text != string.Empty)
                    {

                        if (this.usersMethods.autentificarCorreo(this.txtEmail.Text.Trim()) == true)
                        {
                            User user = new User();

                            user.usermail = this.txtEmail.Text.Trim();
                            user.phoneNumber = Int32.Parse(this.txtTelefono.Text.Trim());
                            user.rolUser = this.cmbRol.SelectedItem.ToString();

                            this.usersMethods.userModification(user);
                            MessageBox.Show("Se modifico correctamente");

                            this.limpiar();

                        }//fin del if de autetificar
                        else
                        {
                            MessageBox.Show("no existe ese usuario");
                        }//fin del else
                    }//fin del if que verifica el campo del telefono
                    else
                    {
                        if (this.usersMethods.autentificarCorreo(this.txtEmail.Text.Trim()) == true)
                        {
                            User user = new User();
                            user = this.usersMethods.userSearch(this.txtEmail.Text);
                            this.cmbRol.Text = user.rolUser;
                            this.txtTelefono.Text = user.phoneNumber.ToString();

                        }//fin del if que autentifica2
                        else
                        {
                            MessageBox.Show("no existe ese usuario");
                        }//fin del else
                    }//fin del else
                }//fin del try
                catch (Exception ex)
                {
                    throw ex;
                }//fin del catch
            }//fin del id que verifica el que este vacio el campo del email
        }//Fin de metodo

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
