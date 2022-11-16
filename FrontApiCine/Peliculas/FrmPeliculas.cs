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
using DllCineApi.Utils;

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

        private async void FrmPeliculas_Load(object sender, EventArgs e)
        {
            await CargarGenerosAsync();
            await CargarPeliculasAsync();
            await CargarEdadesAsync();
            HabilitarEdicion(false);
            menuStrip1.BackColor = Color.FromArgb(224, 30, 38);
            menuStrip1.ForeColor = Color.White;
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

            for (int i = 0; i < dgvPeliculas.Rows.Count; i++)
            {
                dgvPeliculas.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                if (i % 2 == 0)
                {
                    dgvPeliculas.Rows[i].DefaultCellStyle.BackColor = Color.LightSkyBlue;

                }
            }

        }

        public async Task CargarGenerosAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/generos");
            var lst = JsonConvert.DeserializeObject<List<GeneroPelicula>>(result);
            lGeneros = lst;
            cboGeneros.DataSource = lst;
            cboGeneros.DisplayMember = "Nombre";
            cboGeneros.ValueMember = "IdGeneroPelicula";

        }

        public async Task CargarEdadesAsync()
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
            HabilitarEdicion(true);
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
            txtNombreImg.Text = pelicula.NombrePoster;
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
            HabilitarEdicion(true);
        }



        public void HabilitarEdicion(bool habilitar)
        {
            txtNombre.Enabled = true;
            cboGeneros.Enabled = true;
            txtDescripcion.Enabled = true;
            rbATP.Enabled = true;
            rbTrece.Enabled = true;
            rbQuince.Enabled = true;
            rbDieciocho.Enabled = true;
            btnCancelar.Enabled = true;
            btnEditar.Enabled = habilitar;
            btnAgregar.Enabled = !habilitar;
            btnBorrar.Enabled = habilitar;
            txtNombreImg.Enabled = !habilitar;
            btnAgregarImg.Enabled = !habilitar;
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
            HabilitarEdicion(false);
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

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (VerificarCampos())
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

                if (txtFileName.Text == "")
                {
                    MessageBox.Show("ERROR. Debe agregar una imagen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    File.Copy(txtFileName.Text, $"../../../Assets/Poster/{pelicula.NombrePoster}.jpg");
                    await AgregarPeliculaAsync(pelicula);


                    await CargarPeliculasAsync();
                    HabilitarEdicion(false);
                }

            }
            else
            {
                MessageBox.Show("ERROR. Debe completar todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public async Task AgregarPeliculaAsync(Peliculas peli)
        {
            string bodyContent = JsonConvert.SerializeObject(peli);

            string url = "http://localhost:7046/pelicula";
            var result = await ClientSingleton.GetInstance().PostAsync(url, bodyContent);

            if (result.Equals("true"))//servicio.CrearPresupuesto(nuevo)
            {
                MessageBox.Show("Pelicula agregada", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ERROR. No se pudo registrar la pelicula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async Task BorrarAsync()
        {
            if (MessageBox.Show("Esta seguro de borrar esta pelicula?", "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string url = $"http://localhost:7046/pelicula/{peliculaSeleccionada.IdPelicula}";
                var result = await ClientSingleton.GetInstance().DeleteAsync(url);

                if (result == "true")
                {
                    MessageBox.Show("Pelicula borrada", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al borrar la pelicula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }


        }

        private async void btnBorrar_Click(object sender, EventArgs e)
        {
            await BorrarAsync();
            await CargarPeliculasAsync();
            HabilitarEdicion(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image File(*.jpg)|*.jpg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = open.FileName;
                pictureBox1.Image = new Bitmap(open.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            string nomPost = peliculaSeleccionada.NombrePoster;


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

            peliculaSeleccionada.Nombre = txtNombre.Text;
            peliculaSeleccionada.Descripcion = txtDescripcion.Text;
            peliculaSeleccionada.NombrePoster = txtNombreImg.Text;
            peliculaSeleccionada.EdadMinima = edad;
            peliculaSeleccionada.Genero = lGeneros[cboGeneros.SelectedIndex];

            if (await EditarPeliculaAsync())//servicio.CrearPresupuesto(nuevo)
            {
                MessageBox.Show("Pelicula agregada", "Informe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarPeliculasAsync();
                HabilitarEdicion(false);
            }
            else
            {
                MessageBox.Show("ERROR. No se pudo registrar la pelicula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> EditarPeliculaAsync()
        {


            string bodyContent = JsonConvert.SerializeObject(peliculaSeleccionada);

            string url = $"http://localhost:7046/pelicula/{peliculaSeleccionada.IdPelicula}";
            var result = await ClientSingleton.GetInstance().PutAsync(url, bodyContent);
            if (result.Equals("true"))//servicio.CrearPresupuesto(nuevo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool VerificarCampos()
        {
            List<bool> lVerificado = new List<bool>();
            lVerificado.Add(rbATP.Checked || rbTrece.Checked || rbQuince.Checked || rbDieciocho.Checked);

            lVerificado.Add(txtNombre.Text == "" ? false : true);
            lVerificado.Add(txtDescripcion.Text == "" ? false : true);
            lVerificado.Add(txtNombreImg.Text == "" ? false : true);
            lVerificado.Add(cboGeneros.SelectedIndex < 1 ? false : true);

            foreach (bool verificar in lVerificado)
            {
                if (!verificar)
                {
                    return false;
                }
            }

            return true;

        }


    }

}

