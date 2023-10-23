﻿// <auto-generated />
using Homework_06_05.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Homework_06_05.Data.Migrations
{
    [DbContext(typeof(JokeContext))]
    [Migration("20231012105548_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Homework_06_05.Data.Joke", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("OriginalId")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "jokeId");

                    b.Property<string>("Punchline")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "punchline");

                    b.Property<string>("Riddle")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "setup");

                    b.HasKey("Id");

                    b.ToTable("Jokes");
                });

            modelBuilder.Entity("Homework_06_05.Data.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Homework_06_05.Data.UserLikedJoke", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("JokeId")
                        .HasColumnType("int");

                    b.Property<bool>("Liked")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "JokeId");

                    b.HasIndex("JokeId");

                    b.ToTable("UserLikedJokes");
                });

            modelBuilder.Entity("Homework_06_05.Data.UserLikedJoke", b =>
                {
                    b.HasOne("Homework_06_05.Data.Joke", null)
                        .WithMany("UserLikedJokes")
                        .HasForeignKey("JokeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Homework_06_05.Data.Joke", b =>
                {
                    b.Navigation("UserLikedJokes");
                });
#pragma warning restore 612, 618
        }
    }
}
