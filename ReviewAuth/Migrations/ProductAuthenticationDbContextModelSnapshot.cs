﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductReviewAuthentication.Models;

namespace ReviewAuth.Migrations
{
    [DbContext(typeof(ProductAuthenticationDbContext))]
    partial class ProductAuthenticationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProductReviewAuthentication.Models.Product", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25);

                    b.Property<byte[]>("UserId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductReviewAuthentication.Models.Review", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<byte[]>("ProductId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.Property<int>("Ratings")
                        .HasColumnType("int");

                    b.Property<byte[]>("UserId")
                        .IsRequired()
                        .HasColumnType("varbinary(16)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ProductReviewAuthentication.Models.User", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(6)")
                        .HasMaxLength(6);

                    b.Property<string>("HashSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(70)")
                        .HasMaxLength(70);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProductReviewAuthentication.Models.Product", b =>
                {
                    b.HasOne("ProductReviewAuthentication.Models.User", "User")
                        .WithMany("Product")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductReviewAuthentication.Models.Review", b =>
                {
                    b.HasOne("ProductReviewAuthentication.Models.Product", "Product")
                        .WithMany("Review")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductReviewAuthentication.Models.User", "User")
                        .WithMany("Review")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
