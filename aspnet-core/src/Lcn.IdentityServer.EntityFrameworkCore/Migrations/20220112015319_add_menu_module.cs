using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lcn.IdentityServer.Migrations
{
    public partial class add_menu_module : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menu_managementMenuGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerProvider = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_managementMenuGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "menu_managementMenuGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_managementMenuGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "menu_managementMenuItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentMenuItem = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    CustomData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CssClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMenuItem = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_managementMenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "menu_managementUserMenuGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_managementUserMenuGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_menu_managementUserMenuGroups_menu_managementMenuGroups_MenuGroupId",
                        column: x => x.MenuGroupId,
                        principalTable: "menu_managementMenuGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_menu_managementUserMenuGroups_MenuGroupId",
                table: "menu_managementUserMenuGroups",
                column: "MenuGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_managementMenuGrants");

            migrationBuilder.DropTable(
                name: "menu_managementMenuItems");

            migrationBuilder.DropTable(
                name: "menu_managementUserMenuGroups");

            migrationBuilder.DropTable(
                name: "menu_managementMenuGroups");
        }
    }
}
