﻿// <auto-generated />
using System;
using LibraryManagementSystemAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LibraryManagementSystemAPI.Migrations
{
    [DbContext(typeof(BookContext))]
    [Migration("20240724134718_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LibraryManagementSystemAPI.Books.Data.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Books.Data.BookAuthor", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthor");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Books.Data.BookCover", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("CoverFile")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BookId");

                    b.ToTable("BookCover");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Books.Data.BookGenre", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.HasKey("BookId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("BookGenre");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Models.BookAmount", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.HasKey("BookId");

                    b.ToTable("BooksAmount");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Models.BookRating", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("BookId");

                    b.ToTable("BooksRating");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Books.Data.Book", b =>
                {
                    b.HasOne("LibraryManagementSystemAPI.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Books.Data.BookAuthor", b =>
                {
                    b.HasOne("LibraryManagementSystemAPI.Models.Author", null)
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManagementSystemAPI.Books.Data.Book", null)
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Books.Data.BookCover", b =>
                {
                    b.HasOne("LibraryManagementSystemAPI.Books.Data.Book", "Book")
                        .WithOne("Cover")
                        .HasForeignKey("LibraryManagementSystemAPI.Books.Data.BookCover", "BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Books.Data.BookGenre", b =>
                {
                    b.HasOne("LibraryManagementSystemAPI.Books.Data.Book", null)
                        .WithMany("BookGenres")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManagementSystemAPI.Models.Genre", null)
                        .WithMany("BookGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Models.BookAmount", b =>
                {
                    b.HasOne("LibraryManagementSystemAPI.Books.Data.Book", "Book")
                        .WithOne("Amount")
                        .HasForeignKey("LibraryManagementSystemAPI.Models.BookAmount", "BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Models.BookRating", b =>
                {
                    b.HasOne("LibraryManagementSystemAPI.Books.Data.Book", "Book")
                        .WithOne("Rating")
                        .HasForeignKey("LibraryManagementSystemAPI.Models.BookRating", "BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Books.Data.Book", b =>
                {
                    b.Navigation("Amount");

                    b.Navigation("BookAuthors");

                    b.Navigation("BookGenres");

                    b.Navigation("Cover");

                    b.Navigation("Rating");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Models.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Models.Genre", b =>
                {
                    b.Navigation("BookGenres");
                });

            modelBuilder.Entity("LibraryManagementSystemAPI.Models.Publisher", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}