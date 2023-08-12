using AutoMapper;
using Peliculas.Entidades;
using System.Collections.ObjectModel;

namespace Peliculas.Servicios
{
    public class ActualizadorObservableCollection
    {
        private readonly IMapper mapper;

        public ActualizadorObservableCollection(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void Actualizar<ENT, DTO>(ObservableCollection<ENT> entidades, IEnumerable<DTO> dtos)
            where ENT : IId
            where DTO : IId
        {
            var diccionarioEntidades = entidades.ToDictionary(x => x.Id);
            var diccionarioDTOs = dtos.ToDictionary(x => x.Id);

            var idsEntidades = diccionarioEntidades.Select(x => x.Key);
            var idsDTOs = diccionarioDTOs.Select(x => x.Key);

            var crear = idsDTOs.Except(idsEntidades);
            var borrar = idsEntidades.Except(idsDTOs);
        }
    }
}
