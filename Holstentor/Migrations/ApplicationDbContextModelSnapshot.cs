using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Holstentor.Data;

namespace Holstentor.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Album_Db", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<string>("NamePic")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Tbl_Album");
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Galerie_Db", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<string>("NamePic")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ProductID");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("Tbl_Galerie");
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Hochladen_Db", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageAbout")
                        .IsRequired();

                    b.Property<string>("ImageFooter")
                        .IsRequired();

                    b.Property<string>("ImageGallery")
                        .IsRequired();

                    b.Property<string>("Logo")
                        .IsRequired();

                    b.Property<string>("Video")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Tbl_Hochladen");
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Index_Db", b =>
                {
                    b.Property<int>("IDIndex")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("TypedText1")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("TypedText2")
                        .HasMaxLength(300);

                    b.Property<string>("TypedText3")
                        .HasMaxLength(300);

                    b.Property<string>("TypedText4")
                        .HasMaxLength(300);

                    b.Property<string>("TypedText5")
                        .HasMaxLength(300);

                    b.HasKey("IDIndex");

                    b.ToTable("Tbl_Index");
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.IndexET_Db", b =>
                {
                    b.Property<int>("IDIndex")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("EmbedLinkGoogleMap")
                        .IsRequired();

                    b.Property<string>("NameSite")
                        .IsRequired();

                    b.Property<string>("Number")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<string>("PostCode")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.HasKey("IDIndex");

                    b.ToTable("Tbl_IndexET");
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Kategorie_Db", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActivePassive");

                    b.Property<string>("Description")
                        .HasMaxLength(150);

                    b.Property<string>("FontName")
                        .HasMaxLength(30);

                    b.Property<string>("TitleCategory")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Tbl_Kategorie");
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Nachricht_Db", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Confirm");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime?>("DateUpdate");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("TextAdmin")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("UserIdRecive");

                    b.Property<string>("UserIdSend");

                    b.HasKey("ID");

                    b.ToTable("Tbl_Nachricht");
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Produkte_Db", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActiveInactive");

                    b.Property<int?>("CategoryID");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("Price1")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Price2")
                        .HasMaxLength(50);

                    b.Property<string>("Price3")
                        .HasMaxLength(50);

                    b.Property<int?>("SubCategoryID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SubCategoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Tbl_Produkte");
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Unterkategorie_Db", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActivePassive");

                    b.Property<int?>("CategoryID");

                    b.Property<string>("Description")
                        .HasMaxLength(150);

                    b.Property<string>("FontName")
                        .HasMaxLength(30);

                    b.Property<string>("Subtitle1")
                        .HasMaxLength(50);

                    b.Property<string>("Subtitle2")
                        .HasMaxLength(50);

                    b.Property<string>("Subtitle3")
                        .HasMaxLength(50);

                    b.Property<string>("TitleSubCategory")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Tbl_Unterkategorie");
                });

            modelBuilder.Entity("Holstentor.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NameFamily")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Galerie_Db", b =>
                {
                    b.HasOne("Holstentor.Data.Class_DbContext.Produkte_Db", "Tbl_Produkte")
                        .WithMany("Tbl_Galerie")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Produkte_Db", b =>
                {
                    b.HasOne("Holstentor.Data.Class_DbContext.Kategorie_Db", "Tbl_Kategorie")
                        .WithMany()
                        .HasForeignKey("CategoryID");

                    b.HasOne("Holstentor.Data.Class_DbContext.Unterkategorie_Db", "Tbl_Unterkategorie")
                        .WithMany()
                        .HasForeignKey("SubCategoryID");

                    b.HasOne("Holstentor.Models.ApplicationUser", "Tbl_User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("Holstentor.Data.Class_DbContext.Unterkategorie_Db", b =>
                {
                    b.HasOne("Holstentor.Data.Class_DbContext.Kategorie_Db", "Tbl_Kategorie")
                        .WithMany("Tbl_Unterkategorie")
                        .HasForeignKey("CategoryID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Holstentor.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Holstentor.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Holstentor.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
