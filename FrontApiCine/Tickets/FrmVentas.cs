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
        Tickets ticketsSeleccionada = new Tickets();
        List<Asientos> lAsientos = new List<Asientos>();
        List<Funciones> lFunciones = new List<Funciones>();
        List<Personal> lPersonal = new List<Personal>();
        List<Clientes> lClientes = new List<Clientes>();


        public FrmVentas()
        {
            InitializeComponent();


        }

        private async void FrmVentas_Load(object sender, EventArgs e)
        {
            //cargarCbo(cboFuncion, "SELECT * FROM Funciones", "horario", "id_funcion");
            //cargarCbo(cboCliente, "SELECT * FROM Clientes", "apellido", "id_cliente");
            //cargarCbo(cboEmpleado, "SELECT * FROM Empleados", "apellido_empleado", "id_empleado");
            //cargarCbo(cboFormaPago, "SELECT * FROM Tipos_Pagos", "nombre_tipo_pago", "id_tipo_pago");
            //cargarTickets();

            await cargarFuncionesAsync();
            await cargarClientesAsync();
            await CargarDgvAsync();
            await CargarEmpleadoAsync();
            habilitar(true);
            menuStrip1.BackColor = Color.FromArgb(224, 30, 38);
            menuStrip1.ForeColor = Color.White;
        }

        private void habilitar(bool v)
        {
            cboCliente.Enabled = v;
            cboEmpleado.Enabled = v;
            cboFormaPago.Enabled = v;
            cboFuncion.Enabled = v;
            txtPrecioEntrada.Enabled = v;
            dtpFechaPago.Enabled = v;
            nupTickets.Enabled = v;
            //btnAgregar.Enabled = !v;
            //btnBorrar.Enabled = !v;
            //btnEditar.Enabled = !v;

        }

        private async Task CargarEmpleadoAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/personal");
            var lst = JsonConvert.DeserializeObject<List<Personal>>(result);

            if (lst != null)
            {
                foreach (var item in lst)
                {
                    lPersonal.Add(item);
                }
            }

            cboEmpleado.DataSource = lPersonal;
            cboEmpleado.ValueMember = "IdEmpleado";
            cboEmpleado.DisplayMember = "Nombre";

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
                    lTickets.Add(ticket);

                    dgvTickets.Rows.Add(ticket.Clientes.ToString(), ticket.Personal.ToString(), ticket.Funciones, ticket.fecha_ticket, "Efectivo" );
                }
            }

            for (int i = 0; i < dgvTickets.Rows.Count; i++)
            {
                dgvTickets.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                if (i % 2 == 0)
                {
                    dgvTickets.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;

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
                    lClientes.Add(cliente);

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
                    cboFuncion.Items.Add(funcion.ToString() );
                    lFunciones.Add(funcion);
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

        private async Task AsientosVaciosAsync(int id)
        {
            lAsientos.Clear();

            var result = await ClientSingleton.GetInstance().GetAsync($"http://localhost:7046/asientos-ocupaoas-by-func/{id}");
            var lst = JsonConvert.DeserializeObject<List<Asientos>>(result);

            if (lst != null)
            {

                foreach (var item in lst)
                {
                    Asientos asiento = item as Asientos;
                    if (asiento.Disponible)
                    {
                        lAsientos.Add(asiento);
                    }
                }
            }
        }

        public async Task AgregarTicketAsync(Tickets ticket)
        {
            string bodyContent = JsonConvert.SerializeObject(ticket);

            string url = "http://localhost:7046/ticket";
            var result = await ClientSingleton.GetInstance().PostAsync(url, bodyContent);

            if (result.Equals("true"))//servicio.CrearPresupuesto(nuevo)
            {
                MessageBox.Show("Ticket agregado", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ERROR. No se pudo registrar el ticket", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            habilitar(true);

            if (VerificarCampos())
            {
                Tickets ticket = new Tickets();
                ticket.Clientes = lClientes[cboCliente.SelectedIndex];
                ticket.Personal = lPersonal[cboEmpleado.SelectedIndex];
                ticket.Funciones = lFunciones[cboFuncion.SelectedIndex];
                ticket.fecha_ticket = dtpFechaPago.Value;
                ticket.Reservas = new Reservas();

                List<DetallesTickets> dtList = new List<DetallesTickets>();

                for (int i = 0; i < nupTickets.Value; i++)
                {
                    Tipos_pagos tp = new Tipos_pagos();
                    tp.id_tipo_pago = cboFormaPago.SelectedIndex;

                    DetallesTickets dt = new DetallesTickets(0, 0, tp, float.Parse(txtPrecioEntrada.Text), 1, lAsientos[i]);
                    dtList.Add(dt);

                }
                ticket.DetallesTickets = dtList;

                await AgregarTicketAsync(ticket);

                await CargarTicketsAsync();

            }

        }

        private bool VerificarCampos()
        {


            List<bool> lVerificado = new List<bool>();


            lVerificado.Add(txtPrecioEntrada.Text == "" ? false : true);
            lVerificado.Add(cboCliente.SelectedIndex < 1 ? false : true);
            lVerificado.Add(cboEmpleado.SelectedIndex < 1 ? false : true);
            lVerificado.Add(cboFormaPago.SelectedIndex < 1 ? false : true);
            lVerificado.Add(cboFuncion.SelectedIndex < 1 ? false : true);
            lVerificado.Add(nupTickets.Value <= lAsientos.Count());

            foreach (bool verificar in lVerificado)
            {
                if (!verificar)
                {
                    return false;
                }
            }

            return true;


        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            ticketsSeleccionada.Clientes.Id_Cliente = cboCliente.SelectedIndex;
            ticketsSeleccionada.Personal.IdEmpleado = cboEmpleado.SelectedIndex;
            ticketsSeleccionada.Funciones.IdFuncion = cboFuncion.SelectedIndex;
            ticketsSeleccionada.fecha_ticket = dtpFechaPago.Value;
            //ticketsSeleccionada.Reservas.id_reserva=


        }

        private async Task CargarTicketsAsync()
        {
            dgvTickets.Rows.Clear();

            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/tickets");
            var lst = JsonConvert.DeserializeObject<List<Tickets>>(result);

            if (lst != null)
            {

                foreach (var tickets in lst)
                {
                    Tickets tkt = new Tickets(tickets.id_ticket, tickets.Reservas, tickets.Funciones, tickets.Personal, tickets.Clientes,  tickets.fecha_ticket,  tickets.DetallesTickets);
                    lTickets.Add(tkt);
                    dgvTickets.Rows.Add(tickets.Funciones, tickets.Clientes, tickets.Personal, tickets.fecha_ticket, tickets.Reservas);
                }
            }

            for (int i = 0; i < dgvTickets.Rows.Count; i++)
            {
                dgvTickets.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                if (i % 2 == 0)
                {
                    dgvTickets.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;

                }
            }
        }

        private async Task BorrarAsync()
        {
            if (MessageBox.Show("Esta seguro de borrar este ticket?", "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string url = $"http://localhost:7046/ticket/{ticketsSeleccionada.id_ticket}";
                var result = await ClientSingleton.GetInstance().DeleteAsync(url);

                if (result == "true")
                {
                    MessageBox.Show("Ticket borrado", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al borrar el ticket", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }


        }

        private async Task btnBorrar_ClickAsync(object sender, EventArgs e)
        {
            await BorrarAsync();
            await CargarTicketsAsync();

        }

        private async void cboFuncion_DisplayMemberChanged(object sender, EventArgs e)
        {
            
        }

        private async void cboFuncion_SelectedValueChanged(object sender, EventArgs e)
        {
            await AsientosVaciosAsync(lFunciones[cboFuncion.SelectedIndex].IdFuncion);
            txtAsientosLibres.Text = lAsientos.Count().ToString();
        }

        private void nupTickets_ValueChanged(object sender, EventArgs e)
        {
            float precio = float.Parse(txtPrecioEntrada.Text);
            float cant = (float)nupTickets.Value;

            txtPrecioFinal.Text = (precio * cant).ToString();
        }

        private void txtPrecioEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            int precio;
            int cant = (int)nupTickets.Value;
            Int32.TryParse(txtPrecioEntrada.Text, out precio);

            txtPrecioFinal.Text = (precio * cant).ToString();
        }
    }
}

