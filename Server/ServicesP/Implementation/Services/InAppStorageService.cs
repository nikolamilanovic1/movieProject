//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Hosting; // I TRY WITH HOSTING BUT DONT WORK
//using Microsoft.Extensions.FileProviders;
//using Microsoft.Extensions.Hosting;

//namespace ServicesP.Services
//{
//    public class InAppStorageService : IFileStorageService
//    {
//        private readonly IWebHostEnvironment _env; //I CANT IMPLEMENT THIS ONE
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public InAppStorageService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
//        {
//            _env = webHostEnvironment;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public Task DeleteFile(string fileRoute, string containerName)
//        {
//            if (string.IsNullOrEmpty(fileRoute))
//            {
//                return Task.CompletedTask;
//            }

//            var fileName = Path.GetFileName(fileRoute);
//            var filedirectory = Path.Combine(_env.WebRootPath, containerName, fileName);
//            if (!Directory.Exists(filedirectory))
//            {
//                File.Delete(filedirectory);
//            }
//            return Task.CompletedTask;
//        }

//        public async Task<string> EditFile(string containerName, IFormFile file, string fileRoute)
//        {
//            await DeleteFile(fileRoute, containerName);
//            return await SaveFile(containerName, file);
//        }

//        public async Task<string> SaveFile(string containerName, IFormFile file)
//        {
//            var extension = Path.GetExtension(file.FileName);
//            var fileName = $"{Guid.NewGuid()}{extension}";
//            string folder = Path.Combine(_env.WebRootPath, containerName);

//            if (!Directory.Exists(folder))
//            {
//                Directory.CreateDirectory(folder);
//            }
//            string route = Path.Combine(folder, fileName);
//            using (var ms = new MemoryStream())
//            {
//                await file.CopyToAsync(ms);
//                var content = ms.ToArray();
//                await File.WriteAllBytesAsync(route, content);
//            }

//            var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
//            var routeForDB = Path.Combine(url, containerName, fileName).Replace("\\", "/");
//            return routeForDB;
//        }
//    }
//}
