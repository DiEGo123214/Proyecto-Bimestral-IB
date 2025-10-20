using System;
using System.Collections.Generic;

namespace RedSocial.Modelo
{
    public class Usuario
    {
        public string nombreUsuario { get; private set; }
        public string nombreReal { get; }
        public string contrasena { get; private set; }
        public List<Publicacion> Publicaciones { get; } = new List<Publicacion>();
        public HashSet<Usuario> Amigos { get; } = new HashSet<Usuario>();
        public HashSet<Usuario> Solicitudes { get; } = new HashSet<Usuario>();
        public Queue<Notificacion> Notificaciones { get; } = new Queue<Notificacion>();

        public Usuario(string nombreReal, string nombreUsuario, string contrasena)
        {
            this.nombreReal = nombreReal;
            this.nombreUsuario = nombreUsuario;
            this.contrasena = contrasena;
        }

        public void setNombreUsuario(string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                throw new ArgumentException("El nombre de usuario no puede estar vacío.");
            }
            nombreUsuario = nuevoNombre.Trim();
        }

        public bool ValidarContrasena(string contrasena)
        {
            return this.contrasena == contrasena;
        }

        public void publicar(string mensaje)
        {
            var pub = new Publicacion(this, mensaje);
            Publicaciones.Add(pub);
            Notificaciones.Enqueue(new Notificacion("Nueva Publicación"));
        }

        public void comentar(Publicacion pub, string mensaje)
        {
            var com = new Comentario(this, mensaje);
            pub.agregarComentario(com);
            if (!ReferenceEquals(pub.autor, this))
            {
                pub.autor.Notificaciones.Enqueue(new Notificacion($"{nombreUsuario} comentó tu publicación"));
            }
        }

        public void enviarSolicitud(Usuario otro)
        {
            if (ReferenceEquals(this, otro)) return;
            if (Amigos.Contains(otro)) return;
            if (otro.Solicitudes.Contains(this)) return;
            if (otro != null)
            {
                otro.Solicitudes.Add(this);
                otro.Notificaciones.Enqueue(new Notificacion($"{nombreUsuario} te ha enviado una solicitud de amistad."));
            }
        }

        public void aceptarSolicitud(Usuario otro)
        {
            if (Solicitudes.Remove(otro))
            {
                Amigos.Add(otro);
                otro.Amigos.Add(this);
                Notificaciones.Enqueue(new Notificacion($"Ahora eres amigo de {otro.nombreUsuario}"));
                otro.Notificaciones.Enqueue(new Notificacion($"Ahora eres amigo de {nombreUsuario}"));
            }
        }

        public IEnumerable<string> VaciarNotificaciones()
        {
            while (Notificaciones.Count > 0)
            {
                yield return Notificaciones.Dequeue().ToString();
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Usuario u) return false;
            return string.Equals(nombreUsuario, u.nombreUsuario, StringComparison.Ordinal);
        }

        public override int GetHashCode() => HashCode.Combine(nombreUsuario);

        public override string ToString()
        {
            return $"{nombreUsuario} ({nombreReal})";
        }
    }
}
