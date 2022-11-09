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
    public partial class FrmPeliculas : Form
    {

        List<Peliculas> lPeliculas = new List<Peliculas>();
        List<EdadesPermitidas> lEdades = new List<EdadesPermitidas>();
        List<GeneroPelicula> lGeneros = new List<GeneroPelicula>();
        Peliculas peliculaSeleccionada;
        IPeliculasApi dao;


        public FrmPeliculas()
        {
            InitializeComponent();
            dao = new PeliculasApiImp();
        }

        private void FrmPeliculas_Load(object sender, EventArgs e)
        {
            CargarGenerosAsync();
            CargarPeliculasAsync();
            CargarEdadesAsync();
            HabilitarEdicion(true);
        }

        private void recaudacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRecaudacionPelicula frmRecaudacionPelicula = new FrmRecaudacionPelicula();
            frmRecaudacionPelicula.Show();
        }

        public async Task CargarPeliculasAsync()
        {
            dgvPeliculas.Rows.Clear();

            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/peliculas");
            var lst = JsonConvert.DeserializeObject<List<Peliculas>>(result);

            if (lst != null)
            {

                foreach (var pelicula in lst)
                {
                    Peliculas peli = new Peliculas(pelicula.IdPelicula, pelicula.Nombre, pelicula.EdadMinima, pelicula.Genero, pelicula.Descripcion, pelicula.NombrePoster);
                    lPeliculas.Add(peli);
                    dgvPeliculas.Rows.Add(pelicula.IdPelicula, pelicula.Nombre, pelicula.Genero.ToString(), pelicula.EdadMinima.ToString());
                }
            }
        }

        public async void CargarGenerosAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/generos");
            var lst = JsonConvert.DeserializeObject<List<GeneroPelicula>>(result);
            lGeneros = lst;
            cboGeneros.DataSource = lst;
            cboGeneros.DisplayMember = "Nombre";
            cboGeneros.ValueMember = "IdGeneroPelicula";

        }

        public async void CargarEdadesAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/edades-permitidas");
            var lst = JsonConvert.DeserializeObject<List<EdadesPermitidas>>(result);
            if (lst != null)
            {

                foreach (var edades in lst)
                {
                    EdadesPermitidas edad = new EdadesPermitidas(edades.IdEdadMinima, edades.Nombre, edades.Edad);
                    lEdades.Add(edad);
                }
            }


        }

        private void dgvPeliculas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = int.Parse(dgvPeliculas.CurrentRow.Cells[0].Value.ToString());

            for (int i = 0; i < lPeliculas.Count; i++)
            {
                if (lPeliculas[i].IdPelicula.Equals(index))
                {
                    peliculaSeleccionada = lPeliculas[i];
                }
            }

            SeleccionarPelicula(peliculaSeleccionada);
            SeleccionarPoster(peliculaSeleccionada);
        }

        private void SeleccionarPelicula(Peliculas pelicula)
        {
            txtNombre.Text = pelicula.Nombre;
            cboGeneros.SelectedItem = pelicula.Genero.Nombre;
            txtDescripcion.Text = pelicula.Descripcion;
            switch (pelicula.EdadMinima.Edad)
            {
                case 0:
                    rbATP.Checked = true;
                    break;
                case 13:
                    rbTrece.Checked = true;
                    break;
                case 15:
                    rbQuince.Checked = true;
                    break;
                case 18:
                    rbDieciocho.Checked = true;
                    break;
                default:
                    break;
            }
            HabilitarEdicion(false);
        }



        public void HabilitarEdicion(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            cboGeneros.Enabled = habilitar;
            txtDescripcion.Enabled = habilitar;
            rbATP.Enabled = habilitar;
            rbTrece.Enabled = habilitar;
            rbQuince.Enabled = habilitar;
            rbDieciocho.Enabled = habilitar;
            btnCancelar.Enabled = true;
            btnEditar.Enabled = false;
            btnAgregar.Enabled = habilitar;
            btnBorrar.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            cboGeneros.SelectedIndex = -1;
            txtDescripcion.Text = string.Empty;
            rbATP.Checked = false;
            rbTrece.Checked = false;
            rbQuince.Checked = false;
            rbDieciocho.Checked = false;
            pictureBox1.Image = null;
            HabilitarEdicion(true);
        }

        private void SeleccionarPoster(Peliculas peliculas)
        {

            if (File.Exists($"../../../Assets/Poster/{peliculas.NombrePoster}.jpg"))
            {
                pictureBox1.Image = Image.FromFile($"../../../Assets/Poster/{peliculas.NombrePoster}.jpg");

                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            EdadesPermitidas edad;
            if (rbATP.Checked == true)
            {
                edad = lEdades[0];
            }
            else if (rbTrece.Checked == true)
            {
                edad = lEdades[1];
            }
            else if (rbQuince.Checked == true)
            {
                edad = lEdades[2];
            }
            else
            {
                edad = lEdades[3];
            }

            Peliculas pelicula = new Peliculas();
            pelicula.Nombre = txtNombre.Text;
            pelicula.Descripcion = txtDescripcion.Text;
            pelicula.NombrePoster = txtNombreImg.Text;
            pelicula.EdadMinima = edad;
            pelicula.Genero = lGeneros[cboGeneros.SelectedIndex];

            AgregarPeliculaAsync(pelicula);

        }

        public async Task AgregarPeliculaAsync(Peliculas peli)
        {
            string bodyContent = JsonConvert.SerializeObject(peli);

            string url = "http://localhost:7046/pelicula";
            var result = await ClientSingleton.GetInstance().PostAsync(url, bodyContent);

            if (result.Equals("true"))//servicio.CrearPresupuesto(nuevo)
            {
                MessageBox.Show("Pelicula agregada", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("ERROR. No se pudo registrar la pelicula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async Task BorrarAsync()
        {

            string url = $"http://localhost:7046/pelicula/{peliculaSeleccionada.IdPelicula}";
            var result = await ClientSingleton.GetInstance().DeleteAsync(url);

            if (result == "true")
            {
                MessageBox.Show("Pelicula borrada", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            BorrarAsync();
            CargarPeliculasAsync();
        }
    }

}

