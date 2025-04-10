

using AutoMapper;
using SocialNetwork.Application.Dtos.dbo;
using SocialNetwork.Application.Model.ViewModel;
using SocialNetwork.Domain.Entities.dbo;
using SocialNetwork.Persistance.Models.dbo;

namespace SocialNetwork.Application.Mapping.dbo
{
    public class DboMapping : Profile
    {
        public DboMapping()
        {
            CreateMap<AmigosDto, Amigos>()
                .ReverseMap();

            CreateMap<Comentarios, ComentariosDto>();
            CreateMap<ComentariosDto, Comentarios>();

            CreateMap<Notificaciones, NotificacionesDto>();
            CreateMap<NotificacionesDto, Notificaciones>();

            CreateMap<Publicaciones, PublicacionesDto>();
            CreateMap<PublicacionesDto, Publicaciones>();

            CreateMap<Usuarios, UsuariosDto>();
            CreateMap<UsuariosDto, Usuarios>();

            CreateMap<PublicacionesModel, PublicacionesViewModel>()
                .ReverseMap();
        }
    }
}
