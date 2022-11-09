using DllCineApi.Datos;
using DllCineApi.Datos.Implementacion;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Dominios;
using DllCineApi.Fachada.Implementacion;
using DllCineApi.Fachada.Interfaz;
using FrontApiCine.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CineProyectoUTN.Formularios
{
    public partial class FrmClientes : Form
    {
        List<Clientes> lClientes = new List<Clientes>();
        List<Ciudades> lCiudades = new List<Ciudades>();


        public FrmClientes()
        {
            InitializeComponent();
            
        }
        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CargarClienteAsync();
            CargarCiudadAsync();
        }
        private async Task CargarClienteAsync()
        {
            dataGridView1.Rows.Clear();
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/clientes");
            var lst = JsonConvert.DeserializeObject<List<Clientes>>(result);

            if (lst != null)
            {
                foreach (Clientes cliente in lst)
                {
                    dataGridView1.Rows.Add(cliente.Nombre, cliente.Apellido, cliente.FechaNac, cliente.Ciudad, cliente.Email, cliente.Socio);
                }
            }
        }
        private async Task CargarCiudadAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/ciudades");
            var lst = JsonConvert.DeserializeObject<List<Ciudades>>(result);
            lCiudades = lst;
            comboBox1.DataSource = lst;
            comboBox1.DisplayMember = "Nombre_ciudad";
            comboBox1.ValueMember = "Idciudad";
        }
        //private void cantidadDeSociosToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    FrmCantidadSocios frmCantidadSocios = new FrmCantidadSocios();
        //    frmCantidadSocios.Show();
        //}
        private void clientesExtranjerosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmClientesExtranjeros frmClientesExtranjeros = new FrmClientesExtranjeros();
            //frmClientesExtranjeros.Show();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
            textBox4.Text = dataGridView1.SelectedCells[4].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[5].Value.Equals(true))
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}


