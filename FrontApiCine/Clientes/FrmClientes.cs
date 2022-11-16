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
        Clientes clienteSeleccionado;


        public FrmClientes()
        {
            InitializeComponent();
            
        }
        private async void FrmClientes_Load(object sender, EventArgs e)
        {
            await CargarClienteAsync();
            await CargarCiudadAsync();
            menuStrip1.BackColor = Color.FromArgb(224, 30, 38);
            menuStrip1.ForeColor = Color.White;
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
                    dataGridView1.Rows.Add( cliente.Id_Cliente,cliente.Nombre, cliente.FechaNac, cliente.Ciudad, cliente.Email==""?null: cliente.Email, cliente.Socio);
                }
                lClientes = lst;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                if (i % 2 == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;

                }
            }

        }
        private async Task CargarCiudadAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/ciudades");
            var lst = JsonConvert.DeserializeObject<List<Ciudades>>(result);
            lCiudades = lst;
            comboBox1.DataSource = lCiudades;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "Id_Ciudad";
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
            int index = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            for (int i = 0; i < lClientes.Count; i++)
            {
                if (lClientes[i].Id_Cliente.Equals(index))
                {
                    clienteSeleccionado = lClientes[i];
                }
            }

            SeleccionarCliente(clienteSeleccionado);

        }

        private async void SeleccionarCliente(Clientes c)
        {
            textBox1.Text = c.Nombre;
            textBox4.Text = c.Email;
            comboBox1.SelectedIndex = c.Ciudad.Id_Ciudad;
            dateTimePicker1.Value = c.FechaNac== Convert.ToDateTime("01/01/0001 0:00:00")?DateTime.Now: c.FechaNac;
            if (c.Socio)
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

        private async void button3_Click(object sender, EventArgs e)
        {
            habilitarBotones(true);
            if (textBox1.Text == string.Empty || textBox4.Text == string.Empty || comboBox1.Text.Equals(string.Empty)
                || dateTimePicker1.Value.Equals(string.Empty) || radioButton1.Text.Equals(string.Empty) ||
                radioButton2.Text.Equals(string.Empty))
            {
                MessageBox.Show("Debe completar todos los campos", "CONTROL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Clientes cliente = new Clientes();
                Ciudades ciudad = lCiudades[comboBox1.SelectedIndex];

                cliente.Nombre = textBox1.Text;
                cliente.Ciudad = ciudad;
                cliente.Email = textBox4.Text;
                cliente.FechaNac = dateTimePicker1.Value;
                if (radioButton2.Checked == true)
                {
                    cliente.Socio = true;
                }
                else if (radioButton1.Checked == true)
                {
                    cliente.Socio = false;
                }

                string bodyContent = JsonConvert.SerializeObject(cliente);

                string url = "http://localhost:7046/clientes";
                var result = await ClientSingleton.GetInstance().PostAsync(url, bodyContent);

                if (result == "true")
                {
                    MessageBox.Show("Personal cargado con Exito.");
                    await CargarClienteAsync();
                }
                else MessageBox.Show("Error al cargar al Empleado.");
               
                habilitarBotones(false);
            }
        }

        private void habilitarBotones(bool x)
        {
            textBox1.Enabled = x;
            textBox4.Enabled = x;
            comboBox1.Enabled = x;
            dateTimePicker1.Enabled = x;
            radioButton1.Enabled = x;
            radioButton2.Enabled = x;

        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            if (true)
            {
                clienteSeleccionado.Ciudad = lCiudades[comboBox1.SelectedIndex];
                clienteSeleccionado.Nombre = textBox1.Text;
                clienteSeleccionado.FechaNac = Convert.ToDateTime(dateTimePicker1.Text);
                clienteSeleccionado.Email = textBox4.Text;

                if (radioButton2.Checked)
                {
                    clienteSeleccionado.Socio = true;
                }
                else
                {
                    clienteSeleccionado.Socio = false;
                }

                string bodyContent = JsonConvert.SerializeObject(clienteSeleccionado);

                string url = $"http://localhost:7046/cliente/{clienteSeleccionado.Id_Cliente}";
                var result = await ClientSingleton.GetInstance().PutAsync(url, bodyContent);

                if (result == "true")
                {
                    MessageBox.Show("Cliente editado con Exito.");
                    await CargarClienteAsync();
                }
                else MessageBox.Show("Error al editar al cliente.");
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de borrar este cliente?", "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string url = $"http://localhost:7046/cliente/{clienteSeleccionado.Id_Cliente}";
                var result = await ClientSingleton.GetInstance().DeleteAsync(url);

                if (result == "true")
                {
                    MessageBox.Show("Cliente borrada", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarClienteAsync();
                }
                else
                {
                    MessageBox.Show("Error al borrar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}


