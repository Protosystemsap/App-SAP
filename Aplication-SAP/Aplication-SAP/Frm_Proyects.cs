using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//Referencia del BLL y DAL
using DAL;
using BLL;

namespace Aplication_SAP
{
    public partial class Frm_Proyects : Form
    {
        //Referencia a la clase de metodos de los proyectos
        MethodsProjects metodosProyectos;

        //Constructor sin parametros
        public Frm_Proyects()
        {
            InitializeComponent();
            this.metodosProyectos = new MethodsProjects(Frm_Main.getStringConexion());
        }

        /// <summary>
        /// Constructor con parámetros que carga los datos
        /// del producto seleccionado en la ventana de 
        /// consulta
        /// </summary>
        public Frm_Proyects(Project project)
        {
            InitializeComponent();

            //Instanciamos la clase métodos proyectos
             this.metodosProyectos = new MethodsProjects(Frm_Main.getStringConexion());

            //Cargamos los componentes con los datos
             this.chargeDataProject(project);

        }//Fin del constructor

        public void chargeDataProject(Project project)
        {
            //Cargamos los campos de texto
            this.txtCodeProyecto.Text = project.projectCode.ToString();
            this.txtNombreProyecto.Text = project.projectName.ToString();
            this.txtDescripcion.Text = project.description.ToString();
            this.dateTimeStart.Text = project.startDate.ToString();
            this.dateTimeEnd.Text = project.endDate.ToString();

        }//Fin del método

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            //Verificamos que no hayan espacios vacíos
            if (this.txtCodeProyecto.Text != string.Empty && txtNombreProyecto.Text != string.Empty && txtDescripcion.Text != string.Empty && this.dateTimeStart.Text != string.Empty && this.dateTimeEnd.Text != string.Empty)
            {
                try
                {
                    //Verificamos que no exista el producto
                    if (this.metodosProyectos.searchCode(this.txtCodeProyecto.Text) == false)
                    {
                        //Referenciamos un nuevo objeto producto
                        Project project = new Project();

                        //Cargamos los datos
                        project.projectCode = Convert.ToInt32(this.txtCodeProyecto.Text.Trim());
                        project.projectName = this.txtNombreProyecto.Text.Trim().ToString();
                        project.description = this.txtDescripcion.Text.Trim().ToString();
                        project.startDate = Convert.ToDateTime(this.dateTimeStart.Text);
                        project.endDate = Convert.ToDateTime(this.dateTimeEnd.Text);


                        //Ejecutamos el método agregar
                        this.metodosProyectos.addProject(project);

                        MessageBox.Show("Proyecto agregado de forma exitosa", "Proyecto agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Limpiamos los componentes
                        this.cleanTxt();

                    }//Fin del if
                    else
                    {
                        MessageBox.Show("El Código ingresado ya se encuentra registrado en el sistema", "Código existente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.cleanTxt();
                    }//Fin del else
                }//Fin del try
                catch (Exception ex)
                {
                    throw ex;
                }//Fin del catch
            }//Fin del if
            else
            {
                MessageBox.Show("Complete todos los espacios", "Espacios vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//Fin del else
        }//Fin Metodo


        private void BtnEditProject_Click(object sender, EventArgs e)
        {
            //Verificamos que no hayan espacios vacíos
            if (this.txtCodeProyecto.Text != string.Empty && txtNombreProyecto.Text != string.Empty && txtDescripcion.Text != string.Empty && this.dateTimeStart.Text != string.Empty && this.dateTimeEnd.Text != string.Empty)
            {
                try
                {
                    //Verificamos que no exista el producto
                    if (this.metodosProyectos.searchCode(this.txtCodeProyecto.Text))
                    {
                        //Referenciamos un nuevo objeto producto
                        Project project = new Project();

                        //Cargamos los datos
                        project.projectCode = Convert.ToInt32(this.txtCodeProyecto.Text.Trim());
                        project.projectName = this.txtNombreProyecto.Text.Trim().ToString();
                        project.description = this.txtDescripcion.Text.Trim().ToString();
                        project.startDate = Convert.ToDateTime(this.dateTimeStart.Text);
                        project.endDate = Convert.ToDateTime(this.dateTimeEnd.Text);


                        //Ejecutamos el método editar
                        this.metodosProyectos.editProject(project);

                        MessageBox.Show("Proyecto editado de forma exitosa", "Proyecto editado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Limpiamos los componentes
                        this.cleanTxt();

                    }//Fin del if
                    else
                    {
                        MessageBox.Show("El Código a editar no existe", "Verefica el código que desea editar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.cleanTxt();
                    }//Fin del else
                }//Fin del try
                catch (Exception ex)
                {
                    throw ex;
                }//Fin del catch
            }//Fin del if
            else
            {
                MessageBox.Show("Complete todos los espacios", "Espacios vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }//Fin del else
        }//Fin Metodo


        private void BtnDeleteProject_Click(object sender, EventArgs e)
        {
            //Cuadro de dialogo de confirmación de operación de eliminar
            DialogResult opcion;
            opcion = MessageBox.Show("Confirmar operación de eliminar", "Confirmar Operación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (opcion == DialogResult.OK)
            {

                //Verificamos que no hayan espacios vacíos
                if (this.txtCodeProyecto.Text != string.Empty)
                {
                    try
                    {
                        //Verificamos que exista el  producto
                        if (this.metodosProyectos.searchCode(this.txtCodeProyecto.Text.Trim()) == true)
                        {

                            //Ejecutamos el método eliminar
                            this.metodosProyectos.deleteProject(this.txtCodeProyecto.Text.Trim());

                            MessageBox.Show("Proyecto eliminado de forma exitosa", "Proyecto eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Limpiamos los componentes
                            this.cleanTxt();

                        }//Fin del if
                        else
                        {
                            MessageBox.Show("El Código ingresado no se encuentra registrado en el sistema", "Código inexistente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.cleanTxt();
                        }//Fin del else
                    }//Fin del try
                    catch (Exception ex)
                    {
                        throw ex;
                    }//Fin del catch
                }//Fin del if
                else
                {
                    MessageBox.Show("Complete todos los espacios", "Espacios vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }//Fin del else
            }//Fin del if
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.cleanTxt();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Método para limpir los componentes de la ventana
        /// </summary>
        public void cleanTxt()
        {
            this.txtCodeProyecto.Clear();
            this.txtNombreProyecto.Clear();
            this.txtDescripcion.Clear();

        }//Fin del método

        private void Frm_Proyects_Load(object sender, EventArgs e)
        {
            
        }
    }
}
