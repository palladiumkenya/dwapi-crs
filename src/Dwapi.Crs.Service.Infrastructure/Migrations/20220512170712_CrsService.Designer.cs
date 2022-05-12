﻿// <auto-generated />
using System;
using Dwapi.Crs.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dwapi.Crs.Service.Infrastructure.Migrations
{
    [DbContext(typeof(CrsServiceContext))]
    [Migration("20220512170712_CrsService")]
    partial class CrsService
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dwapi.Crs.Service.Application.Domain.RegistryManifest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ManifestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SiteCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RegistryManifests");
                });

            modelBuilder.Entity("Dwapi.Crs.Service.Application.Domain.TransmissionLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Registry")
                        .HasColumnType("int");

                    b.Property<Guid>("RegistryManifestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Response")
                        .HasColumnType("int");

                    b.Property<string>("ResponseInfo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RegistryManifestId");

                    b.ToTable("TransmissionLogs");
                });

            modelBuilder.Entity("Dwapi.Crs.Service.Application.Domain.TransmissionLog", b =>
                {
                    b.HasOne("Dwapi.Crs.Service.Application.Domain.RegistryManifest", null)
                        .WithMany("TransmissionLogs")
                        .HasForeignKey("RegistryManifestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
