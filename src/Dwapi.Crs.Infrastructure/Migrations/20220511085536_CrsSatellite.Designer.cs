﻿// <auto-generated />
using System;
using Dwapi.Crs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dwapi.Crs.Infrastructure.Migrations
{
    [DbContext(typeof(CrsContext))]
    [Migration("20220511085536_CrsSatellite")]
    partial class CrsSatellite
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.Cargo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Items")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ManifestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ManifestId");

                    b.ToTable("Cargoes");
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.ClientRegistry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlienIdNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlternativePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BirthCertificateNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CCCNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateExtracted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfInitiation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfLastEncounter")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfLastViralLoad")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Date_Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Date_Last_Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("DrivingLicenseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FacilityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FacilityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HighestLevelOfEducation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HudumaNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Landmark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MFLCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameOfNextOfKin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NextAppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NextOfKinRelationship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NextOfKinTelNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientClinicNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientPk")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Processed")
                        .HasColumnType("bit");

                    b.Property<string>("Project")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QueueId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("SatelliteId")
                        .HasColumnType("int");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SiteCode")
                        .HasColumnType("int");

                    b.Property<string>("SpousePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StatusDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubCounty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TreatmentOutcome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Village")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ward")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("ClientRegistries");
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.Docket", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Instance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Dockets");
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.Facility", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Emr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MasterFacilityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<Guid?>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SiteCode")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SnapshotDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SnapshotSiteCode")
                        .HasColumnType("int");

                    b.Property<int?>("SnapshotVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MasterFacilityId");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.Manifest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateArrived")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateLogged")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EmrId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmrName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmrSetup")
                        .HasColumnType("int");

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FacilityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Recieved")
                        .HasColumnType("int");

                    b.Property<Guid?>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Sent")
                        .HasColumnType("int");

                    b.Property<Guid?>("Session")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SiteCode")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("StatusDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("Manifests");
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.MasterFacility", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<Guid?>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("SnapshotDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SnapshotSiteCode")
                        .HasColumnType("int");

                    b.Property<int?>("SnapshotVersion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MasterFacilities");
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.Subscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocketId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DocketId");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.Cargo", b =>
                {
                    b.HasOne("Dwapi.Crs.Core.Domain.Manifest", null)
                        .WithMany("Cargoes")
                        .HasForeignKey("ManifestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.ClientRegistry", b =>
                {
                    b.HasOne("Dwapi.Crs.Core.Domain.Facility", null)
                        .WithMany("ClientRegistries")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.Facility", b =>
                {
                    b.HasOne("Dwapi.Crs.Core.Domain.MasterFacility", null)
                        .WithMany("Mentions")
                        .HasForeignKey("MasterFacilityId");
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.Manifest", b =>
                {
                    b.HasOne("Dwapi.Crs.Core.Domain.Facility", null)
                        .WithMany("Manifests")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dwapi.Crs.Core.Domain.Subscriber", b =>
                {
                    b.HasOne("Dwapi.Crs.Core.Domain.Docket", null)
                        .WithMany("Subscribers")
                        .HasForeignKey("DocketId");
                });
#pragma warning restore 612, 618
        }
    }
}
