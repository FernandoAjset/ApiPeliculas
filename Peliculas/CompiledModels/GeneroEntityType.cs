﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Peliculas.Entidades;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Peliculas.CompiledModels
{
    internal partial class GeneroEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Peliculas.Entidades.Genero",
                typeof(Genero),
                baseEntityType);

            var identificador = runtimeEntityType.AddProperty(
                "Identificador",
                typeof(int),
                propertyInfo: typeof(Genero).GetProperty("Identificador", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Genero).GetField("<Identificador>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd,
                afterSaveBehavior: PropertySaveBehavior.Throw);
            identificador.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            var desactivado = runtimeEntityType.AddProperty(
                "Desactivado",
                typeof(bool),
                propertyInfo: typeof(Genero).GetProperty("Desactivado", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Genero).GetField("<Desactivado>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            desactivado.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var fechaCreacion = runtimeEntityType.AddProperty(
                "FechaCreacion",
                typeof(DateTime),
                valueGenerated: ValueGenerated.OnAdd);
            fechaCreacion.AddAnnotation("Relational:ColumnType", "datetime2");
            fechaCreacion.AddAnnotation("Relational:DefaultValueSql", "GetDate()");
            fechaCreacion.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var nombre = runtimeEntityType.AddProperty(
                "Nombre",
                typeof(string),
                propertyInfo: typeof(Genero).GetProperty("Nombre", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Genero).GetField("<Nombre>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                maxLength: 150);
            nombre.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var key = runtimeEntityType.AddKey(
                new[] { identificador });
            runtimeEntityType.SetPrimaryKey(key);

            var index = runtimeEntityType.AddIndex(
                new[] { nombre },
                unique: true);

            return runtimeEntityType;
        }

        public static RuntimeSkipNavigation CreateSkipNavigation1(RuntimeEntityType declaringEntityType, RuntimeEntityType targetEntityType, RuntimeEntityType joinEntityType)
        {
            var skipNavigation = declaringEntityType.AddSkipNavigation(
                "Peliculas",
                targetEntityType,
                joinEntityType.FindForeignKey(
                    new[] { joinEntityType.FindProperty("GenerosIdentificador") },
                    declaringEntityType.FindKey(new[] { declaringEntityType.FindProperty("Identificador") }),
                    declaringEntityType),
                true,
                false,
                typeof(HashSet<Pelicula>),
                propertyInfo: typeof(Genero).GetProperty("Peliculas", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Genero).GetField("<Peliculas>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var inverse = targetEntityType.FindSkipNavigation("Generos");
            if (inverse != null)
            {
                skipNavigation.Inverse = inverse;
                inverse.Inverse = skipNavigation;
            }

            return skipNavigation;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "Generos");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
