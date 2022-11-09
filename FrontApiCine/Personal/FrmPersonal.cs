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

    IPersonalApi daoPersonal;

    public FrmPersonal()
    {
        InitializeComponent();
        daoPersonal = new PersonalApiImp();
    }
    private void FrmPersonal_Load(object sender, EventArgs e)
    {
        CargarPersonalAsync();
        CargarCiudadAsync();
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
                dataGridView1.Rows.Add(personal.Nombre, personal.Apellido, personal.FechaNac, personal.Telefono ,personal.Cuil, personal.FechaIngreso, personal.Cargo.ToString(), personal.Ciudad.Nombre);

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
}


