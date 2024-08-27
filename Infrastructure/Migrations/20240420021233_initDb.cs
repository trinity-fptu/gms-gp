using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inspectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchasingManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasingManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchasingStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasingStaffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyTaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseStaffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionPlanCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionPlans_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RawMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<int>(type: "int", nullable: true),
                    Shape = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<int>(type: "int", nullable: true),
                    Package = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageGuide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatePrice = table.Column<double>(type: "float", nullable: true),
                    MaterialCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RawMaterials_MaterialCategories_MaterialCategoryId",
                        column: x => x.MaterialCategoryId,
                        principalTable: "MaterialCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<double>(type: "float", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierAccountRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApproveStatus = table.Column<int>(type: "int", nullable: false),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyTaxCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestStaffId = table.Column<int>(type: "int", nullable: true),
                    ApproveManagerId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierAccountRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierAccountRequests_Managers_ApproveManagerId",
                        column: x => x.ApproveManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupplierAccountRequests_PurchasingStaffs_RequestStaffId",
                        column: x => x.RequestStaffId,
                        principalTable: "PurchasingStaffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    PurchasingManagerId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    PurchasingStaffId = table.Column<int>(type: "int", nullable: true),
                    WarehouseStaffId = table.Column<int>(type: "int", nullable: true),
                    InspectorId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Inspectors_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "Inspectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_PurchasingManagers_PurchasingManagerId",
                        column: x => x.PurchasingManagerId,
                        principalTable: "PurchasingManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_PurchasingStaffs_PurchasingStaffId",
                        column: x => x.PurchasingStaffId,
                        principalTable: "PurchasingStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_WarehouseStaffs_WarehouseStaffId",
                        column: x => x.WarehouseStaffId,
                        principalTable: "WarehouseStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasingPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproveStatus = table.Column<int>(type: "int", nullable: false),
                    ProcessStatus = table.Column<int>(type: "int", nullable: false),
                    PurchasingManagerId = table.Column<int>(type: "int", nullable: true),
                    ProductionPlanId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasingPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasingPlans_ProductionPlans_ProductionPlanId",
                        column: x => x.ProductionPlanId,
                        principalTable: "ProductionPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchasingPlans_PurchasingManagers_PurchasingManagerId",
                        column: x => x.PurchasingManagerId,
                        principalTable: "PurchasingManagers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpectedMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequireQuantity = table.Column<double>(type: "float", nullable: false),
                    ProductionPlanId = table.Column<int>(type: "int", nullable: false),
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpectedMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpectedMaterials_ProductionPlans_ProductionPlanId",
                        column: x => x.ProductionPlanId,
                        principalTable: "ProductionPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpectedMaterials_RawMaterials_RawMaterialId",
                        column: x => x.RawMaterialId,
                        principalTable: "RawMaterials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WarehouseMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    ReturnQuantity = table.Column<double>(type: "float", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseMaterials_RawMaterials_RawMaterialId",
                        column: x => x.RawMaterialId,
                        principalTable: "RawMaterials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseMaterials_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductInPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    ProductionPlanId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInPlans_ProductionPlans_ProductionPlanId",
                        column: x => x.ProductionPlanId,
                        principalTable: "ProductionPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductInPlans_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMaterials_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductMaterials_RawMaterials_RawMaterialId",
                        column: x => x.RawMaterialId,
                        principalTable: "RawMaterials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchasingOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupplierApproveStatus = table.Column<int>(type: "int", nullable: true),
                    ManagerApproveStatus = table.Column<int>(type: "int", nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierTaxCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuppplierEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverCompanyPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverCompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverCompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOfDeliveryStage = table.Column<int>(type: "int", nullable: true),
                    TotalMaterialType = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    OrderStatus = table.Column<int>(type: "int", nullable: true),
                    PurchasingPlanId = table.Column<int>(type: "int", nullable: false),
                    PurchasingStaffId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasingOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasingOrders_PurchasingPlans_PurchasingPlanId",
                        column: x => x.PurchasingPlanId,
                        principalTable: "PurchasingPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchasingOrders_PurchasingStaffs_PurchasingStaffId",
                        column: x => x.PurchasingStaffId,
                        principalTable: "PurchasingStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchasingOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchasingTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    AssignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaskStatus = table.Column<int>(type: "int", nullable: true),
                    FinishedQuantity = table.Column<double>(type: "float", nullable: true),
                    RemainedQuantity = table.Column<double>(type: "float", nullable: true),
                    ProcessedQuantity = table.Column<double>(type: "float", nullable: true),
                    PurchasingPlanId = table.Column<int>(type: "int", nullable: false),
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    PurchasingStaffId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasingTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasingTasks_PurchasingPlans_PurchasingPlanId",
                        column: x => x.PurchasingPlanId,
                        principalTable: "PurchasingPlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchasingTasks_PurchasingStaffs_PurchasingStaffId",
                        column: x => x.PurchasingStaffId,
                        principalTable: "PurchasingStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchasingTasks_RawMaterials_RawMaterialId",
                        column: x => x.RawMaterialId,
                        principalTable: "RawMaterials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StageOrder = table.Column<int>(type: "int", nullable: true),
                    TotalTypeMaterial = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    DeliveryStatus = table.Column<int>(type: "int", nullable: true),
                    IsSupplemental = table.Column<bool>(type: "bit", nullable: false),
                    PurchasingOrderId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryStages_PurchasingOrders_PurchasingOrderId",
                        column: x => x.PurchasingOrderId,
                        principalTable: "PurchasingOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PurchasingOrderId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_PurchasingOrders_UserId",
                        column: x => x.UserId,
                        principalTable: "PurchasingOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageQuantity = table.Column<double>(type: "float", nullable: false),
                    PackagePrice = table.Column<double>(type: "float", nullable: false),
                    MaterialPerPackage = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    PurchasingOrderId = table.Column<int>(type: "int", nullable: false),
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMaterials_PurchasingOrders_PurchasingOrderId",
                        column: x => x.PurchasingOrderId,
                        principalTable: "PurchasingOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderMaterials_RawMaterials_RawMaterialId",
                        column: x => x.RawMaterialId,
                        principalTable: "RawMaterials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PO_Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResolveStatus = table.Column<int>(type: "int", nullable: true),
                    ReportTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierApprovingStatus = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    PurchasingStaffId = table.Column<int>(type: "int", nullable: true),
                    PurchasingOrderId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PO_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PO_Reports_PurchasingOrders_PurchasingOrderId",
                        column: x => x.PurchasingOrderId,
                        principalTable: "PurchasingOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PO_Reports_PurchasingStaffs_PurchasingStaffId",
                        column: x => x.PurchasingStaffId,
                        principalTable: "PurchasingStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PO_Reports_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImportMainWarehouseRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    POCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproveStatus = table.Column<int>(type: "int", nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestReasonContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryStageId = table.Column<int>(type: "int", nullable: false),
                    InspectorId = table.Column<int>(type: "int", nullable: false),
                    ApproveWStaffId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportMainWarehouseRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportMainWarehouseRequests_DeliveryStages_DeliveryStageId",
                        column: x => x.DeliveryStageId,
                        principalTable: "DeliveryStages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImportMainWarehouseRequests_Inspectors_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "Inspectors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImportMainWarehouseRequests_WarehouseStaffs_ApproveWStaffId",
                        column: x => x.ApproveWStaffId,
                        principalTable: "WarehouseStaffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InspectionRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestInspectDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApproveStatus = table.Column<int>(type: "int", nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryStageId = table.Column<int>(type: "int", nullable: false),
                    RequestStaffId = table.Column<int>(type: "int", nullable: false),
                    ApprovingInspectorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionRequests_DeliveryStages_DeliveryStageId",
                        column: x => x.DeliveryStageId,
                        principalTable: "DeliveryStages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InspectionRequests_Inspectors_ApprovingInspectorId",
                        column: x => x.ApprovingInspectorId,
                        principalTable: "Inspectors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InspectionRequests_PurchasingStaffs_RequestStaffId",
                        column: x => x.RequestStaffId,
                        principalTable: "PurchasingStaffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyMaterialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Package = table.Column<int>(type: "int", nullable: false),
                    MaterialPerPackage = table.Column<double>(type: "float", nullable: false),
                    TotalQuantity = table.Column<double>(type: "float", nullable: true),
                    PackagePrice = table.Column<double>(type: "float", nullable: false),
                    DeliveredQuantity = table.Column<double>(type: "float", nullable: true),
                    ReturnQuantity = table.Column<double>(type: "float", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    AfterInspectQuantity = table.Column<double>(type: "float", nullable: true),
                    TempImportDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TempExportDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MainImportDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanInspectDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WarehouseStatus = table.Column<int>(type: "int", nullable: true),
                    DeliveryStageId = table.Column<int>(type: "int", nullable: true),
                    RawMaterialId = table.Column<int>(type: "int", nullable: false),
                    WarehouseMaterialId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseMaterials_DeliveryStages_DeliveryStageId",
                        column: x => x.DeliveryStageId,
                        principalTable: "DeliveryStages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseMaterials_RawMaterials_RawMaterialId",
                        column: x => x.RawMaterialId,
                        principalTable: "RawMaterials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseMaterials_WarehouseMaterials_WarehouseMaterialId",
                        column: x => x.WarehouseMaterialId,
                        principalTable: "WarehouseMaterials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TempWarehouseRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    POCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproveStatus = table.Column<int>(type: "int", nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestReasonContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestType = table.Column<int>(type: "int", nullable: true),
                    RequestExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryStageId = table.Column<int>(type: "int", nullable: false),
                    RequestStaffId = table.Column<int>(type: "int", nullable: true),
                    RequestInspectorId = table.Column<int>(type: "int", nullable: true),
                    ApproveWStaffId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempWarehouseRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempWarehouseRequests_DeliveryStages_DeliveryStageId",
                        column: x => x.DeliveryStageId,
                        principalTable: "DeliveryStages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TempWarehouseRequests_Inspectors_RequestInspectorId",
                        column: x => x.RequestInspectorId,
                        principalTable: "Inspectors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TempWarehouseRequests_PurchasingStaffs_RequestStaffId",
                        column: x => x.RequestStaffId,
                        principalTable: "PurchasingStaffs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TempWarehouseRequests_WarehouseStaffs_ApproveWStaffId",
                        column: x => x.ApproveWStaffId,
                        principalTable: "WarehouseStaffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InspectionForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InspectLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InspectorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InspectionRequestId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectionForms_InspectionRequests_InspectionRequestId",
                        column: x => x.InspectionRequestId,
                        principalTable: "InspectionRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WarehouseForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormType = table.Column<int>(type: "int", nullable: true),
                    POCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiveCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiveWarehouse = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    RequestStaffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproveWarehouseStaffName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempWarehouseRequestId = table.Column<int>(type: "int", nullable: true),
                    ImportMainWarehouseRequestId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseForms_ImportMainWarehouseRequests_ImportMainWarehouseRequestId",
                        column: x => x.ImportMainWarehouseRequestId,
                        principalTable: "ImportMainWarehouseRequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseForms_TempWarehouseRequests_TempWarehouseRequestId",
                        column: x => x.TempWarehouseRequestId,
                        principalTable: "TempWarehouseRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaterialInspectResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestQuantity = table.Column<double>(type: "float", nullable: false),
                    MaterialPerPackage = table.Column<double>(type: "float", nullable: false),
                    InspectionPassQuantity = table.Column<double>(type: "float", nullable: true),
                    InspectionFailQuantity = table.Column<double>(type: "float", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InspectStatus = table.Column<int>(type: "int", nullable: true),
                    PurchaseMaterialId = table.Column<int>(type: "int", nullable: false),
                    InspectionFormId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialInspectResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialInspectResults_InspectionForms_InspectionFormId",
                        column: x => x.InspectionFormId,
                        principalTable: "InspectionForms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialInspectResults_PurchaseMaterials_PurchaseMaterialId",
                        column: x => x.PurchaseMaterialId,
                        principalTable: "PurchaseMaterials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WarehouseFormMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestQuantity = table.Column<double>(type: "float", nullable: true),
                    MaterialPerPackage = table.Column<double>(type: "float", nullable: false),
                    PackagePrice = table.Column<double>(type: "float", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: true),
                    FormStatus = table.Column<int>(type: "int", nullable: true),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WarehouseFormId = table.Column<int>(type: "int", nullable: false),
                    PurchaseMaterialId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseFormMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseFormMaterials_PurchaseMaterials_PurchaseMaterialId",
                        column: x => x.PurchaseMaterialId,
                        principalTable: "PurchaseMaterials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseFormMaterials_WarehouseForms_WarehouseFormId",
                        column: x => x.WarehouseFormId,
                        principalTable: "WarehouseForms",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3504), null, false, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3530), "Manager" },
                    { 2, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3541), null, false, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3542), "Purchasing Manager" },
                    { 3, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3543), null, false, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3544), "Purchasing Staff" },
                    { 4, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3545), null, false, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3546), "Warehouse Staff" },
                    { 5, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3547), null, false, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3561), "Inspector" },
                    { 6, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3563), null, false, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3564), "Supplier" },
                    { 7, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3565), null, false, null, new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3566), "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryStages_PurchasingOrderId",
                table: "DeliveryStages",
                column: "PurchasingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedMaterials_ProductionPlanId",
                table: "ExpectedMaterials",
                column: "ProductionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedMaterials_RawMaterialId",
                table: "ExpectedMaterials",
                column: "RawMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportMainWarehouseRequests_ApproveWStaffId",
                table: "ImportMainWarehouseRequests",
                column: "ApproveWStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportMainWarehouseRequests_DeliveryStageId",
                table: "ImportMainWarehouseRequests",
                column: "DeliveryStageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportMainWarehouseRequests_InspectorId",
                table: "ImportMainWarehouseRequests",
                column: "InspectorId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionForms_InspectionRequestId",
                table: "InspectionForms",
                column: "InspectionRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InspectionRequests_ApprovingInspectorId",
                table: "InspectionRequests",
                column: "ApprovingInspectorId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionRequests_DeliveryStageId",
                table: "InspectionRequests",
                column: "DeliveryStageId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionRequests_RequestStaffId",
                table: "InspectionRequests",
                column: "RequestStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInspectResults_InspectionFormId",
                table: "MaterialInspectResults",
                column: "InspectionFormId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInspectResults_PurchaseMaterialId",
                table: "MaterialInspectResults",
                column: "PurchaseMaterialId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterials_PurchasingOrderId",
                table: "OrderMaterials",
                column: "PurchasingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterials_RawMaterialId",
                table: "OrderMaterials",
                column: "RawMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_PO_Reports_PurchasingOrderId",
                table: "PO_Reports",
                column: "PurchasingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PO_Reports_PurchasingStaffId",
                table: "PO_Reports",
                column: "PurchasingStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_PO_Reports_SupplierId",
                table: "PO_Reports",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInPlans_ProductId",
                table: "ProductInPlans",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInPlans_ProductionPlanId",
                table: "ProductInPlans",
                column: "ProductionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlans_ManagerId",
                table: "ProductionPlans",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlans_ProductionPlanCode",
                table: "ProductionPlans",
                column: "ProductionPlanCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterials_ProductId",
                table: "ProductMaterials",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterials_RawMaterialId",
                table: "ProductMaterials",
                column: "RawMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseMaterials_DeliveryStageId",
                table: "PurchaseMaterials",
                column: "DeliveryStageId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseMaterials_RawMaterialId",
                table: "PurchaseMaterials",
                column: "RawMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseMaterials_WarehouseMaterialId",
                table: "PurchaseMaterials",
                column: "WarehouseMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingOrders_POCode",
                table: "PurchasingOrders",
                column: "POCode",
                unique: true,
                filter: "[POCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingOrders_PurchasingPlanId",
                table: "PurchasingOrders",
                column: "PurchasingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingOrders_PurchasingStaffId",
                table: "PurchasingOrders",
                column: "PurchasingStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingOrders_SupplierId",
                table: "PurchasingOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingPlans_PlanCode",
                table: "PurchasingPlans",
                column: "PlanCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingPlans_ProductionPlanId",
                table: "PurchasingPlans",
                column: "ProductionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingPlans_PurchasingManagerId",
                table: "PurchasingPlans",
                column: "PurchasingManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingTasks_PurchasingPlanId",
                table: "PurchasingTasks",
                column: "PurchasingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingTasks_PurchasingStaffId",
                table: "PurchasingTasks",
                column: "PurchasingStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasingTasks_RawMaterialId",
                table: "PurchasingTasks",
                column: "RawMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterials_MaterialCategoryId",
                table: "RawMaterials",
                column: "MaterialCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAccountRequests_ApproveManagerId",
                table: "SupplierAccountRequests",
                column: "ApproveManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAccountRequests_RequestStaffId",
                table: "SupplierAccountRequests",
                column: "RequestStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TempWarehouseRequests_ApproveWStaffId",
                table: "TempWarehouseRequests",
                column: "ApproveWStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TempWarehouseRequests_DeliveryStageId",
                table: "TempWarehouseRequests",
                column: "DeliveryStageId");

            migrationBuilder.CreateIndex(
                name: "IX_TempWarehouseRequests_RequestInspectorId",
                table: "TempWarehouseRequests",
                column: "RequestInspectorId");

            migrationBuilder.CreateIndex(
                name: "IX_TempWarehouseRequests_RequestStaffId",
                table: "TempWarehouseRequests",
                column: "RequestStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_InspectorId",
                table: "Users",
                column: "InspectorId",
                unique: true,
                filter: "[InspectorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ManagerId",
                table: "Users",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PurchasingManagerId",
                table: "Users",
                column: "PurchasingManagerId",
                unique: true,
                filter: "[PurchasingManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PurchasingStaffId",
                table: "Users",
                column: "PurchasingStaffId",
                unique: true,
                filter: "[PurchasingStaffId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StaffCode",
                table: "Users",
                column: "StaffCode",
                unique: true,
                filter: "[StaffCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SupplierId",
                table: "Users",
                column: "SupplierId",
                unique: true,
                filter: "[SupplierId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WarehouseStaffId",
                table: "Users",
                column: "WarehouseStaffId",
                unique: true,
                filter: "[WarehouseStaffId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseFormMaterials_PurchaseMaterialId",
                table: "WarehouseFormMaterials",
                column: "PurchaseMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseFormMaterials_WarehouseFormId",
                table: "WarehouseFormMaterials",
                column: "WarehouseFormId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseForms_ImportMainWarehouseRequestId",
                table: "WarehouseForms",
                column: "ImportMainWarehouseRequestId",
                unique: true,
                filter: "[ImportMainWarehouseRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseForms_TempWarehouseRequestId",
                table: "WarehouseForms",
                column: "TempWarehouseRequestId",
                unique: true,
                filter: "[TempWarehouseRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMaterials_RawMaterialId",
                table: "WarehouseMaterials",
                column: "RawMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseMaterials_WarehouseId",
                table: "WarehouseMaterials",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpectedMaterials");

            migrationBuilder.DropTable(
                name: "MaterialInspectResults");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrderMaterials");

            migrationBuilder.DropTable(
                name: "PO_Reports");

            migrationBuilder.DropTable(
                name: "ProductInPlans");

            migrationBuilder.DropTable(
                name: "ProductMaterials");

            migrationBuilder.DropTable(
                name: "PurchasingTasks");

            migrationBuilder.DropTable(
                name: "SupplierAccountRequests");

            migrationBuilder.DropTable(
                name: "WarehouseFormMaterials");

            migrationBuilder.DropTable(
                name: "InspectionForms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PurchaseMaterials");

            migrationBuilder.DropTable(
                name: "WarehouseForms");

            migrationBuilder.DropTable(
                name: "InspectionRequests");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "WarehouseMaterials");

            migrationBuilder.DropTable(
                name: "ImportMainWarehouseRequests");

            migrationBuilder.DropTable(
                name: "TempWarehouseRequests");

            migrationBuilder.DropTable(
                name: "RawMaterials");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "DeliveryStages");

            migrationBuilder.DropTable(
                name: "Inspectors");

            migrationBuilder.DropTable(
                name: "WarehouseStaffs");

            migrationBuilder.DropTable(
                name: "MaterialCategories");

            migrationBuilder.DropTable(
                name: "PurchasingOrders");

            migrationBuilder.DropTable(
                name: "PurchasingPlans");

            migrationBuilder.DropTable(
                name: "PurchasingStaffs");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "ProductionPlans");

            migrationBuilder.DropTable(
                name: "PurchasingManagers");

            migrationBuilder.DropTable(
                name: "Managers");
        }
    }
}
