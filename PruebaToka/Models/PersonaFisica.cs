using PruebaToka.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PruebaToka.Models
{
    public class PersonaFisica
    {
        public int IdPersonaFisica { get; set; }

        [Display (Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Display (Name = "Fecha Actualizacion")]
        public DateTime FechaActualizacion { get; set; }

        [Required]
        [StringLength (maximumLength: 50, ErrorMessage = "El {0} no debe pasar {1} caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [Display (Name = "ApellidoMaterno Paterno")]
        [StringLength(maximumLength: 50, ErrorMessage = "El {0} no debe pasar {1} caracteres.")]
        public string ApellidoPaterno { get; set; }

        [Required]
        [Display(Name = "ApellidoMaterno Paterno")]
        [StringLength(maximumLength: 50, ErrorMessage = "El {0} no debe pasar {1} caracteres.")]
        public string ApellidoMaterno { get; set; }

        [TrecePalabras]
        [Required]
        public string RFC { get; set; }

        [Required]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        public int UsuarioAgrega { get; set; }
        public PersonaFisicaActivo Activo { get; set; } = PersonaFisicaActivo.Activado;
    }
}
