using Microsoft.AspNetCore.Http;

namespace SC.API.CleanArchitecture.API.Models
{
    public class UploadDocumentModel
    {
        public long EntityId { get; set; }
        public string DocumentName { get; set; }
        public IFormFile Document { get; set; }
    }
}
