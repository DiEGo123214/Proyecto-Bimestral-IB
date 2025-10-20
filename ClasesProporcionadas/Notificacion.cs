using System;

namespace RedSocial.Modelo
{
    public class Notificacion
    {
        public string notificacion { get; }
        public DateTime fecha { get; }

        public Notificacion(string mensaje)
        {
            notificacion = mensaje;
            fecha = DateTime.Now;
        }

        public override string ToString()
        {
            return $"({fecha:g}) {notificacion}";
        }
    }
}