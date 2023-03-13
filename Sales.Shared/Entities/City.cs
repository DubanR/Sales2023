using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class City
    {
        public int id { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Ciudad")]
        public string name { get; set; } = null!;

        public int Stateid { get; set; }

        public State? State { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
