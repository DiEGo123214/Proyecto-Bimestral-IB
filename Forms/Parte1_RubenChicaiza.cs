using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RedSocial.Modelo;

namespace RedSocial
{
    public partial class Form1 : Form {
        private Dictionary<string, Usuario> _usuarios = new Dictionary<string, Usuario>();
        private Usuario _usuarioActual;
        private List<Publicacion> _publicacionesMostradas = new List<Publicacion>();

        public Form1() {
            InitializeComponent();
            groupBoxRedSocial.Visible = false;
            txtUsuarioLogin.Enabled = true;
            txtContrasenaLogin.Enabled = true;

            txtNombreReal.Enabled = false;
            txtNombreUsuario.Enabled = false;
            txtContrasenaRegistro.Enabled = false;
            btnRegistrar.Enabled = false;
        }

        private void btnRegistrar_Click_1(object sender, EventArgs e) {
            string nombreReal = txtNombreReal.Text.Trim();
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string contrasena = txtContrasenaRegistro.Text.Trim();

            if (string.IsNullOrEmpty(nombreReal) || string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                lblMensaje.Text = "Todos los campos son obligatorios.";
                return;
            }

            if (_usuarios.ContainsKey(nombreUsuario))
            {
                lblMensaje.Text = $"El nombre de usuario {nombreUsuario} ya existe.";
                txtNombreReal.Clear();
                txtNombreUsuario.Clear();
                txtContrasenaRegistro.Clear();
                return;
            }

            var nuevoUsuario = new Usuario(nombreReal, nombreUsuario, contrasena);
            _usuarios.Add(nombreUsuario, nuevoUsuario);

            MessageBox.Show($"Usuario {nombreUsuario} creado con éxito.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            limpiarCamposRegistro();

            txtUsuarioLogin.Enabled = true;
            txtContrasenaLogin.Enabled = true;
            txtNombreReal.Enabled = false;
            txtNombreUsuario.Enabled = false;
            txtContrasenaRegistro.Enabled = false;
            btnRegistrar.Enabled = false;
            btnIniciarSesion.Enabled = true;
            btnCrarUsuario.Enabled = true;

            lblMensaje.Text = "";
        }

        private void btnIniciarSesion_Click_1(object sender, EventArgs e) {
            string usuario = txtUsuarioLogin.Text.Trim();
            string contrasena = txtContrasenaLogin.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                lblLogin.Text = "Usuario y contraseña son obligatorios.";
                return;
            }

            if (_usuarios.TryGetValue(usuario, out Usuario u) && u.ValidarContrasena(contrasena))
            {
                _usuarioActual = u;
                lblBienvenida.Text = $"¡Bienvenido, {u.nombreUsuario}!";
                groupBoxRedSocial.Visible = true;
                ActualizarInterfazUsuario();
                MessageBox.Show("Sesión iniciada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMensaje.Text = "";
            }
            else
            {
                lblLogin.Text = "Usuario o contraseña incorrectos.";
            }
            limpairCamposLogin();
            txtContrasenaLogin.Enabled = false;
            txtUsuarioLogin.Enabled = false;
            btnIniciarSesion.Enabled = false;
            btnCrarUsuario.Enabled = false;
        }

        private void btnCerrarSesion_Click_1(object sender, EventArgs e) {
            _usuarioActual = null;
            groupBoxRedSocial.Visible = false;
            MessageBox.Show("Sesión cerrada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtContrasenaLogin.Enabled = true;
            txtUsuarioLogin.Enabled = true;
            btnIniciarSesion.Enabled = true;
            btnCrarUsuario.Enabled = true;
        }
    }
}
