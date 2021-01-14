using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Klinik
    {
        public int Id { get; set; }
        [Required]
        public string Adi { get; set; }
    }
}