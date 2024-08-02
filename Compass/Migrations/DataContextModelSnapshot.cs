﻿// <auto-generated />
using System;
using Compass.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Compass.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Compass.Models.AirFlight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AirlineId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Arrival_datetime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Departure_datetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AirlineId")
                        .IsUnique();

                    b.ToTable("AirFlights");
                });

            modelBuilder.Entity("Compass.Models.Airline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("Compass.Models.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AirFlightId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AirFlightId");

                    b.HasIndex("CityId")
                        .IsUnique();

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("Compass.Models.AppRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FeedBack")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Rate")
                        .HasColumnType("real");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("AppRates");
                });

            modelBuilder.Entity("Compass.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("HourPrice")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Compass.Models.Car_User", b =>
                {
                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CarId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Car_Users");
                });

            modelBuilder.Entity("Compass.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Compass.Models.FlightClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AirFlightId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AirFlightId");

                    b.ToTable("FlightClasses");
                });

            modelBuilder.Entity("Compass.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<float>("Stars")
                        .HasColumnType("real");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId")
                        .IsUnique();

                    b.HasIndex("PackageId")
                        .IsUnique();

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Compass.Models.Hotel_Reservation", b =>
                {
                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("HotelId", "ReservationId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Hotel_Reservations");
                });

            modelBuilder.Entity("Compass.Models.HotelRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelRooms");
                });

            modelBuilder.Entity("Compass.Models.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AirFlightId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date_End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_Start")
                        .HasColumnType("datetime2");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Tourism_PlaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AirFlightId")
                        .IsUnique();

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Compass.Models.Package_Resturant", b =>
                {
                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<int>("ResturantId")
                        .HasColumnType("int");

                    b.HasKey("PackageId", "ResturantId");

                    b.HasIndex("ResturantId");

                    b.ToTable("Package_Resturants");
                });

            modelBuilder.Entity("Compass.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Compass.Models.Resturant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Loctation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<float>("RegisterPrice")
                        .HasColumnType("real");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId")
                        .IsUnique();

                    b.ToTable("Resturants");
                });

            modelBuilder.Entity("Compass.Models.Resturant_Reservation", b =>
                {
                    b.Property<int>("ResturantId")
                        .HasColumnType("int");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("ResturantId", "ReservationId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Resturant_Reservations");
                });

            modelBuilder.Entity("Compass.Models.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<int>("YearlyVisitors")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId")
                        .IsUnique();

                    b.HasIndex("PackageId");

                    b.ToTable("Tourism_Places");
                });

            modelBuilder.Entity("Compass.Models.TourCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TourId")
                        .IsUnique();

                    b.ToTable("TourCategories");
                });

            modelBuilder.Entity("Compass.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Compass.Models.AirFlight", b =>
                {
                    b.HasOne("Compass.Models.Airline", "Airline")
                        .WithOne("AirFlight")
                        .HasForeignKey("Compass.Models.AirFlight", "AirlineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Airline");
                });

            modelBuilder.Entity("Compass.Models.Airport", b =>
                {
                    b.HasOne("Compass.Models.AirFlight", "AirFlight")
                        .WithMany("Airports")
                        .HasForeignKey("AirFlightId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Compass.Models.City", "City")
                        .WithOne("Airport")
                        .HasForeignKey("Compass.Models.Airport", "CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AirFlight");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Compass.Models.AppRate", b =>
                {
                    b.HasOne("Compass.Models.User", "User")
                        .WithOne("AppRate")
                        .HasForeignKey("Compass.Models.AppRate", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Compass.Models.Car_User", b =>
                {
                    b.HasOne("Compass.Models.User", "User")
                        .WithMany("Car_Users")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Compass.Models.Car", "Car")
                        .WithMany("Car_Users")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Compass.Models.FlightClass", b =>
                {
                    b.HasOne("Compass.Models.AirFlight", "AirFlight")
                        .WithMany("FlightClasses")
                        .HasForeignKey("AirFlightId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AirFlight");
                });

            modelBuilder.Entity("Compass.Models.Hotel", b =>
                {
                    b.HasOne("Compass.Models.City", "City")
                        .WithOne("Hotel")
                        .HasForeignKey("Compass.Models.Hotel", "CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Compass.Models.Package", "package")
                        .WithOne("Hotel")
                        .HasForeignKey("Compass.Models.Hotel", "PackageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("package");
                });

            modelBuilder.Entity("Compass.Models.Hotel_Reservation", b =>
                {
                    b.HasOne("Compass.Models.Reservation", "Reservation")
                        .WithMany("Hotel_Reservations")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Compass.Models.Hotel", "Hotel")
                        .WithMany("Hotel_Reservations")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("Compass.Models.HotelRoom", b =>
                {
                    b.HasOne("Compass.Models.Hotel", "Hotel")
                        .WithMany("HotelRooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Compass.Models.Package", b =>
                {
                    b.HasOne("Compass.Models.AirFlight", "AirFlight")
                        .WithOne("Package")
                        .HasForeignKey("Compass.Models.Package", "AirFlightId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AirFlight");
                });

            modelBuilder.Entity("Compass.Models.Package_Resturant", b =>
                {
                    b.HasOne("Compass.Models.Resturant", "Resturant")
                        .WithMany("Package_Resturants")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Compass.Models.Package", "Package")
                        .WithMany("Package_Resturants")
                        .HasForeignKey("ResturantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Package");

                    b.Navigation("Resturant");
                });

            modelBuilder.Entity("Compass.Models.Resturant", b =>
                {
                    b.HasOne("Compass.Models.City", "City")
                        .WithOne("Resturant")
                        .HasForeignKey("Compass.Models.Resturant", "CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Compass.Models.Resturant_Reservation", b =>
                {
                    b.HasOne("Compass.Models.Resturant", "Resturant")
                        .WithMany("Resturant_Reservations")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Compass.Models.Reservation", "Reservation")
                        .WithMany("Resturant_Reservations")
                        .HasForeignKey("ResturantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("Resturant");
                });

            modelBuilder.Entity("Compass.Models.Tour", b =>
                {
                    b.HasOne("Compass.Models.City", "City")
                        .WithOne("Tour")
                        .HasForeignKey("Compass.Models.Tour", "CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Compass.Models.Package", "Package")
                        .WithMany("Tours")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("Compass.Models.TourCategory", b =>
                {
                    b.HasOne("Compass.Models.Tour", "Tour")
                        .WithOne("TourCategory")
                        .HasForeignKey("Compass.Models.TourCategory", "TourId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("Compass.Models.User", b =>
                {
                    b.HasOne("Compass.Models.City", "Nationality")
                        .WithOne("User")
                        .HasForeignKey("Compass.Models.User", "CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Nationality");
                });

            modelBuilder.Entity("Compass.Models.AirFlight", b =>
                {
                    b.Navigation("Airports");

                    b.Navigation("FlightClasses");

                    b.Navigation("Package")
                        .IsRequired();
                });

            modelBuilder.Entity("Compass.Models.Airline", b =>
                {
                    b.Navigation("AirFlight")
                        .IsRequired();
                });

            modelBuilder.Entity("Compass.Models.Car", b =>
                {
                    b.Navigation("Car_Users");
                });

            modelBuilder.Entity("Compass.Models.City", b =>
                {
                    b.Navigation("Airport")
                        .IsRequired();

                    b.Navigation("Hotel")
                        .IsRequired();

                    b.Navigation("Resturant")
                        .IsRequired();

                    b.Navigation("Tour")
                        .IsRequired();

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Compass.Models.Hotel", b =>
                {
                    b.Navigation("HotelRooms");

                    b.Navigation("Hotel_Reservations");
                });

            modelBuilder.Entity("Compass.Models.Package", b =>
                {
                    b.Navigation("Hotel")
                        .IsRequired();

                    b.Navigation("Package_Resturants");

                    b.Navigation("Tours");
                });

            modelBuilder.Entity("Compass.Models.Reservation", b =>
                {
                    b.Navigation("Hotel_Reservations");

                    b.Navigation("Resturant_Reservations");
                });

            modelBuilder.Entity("Compass.Models.Resturant", b =>
                {
                    b.Navigation("Package_Resturants");

                    b.Navigation("Resturant_Reservations");
                });

            modelBuilder.Entity("Compass.Models.Tour", b =>
                {
                    b.Navigation("TourCategory")
                        .IsRequired();
                });

            modelBuilder.Entity("Compass.Models.User", b =>
                {
                    b.Navigation("AppRate")
                        .IsRequired();

                    b.Navigation("Car_Users");
                });
#pragma warning restore 612, 618
        }
    }
}
