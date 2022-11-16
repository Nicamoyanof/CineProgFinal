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
using FrontApiCine;

namespace CineProyectoUTN.Formularios
{
    public partial class FrmLogin : Form
    {

        public static FrmLogin instancia = new FrmLogin();
        bool passOculta = true;

        bool isLogged = false;

        public DialogResult idLogged;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile($"../../../Assets/logo.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            txtPass.UseSystemPasswordChar = true;
            btnContra.BackgroundImage = Image.FromFile($"../../../Assets/mostrar.png");
            btnContra.BackgroundImageLayout = ImageLayout.Stretch;
            btnContra.TabStop = false;
            btnContra.FlatStyle = FlatStyle.Flat;
            btnContra.FlatAppearance.BorderSize = 0;
        }


        private async Task<bool> LoginAsync()
        {
            string user = txtUsername.Text;
            string pass = txtPass.Text;

            if(user == "" || pass == "")
            {
                return false;
            }
            var result = await ClientSingleton.GetInstance().GetAsync($"http://localhost:7046/login/{user}/{pass}");
            var lst = JsonConvert.DeserializeObject<bool>(result);

            isLogged = lst;
            return lst;
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            txtError.Text = "";

            if (await LoginAsync())
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                txtError.Text = "Error! Usuario o contraseña incorrecta.";
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (passOculta)
            {
                passOculta = false;
                btnContra.BackgroundImage = Image.FromFile($"../../../Assets/ocultar.png");
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                passOculta = true;
                btnContra.BackgroundImage = Image.FromFile($"../../../Assets/mostrar.png");
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmRegister registro = new FrmRegister();
            if(registro.ShowDialog() == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
//Error! Usuario o contraseña incorrecta.