using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Data.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sedan" },
                    { 2, "Station Wagon" },
                    { 3, "SUV" },
                    { 4, "Hatchback" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address" },
                values: new object[,]
                {
                    { 1, "Sofia - Airport" },
                    { 2, "Plovdiv - Airport" },
                    { 3, "Varna - Airport" },
                    { 4, "Burgas - Airport" },
                    { 5, "Sofia - office" },
                    { 6, "Plovdiv - office" },
                    { 7, "Varna - office" },
                    { 8, "Burgas - office" },
                    { 9, "Sozopol - office" },
                    { 10, "Sunny Beach - office" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AgentId", "CategoryId", "Description", "FuelType", "GearboxType", "ImageUrl", "Model", "NumberOfSeats", "PricePerDay", "RenterId", "Year" },
                values: new object[,]
                {
                    { 1, new Guid("3f7bc99c-804b-4975-9f2e-c2f6a0ae3b5e"), 4, "Volkswagen Golf 8 is the beginning of a whole new generation of models. With many digital innovations, dynamic design and exceptional comfort of function management. The new Golf is a great choice for a compact rental car.", 3, 2, "https://upload.wikimedia.org/wikipedia/commons/8/8a/2020_Volkswagen_Golf_Style_1.5_Front.jpg", "VW Golf 8", 5, 90m, null, 2022 },
                    { 2, new Guid("3f7bc99c-804b-4975-9f2e-c2f6a0ae3b5e"), 1, "Toyota Corolla offers everything that might be expected from Toyota: it has a stylish look, sporty handling, fuel efficiency. It is great car rental choice.", 1, 1, "https://di-uploads-pod28.dealerinspire.com/colonialtoyota/uploads/2020/09/2021-Toyota-Corolla-Indiana-PA-White-Left-1.jpg", "Toyota Corolla", 5, 100m, null, 2021 },
                    { 3, new Guid("3f7bc99c-804b-4975-9f2e-c2f6a0ae3b5e"), 2, "The new VW Passat station wagon with speed automatic transmission has notable vision and very clean shapes. You can use it to travel with your family or friends - a great choice for car rental.", 2, 2, "https://resource.digitaldealer.com.au/image/15009168736417dea75b995464285423_900_600-c.jpg", "VW Passat", 5, 110m, null, 2022 },
                    { 4, new Guid("3f7bc99c-804b-4975-9f2e-c2f6a0ae3b5e"), 3, "If the pleasure of driving in natural landscapes is what you are looking for, then Hyundai Tucson 4x4 is your car. Hyundai's new crossover for rent is designed to excel in all areas.", 1, 2, "https://media.ed.edmunds-media.com/hyundai/tucson/2022/oem/2022_hyundai_tucson_4dr-suv_limited_fq_oem_1_1280.jpg", "Hyundai Tucson", 5, 120m, null, 2022 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
