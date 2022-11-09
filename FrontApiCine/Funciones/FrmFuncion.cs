using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DllCineApi.Dominios;
using DllCineApi.Datos;
using DllCineApi.Fachada.Interfaz;
using DllCineApi.Fachada.Implementacion;
using FrontApiCine.Http;
using Newtonsoft.Json;

namespace CineProyectoUTN.Formularios
{
    public partial class Funcion : Form
    {

        List<Funciones> lFunciones = new List<Funciones>();
        List<Peliculas> lPeliculas = new List<Peliculas>();
        List<Salas> lSalas = new List<Salas>();
        List<Asientos> lAsientos = new List<Asientos>();
        List<GeneroPelicula> lGeneroPelicula = new List<GeneroPelicula>();
        List<EdadesPermitidas> lEdadesPermitidas = new List<EdadesPermitidas>();
        List<TipoSala> lTipoSala = new List<TipoSala>();

        IPeliculasApi daoPelicula;
        ISalasApi daoSala;
        IFuncionesApi daoFunc;

        public Funcion()
        {
            InitializeComponent();

            daoPelicula = new PeliculasApiImp();
            daoSala = new SalasApiImp();
            daoFunc = new FuncionesApiImp();

            for (int i = 1; i <= 20; i++)
            {
                lAsientos.Add(new Asientos(i, true));
            }
        }

        private void Funcion_Load(object sender, EventArgs e)
        {
            CargarTipoSalasAsync();
            CargarGenerosAsync();
            CargarEdadesAsync();
            CargarAsientos();
            CargarFuncionAsync();

        }

        private void CargarAsientos()
        {
            dgvAsientos.Rows.Clear();
            var C1 = new DataGridViewButtonColumn() { Name = "C1" };
            C1.FlatStyle = FlatStyle.Flat;
            C1.DefaultCellStyle.BackColor = Color.Red;

            int asiento = 0;
            for (int i = 0; i < lAsientos.Count() / 4; i++)
            {

                dgvAsientos.Rows.Add(
                lAsientos[asiento].NumeroAsiento,
                lAsientos[asiento + 1].NumeroAsiento,
                lAsientos[asiento + 2].NumeroAsiento,
                lAsientos[asiento + 3].NumeroAsiento);

                for (int j = 0; j < 4; j++)
                {

                    if (!lAsientos[j + i * 4].Disponible)
                    {
                        dgvAsientos.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgvAsientos.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    }
                }
                asiento += 4;
            }
        }

        public async Task CargarFuncionAsync()
        {
            dgvFunciones.Rows.Clear();

            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/funciones");
            var lst = JsonConvert.DeserializeObject<List<Funciones>>(result);
            if (lst != null)
            {
                foreach (var item in lst)
                {
                    var func = item as Funciones;
                    lFunciones.Add(func);
                }

                foreach (Funciones func in lFunciones)
                {
                    dgvFunciones.Rows.Add(func.IdFuncion, func.Pelicula.ToString(), func.Sala.ToString(), func.Horario);
                }
            }

        }

        public async Task<Peliculas> CargarPeliculaByIdAsync(int id)
        {

            var result = await ClientSingleton.GetInstance().GetAsync($"http://localhost:7046/peliculasById/{id}");
            var lst = JsonConvert.DeserializeObject<Peliculas>(result);

            return lst as Peliculas;

        }

        public async Task CargarGenerosAsync()
        {

            var result = await ClientSingleton.GetInstance().GetAsync($"http://localhost:7046/generos");
            var lst = JsonConvert.DeserializeObject<List<GeneroPelicula>>(result);
            if (lst != null)
            {
                foreach (var item in lst)
                {
                    GeneroPelicula gen = item as GeneroPelicula;
                    lGeneroPelicula.Add(gen);
                }
            }
        }

        public async Task CargarEdadesAsync()
        {

            var result = await ClientSingleton.GetInstance().GetAsync($"http://localhost:7046/edades-permitidas");
            var lst = JsonConvert.DeserializeObject<List<EdadesPermitidas>>(result);
            if (lst != null)
            {
                foreach (var item in lst)
                {
                    EdadesPermitidas edad = item as EdadesPermitidas;
                    lEdadesPermitidas.Add(edad);
                }
            }
        }

        public async Task<Salas> CargarSalaAsync(int id)
        {
            var result = await ClientSingleton.GetInstance().GetAsync($"http://localhost:7046/salas-by-id/{id}");
            var lst = JsonConvert.DeserializeObject<Salas>(result);

            return lst;
        }


        public async Task CargarTipoSalasAsync()
        {

            var result = await ClientSingleton.GetInstance().GetAsync($"http://localhost:7046/tipos-salas");
            var lst = JsonConvert.DeserializeObject<List<TipoSala>>(result);

            if (lst != null)
            {

                foreach (var item in lst)
                {
                    TipoSala tipoSala = item as TipoSala;
                    lTipoSala.Add(tipoSala);
                }
            }
        }

        private void dgvFunciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            HabilitarEdicion(false);

            lAsientos.Clear();

            for (int i = 1; i <= 20; i++)
            {
                lAsientos.Add(new Asientos(i, true));
            }

            int index = int.Parse(dgvFunciones.CurrentRow.Cells[0].Value.ToString());
            Funciones func = lFunciones[index - 1];
            txtPelicula.Text = func.Pelicula.ToString();
            txtSala.Text = func.Sala.ToString();
            dtpHorario.Value = func.Horario;
            AsientosOcupados(index);
        }

        public void AsientosOcupados(int id)
        {

            //Funciones func = null;

            //foreach (Funciones funciones in lFunciones)
            //{
            //    if (funciones.IdFuncion.Equals(id))
            //    {
            //        func = funciones;
            //    }
            //}

            //if(func != null)
            //{
            //    DataTable detalle_ticket = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Detalles_Tickets WHERE id_ticket = " + func.IdFuncion);

            //    foreach (DataRow dataRow in detalle_ticket.Rows)
            //    {
            //        DataTable asiento_sala = Helper.ObtenerInstancia().ConsultarSQLScript("SELECT * FROM Asientos_Por_Salas WHERE id_asiento_sala = " + dataRow["id_asiento_sala"].ToString());

            //        foreach (DataRow asrow in asiento_sala.Rows)
            //        {
            //            AsientoOcupado(int.Parse(asrow["id_asiento"].ToString()));
            //        }
            //    }
            //}


        }

        public void AsientoOcupado(int index)
        {
            lAsientos[index - 1].Disponible = false;
            CargarAsientos();
        }

        public void HabilitarEdicion(bool habilitar)
        {
            txtPelicula.Enabled = habilitar;
            txtSala.Enabled = habilitar;
            dtpHorario.Enabled = habilitar;

            btnCancelar.Enabled = true;
            btnEditar.Enabled = false;
            btnAgregar.Enabled = false;
            btnBorrar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            HabilitarEdicion(true);
        }


        private void dgvFunciones_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            HabilitarEdicion(false);

            lAsientos.Clear();

            for (int i = 1; i <= 20; i++)
            {
                lAsientos.Add(new Asientos(i, true));
            }

            int index = int.Parse(dgvFunciones.CurrentRow.Cells[0].Value.ToString());
            Funciones func = lFunciones[index - 1];
            txtPelicula.Text = func.Pelicula.ToString();
            txtSala.Text = func.Sala.ToString();
            dtpHorario.Value = func.Horario;
            AsientosOcupados(index);
        }

        private void entradasVendidasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmClienteFuncion frmClienteFuncion = new FrmClienteFuncion();
            frmClienteFuncion.Show();
        }
    }
}
