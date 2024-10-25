using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services.FileService
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile formFile);
        Task DeleteFileAsync(string  fileName);
    }
}
