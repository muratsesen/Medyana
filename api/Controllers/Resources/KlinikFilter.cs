using api.Extensions;

namespace api.Controllers.Resources
{
    public class KlinikFilter : IQueryObject
    {
        public string Adi { get; set; }

        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
  
        public byte PageSize { get; set; }
    }
}