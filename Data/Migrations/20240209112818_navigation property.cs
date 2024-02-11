using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Management.Data.Migrations
{
    public partial class navigationproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarTransmissionType_TransmissionTypeId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarType_TypeId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "TransmissionId",
                table: "Car");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Car",
                newName: "CarTypeId");

            migrationBuilder.RenameColumn(
                name: "TransmissionTypeId",
                table: "Car",
                newName: "CarTransmissionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_TypeId",
                table: "Car",
                newName: "IX_Car_CarTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_TransmissionTypeId",
                table: "Car",
                newName: "IX_Car_CarTransmissionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarTransmissionType_CarTransmissionTypeId",
                table: "Car",
                column: "CarTransmissionTypeId",
                principalTable: "CarTransmissionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarType_CarTypeId",
                table: "Car",
                column: "CarTypeId",
                principalTable: "CarType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarTransmissionType_CarTransmissionTypeId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Car_CarType_CarTypeId",
                table: "Car");

            migrationBuilder.RenameColumn(
                name: "CarTypeId",
                table: "Car",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "CarTransmissionTypeId",
                table: "Car",
                newName: "TransmissionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_CarTypeId",
                table: "Car",
                newName: "IX_Car_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_CarTransmissionTypeId",
                table: "Car",
                newName: "IX_Car_TransmissionTypeId");

            migrationBuilder.AddColumn<int>(
                name: "TransmissionId",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarTransmissionType_TransmissionTypeId",
                table: "Car",
                column: "TransmissionTypeId",
                principalTable: "CarTransmissionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_CarType_TypeId",
                table: "Car",
                column: "TypeId",
                principalTable: "CarType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
