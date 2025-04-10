//using SocialNetwork.Application.Dtos.dbo;

//namespace SocialNetwork.Web.Helpers.perfil
//{
//    public class PublicacionesHelper
//    {
//        private IWebHostEnvironment _webHost;

//        public PublicacionesHelper(IWebHostEnvironment webHost)
//        {
//            _webHost = webHost;
//        }
//        public async Task<PublicacionesDto> LoadPhoto(PublicacionesDto dto, IFormFile Foto)
//        {
//            if (Foto != null && Foto.Length > 0)
//            {
//                // Verificar que la carpeta existe, si no, crearla
//                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images/publicaciones");
//                if (!Directory.Exists(uploadsFolder))
//                {
//                    Directory.CreateDirectory(uploadsFolder);
//                }

//                // Crear un nombre único para la imagen
//                string fileExtension = Path.GetExtension(Foto.FileName);
//                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
//                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

//                // Guardar la imagen
//                using (var fileStream = new FileStream(filePath, FileMode.Create))
//                {
//                    // Asegurarse de esperar la operación asincrónica
//                    await Foto.CopyToAsync(fileStream);
//                }

//                // Guardar la ruta en el modelo DTO
//                dto.Imagen = "/images/publicaciones/" + uniqueFileName;
//            }
//            return dto;
//        }
//    }
//}
