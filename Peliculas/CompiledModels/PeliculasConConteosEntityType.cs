﻿// <auto-generated />
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Peliculas.Entidades.SinLlave;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Peliculas.CompiledModels
{
    internal partial class PeliculasConConteosEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Peliculas.Entidades.SinLlave.PeliculasConConteos",
                typeof(PeliculasConConteos),
                baseEntityType);

            var cantidadActores = runtimeEntityType.AddProperty(
                "CantidadActores",
                typeof(int),
                propertyInfo: typeof(PeliculasConConteos).GetProperty("CantidadActores", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PeliculasConConteos).GetField("<CantidadActores>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            cantidadActores.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var cantidadCines = runtimeEntityType.AddProperty(
                "CantidadCines",
                typeof(int),
                propertyInfo: typeof(PeliculasConConteos).GetProperty("CantidadCines", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PeliculasConConteos).GetField("<CantidadCines>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            cantidadCines.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var cantidadGeneros = runtimeEntityType.AddProperty(
                "CantidadGeneros",
                typeof(int),
                propertyInfo: typeof(PeliculasConConteos).GetProperty("CantidadGeneros", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PeliculasConConteos).GetField("<CantidadGeneros>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            cantidadGeneros.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(int),
                propertyInfo: typeof(PeliculasConConteos).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PeliculasConConteos).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            id.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var titulo = runtimeEntityType.AddProperty(
                "Titulo",
                typeof(string),
                propertyInfo: typeof(PeliculasConConteos).GetProperty("Titulo", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(PeliculasConConteos).GetField("<Titulo>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            titulo.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            return runtimeEntityType;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewDefinitionSql", null);
            runtimeEntityType.AddAnnotation("Relational:ViewName", "PeliculasConConteos");
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
