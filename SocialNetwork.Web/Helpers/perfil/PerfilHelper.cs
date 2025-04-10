using SocialNetwork.Application.Dtos.dbo;
using SocialNetwork.Application.Dtos.identity.account;
using SocialNetwork.Web.Helpers.perfil.Base;

namespace SocialNetwork.Web.Helpers.Perfil;
public class PerfilHelper : ILoadPhoto<RegisterDto, IFormFile>
{
    private readonly IWebHostEnvironment _webHost;
    private readonly LoadPhoto _loadPhoto;

    public PerfilHelper(IWebHostEnvironment webHostEnvironment, LoadPhoto loadPhoto)
    {
        _webHost = webHostEnvironment;
        _loadPhoto = loadPhoto;
    }

    public async Task<RegisterDto> LoadPhoto(RegisterDto dto, IFormFile Foto)
    {
        if (Foto != null && Foto.Length > 0)
        {
            string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images/usuario");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileExtension = Path.GetExtension(Foto.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await Foto.CopyToAsync(fileStream);
            }

            dto.Foto = "/images/usuario/" + uniqueFileName;
        }
        return dto;
    }

    public async Task<PerfilDto> UpdatePerfilPhoto(PerfilDto dto, IFormFile Foto)
    {
        // Solo borrar la foto si se sube una nueva
        if (Foto != null && Foto.Length > 0)
        {
            if (!string.IsNullOrEmpty(dto.Foto))
            {
                string oldPhotoPath = dto.Foto;
                string fullOldPhotoPath = Path.Combine(_webHost.WebRootPath, oldPhotoPath.TrimStart('/'));
                if (File.Exists(fullOldPhotoPath))
                {
                    File.Delete(fullOldPhotoPath);
                }
            }

            string filePath = await _loadPhoto.SaveFileAsync(Foto);

            if (!string.IsNullOrEmpty(filePath))
            {
                dto.Foto = filePath;
            }
        }

        return dto;
    }

    public async Task<PublicacionesDto> UpdatePostPhoto(PublicacionesDto dto, IFormFile Foto)
    {
        // Solo borrar la imagen si se sube una nueva
        if (Foto != null && Foto.Length > 0)
        {
            if (!string.IsNullOrEmpty(dto.Imagen))
            {
                string oldPhotoPath = dto.Imagen;
                string fullOldPhotoPath = Path.Combine(_webHost.WebRootPath, oldPhotoPath.TrimStart('/'));
                if (File.Exists(fullOldPhotoPath))
                {
                    File.Delete(fullOldPhotoPath);
                }
            }

            string filePath = await _loadPhoto.SaveFileAsync(Foto);

            if (!string.IsNullOrEmpty(filePath))
            {
                dto.Imagen = filePath;
            }
        }

        return dto;
    }



}
