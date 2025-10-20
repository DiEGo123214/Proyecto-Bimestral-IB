using System;

namespace RedSocial.Modelo
{
    public class Comentario
    {
        public Usuario autor { get; }
        public string texto { get; }
        public DateTime fecha { get; }

        public Comentario(Usuario autor, string texto)
        {
            this.autor = autor;
            this.texto = texto;
            fecha = DateTime.Now;
        }

        public override string ToString()
        {
            return $"- {autor.nombreUsuario} ({fecha:g}): {texto}";
        }
    }
}