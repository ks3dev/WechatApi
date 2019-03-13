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
                name: "operationlog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ip = table.Column<string>(maxLength: 15, nullable: false),
                    Telephone = table.Column<string>(maxLength: 15, nullable: true),
                    ObjectType = table.Column<string>(maxLength: 15, nullable: true),
                    ObjectID = table.Column<string>(maxLength: 15, nullable: true),
                    Product = table.Column<string>(maxLength: 15, nullable: true),
                    Project = table.Column<string>(maxLength: 15, nullable: true),
                    Action = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    OperationTime = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    OperationData = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operationlog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wxusers",
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
                    table.PrimaryKey("PK_wxusers", x => x.Id);
                    table.UniqueConstraint("AK_wxusers_Uin", x => x.Uin);
                });

            migrationBuilder.CreateTable(
                name: "wxfriends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Uin = table.Column<int>(nullable: false),
                    Telephone = table.Column<string>(maxLength: 11, nullable: true),
                    UserName = table.Column<string>(maxLength: 1000, nullable: true),
                    NickName = table.Column<string>(maxLength: 500, nullable: true),
                    HeadImgUrl = table.Column<string>(nullable: true),
                    ContactFlag = table.Column<long>(nullable: false),
                    VerifyFlag = table.Column<long>(nullable: false),
                    AttrStatus = table.Column<long>(nullable: false),
                    RemarkName = table.Column<string>(maxLength: 500, nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Signature = table.Column<string>(maxLength: 255, nullable: true),
                    StarFriend = table.Column<bool>(nullable: false),
                    Province = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 80, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UserUin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wxfriends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wxfriends_wxusers_UserUin",
                        column: x => x.UserUin,
                        principalTable: "wxusers",
                        principalColumn: "Uin",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "wxuserdetails",
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
                    table.PrimaryKey("PK_wxuserdetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wxuserdetails_wxusers_UserUin",
                        column: x => x.UserUin,
                        principalTable: "wxusers",
                        principalColumn: "Uin",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wxfriends_UserUin",
                table: "wxfriends",
                column: "UserUin");

            migrationBuilder.CreateIndex(
                name: "IX_wxuserdetails_UserUin",
                table: "wxuserdetails",
                column: "UserUin",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operationlog");

            migrationBuilder.DropTable(
                name: "wxfriends");

            migrationBuilder.DropTable(
                name: "wxuserdetails");

            migrationBuilder.DropTable(
                name: "wxusers");
        }
    }
}
