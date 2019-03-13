using Microsoft.EntityFrameworkCore.Migrations;

namespace WeiXin.Migrations
{
    public partial class friends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PYInitial",
                table: "WxFriends");

            migrationBuilder.DropColumn(
                name: "PYQuanPin",
                table: "WxFriends");

            migrationBuilder.DropColumn(
                name: "RemarkPYInitial",
                table: "WxFriends");

            migrationBuilder.DropColumn(
                name: "RemarkPYQuanPin",
                table: "WxFriends");

            migrationBuilder.AlterColumn<long>(
                name: "ContactFlag",
                table: "WxFriends",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<long>(
                name: "AttrStatus",
                table: "WxFriends",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VerifyFlag",
                table: "WxFriends",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttrStatus",
                table: "WxFriends");

            migrationBuilder.DropColumn(
                name: "VerifyFlag",
                table: "WxFriends");

            migrationBuilder.AlterColumn<int>(
                name: "ContactFlag",
                table: "WxFriends",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "PYInitial",
                table: "WxFriends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PYQuanPin",
                table: "WxFriends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkPYInitial",
                table: "WxFriends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkPYQuanPin",
                table: "WxFriends",
                nullable: true);
        }
    }
}
