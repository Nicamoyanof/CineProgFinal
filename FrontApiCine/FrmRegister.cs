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
using DllCineAPi.Dominios;
using FrontApiCine.Http;
using Newtonsoft.Json;

namespace FrontApiCine
{
    public partial class FrmRegister : Form
    {
        List<Usuarios> lUsuarios = new List<Usuarios>();

        bool passOculta = true;
        bool repetirPassOculta = true;

        public FrmRegister()
        {
            InitializeComponent();
        }

        private async void FrmRegister_Load(object sender, EventArgs e)
        {
            await GetUsuariosAsync();

            pictureBox1.Image = Image.FromFile($"../../../Assets/logo.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            txtPass.UseSystemPasswordChar = true;
            txtRepetirPass.UseSystemPasswordChar = true;
            btnContra.BackgroundImage = Image.FromFile($"../../../Assets/mostrar.png");
            btnContra.BackgroundImageLayout = ImageLayout.Stretch;
            btnContra.TabStop = false;
            btnContra.FlatStyle = FlatStyle.Flat;
            btnContra.FlatAppearance.BorderSize = 0;
            btnRepetirContra.BackgroundImage = Image.FromFile($"../../../Assets/mostrar.png");
            btnRepetirContra.BackgroundImageLayout = ImageLayout.Stretch;
            btnRepetirContra.TabStop = false;
            btnRepetirContra.FlatStyle = FlatStyle.Flat;
            btnRepetirContra.FlatAppearance.BorderSize = 0;
        }

        private void btnContra_Click(object sender, EventArgs e)
        {
            VerPass(passOculta ,txtPass, btnContra);
        }

        private void VerPass(bool oculta, TextBox txt, Button btn)
        {
            if (oculta)
            {
                oculta = false;
                btn.BackgroundImage = Image.FromFile($"../../../Assets/ocultar.png");
                txt.UseSystemPasswordChar = false;
            }
            else
            {
                oculta = true;
                btn.BackgroundImage = Image.FromFile($"../../../Assets/mostrar.png");
                txt.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VerPass(repetirPassOculta ,txtRepetirPass, btnRepetirContra);
        }

        private bool ValidarRegistro()
        {
            if (!txtRepetirPass.Text.Equals(txtPass.Text))
            {
                txtError.Text = "ERROR! Las contraseñas no coinciden";
                return false;
            }
            else if (txtUsername.Text == "" || txtPass.Text == "" || txtRepetirPass.Text == "")
            {
                txtError.Text = "ERROR! Debe completar todos los campos";
                return false;
            }
            else
            {
                foreach (Usuarios user in lUsuarios)
                {
                    if (txtUsername.Text.Equals(user.Username))
                    {
                        txtError.Text = "ERROR! Ya existe un usuario con ese nombre";
                        return false;
                    }
                }
            }

            return true;

        }

        private async Task GetUsuariosAsync()
        {
            var result = await ClientSingleton.GetInstance().GetAsync("http://localhost:7046/usuarios");
            var lst = JsonConvert.DeserializeObject<List<Usuarios>>(result);

            lUsuarios = lst;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (ValidarRegistro())
            {
                Usuarios user = new Usuarios();
                user.Username = txtUsername.Text;
                user.Password = txtPass.Text;


                string bodyContent = JsonConvert.SerializeObject(user);
                var result = await ClientSingleton.GetInstance().PostAsync("http://localhost:7046/usuario", bodyContent);

                if (result.Equals("true"))
                {
                    txtError.Text = "USUARIO CREADO CON EXITO";
                    txtError.ForeColor = Color.Green;
                    System.Threading.Thread.Sleep(3000);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    txtError.Text = "Error en el servidor!";
                }
                
            }
            
        }
    }
}
