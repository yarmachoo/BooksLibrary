using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Services.FileService
{
    public class ApiFileService : IFileService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;
        //private readonly 
        public async Task DeleteFileAsync(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("Error: there is no Uri to delete file.");
                throw new ArgumentNullException(nameof(fileName));
            }
            Console.WriteLine($"Start deleating file via Uri: {fileName}");

            var response = await _httpClient.DeleteAsync(fileName);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("File deleated successfill.");
            }
            else
            {
                Console.WriteLine($"Error with deleating file: {response}");
                throw new InvalidOperationException($"Error with deleating file: {response.StatusCode}");
            }
        }

        public async Task<string> SaveFileAsync(IFormFile formFile)
        {
            if(formFile is null)
            {
                Console.WriteLine($"Error: File is not get");
                throw new ArgumentNullException(nameof(formFile));
            }

            Console.WriteLine($"Stert uploading file {formFile.FileName}");


            var request = new HttpRequestMessage(HttpMethod.Post, "files");

            var extension = Path.GetExtension(formFile.FileName);
            var fileName = Path.ChangeExtension(Path.GetRandomFileName(), extension);

            Console.WriteLine($"New file name is {fileName}");

            var content = new MultipartFormDataContent();
            var sc = new StreamContent(formFile.OpenReadStream());
            content.Add(sc, "file", fileName);

            request.Content = content;

            Console.WriteLine("Send request to server");
            var response = await _httpClient.SendAsync(request);
            
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new InvalidOperationException("Error with uploading file");
            }
        }
    }
}
