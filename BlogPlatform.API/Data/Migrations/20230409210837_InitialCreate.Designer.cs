﻿// <auto-generated />
using System;
using BlogPlatform.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlogPlatform.API.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230409210837_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.2.23128.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BlogPlatform.API.Models.BlogPost", b =>
                {
                    b.Property<string>("Slug")
                        .HasColumnType("text");

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Slug");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("BlogPlatform.API.Models.BlogPostTag", b =>
                {
                    b.Property<string>("BlogPostSlug")
                        .HasColumnType("text");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("BlogPostSlug", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("BlogPostsTags");
                });

            modelBuilder.Entity("BlogPlatform.API.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BlogPostSlug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostSlug");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BlogPlatform.API.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("BlogPlatform.API.Models.BlogPostTag", b =>
                {
                    b.HasOne("BlogPlatform.API.Models.BlogPost", null)
                        .WithMany()
                        .HasForeignKey("BlogPostSlug")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogPlatform.API.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlogPlatform.API.Models.Comment", b =>
                {
                    b.HasOne("BlogPlatform.API.Models.BlogPost", "BlogPost")
                        .WithMany("Comments")
                        .HasForeignKey("BlogPostSlug")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlogPost");
                });

            modelBuilder.Entity("BlogPlatform.API.Models.BlogPost", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
