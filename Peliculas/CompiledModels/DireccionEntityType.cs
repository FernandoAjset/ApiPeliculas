﻿// <auto-generated />
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Peliculas.Entidades;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Peliculas.CompiledModels
{
    internal partial class DireccionEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Peliculas.Entidades.Actor.Direccion#Direccion",
                typeof(Direccion),
                baseEntityType,
                sharedClrType: true);

            var actorId = runtimeEntityType.AddProperty(
                "ActorId",
                typeof(int),
                afterSaveBehavior: PropertySaveBehavior.Throw);
            actorId.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var calle = runtimeEntityType.AddProperty(
                "Calle",
                typeof(string),
                propertyInfo: typeof(Direccion).GetProperty("Calle", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Direccion).GetField("<Calle>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            calle.AddAnnotation("Relational:ColumnName", "Calle");
            calle.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var pais = runtimeEntityType.AddProperty(
                "Pais",
                typeof(string),
                propertyInfo: typeof(Direccion).GetProperty("Pais", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Direccion).GetField("<Pais>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            pais.AddAnnotation("Relational:ColumnName", "Pais");
            pais.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var provincia = runtimeEntityType.AddProperty(
                "Provincia",
                typeof(string),
                propertyInfo: typeof(Direccion).GetProperty("Provincia", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Direccion).GetField("<Provincia>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            provincia.AddAnnotation("Relational:ColumnName", "Provincia");
            provincia.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var key = runtimeEntityType.AddKey(
                new[] { actorId });
            runtimeEntityType.SetPrimaryKey(key);

            return runtimeEntityType;
        }

        public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("ActorId") },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id") }),
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade,
                unique: true,
                required: true,
                ownership: true);

            var direccion = principalEntityType.AddNavigation("Direccion",
                runtimeForeignKey,
                onDependent: false,
                typeof(Direccion),
                propertyInfo: typeof(Actor).GetProperty("Direccion", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Actor).GetField("<Direccion>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                eagerLoaded: true);

            return runtimeForeignKey;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "Actores");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
