using System;
using System.Collections.Generic;
using System.Text;

namespace RedSocial.Modelo
{
    public class Publicacion
    {
        public Usuario autor { get; }
        public string publicacion { get; }
        public DateTime fechaPub { get; }
        public List<Comentario> comentarios { get; } = new List<Comentario>();

        public Publicacion(Usuario autor, string mensaje)
        {
            this.autor = autor;
            publicacion = mensaje;
            fechaPub = DateTime.Now;
        }

        public void agregarComentario(Comentario comentario)
        {
            comentarios.Add(comentario);
        }

        public string listaComentarios()
        {
            var intermedio = new StringBuilder();
            intermedio.AppendLine(ToString());
            foreach (var e in comentarios)
            {
                intermedio.AppendLine("    " + e.ToString());
            }
            return intermedio.ToString();
        }

        public override string ToString()
        {
            return $"{autor.nombreUsuario} ({fechaPub:g}) dijo: {publicacion}";
        }
    }
}