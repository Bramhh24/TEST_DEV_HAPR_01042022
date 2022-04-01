using PruebaToka.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PruebaToka.Models
{
    public class PersonaFisica
    {
        public int IdPersonaFisica { get; set; }

        [Display (Name = "Fecha de registro")] // Validación para mostrar otro nombre
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Display (Name = "Fecha Actualizacion")]
        public DateTime FechaActualizacion { get; set; }

        [Required] // Validación para que sea requerido
        [StringLength (maximumLength: 50, ErrorMessage = "El {0} no debe pasar {1} caracteres.")] // Validación donde el maximo de caracteres es 50.
        public string Nombre { get; set; }

        [Required]
        [Display (Name = "ApellidoMaterno Paterno")]
        [StringLength(maximumLength: 50, ErrorMessage = "El {0} no debe pasar {1} caracteres.")]
        public string ApellidoPaterno { get; set; }

        [Required]
        [Display(Name = "ApellidoMaterno Paterno")]
        [StringLength(maximumLength: 50, ErrorMessage = "El {0} no debe pasar {1} caracteres.")]
        public string ApellidoMaterno { get; set; }

        [TrecePalabras] // Validación creada para que ingresen 13 palabras.
        [Required]
        public string RFC { get; set; }

        [Required]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        public int UsuarioAgrega { get; set; }
        public PersonaFisicaActivo Activo { get; set; } = PersonaFisicaActivo.Activado; // Cree un enum pero no se necesito
    }
}
