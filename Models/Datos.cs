using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRest.Models
{
    public class Datos
    {
        [Key]
        public string Nombre{ get; set; }
        public float Ultimo{ get; set; }
        public float Max{ get; set; }

    }
}