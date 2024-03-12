﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NBD6.Data;

#nullable disable

namespace NBD6.Data.NBDMigrations
{
    [DbContext(typeof(NBDContext))]
    partial class NBDContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.16");

            modelBuilder.Entity("BidStaff", b =>
                {
                    b.Property<int>("BidsBidID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StaffsStaffID")
                        .HasColumnType("INTEGER");

                    b.HasKey("BidsBidID", "StaffsStaffID");

                    b.HasIndex("StaffsStaffID");

                    b.ToTable("BidStaff");
                });

            modelBuilder.Entity("NBD6.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClientID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Postal")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProjectID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("AddressID");

                    b.HasIndex("ClientID")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("NBD6.Models.Bid", b =>
                {
                    b.Property<int>("BidID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BidEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("BidName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BidStart")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ClientApproved")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NBDApproved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectID")
                        .HasColumnType("INTEGER");

                    b.HasKey("BidID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("NBD6.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClientContact")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientPhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("NBD6.Models.Labour", b =>
                {
                    b.Property<int>("LabourID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BidID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BidID1")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LabourDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("LabourHours")
                        .HasColumnType("REAL");

                    b.Property<decimal>("LabourPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("LabourID");

                    b.HasIndex("BidID");

                    b.HasIndex("BidID1")
                        .IsUnique();

                    b.ToTable("Labours");
                });

            modelBuilder.Entity("NBD6.Models.Material", b =>
                {
                    b.Property<int>("MaterialID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BidID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BidID1")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MaterialDescription")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MaterialPrice")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaterialQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MaterialSize")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MaterialType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MaterialID");

                    b.HasIndex("BidID");

                    b.HasIndex("BidID1")
                        .IsUnique();

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("NBD6.Models.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ProjectEndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectSite")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ProjectStartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ProjectID");

                    b.HasIndex("AddressID")
                        .IsUnique();

                    b.HasIndex("ClientID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("NBD6.Models.Staff", b =>
                {
                    b.Property<int>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("StaffPhone")
                        .HasColumnType("TEXT");

                    b.Property<string>("StaffPosition")
                        .HasColumnType("TEXT");

                    b.HasKey("StaffID");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("BidStaff", b =>
                {
                    b.HasOne("NBD6.Models.Bid", null)
                        .WithMany()
                        .HasForeignKey("BidsBidID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NBD6.Models.Staff", null)
                        .WithMany()
                        .HasForeignKey("StaffsStaffID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NBD6.Models.Address", b =>
                {
                    b.HasOne("NBD6.Models.Client", "Client")
                        .WithOne("Address")
                        .HasForeignKey("NBD6.Models.Address", "ClientID");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("NBD6.Models.Bid", b =>
                {
                    b.HasOne("NBD6.Models.Project", "Project")
                        .WithMany("Bids")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("NBD6.Models.Labour", b =>
                {
                    b.HasOne("NBD6.Models.Bid", "Bid")
                        .WithMany("Labours")
                        .HasForeignKey("BidID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NBD6.Models.Bid", null)
                        .WithOne("Labour")
                        .HasForeignKey("NBD6.Models.Labour", "BidID1");

                    b.Navigation("Bid");
                });

            modelBuilder.Entity("NBD6.Models.Material", b =>
                {
                    b.HasOne("NBD6.Models.Bid", "Bid")
                        .WithMany("Materials")
                        .HasForeignKey("BidID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NBD6.Models.Bid", null)
                        .WithOne("Material")
                        .HasForeignKey("NBD6.Models.Material", "BidID1");

                    b.Navigation("Bid");
                });

            modelBuilder.Entity("NBD6.Models.Project", b =>
                {
                    b.HasOne("NBD6.Models.Address", "Address")
                        .WithOne("Project")
                        .HasForeignKey("NBD6.Models.Project", "AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NBD6.Models.Client", "Client")
                        .WithMany("Projects")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("NBD6.Models.Address", b =>
                {
                    b.Navigation("Project");
                });

            modelBuilder.Entity("NBD6.Models.Bid", b =>
                {
                    b.Navigation("Labour");

                    b.Navigation("Labours");

                    b.Navigation("Material");

                    b.Navigation("Materials");
                });

            modelBuilder.Entity("NBD6.Models.Client", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("NBD6.Models.Project", b =>
                {
                    b.Navigation("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}
