using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Holstentor.Migrations
{
    public partial class mig0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Tbl_Album",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Date = table.Column<DateTime>(nullable: false),
            //        Image = table.Column<string>(nullable: false),
            //        NamePic = table.Column<string>(maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tbl_Album", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tbl_Hochladen",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ImageAbout = table.Column<string>(nullable: false),
            //        ImageFooter = table.Column<string>(nullable: false),
            //        ImageGallery = table.Column<string>(nullable: false),
            //        Logo = table.Column<string>(nullable: false),
            //        Video = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tbl_Hochladen", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tbl_Index",
            //    columns: table => new
            //    {
            //        IDIndex = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Title = table.Column<string>(maxLength: 100, nullable: false),
            //        TypedText1 = table.Column<string>(maxLength: 300, nullable: false),
            //        TypedText2 = table.Column<string>(maxLength: 300, nullable: true),
            //        TypedText3 = table.Column<string>(maxLength: 300, nullable: true),
            //        TypedText4 = table.Column<string>(maxLength: 300, nullable: true),
            //        TypedText5 = table.Column<string>(maxLength: 300, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tbl_Index", x => x.IDIndex);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tbl_IndexET",
            //    columns: table => new
            //    {
            //        IDIndex = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        City = table.Column<string>(nullable: false),
            //        Country = table.Column<string>(nullable: false),
            //        Description = table.Column<string>(nullable: false),
            //        Email = table.Column<string>(maxLength: 256, nullable: false),
            //        EmbedLinkGoogleMap = table.Column<string>(nullable: false),
            //        NameSite = table.Column<string>(nullable: false),
            //        Number = table.Column<string>(nullable: false),
            //        PhoneNumber = table.Column<string>(nullable: false),
            //        PostCode = table.Column<string>(nullable: false),
            //        Street = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tbl_IndexET", x => x.IDIndex);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tbl_Kategorie",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ActivePassive = table.Column<bool>(nullable: false),
            //        Description = table.Column<string>(maxLength: 150, nullable: true),
            //        FontName = table.Column<string>(maxLength: 30, nullable: true),
            //        TitleCategory = table.Column<string>(maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tbl_Kategorie", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tbl_Nachricht",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Confirm = table.Column<bool>(nullable: false),
            //        Date = table.Column<DateTime>(nullable: false),
            //        DateUpdate = table.Column<DateTime>(nullable: true),
            //        Text = table.Column<string>(maxLength: 250, nullable: false),
            //        TextAdmin = table.Column<string>(maxLength: 250, nullable: false),
            //        UserIdRecive = table.Column<string>(nullable: true),
            //        UserIdSend = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tbl_Nachricht", x => x.ID);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(nullable: false),
            //        AccessFailedCount = table.Column<int>(nullable: false),
            //        ConcurrencyStamp = table.Column<string>(nullable: true),
            //        Date = table.Column<DateTime>(nullable: false),
            //        Email = table.Column<string>(maxLength: 256, nullable: true),
            //        EmailConfirmed = table.Column<bool>(nullable: false),
            //        LockoutEnabled = table.Column<bool>(nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
            //        Name = table.Column<string>(maxLength: 256, nullable: true),
            //        NameFamily = table.Column<string>(maxLength: 256, nullable: true),
            //        NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
            //        NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
            //        PasswordHash = table.Column<string>(nullable: true),
            //        PhoneNumber = table.Column<string>(nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(nullable: false),
            //        SecurityStamp = table.Column<string>(nullable: true),
            //        TwoFactorEnabled = table.Column<bool>(nullable: false),
            //        UserName = table.Column<string>(maxLength: 256, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoles",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(nullable: false),
            //        ConcurrencyStamp = table.Column<string>(nullable: true),
            //        Name = table.Column<string>(maxLength: 256, nullable: true),
            //        NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserTokens",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        LoginProvider = table.Column<string>(nullable: false),
            //        Name = table.Column<string>(nullable: false),
            //        Value = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tbl_Unterkategorie",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ActivePassive = table.Column<bool>(nullable: false),
            //        CategoryID = table.Column<int>(nullable: true),
            //        Description = table.Column<string>(maxLength: 150, nullable: true),
            //        FontName = table.Column<string>(maxLength: 30, nullable: true),
            //        Subtitle1 = table.Column<string>(maxLength: 50, nullable: true),
            //        Subtitle2 = table.Column<string>(maxLength: 50, nullable: true),
            //        Subtitle3 = table.Column<string>(maxLength: 50, nullable: true),
            //        TitleSubCategory = table.Column<string>(maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tbl_Unterkategorie", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_Tbl_Unterkategorie_Tbl_Kategorie_CategoryID",
            //            column: x => x.CategoryID,
            //            principalTable: "Tbl_Kategorie",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ClaimType = table.Column<string>(nullable: true),
            //        ClaimValue = table.Column<string>(nullable: true),
            //        UserId = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserLogins",
            //    columns: table => new
            //    {
            //        LoginProvider = table.Column<string>(nullable: false),
            //        ProviderKey = table.Column<string>(nullable: false),
            //        ProviderDisplayName = table.Column<string>(nullable: true),
            //        UserId = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoleClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ClaimType = table.Column<string>(nullable: true),
            //        ClaimValue = table.Column<string>(nullable: true),
            //        RoleId = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserRoles",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(nullable: false),
            //        RoleId = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tbl_Produkte",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        ActiveInactive = table.Column<bool>(nullable: false),
            //        CategoryID = table.Column<int>(nullable: true),
            //        Date = table.Column<DateTime>(nullable: false),
            //        Description = table.Column<string>(maxLength: 500, nullable: true),
            //        Price1 = table.Column<string>(maxLength: 50, nullable: false),
            //        Price2 = table.Column<string>(maxLength: 50, nullable: true),
            //        Price3 = table.Column<string>(maxLength: 50, nullable: true),
            //        SubCategoryID = table.Column<int>(nullable: true),
            //        Title = table.Column<string>(maxLength: 50, nullable: false),
            //        UserID = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tbl_Produkte", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_Tbl_Produkte_Tbl_Kategorie_CategoryID",
            //            column: x => x.CategoryID,
            //            principalTable: "Tbl_Kategorie",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Tbl_Produkte_Tbl_Unterkategorie_SubCategoryID",
            //            column: x => x.SubCategoryID,
            //            principalTable: "Tbl_Unterkategorie",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Tbl_Produkte_AspNetUsers_UserID",
            //            column: x => x.UserID,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tbl_Galerie",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Date = table.Column<DateTime>(nullable: false),
            //        Image = table.Column<string>(nullable: false),
            //        NamePic = table.Column<string>(maxLength: 50, nullable: false),
            //        ProductID = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tbl_Galerie", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_Tbl_Galerie_Tbl_Produkte_ProductID",
            //            column: x => x.ProductID,
            //            principalTable: "Tbl_Produkte",
            //            principalColumn: "ID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Tbl_Galerie_ProductID",
            //    table: "Tbl_Galerie",
            //    column: "ProductID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Tbl_Produkte_CategoryID",
            //    table: "Tbl_Produkte",
            //    column: "CategoryID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Tbl_Produkte_SubCategoryID",
            //    table: "Tbl_Produkte",
            //    column: "SubCategoryID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Tbl_Produkte_UserID",
            //    table: "Tbl_Produkte",
            //    column: "UserID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Tbl_Unterkategorie_CategoryID",
            //    table: "Tbl_Unterkategorie",
            //    column: "CategoryID");

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedUserName",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "RoleNameIndex",
            //    table: "AspNetRoles",
            //    column: "NormalizedName",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetRoleClaims_RoleId",
            //    table: "AspNetRoleClaims",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserClaims_UserId",
            //    table: "AspNetUserClaims",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserLogins_UserId",
            //    table: "AspNetUserLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_RoleId",
            //    table: "AspNetUserRoles",
            //    column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Album");

            migrationBuilder.DropTable(
                name: "Tbl_Galerie");

            migrationBuilder.DropTable(
                name: "Tbl_Hochladen");

            migrationBuilder.DropTable(
                name: "Tbl_Index");

            migrationBuilder.DropTable(
                name: "Tbl_IndexET");

            migrationBuilder.DropTable(
                name: "Tbl_Nachricht");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Tbl_Produkte");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tbl_Unterkategorie");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tbl_Kategorie");
        }
    }
}
