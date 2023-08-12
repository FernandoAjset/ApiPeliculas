using AutoMapper;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Peliculas.DTOs;
using Peliculas.Entidades;

namespace Peliculas.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, ActorDTO>();
            CreateMap<Cine, CineDTO>()
                .ForMember(dto => dto.Latitud, ent => ent.MapFrom(prop => prop.Ubicacion.Y))
                .ForMember(dto => dto.Longitud, ent => ent.MapFrom(prop => prop.Ubicacion.X));

            CreateMap<GeneroActualizacionDTO, Genero>();

            CreateMap<Genero, GeneroDTO>();
            CreateMap<Pelicula, PeliculaDTO>()
                .ForMember(dto => dto.Cines, ent => ent.MapFrom(prop => prop.SalasDeCine.Select(s => s.Cine)))
                .ForMember(dto => dto.Actores, ent => ent.MapFrom(prop => prop.PeliculasActores.Select(pa => pa.Actor)));

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            CreateMap<CineCreacionDTO, Cine>()
                .ForMember(ent => ent.Ubicacion,
                            dto => dto.MapFrom(campo =>
                              geometryFactory.CreatePoint(new Coordinate
                              (campo.Longitud, campo.Latitud))));
            CreateMap<CineOfertaCreacionDTO, CineOferta>();
            CreateMap<SalaDeCineCreacionDTO, SalaDeCine>();

            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(ent => ent.Generos, dto => dto.MapFrom(campo => campo.Generos.Select(id => new Genero() { Identificador = id })))
                .ForMember(ent => ent.SalasDeCine, dto => dto.MapFrom(campo => campo.SalasDeCine.Select(id => new SalaDeCine() { Id = id })));

            CreateMap<PeliculaActorCreacionDTO, PeliculaActor>();

            CreateMap<ActorCreacionDTO, Actor>();
        }
    }
}
