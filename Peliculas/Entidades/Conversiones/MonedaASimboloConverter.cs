using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Peliculas.Entidades.Conversiones
{
    public class MonedaASimboloConverter : ValueConverter<Moneda, string>
    {

        public MonedaASimboloConverter()
        : base(
              valor => MapeoMonedaString(valor),
              valor => MapeoStringMoneda(valor)
              )
        {

        }

        private static string MapeoMonedaString(Moneda valor)
        {
            return valor switch
            {
                Moneda.PesoMexicano => "MXN$",
                Moneda.DolarEstadounidense => "USD$",
                Moneda.Quetzal => "Q",
                _ => ""
            };
        }
        private static Moneda MapeoStringMoneda(string valor)
        {
            return valor switch
            {
                "MXN$" => Moneda.PesoMexicano,
                "USD$" => Moneda.DolarEstadounidense,
                "Q" => Moneda.Quetzal,
                _ => Moneda.Desconocida
            };
        }
    }
}
