using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterDto_Users_UserDtoId",
                table: "CharacterDto");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterDto_WeaponDto_WeaponId",
                table: "CharacterDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeaponDto",
                table: "WeaponDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterDto",
                table: "CharacterDto");

            migrationBuilder.RenameTable(
                name: "WeaponDto",
                newName: "Weapons");

            migrationBuilder.RenameTable(
                name: "CharacterDto",
                newName: "Characters");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterDto_WeaponId",
                table: "Characters",
                newName: "IX_Characters_WeaponId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterDto_UserDtoId",
                table: "Characters",
                newName: "IX_Characters_UserDtoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Characters",
                table: "Characters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserDtoId",
                table: "Characters",
                column: "UserDtoId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Weapons_WeaponId",
                table: "Characters",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserDtoId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Weapons_WeaponId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Weapons",
                table: "Weapons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Characters",
                table: "Characters");

            migrationBuilder.RenameTable(
                name: "Weapons",
                newName: "WeaponDto");

            migrationBuilder.RenameTable(
                name: "Characters",
                newName: "CharacterDto");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_WeaponId",
                table: "CharacterDto",
                newName: "IX_CharacterDto_WeaponId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_UserDtoId",
                table: "CharacterDto",
                newName: "IX_CharacterDto_UserDtoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeaponDto",
                table: "WeaponDto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterDto",
                table: "CharacterDto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterDto_Users_UserDtoId",
                table: "CharacterDto",
                column: "UserDtoId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterDto_WeaponDto_WeaponId",
                table: "CharacterDto",
                column: "WeaponId",
                principalTable: "WeaponDto",
                principalColumn: "Id");
        }
    }
}
