﻿// <auto-generated />
using Fakultet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fakultet.Migrations
{
    [DbContext(typeof(FakultetContext))]
    partial class FakultetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Fakultet.Models.Predmet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BrCasova")
                        .HasColumnType("int");

                    b.Property<string>("ImePredmeta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Predavac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Predmeti");
                });

            modelBuilder.Entity("Fakultet.Models.Student", b =>
                {
                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Godine")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Index");

                    b.ToTable("Studenti");
                });

            modelBuilder.Entity("Fakultet.Models.StudentPredmet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("IdPredmeta")
                        .HasColumnType("int");

                    b.Property<int>("IndexStudenta")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("StudentPredmet");
                });
#pragma warning restore 612, 618
        }
    }
}
