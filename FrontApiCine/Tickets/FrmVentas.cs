using DllCineApi.Datos;
using DllCineApi.Dominios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Fachada.Implementacion;
using Newtonsoft.Json;
using FrontApiCine.Http;

namespace CineProyectoUTN.Formularios
{
    public partial class FrmVentas : Form
    {

        List<Tickets> lTickets = new List<Tickets>();


        public FrmVentas()
        {
            InitializeComponent();


        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            //cargarCbo(cboFuncion, "SELECT * FROM Funciones", "horario", "id_funcion");
            //cargarCbo(cboCliente, "SELECT * FROM Clientes", "apellido", "id_cliente");
            //cargarCbo(cboEmpleado, "SELECT * FROM Empleados", "apellido_empleado", "id_empleado");
            //cargarCbo(cboFormaPago, "SELECT * FROM Tipos_Pagos", "nombre_tipo_pago", "id_tipo_pago");
            //cargarTickets();

            cargarFuncionesAsync();
            cargarClientesAsync();
            CargarDgvAsync();
            habilitar(true);

        }

        private void habilitar(bool v)
        {
            cboCliente.Enabled = !v;
            cboEmpleado.Enabled = !v;
            cboFormaPago.Enabled = !v;
            cboFuncion.Enabled = !v;
            txtPrecioEntrada.Enabled = !v;
            dtpFechaPago.Enabled = !v;
            nupTickets.Enabled = !v;

        }
        private async Task CargarDgvAsync()
        {
            //List<Tickets> lTickets = dao.ObtenerTickets();

            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/tickets");
            var lst = JsonConvert.DeserializeObject<List<Tickets>>(result);

            if (lst != null)
            {
                foreach (var ticket in lst)
                {
                    Tickets tkt = new Tickets(new Reservas(), ticket.Funciones, ticket.Personal, ticket.Clientes, ticket.fecha_ticket);
                    lTickets.Add(tkt);

                    dgvTickets.Rows.Add(ticket.Clientes, ticket.Personal, ticket.Funciones, ticket.fecha_ticket);
                }
            }
        }

        private async Task cargarClientesAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/clientes");
            var lst = JsonConvert.DeserializeObject<List<Clientes>>(result);

            if (lst != null)
            {


                foreach (Clientes cliente in lst)
                {
                    cboCliente.Items.Add(cliente.ToString());
                }
            }
        }

        private async Task cargarFuncionesAsync()
        {

            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/funciones");
            var lst = JsonConvert.DeserializeObject<List<Funciones>>(result);
            if (lst != null)
            {
                foreach (Funciones funcion in lst)
                {
                    cboFuncion.Items.Add(funcion.ToString());
                }
            }
        }
        private async Task cargarPersonalAsync()
        {

            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/funciones");
            var lst = JsonConvert.DeserializeObject<List<Personal>>(result);

            foreach (Personal personal in lst)
            {
                cboEmpleado.Items.Add(personal.ToString());
            }
        }
        private void cargarTipoPago()
        {

            //cboFormaPago.DataSource = Helper.ObtenerInstancia().ConsultarSQLScript("select * from tipos_pagos");
            //cboFormaPago.ValueMember = "id_tipo_pago";
            //cboFormaPago.DisplayMember = "nombre_tipo_pago";
        }

        //private void cargarCbo(ComboBox cbo, string select, string display, string value)
        //{
        //    cbo.DataSource = select;
        //    cbo.DisplayMember = display;
        //    cbo.ValueMember = value;
        //}

        //private void cargarTickets()
        //{
        //    DataTable tabla =
        //           Helper.ObtenerInstancia().ConsultarSQLScript
        //         ("select t.id_ticket 'ID Ticket',c.apellido +' '+ c.nombre 'Cliente',e.apellido_empleado +' '+ e.nombre_empleado 'Empleado', c.fecha_nac 'Fecha de Nacimiento Cliente'," +
        //         "p.nombre_pelicula 'Pelicula', f.horario 'Horario-Funcion', socio 'Socio', Nombre_ciudad 'Ciudad' " +
        //         "from tickets t join clientes c on c.id_cliente = t.id_cliente join ciudades ciu on c.id_ciudad=ciu.id_ciudad join empleados e on e.id_empleado=t.id_empleado join funciones f on f.id_funcion=t.id_funcion join peliculas p on p.id_pelicula=f.id_pelicula");
        //    dgvTickets.DataSource = tabla;
        //}

        private void filtroPorEdadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dgvTickets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void filtroPorEdadToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //FrmGastoClientexEdad frmFiltroporEdad = new FrmGastoClientexEdad();
            //frmFiltroporEdad.Show();
        }

        private void gastoPromedioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmGastoPromedioxCliente frmGastoPromedioxCliente = new FrmGastoPromedioxCliente();
            //frmGastoPromedioxCliente.Show();
        }

        private void dgvTickets_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            cboCliente.Text = dgvTickets.CurrentRow.Cells[0].Value.ToString();
            cboEmpleado.Text = dgvTickets.CurrentRow.Cells[1].Value.ToString();
            cboFuncion.Text = dgvTickets.CurrentRow.Cells[3].Value.ToString();
            cboFormaPago.Text = "Tarjeta de Credito";
            txtPrecioEntrada.Text = "1040";
            dtpFechaPago.Value = Convert.ToDateTime(dgvTickets.CurrentRow.Cells[3].Value);

            //DataTable dt = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT COUNT(*) 'cantidad' FROM DETALLES_TICKETS WHERE id_ticket = ");/*+ dgvTickets.CurrentRow.Cells[0].Value.ToString());*/
            //foreach (DataRow dr in dt.Rows)
            //{
            //    nupTickets.Value = int.Parse(dr["cantidad"].ToString());
            //}

            txtPrecioFinal.Text = (nupTickets.Value * int.Parse(txtPrecioEntrada.Text)).ToString();

        }
    }
}

