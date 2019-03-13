using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeiXin.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WxUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Uin = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 1000, nullable: true),
                    NickName = table.Column<string>(maxLength: 500, nullable: true),
                    HeadImgUrl = table.Column<string>(nullable: true),
                    PYInitial = table.Column<string>(nullable: true),
                    PYQuanPin = table.Column<string>(nullable: true),
                    StarFriend = table.Column<bool>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WxUsers", x => x.Id);
                    table.UniqueConstraint("AK_WxUsers_Uin", x => x.Uin);
                });

            migrationBuilder.CreateTable(
                name: "WxFriends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Uin = table.Column<int>(nullable: false),
                    Telephone = table.Column<string>(maxLength: 11, nullable: true),
                    UserName = table.Column<string>(maxLength: 1000, nullable: true),
                    NickName = table.Column<string>(maxLength: 500, nullable: true),
                    HeadImgUrl = table.Column<string>(nullable: true),
                    ContactFlag = table.Column<int>(nullable: false),
                    RemarkName = table.Column<string>(maxLength: 500, nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Signature = table.Column<string>(maxLength: 255, nullable: true),
                    PYInitial = table.Column<string>(nullable: true),
                    PYQuanPin = table.Column<string>(nullable: true),
                    RemarkPYInitial = table.Column<string>(nullable: true),
                    RemarkPYQuanPin = table.Column<string>(nullable: true),
                    StarFriend = table.Column<bool>(nullable: false),
                    Province = table.Column<string>(maxLength: 8, nullable: true),
                    City = table.Column<string>(maxLength: 30, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UserUin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WxFriends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WxFriends_WxUsers_UserUin",
                        column: x => x.UserUin,
                        principalTable: "WxUsers",
                        principalColumn: "Uin",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WxUserDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Hosts = table.Column<string>(maxLength: 10, nullable: true),
                    WxKeysJson = table.Column<string>(nullable: true),
                    UserUin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WxUserDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WxUserDetails_WxUsers_UserUin",
                        column: x => x.UserUin,
                        principalTable: "WxUsers",
                        principalColumn: "Uin",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WxFriends_UserUin",
                table: "WxFriends",
                column: "UserUin");

            migrationBuilder.CreateIndex(
                name: "IX_WxUserDetails_UserUin",
                table: "WxUserDetails",
                column: "UserUin",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WxFriends");

            migrationBuilder.DropTable(
                name: "WxUserDetails");

            migrationBuilder.DropTable(
                name: "WxUsers");
        }
    }
}
