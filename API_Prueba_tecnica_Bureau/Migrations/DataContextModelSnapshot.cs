﻿// <auto-generated />
using System;
using API_Prueba_tecnica_Bureau.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIPruebatecnicaBureau.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API_Prueba_tecnica_Bureau.Models.Rol", b =>
                {
                    b.Property<int>("Id_rol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_rol"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id_rol");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("API_Prueba_tecnica_Bureau.Models.Usuario", b =>
                {
                    b.Property<int>("Id_usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_usuario"));

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo_electronico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Id_usuario_autenticacion")
                        .HasColumnType("int");

                    b.HasKey("Id_usuario");

                    b.HasIndex("Id_usuario_autenticacion");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("API_Prueba_tecnica_Bureau.Models.Usuario_autenticacion", b =>
                {
                    b.Property<int>("Id_usuario_autenticacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_usuario_autenticacion"));

                    b.Property<int>("Id_rol")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_usuario_autenticacion");

                    b.HasIndex("Id_rol");

                    b.ToTable("Usuarios_autenticacion");
                });

            modelBuilder.Entity("API_Prueba_tecnica_Bureau.Models.Usuario", b =>
                {
                    b.HasOne("API_Prueba_tecnica_Bureau.Models.Usuario_autenticacion", "Usuario_autenticacion")
                        .WithMany()
                        .HasForeignKey("Id_usuario_autenticacion");

                    b.Navigation("Usuario_autenticacion");
                });

            modelBuilder.Entity("API_Prueba_tecnica_Bureau.Models.Usuario_autenticacion", b =>
                {
                    b.HasOne("API_Prueba_tecnica_Bureau.Models.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("Id_rol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });
#pragma warning restore 612, 618
        }
    }
}
