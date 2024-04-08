﻿// <auto-generated />
using System;
using System.Collections.Generic;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Migrations
{
    [DbContext(typeof(FellowshipDbContext))]
    partial class FellowshipDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BoardGame.WarOfTheRing.Fellowships.Application.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<DateTime>("OccuredOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ProcessedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Fellowship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("HuntingId")
                        .HasColumnType("uuid");

                    b.ComplexProperty<Dictionary<string, object>>("CorruptionCounter", "BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Fellowship.CorruptionCounter#CorruptionCounter", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ProgressCounter", "BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Fellowship.ProgressCounter#ProgressCounter", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<bool>("IsHidden")
                                .HasColumnType("boolean");

                            b1.Property<int>("Value")
                                .HasColumnType("integer");
                        });

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("Fellowships");
                });

            modelBuilder.Entity("BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Hunting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FellowshipId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.ComplexProperty<Dictionary<string, object>>("ActiveHunt", "BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Hunting.ActiveHunt#Hunt", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("AvailableReRollCount")
                                .HasColumnType("integer");

                            b1.Property<int>("NumberOfSuccessfulDiceResult")
                                .HasColumnType("integer");

                            b1.ComplexProperty<Dictionary<string, object>>("DrawnHuntTile", "BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Hunting.ActiveHunt#Hunt.DrawnHuntTile#HuntTile", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<bool>("HasDieIcon")
                                        .HasColumnType("boolean");

                                    b2.Property<bool>("HasEyeIcon")
                                        .HasColumnType("boolean");

                                    b2.Property<bool>("HasRevealIcon")
                                        .HasColumnType("boolean");

                                    b2.Property<bool>("HasStopIcon")
                                        .HasColumnType("boolean");

                                    b2.Property<int>("HuntDamage")
                                        .HasColumnType("integer");
                                });

                            b1.ComplexProperty<Dictionary<string, object>>("State", "BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Hunting.ActiveHunt#Hunt.State#HuntState", b2 =>
                                {
                                    b2.IsRequired();

                                    b2.Property<string>("Value")
                                        .HasColumnType("text");
                                });
                        });

                    b.ComplexProperty<Dictionary<string, object>>("HuntBox", "BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Hunting.HuntBox#HuntBox", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("NumberOfCharacterResultDice")
                                .HasColumnType("integer");

                            b1.Property<int>("NumberOfEyeResultDice")
                                .HasColumnType("integer");
                        });

                    b.HasKey("Id");

                    b.HasIndex("FellowshipId")
                        .IsUnique();

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("Huntings");
                });

            modelBuilder.Entity("BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Fellowship", b =>
                {
                    b.OwnsMany("BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.ValueObjects.Character", "Characters", b1 =>
                        {
                            b1.Property<Guid>("FellowshipId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<int>("Level")
                                .HasColumnType("integer");

                            b1.Property<int>("Name")
                                .HasColumnType("integer");

                            b1.HasKey("FellowshipId", "Id");

                            b1.ToTable("Fellowships");

                            b1.ToJson("Characters");

                            b1.WithOwner()
                                .HasForeignKey("FellowshipId");
                        });

                    b.Navigation("Characters");
                });

            modelBuilder.Entity("BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Hunting", b =>
                {
                    b.HasOne("BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Fellowship", null)
                        .WithOne()
                        .HasForeignKey("BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Hunting", "FellowshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects.HuntPool", "HuntPool", b1 =>
                        {
                            b1.Property<Guid>("HuntingId")
                                .HasColumnType("uuid");

                            b1.HasKey("HuntingId");

                            b1.ToTable("Huntings");

                            b1.ToJson("HuntPool");

                            b1.WithOwner()
                                .HasForeignKey("HuntingId");

                            b1.OwnsMany("BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects.HuntTile", "HuntTiles", b2 =>
                                {
                                    b2.Property<Guid>("HuntPoolHuntingId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    b2.Property<bool>("HasDieIcon")
                                        .HasColumnType("boolean");

                                    b2.Property<bool>("HasEyeIcon")
                                        .HasColumnType("boolean");

                                    b2.Property<bool>("HasRevealIcon")
                                        .HasColumnType("boolean");

                                    b2.Property<bool>("HasStopIcon")
                                        .HasColumnType("boolean");

                                    b2.Property<int>("HuntDamage")
                                        .HasColumnType("integer");

                                    b2.HasKey("HuntPoolHuntingId", "Id");

                                    b2.ToTable("Huntings");

                                    b2.ToJson("HuntTiles");

                                    b2.WithOwner()
                                        .HasForeignKey("HuntPoolHuntingId");
                                });

                            b1.Navigation("HuntTiles");
                        });

                    b.Navigation("HuntPool");
                });
#pragma warning restore 612, 618
        }
    }
}
