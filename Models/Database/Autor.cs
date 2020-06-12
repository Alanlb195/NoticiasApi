using System;
using System.Collections.Generic;

namespace NoticiasApi.Database
{
    public partial class Autor
    {
        public Autor()
        {
            Noticia = new HashSet<Noticia>();
        }

        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public virtual ICollection<Noticia> Noticia { get; set; }
    }
}
