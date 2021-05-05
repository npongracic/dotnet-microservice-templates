namespace SC.API.CleanArchitecture.Application.Common.Models
{
    public class FileInput
    {
        public byte[] Content { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
