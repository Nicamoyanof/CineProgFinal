using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DllCineApi.Datos;
using DllCineApi.Dominios;
using Newtonsoft.Json;
using FrontApiCine.Http;

namespace CineProyectoUTN.Formularios
{
    public partial class FrmClienteFuncion : Form
    {

        public FrmClienteFuncion()
        {
            InitializeComponent();
        }

        private void FrmClienteFuncion_Load(object sender, EventArgs e)
        {
            CargarClienteFuncionAsync();
        }

        public async Task CargarClienteFuncionAsync()
        {

            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/funcion-cliente");
            var lst = JsonConvert.DeserializeObject<DataTable>(result);

            foreach (DataRow item in lst.Rows)
            {
                dgvClienteFuncion.Rows.Add(item["Nombre cliente"].ToString(), item[" pelicula"].ToString(), item["Cantidad de entradas compradas"].ToString(), item["Horario funcion"].ToString());

            }
        }

    }
}
