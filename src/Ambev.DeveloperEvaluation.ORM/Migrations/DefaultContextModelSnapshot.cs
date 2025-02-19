﻿// <auto-generated />
using System;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    [DbContext(typeof(DefaultContext))]
    partial class DefaultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamptz");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Branches", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
                            Address = "Av. Paulista, 1000 - Bela Vista, São Paulo - SP",
                            Code = "MATRIX-001",
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = true,
                            Name = "São Paulo Headquarters"
                        },
                        new
                        {
                            Id = new Guid("9c9e6679-7425-40de-944b-e07fc1f90ae8"),
                            Address = "Av. Rio Branco, 500 - Centro, Rio de Janeiro - RJ",
                            Code = "BRANCH-RJ-001",
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = true,
                            Name = "Rio de Janeiro Branch"
                        },
                        new
                        {
                            Id = new Guid("5c9e6679-7425-40de-944b-e07fc1f90ae9"),
                            Address = "Av. Afonso Pena, 2000 - Centro, Belo Horizonte - MG",
                            Code = "BRANCH-BH-001",
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = true,
                            Name = "Belo Horizonte Branch"
                        },
                        new
                        {
                            Id = new Guid("3c9e6679-7425-40de-944b-e07fc1f90ae0"),
                            Address = "Rua XV de Novembro, 1500 - Centro, Curitiba - PR",
                            Code = "BRANCH-CWB-001",
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = true,
                            Name = "Curitiba Branch"
                        });
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Image")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamptz");

                    b.HasKey("Id");

                    b.HasIndex("Category");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Cervejas",
                            Code = "BEER-001",
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Cerveja Brahma Duplo Malte 350ml",
                            Image = "brahma-duplo-malte.jpg",
                            IsActive = true,
                            Name = "Brahma Duplo Malte 350ml",
                            Price = 4.99m,
                            Title = "Brahma Duplo Malte"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Cervejas",
                            Code = "BEER-002",
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Cerveja Skol Puro Malte 350ml",
                            Image = "skol-puro-malte.jpg",
                            IsActive = true,
                            Name = "Skol Puro Malte 350ml",
                            Price = 4.49m,
                            Title = "Skol Puro Malte"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Cervejas",
                            Code = "BEER-003",
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Cerveja Original 600ml",
                            Image = "original.jpg",
                            IsActive = true,
                            Name = "Cerveja Original 600ml",
                            Price = 8.99m,
                            Title = "Original"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Cervejas Premium",
                            Code = "BEER-004",
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Description = "Cerveja Corona Extra 330ml",
                            Image = "corona-extra.jpg",
                            IsActive = true,
                            Name = "Cerveja Corona Extra 330ml",
                            Price = 7.99m,
                            Title = "Corona Extra"
                        });
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CancelledAt")
                        .HasColumnType("timestamptz");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsCancelled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("timestamptz");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Sales", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.SaleItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime?>("CancelledAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<decimal>("DiscountAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<decimal>("DiscountPercentage")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)");

                    b.Property<bool>("IsCancelled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int?>("ProductId1")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("SaleId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Subtotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<decimal>("Total")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductId1");

                    b.HasIndex("SaleId");

                    b.ToTable("SaleItems", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae1"),
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Email = "admin@ambev.com.br",
                            Password = "AQAAAAIAAYagAAAAEPVEyR7qQKqgXgGUPtQzreG7Yl1Pq7FWvQE0atQQjJzPe7vxKrCxZhQGz/AJvEXvww==",
                            Phone = "(11) 99999-9999",
                            Role = "Admin",
                            Status = "Active",
                            Username = "admin"
                        },
                        new
                        {
                            Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae2"),
                            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Email = "customer@email.com",
                            Password = "AQAAAAIAAYagAAAAEPVEyR7qQKqgXgGUPtQzreG7Yl1Pq7FWvQE0atQQjJzPe7vxKrCxZhQGz/AJvEXvww==",
                            Phone = "(11) 88888-8888",
                            Role = "Customer",
                            Status = "Active",
                            Username = "customer"
                        });
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Product", b =>
                {
                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.Models.ProductRating", "Rating", b1 =>
                        {
                            b1.Property<int>("ProductId")
                                .HasColumnType("integer");

                            b1.Property<int>("Count")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasDefaultValue(0)
                                .HasColumnName("RatingCount");

                            b1.Property<decimal>("Rate")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("decimal(3,2)")
                                .HasDefaultValue(0m)
                                .HasColumnName("Rating");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");

                            b1.HasData(
                                new
                                {
                                    ProductId = 1,
                                    Count = 150,
                                    Rate = 4.5m
                                },
                                new
                                {
                                    ProductId = 2,
                                    Count = 120,
                                    Rate = 4.2m
                                },
                                new
                                {
                                    ProductId = 3,
                                    Count = 200,
                                    Rate = 4.8m
                                },
                                new
                                {
                                    ProductId = 4,
                                    Count = 180,
                                    Rate = 4.7m
                                });
                        });

                    b.Navigation("Rating")
                        .IsRequired();
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sale", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Branch", "Branch")
                        .WithMany("Sales")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.SaleItem", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Product", null)
                        .WithMany("SaleItems")
                        .HasForeignKey("ProductId1");

                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Sale", "Sale")
                        .WithMany("Items")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Branch", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Product", b =>
                {
                    b.Navigation("SaleItems");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sale", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
