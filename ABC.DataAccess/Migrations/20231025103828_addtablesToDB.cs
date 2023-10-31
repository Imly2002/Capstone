using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABC.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addtablesToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApSuUn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetorSubd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ShipmentPreference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTerm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<long>(type: "bigint", nullable: false),
                    AdditionalNote = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplierCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierContactNumber = table.Column<long>(type: "bigint", nullable: false),
                    supplierEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierLotBlk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierStreetSubdv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierBarangay = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    supplierCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplierProvince = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    supplierZipCode = table.Column<int>(type: "int", nullable: false),
                    supplierNote = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ContactNumber = table.Column<long>(type: "bigint", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersManagement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<long>(type: "bigint", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    subCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Warehouse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostPrice = table.Column<float>(type: "real", nullable: false),
                    RetailPrice = table.Column<float>(type: "real", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    MinimumStockQuantity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecOne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecTwo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecThree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "CCTV" },
                    { 2, 2, "Printers" },
                    { 3, 3, "Computer Accesories" },
                    { 4, 4, "Cables & Tools" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "ApSuUn", "Barangay", "City", "ContactNumber", "EmailAddress", "FirstName", "LastName", "Province", "StreetorSubd", "Type", "ZipCode" },
                values: new object[] { 1, "Unit 1234", "Batman", "Antipolo", 9568271611L, "neiljejomar@gmail.com", "Kurt", "Betonio", "Rizal", "Jasmine St.", "Walk in", 1870 });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "Id", "AdditionalNote", "ContactNumber", "EmployeeName", "ExpectedDeliveryDate", "LocationDelivery", "PaymentTerm", "ShipmentPreference", "SupplierName" },
                values: new object[] { 1, "additional note goes in here", 9568271611L, "Neil", new DateTime(2023, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pasig", "Cash", "Cash On Delivery", "Kurt" });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "supplierBarangay", "supplierCity", "supplierCompanyName", "supplierContactNumber", "supplierDescription", "supplierEmail", "supplierLotBlk", "supplierNote", "supplierProvince", "supplierStatus", "supplierStreetSubdv", "supplierZipCode" },
                values: new object[,]
                {
                    { 1, "Maybancal", "Tanay", "Addvert", 9651232235L, "N/A", "addvert214@gmail.com", "c4 l5", "My supplier", "Rizal", "Active", "E. Corazon", 1870 },
                    { 2, "Maybancal", "Tanay", "Addvert", 9651232235L, "N/A", "addvert214@gmail.com", "c4 l5", "My supplier", "Rizal", "Active", "E. Corazon", 1870 }
                });

            migrationBuilder.InsertData(
                table: "UsersManagement",
                columns: new[] { "Id", "AccessLevel", "Address", "ContactNumber", "DateCreated", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { 1, "Admin", "Taytay Rizal", 9568271611L, new DateTime(2023, 10, 25, 18, 38, 28, 235, DateTimeKind.Local).AddTicks(4759), "neiljejomar@gmail.com", "Kurt", "Alarcos", "Hatdog" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Barcode", "Brand", "Category", "CostPrice", "Description", "Duration", "MinimumStockQuantity", "Provider", "RetailPrice", "SKU", "SpecOne", "SpecThree", "SpecTwo", "StockQuantity", "SupplierId", "Type", "Warehouse", "addNotes", "productName", "subCategory" },
                values: new object[,]
                {
                    { 1, 832175698L, "HP", "TBA", 800f, "Versatile all-in-one printer for printing, copying, and scanning", "12 months from date of purchase", 5, "Third-Party Warranty Company", 1299f, "printer-AllInOne-XYZ123", "Wireless connectivity", "Color touchscreen interface", "Automatic document feeder", 20, 2, "Extended Warranty", "Makati", "Additional Notes is here color touchscreen interface ", "XYZ123 All-in-One Printer", "office" },
                    { 2, 954532414L, "Samsung", "TBA", 1200f, "Panoramic view with motion detection", "7 days from date of purchase", 4, "Manufacturer", 1999f, "cctv-SmartCam-360", "all-in-one printer (print, copy, and scan)", "all-in-one printer (print, copy, and scan)", "all-in-one printer (print, copy, and scan)", 15, 1, "Manufacturers Warranty", "Pasig", null, "SmartCam 360 Security Camera", "wire" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "UsersManagement");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
