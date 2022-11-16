using DllCineApi.Datos;
using DllCineApi.Datos.Interfaz;
using DllCineApi.Dominios;
using DllCineApi.Fachada.Implementacion;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Datos;
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

namespace CineProyectoUTN.Formularios;

public partial class FrmPersonal : Form
{
    private FrmClientes frmCli = new FrmClientes();
    List<Personal> lPersonal = new List<Personal>();
    List<Ciudades> lCiudades = new List<Ciudades>();
    List<TiposCargos> lCargos = new List<TiposCargos>();
    Personal empleadoSeleccionada;

    IPersonalApi daoPersonal;

    public FrmPersonal()
    {
        InitializeComponent();
        daoPersonal = new PersonalApiImp();
    }
    private async void FrmPersonal_Load(object sender, EventArgs e)
    {
        await CargarPersonalAsync();
        await CargarCiudadAsync();
        await CargarCargosAsync();
        menuStrip1.BackColor = Color.FromArgb(224, 30, 38);
        menuStrip1.ForeColor = Color.White;
    }

    private async Task CargarCargosAsync()
    {
        var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/tipos-cargos");
        var lst = JsonConvert.DeserializeObject<List<TiposCargos>>(result);
        lCargos = lst;
        cboCargo.DataSource = lst;
        cboCargo.DisplayMember = "Nombre";
        cboCargo.ValueMember = "Id_Tipo_Cargo";
    }
    private async Task CargarCiudadAsync()
    {
        var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/ciudades");
        var lst = JsonConvert.DeserializeObject<List<Ciudades>>(result);
        lCiudades = lst;
        cboCiudad.DataSource = lst;
        cboCiudad.DisplayMember = "Nombre";
        cboCiudad.ValueMember = "Id_Ciudad";
    }
    public async Task CargarPersonalAsync()
    {
        dataGridView1.Rows.Clear();
        var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/personal");
        var lst = JsonConvert.DeserializeObject<List<Personal>>(result);

        if (lst != null)
        {
            foreach (Personal personal in lst)
            {
                lPersonal.Add(personal);
                dataGridView1.Rows.Add(personal.IdEmpleado, personal.Nombre, personal.FechaNac, personal.Telefono ,personal.Cuil, personal.FechaIngreso, personal.Cargo.ToString(), personal.Ciudad.Nombre);

            }

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
    //private void vacacionesToolStripMenuItem_Click(object sender, EventArgs e)
    //{
    //    FrmEmpleadosVacaciones frmEmpleadosVacaciones = new FrmEmpleadosVacaciones();
    //    frmEmpleadosVacaciones.Show();
    //}
    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        //textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
        //textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
        //dateTimePicker1.Text = dataGridView1.SelectedCells[2].Value.ToString();
        //textBox3.Text = dataGridView1.SelectedCells[3].Value.ToString();
        //textBox4.Text = dataGridView1.SelectedCells[4].Value.ToString();
        //dateTimePicker2.Text = dataGridView1.SelectedCells[5].Value.ToString();
        //comboBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        //comboBox2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
    }

    private async void button3_Click(object sender, EventArgs e)
    {
        if (Validar())
        {
            Personal p = new Personal();
            p.Ciudad = lCiudades[cboCiudad.SelectedIndex ];
            p.Cargo = lCargos[cboCargo.SelectedIndex];
            p.Nombre = textBox1.Text;
            p.Cuil = textBox4.Text;
            p.Telefono = textBox3.Text;
            p.FechaIngreso = Convert.ToDateTime(dateTimePicker2.Text);
            p.FechaNac = Convert.ToDateTime(dateTimePicker1.Text);

            string bodyContent = JsonConvert.SerializeObject(p);

            string url = "http://localhost:7046/personal";
            var result = await ClientSingleton.GetInstance().PostAsync(url, bodyContent);

            if (result == "true")
            {
                MessageBox.Show("Personal cargado con Exito.");
                await CargarPersonalAsync();
            }
            else MessageBox.Show("Error al cargar al Empleado.");
        }
    }

    private bool Validar()
    {
        bool resulado = true;
        if (textBox1.Text.Equals(String.Empty))
        {
            resulado = false;
            MessageBox.Show("Debe ingresar el nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBox1.Focus();
        }
        if (textBox3.Text.Equals(String.Empty))
        {
            resulado = false;
            MessageBox.Show("Debe ingresar el numero de telefono.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBox3.Focus();
        }
        if (textBox4.Text.Equals(String.Empty))
        {
            resulado = false;
            MessageBox.Show("Debe ingresar el numero de CUIL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBox4.Focus();
        }
        if (cboCargo.SelectedIndex == -1)
        {
            resulado = false;
            MessageBox.Show("Debe seleccionar el cargo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cboCargo.Focus();
        }
        if (cboCiudad.SelectedIndex == -1)
        {
            resulado = false;
            MessageBox.Show("Debe seleccionar la ciudad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cboCiudad.Focus();
        }
        return resulado;
    }

    private async void button2_Click(object sender, EventArgs e)
    {
        if (Validar())
        {
            empleadoSeleccionada.Ciudad = lCiudades[cboCiudad.SelectedIndex];
            empleadoSeleccionada.Cargo = lCargos[cboCargo.SelectedIndex];
            empleadoSeleccionada.Nombre = textBox1.Text;
            empleadoSeleccionada.Cuil = textBox4.Text;
            empleadoSeleccionada.Telefono = textBox3.Text;
            empleadoSeleccionada.FechaIngreso = Convert.ToDateTime(dateTimePicker2.Text);
            empleadoSeleccionada.FechaNac = Convert.ToDateTime(dateTimePicker1.Text);

            string bodyContent = JsonConvert.SerializeObject(empleadoSeleccionada);

            string url = $"http://localhost:7046/personal/{empleadoSeleccionada.IdEmpleado}";
            var result = await ClientSingleton.GetInstance().PutAsync(url, bodyContent);

            if (result == "true")
            {
                MessageBox.Show("Personal editado con Exito.");
                await CargarPersonalAsync();
            }
            else MessageBox.Show("Error al editar al Empleado.");
        }
    }


    private async void SeleccionarEmpleado(Personal p)
    {
        cboCiudad.SelectedIndex = p.Ciudad.Id_Ciudad-1;
        cboCargo.SelectedIndex = p.Cargo.Id_Tipo_Cargo-1;
        textBox1.Text = p.Nombre;
        textBox4.Text = p.Cuil;
        textBox3.Text = p.Telefono;
        dateTimePicker2.Value = p.FechaIngreso;
        dateTimePicker1.Value = p.FechaNac;
    }


    private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
    {
        int index = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

        for (int i = 0; i < lPersonal.Count; i++)
        {
            if (lPersonal[i].IdEmpleado.Equals(index))
            {
                empleadoSeleccionada = lPersonal[i];
            }
        }

        SeleccionarEmpleado(empleadoSeleccionada);
    }

    private async void button4_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("Esta seguro de borrar este empleado?", "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        {
            string url = $"http://localhost:7046/personal/{empleadoSeleccionada.IdEmpleado}";
            var result = await ClientSingleton.GetInstance().DeleteAsync(url);

            if (result == "true")
            {
                MessageBox.Show("Empleado borrada", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarPersonalAsync();
            }
            else
            {
                MessageBox.Show("Error al borrar el empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}


