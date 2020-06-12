using System;
using System.Collections.Generic;

namespace NoticiasApi.Database
{
    public partial class Noticia
    {
        public int NoticiaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
        public int AutorId { get; set; }

        public virtual Autor Autor { get; set; }
    }
}
