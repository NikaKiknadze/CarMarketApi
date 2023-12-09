﻿// <auto-generated />
using System;
using CarMarketApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarMarketApi.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarMarketApi.Entities.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("PersonalIformationId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Buyers", "market");
                });

            modelBuilder.Entity("CarMarketApi.Entities.BuyerPersonalInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId")
                        .IsUnique()
                        .HasFilter("[BuyerId] IS NOT NULL");

                    b.ToTable("BuyersPersonalInformations", "market");
                });

            modelBuilder.Entity("CarMarketApi.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<int>("Cost")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SellerId");

                    b.ToTable("Items", "market");
                });

            modelBuilder.Entity("CarMarketApi.Entities.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("PersonalInformationId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Sellers", "market");
                });

            modelBuilder.Entity("CarMarketApi.Entities.SellerPersonalInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SellerId")
                        .IsUnique()
                        .HasFilter("[SellerId] IS NOT NULL");

                    b.ToTable("SellersPersonalInformations", "market");
                });

            modelBuilder.Entity("CarMarketApi.Entities.SellersBuyersJoin", b =>
                {
                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.HasKey("BuyerId", "SellerId");

                    b.HasIndex("SellerId");

                    b.ToTable("SellersUsersJoin", "market");
                });

            modelBuilder.Entity("CarMarketApi.Entities.BuyerPersonalInformation", b =>
                {
                    b.HasOne("CarMarketApi.Entities.Buyer", "Buyer")
                        .WithOne("PersonalInformation")
                        .HasForeignKey("CarMarketApi.Entities.BuyerPersonalInformation", "BuyerId");

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("CarMarketApi.Entities.Item", b =>
                {
                    b.HasOne("CarMarketApi.Entities.Buyer", "Buyer")
                        .WithMany("Items")
                        .HasForeignKey("BuyerId");

                    b.HasOne("CarMarketApi.Entities.Seller", "Seller")
                        .WithMany("Items")
                        .HasForeignKey("SellerId");

                    b.Navigation("Buyer");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("CarMarketApi.Entities.SellerPersonalInformation", b =>
                {
                    b.HasOne("CarMarketApi.Entities.Seller", "Seller")
                        .WithOne("SellerPersonalInformation")
                        .HasForeignKey("CarMarketApi.Entities.SellerPersonalInformation", "SellerId");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("CarMarketApi.Entities.SellersBuyersJoin", b =>
                {
                    b.HasOne("CarMarketApi.Entities.Buyer", "Buyer")
                        .WithMany("SellersBuyers")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarMarketApi.Entities.Seller", "Seller")
                        .WithMany("SellersBuyers")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("CarMarketApi.Entities.Buyer", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("PersonalInformation");

                    b.Navigation("SellersBuyers");
                });

            modelBuilder.Entity("CarMarketApi.Entities.Seller", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("SellerPersonalInformation");

                    b.Navigation("SellersBuyers");
                });
#pragma warning restore 612, 618
        }
    }
}
