﻿// <auto-generated />
using System;
using System.Collections.Generic;
using BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Migrations
{
    [DbContext(typeof(PoliticalTrackDbContext))]
    [Migration("20240324174501_GameIdAdded")]
    partial class GameIdAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.ComplexProperty<Dictionary<string, object>>("Name", "BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nation.Name#Name", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .HasColumnType("text");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Position", "BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nation.Position#Position", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Status", "BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nation.Status#Status", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<bool>("IsActive")
                                .HasColumnType("boolean");
                        });

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Nations");
                });
#pragma warning restore 612, 618
        }
    }
}
