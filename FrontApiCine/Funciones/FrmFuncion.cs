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
        List<Label> lLabelAsientos = new List<Label>();
        List<GeneroPelicula> lGeneroPelicula = new List<GeneroPelicula>();
        List<EdadesPermitidas> lEdadesPermitidas = new List<EdadesPermitidas>();
        List<TipoSala> lTipoSala = new List<TipoSala>();
        Funciones funcionSeleccionada ;

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

            dtpHorario.Format = DateTimePickerFormat.Custom;
            dtpHorario.CustomFormat = "dd MMMM yyyy - HH:mm:ss";
        }

        private async void Funcion_Load(object sender, EventArgs e)
        {
            await CargarTipoSalasAsync();
            await CargarGenerosAsync();
            await CargarEdadesAsync();
            await CargarFuncionAsync();
            await GetPeliculasAsync();
            await GetSalasAsync();
            HabilitarRegistro(false);
            ListarAsientos();
            ResetAsientos();
            menuStrip1.BackColor = Color.FromArgb(224, 30, 38);
            menuStrip1.ForeColor = Color.White;
        }



        public async Task CargarFuncionAsync()
        {
            lFunciones.Clear();
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

        public async Task AsientosFuncAsync(int id)
        {
            var result = await ClientSingleton.GetInstance().GetAsync($"http://localhost:7046/asientos-ocupaoas-by-func/{id}");
            var lst = JsonConvert.DeserializeObject<List<Asientos>>(result);

            if (lst != null)
            {

                foreach (var item in lst)
                {
                    Asientos asiento = item as Asientos;
                    lAsientos.Add(asiento);
                }
            }



        }

        private void VerificarAsientos()
        {
            for (int i = 0; i < 20; i++)
            {
                if (!lAsientos[i].Disponible)
                {
                    lLabelAsientos[i].BackColor = Color.Red;
                    lLabelAsientos[i].ForeColor = Color.White;
                }
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            HabilitarRegistro(true);
        }


        private async void dgvFunciones_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ResetAsientos();
            HabilitarRegistro(true);

            lAsientos.Clear();

            int index = int.Parse(dgvFunciones.CurrentRow.Cells[0].Value.ToString());
            funcionSeleccionada = lFunciones[index - 1];
            cboPelicula.SelectedValue = funcionSeleccionada.Pelicula.IdPelicula;
            cboSala.SelectedValue = funcionSeleccionada.Sala.IdSala;
            dtpHorario.Value = funcionSeleccionada.Horario;
            await AsientosFuncAsync(index);
            VerificarAsientos();
        }

        private void entradasVendidasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmClienteFuncion frmClienteFuncion = new FrmClienteFuncion();
            frmClienteFuncion.Show();
        }

        private void ResetAsientos()
        {
            foreach (Label lb in lLabelAsientos)
            {
                lb.BackColor = Color.Chartreuse;
                lb.ForeColor = Color.Black;
            }
        }

        private void ListarAsientos()
        {
            lLabelAsientos.Add(a01);
            lLabelAsientos.Add(a02);
            lLabelAsientos.Add(a03);
            lLabelAsientos.Add(a04);
            lLabelAsientos.Add(a05);
            lLabelAsientos.Add(a06);
            lLabelAsientos.Add(a07);
            lLabelAsientos.Add(a08);
            lLabelAsientos.Add(a09);
            lLabelAsientos.Add(a10);
            lLabelAsientos.Add(a11);
            lLabelAsientos.Add(a12);
            lLabelAsientos.Add(a13);
            lLabelAsientos.Add(a14);
            lLabelAsientos.Add(a15);
            lLabelAsientos.Add(a16);
            lLabelAsientos.Add(a17);
            lLabelAsientos.Add(a18);
            lLabelAsientos.Add(a19);
            lLabelAsientos.Add(a20);
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarRegistro())
            {
                MessageBox.Show("ERROR. Debe completar todos los campos!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Funciones func = new Funciones(0, lPeliculas[cboPelicula.SelectedIndex+1], lSalas[cboSala.SelectedIndex+1] ,dtpHorario.Value);

                await AgregarFuncionAsync(func);
            }
        }

        public async Task GetPeliculasAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/peliculas");
            var lst = JsonConvert.DeserializeObject<List<Peliculas>>(result);

            if (lst != null)
            {

                foreach (var pelicula in lst)
                {
                    Peliculas peli = new Peliculas(pelicula.IdPelicula, pelicula.Nombre, pelicula.EdadMinima, pelicula.Genero, pelicula.Descripcion, pelicula.NombrePoster);
                    lPeliculas.Add(peli);

                }
            }

            cboPelicula.DataSource = lPeliculas;
            cboPelicula.DisplayMember = "Nombre";
            cboPelicula.ValueMember = "IdPelicula";
        }

        public async Task GetSalasAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/salas");
            var lst = JsonConvert.DeserializeObject<List<Salas>>(result);
            lSalas.Clear();

            if (lst != null)
            {
                lSalas = lst;
            }

            cboSala.DataSource = lSalas;
            cboSala.DisplayMember = "NumeroSala";
            cboSala.ValueMember = "IdSala";
        }

        public async Task AgregarFuncionAsync(Funciones func)
        {
            string bodyContent = JsonConvert.SerializeObject(func);

            string url = "http://localhost:7046/funcion";
            var result = await ClientSingleton.GetInstance().PostAsync(url, bodyContent);

            if (result.Equals("true"))//servicio.CrearPresupuesto(nuevo)
            {
                MessageBox.Show("Funcion agregada.", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ERROR. No se pudo registrar la funcion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HabilitarRegistro(bool habilitar)
        {
            btnAgregar.Enabled = !habilitar;
            btnCancelar.Enabled = true;
            btnEditar.Enabled = habilitar;
            btnBorrar.Enabled = habilitar;
        }

        private bool ValidarRegistro()
        {
            if (cboPelicula.SelectedIndex < 0)
            {
                return false;
            }
            if (cboSala.SelectedIndex < 0)
            {
                return false;
            }
            if (dtpHorario.Value == null)
            {
                return false;
            }

            return true;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            cboPelicula.SelectedIndex = 0;
            cboSala.SelectedIndex = 0;

            dtpHorario.Value = DateTime.Now;

            HabilitarRegistro(false);
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (ValidarRegistro())
            {
                funcionSeleccionada = new Funciones(funcionSeleccionada.IdFuncion, lPeliculas[cboPelicula.SelectedIndex], lSalas[cboSala.SelectedIndex], dtpHorario.Value);

                string bodyContent = JsonConvert.SerializeObject(funcionSeleccionada);

                string url = $"http://localhost:7046/funcion/{funcionSeleccionada.IdFuncion}";
                var result = await ClientSingleton.GetInstance().PutAsync(url, bodyContent);
                if (result.Equals("true"))//servicio.CrearPresupuesto(nuevo)
                {
                    MessageBox.Show("Pelicula agregada", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CargarFuncionAsync();
                }
                else
                {
                    MessageBox.Show("ERROR. No se pudo registrar la pelicula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("ERROR. Llena todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private async void btnBorrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de borrar esta funcion?", "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string url = $"http://localhost:7046/funcion/{funcionSeleccionada.IdFuncion}";
                var result = await ClientSingleton.GetInstance().DeleteAsync(url);

                if (result == "true")
                {
                    MessageBox.Show("Funcion borrada", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   await CargarFuncionAsync();
                }
                else
                {
                    MessageBox.Show("Error al borrar la funcion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
