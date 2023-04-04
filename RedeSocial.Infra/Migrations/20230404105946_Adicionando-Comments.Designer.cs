﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RedeSocial.Infra.Context;

#nullable disable

namespace RedeSocial.Infra.Migrations
{
    [DbContext(typeof(RedeSocialDbContext))]
    [Migration("20230404105946_Adicionando-Comments")]
    partial class AdicionandoComments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RedeSocial.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<Guid?>("ProfileIdProfile")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("ProfileIdProfile");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Friendship", b =>
                {
                    b.Property<Guid>("IdProfileA")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdProfileB")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdProfileA", "IdProfileB");

                    b.HasIndex("IdProfileB");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Categoria")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Imagem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Produto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileIdProfile")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProfileIdProfile");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Profile", b =>
                {
                    b.Property<Guid>("IdProfile")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdProfile");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.ProfileDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Aniversario")
                        .HasColumnType("datetime2");

                    b.Property<string>("CorCabelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorPele")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TipoCabelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoPele")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("ProfileDetails");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Comment", b =>
                {
                    b.HasOne("RedeSocial.Domain.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.HasOne("RedeSocial.Domain.Entities.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileIdProfile");

                    b.Navigation("Post");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Friendship", b =>
                {
                    b.HasOne("RedeSocial.Domain.Entities.Profile", "ProfileA")
                        .WithMany("FriendshipsB")
                        .HasForeignKey("IdProfileA")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RedeSocial.Domain.Entities.Profile", "ProfileB")
                        .WithMany("FriendshipsA")
                        .HasForeignKey("IdProfileB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProfileA");

                    b.Navigation("ProfileB");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Post", b =>
                {
                    b.HasOne("RedeSocial.Domain.Entities.Profile", "Profile")
                        .WithMany("Posts")
                        .HasForeignKey("ProfileIdProfile")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.ProfileDetails", b =>
                {
                    b.HasOne("RedeSocial.Domain.Entities.Profile", "Profile")
                        .WithOne("Details")
                        .HasForeignKey("RedeSocial.Domain.Entities.ProfileDetails", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("RedeSocial.Domain.Entities.Profile", b =>
                {
                    b.Navigation("Details");

                    b.Navigation("FriendshipsA");

                    b.Navigation("FriendshipsB");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
