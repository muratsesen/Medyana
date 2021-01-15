using System;
using System.ComponentModel.DataAnnotations;
using api.Extensions;

namespace api.Controllers.Resources
{
    public class EkipmanFilter : IQueryObject
    {
        public string Adi { get; set; }
        public DateTime? TeminTarihi { get; set; }
        public int? Adet { get; set; }
        public float? BirimFiyat { get; set; }
        public float? KullanimOrani { get; set; }
        public int? KlinikId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}