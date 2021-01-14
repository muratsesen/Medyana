using System;

namespace api.Controllers.Resources
{
    public class EkipmanResource
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public DateTime? TeminTarihi { get; set; }
        public int Adet { get; set; }
        public float BirimFiyat { get; set; }
        public float KullanimOrani { get; set; }
        public int KlinikId { get; set; }
    }
}