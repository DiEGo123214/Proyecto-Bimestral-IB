namespace RedSocial
{
    public partial class Form1 : Form {
        private void ActualizarSolicitudes() {
            listBoxSolicitudes.Items.Clear();
            foreach (var s in _usuarioActual.Solicitudes)
            {
                listBoxSolicitudes.Items.Add(s.nombreUsuario);
            }
        }

        private void btnVerAmigos_Click_1(object sender, EventArgs e) {
            lblAceptarSolicitud.Text = "";
            listBoxAmigos.Items.Clear();
            if (_usuarioActual.Amigos.Count == 0)
            {
                lblVerAmigos.Text = "No tienes amigos aún.";
                return;
            }

            foreach (var amigo in _usuarioActual.Amigos)
            {
                listBoxAmigos.Items.Add(amigo.ToString());
            }
        }

        private void btnVerNotificaciones_Click_1(object sender, EventArgs e) {
            listBoxNotificaciones.Items.Clear();
            bool hayNotificaciones = false;
            lblAceptarSolicitud.Text = "";
            lblVerAmigos.Text = "";
            foreach (var notificacion in _usuarioActual.VaciarNotificaciones())
            {
                listBoxNotificaciones.Items.Add(notificacion);
                hayNotificaciones = true;
            }

            if (!hayNotificaciones)
            {
                lblVerNotificacion.Text = "No tienes notificaciones.";
            }
        }

        private void btnCambiarNombreUsuario_Click_1(object sender, EventArgs e) {
            string nuevoNombre = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingresa tu nuevo nombre de usuario:",
                "Cambiar nombre de usuario",
                ""
            );

            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                MessageBox.Show("El nombre de usuario no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_usuarios.ContainsKey(nuevoNombre))
            {
                MessageBox.Show("El nombre de usuario ya está en uso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _usuarios.Remove(_usuarioActual.nombreUsuario);
            _usuarioActual.setNombreUsuario(nuevoNombre);
            _usuarios[_usuarioActual.nombreUsuario] = _usuarioActual;
            MessageBox.Show("Nombre de usuario actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblBienvenida.Text = $"¡Bienvenido, {_usuarioActual.nombreUsuario}!";
        }

        private void limpairCamposLogin() {
            txtUsuarioLogin.Clear();
            txtContrasenaLogin.Clear();
        }

        private void limpiarCamposRegistro() {
            txtNombreReal.Clear();
            txtNombreUsuario.Clear();
            txtContrasenaRegistro.Clear();
        }

        private void btnListarUsuario_Click(object sender, EventArgs e) {
            lblListarUsuario.Text = ""; 

            if (_usuarios.Count == 0)
            {
                lblListarUsuario.Text = "No hay usuarios registrados.";
                return;
            }

            string lista = "Usuarios registrados:\n";
            foreach (var par in _usuarios)
            {
                Usuario u = par.Value;
                lista += $"- {u.nombreUsuario} ({u.nombreReal})\n";
            }

            lblListarUsuario.Text = lista;
        }
    }
}
