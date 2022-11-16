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
    public partial class FrmRecaudacionPelicula : Form
    {

        public FrmRecaudacionPelicula()
        {
            InitializeComponent();
        }

        private async void FrmRecaudacionPelicula_Load(object sender, EventArgs e)
        {
           await CargarPeliculasRecaudacionAsync();
        }

        public async Task CargarPeliculasRecaudacionAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/pelicula-recaudacion");
            var lst = JsonConvert.DeserializeObject<DataTable>(result);

            foreach (DataRow item in lst.Rows)
            {
                if (item["Dinero recaudado"].ToString().Equals(string.Empty))
                {
                    dvgRecaudacion.Rows.Add(item["Pelicula"].ToString(), item["Genero"].ToString(), "null");
                }
                else
                {
                    dvgRecaudacion.Rows.Add(item["Pelicula"].ToString(), item["Genero"].ToString(), item["Dinero recaudado"].ToString());
                }
            }
        }

        private void dvgRecaudacion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
