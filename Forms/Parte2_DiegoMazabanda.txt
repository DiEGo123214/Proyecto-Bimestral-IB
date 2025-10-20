namespace RedSocial
{
    public partial class Form1 : Form {
        private void ActualizarInterfazUsuario() {
            listBoxPublicaciones.Items.Clear();
            _publicacionesMostradas.Clear();

            foreach (var pub in _usuarioActual.Publicaciones)
            {
                listBoxPublicaciones.Items.Add(pub);
                _publicacionesMostradas.Add(pub);

                foreach (var c in pub.comentarios)
                {
                    listBoxPublicaciones.Items.Add("     ↳ " + c.ToString());
                }
            }

            foreach (var amigo in _usuarioActual.Amigos)
            {
                foreach (var pub in amigo.Publicaciones)
                {
                    listBoxPublicaciones.Items.Add(pub);
                    _publicacionesMostradas.Add(pub);

                    foreach (var c in pub.comentarios)
                    {
                        listBoxPublicaciones.Items.Add("     ↳ " + c.ToString());
                    }
                }
            }

            ActualizarSolicitudes();
        }

        private Publicacion ObtenerPublicacionSeleccionada() {
            int indiceSeleccionado = listBoxPublicaciones.SelectedIndex;
            if (indiceSeleccionado < 0) return null;

            int pubIndex = 0;
            for (int i = 0; i < listBoxPublicaciones.Items.Count; i++)
            {
                if (!(listBoxPublicaciones.Items[i] is string itemStr && itemStr.StartsWith("     ↳")))
                {
                    if (i == indiceSeleccionado)
                        return _publicacionesMostradas[pubIndex];
                    pubIndex++;
                }
            }

            return null;
        }

        private void btnPublicar_Click_1(object sender, EventArgs e) {
            string texto = txtPublicacion.Text.Trim();
            if (!string.IsNullOrEmpty(texto))
            {
                _usuarioActual.publicar(texto);
                ActualizarInterfazUsuario();
                MessageBox.Show("Publicación creada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPublicacion.Clear();
                lblPublicacion.Text = "";
                lblComentar.Text = "Selecciona una publicación para comentar.";
            }
            else
            {
                lblPublicacion.Text = "El texto de la publicación no puede estar vacío.";
            }
        }

        private void btnComentar_Click_1(object sender, EventArgs e) {
            Publicacion pubSeleccionada = ObtenerPublicacionSeleccionada();
            string textoComentario = txtComentario.Text.Trim();
            if (string.IsNullOrEmpty(textoComentario))
            {
                lblComentar.Text = "El comentario no puede estar vacío.";
                return;
            }

            if (pubSeleccionada == null)
            {
                MessageBox.Show("Seleccionar una publicación. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblComentar.Text = "";
                return;
            }

            Comentario nuevoComentario = new Comentario(_usuarioActual, textoComentario);
            pubSeleccionada.agregarComentario(nuevoComentario);
            MessageBox.Show("Comentario publicada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtComentario.Clear();
            ActualizarInterfazUsuario();
            lblComentar.Text = "Selecciona una publicación para comentar.";
        }

        private void btnEnviarSolicitud_Click_1(object sender, EventArgs e) {
            string usuarioAmigo = txtUsuarioAmigo.Text.Trim();
            if (usuarioAmigo.Equals(_usuarioActual.nombreUsuario, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("No puedes enviarte una solicitud de amistad a ti mismo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuarioAmigo.Clear();
                return;
            }

            if (_usuarios.TryGetValue(usuarioAmigo, out Usuario amigo))
            {
                _usuarioActual.enviarSolicitud(amigo);
                MessageBox.Show("Solicitud de amistad enviada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsuarioAmigo.Clear();
            }
            else
            {
                MessageBox.Show($"El usuario {usuarioAmigo} no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuarioAmigo.Clear();
            }
        }

        private void btnAceptarSolicitud_Click_1(object sender, EventArgs e) {
            lblVerAmigos.Text = "";
            if (listBoxSolicitudes.SelectedIndex < 0)
            {
                lblAceptarSolicitud.Text = "Selecciona una solicitud.";
                return;
            }

            Usuario solicitudSeleccionada = new List<Usuario>(_usuarioActual.Solicitudes)[listBoxSolicitudes.SelectedIndex];
            _usuarioActual.aceptarSolicitud(solicitudSeleccionada);

            MessageBox.Show($"Solicitud de {solicitudSeleccionada.nombreUsuario} aceptada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ActualizarSolicitudes();
        }
    }
}
