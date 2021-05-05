using Microsoft.AspNetCore.Http;

namespace SC.API.CleanArchitecture.Application.Common
{
    public static class StringExtensions
    {
        public static string GetFileName(this IFormFile file)
        {
            return file.FileName.Substring(file.FileName.Trim().LastIndexOf('.'));
        }
    }
}
