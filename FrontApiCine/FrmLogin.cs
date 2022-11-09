using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using FrontApiCine.Http;

namespace CineProyectoUTN.Formularios
{
    public partial class FrmLogin : Form
    {

        public static FrmLogin instancia = new FrmLogin();

        bool isLogged = false;

        public DialogResult idLogged;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtError.Text = "";

            LoginAsync();

            if (isLogged)
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                txtError.Text = "Error! Usuario o contraseña incorrecta.";
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }


        private async void LoginAsync()
        {
            string user = txtUsername.Text;
            string pass = txtPass.Text;

            var result = await ClientSingleton.GetInstance().GetAsync($"http://localhost:7046/login/{user}/{pass}");
            var lst = JsonConvert.DeserializeObject<bool>(result);

            isLogged = lst;
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
//Error! Usuario o contraseña incorrecta.