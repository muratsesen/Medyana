using System;
using System.ComponentModel.DataAnnotations;

namespace api.Controllers.Resources
{
    public class EkipmanResource
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public DateTime? TeminTarihi { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "En az 1 değeri girilmeli!")]
        public int Adet { get; set; }

        [Range(0.01, Double.MaxValue, ErrorMessage = "Girilen değer 0.01'den az olamaz!")]
        public float BirimFiyat { get; set; }

        [Range(0.0, 100.0, ErrorMessage = "0.0 ile 100.0 arasında bir değer giriniz!")]
        public float KullanimOrani { get; set; }
        
        public int KlinikId { get; set; }
    }
}