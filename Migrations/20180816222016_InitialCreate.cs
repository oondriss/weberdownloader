using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApp.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.CreateTable(
            name: "AdditionalColumns",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_AdditionalColumns", x => x.Id);
            });

        _ = migrationBuilder.CreateTable(
            name: "Heads",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(nullable: true),
                Location = table.Column<string>(nullable: true),
                Hall = table.Column<string>(nullable: true),
                CronExp = table.Column<string>(nullable: true),
                Ip = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Heads", x => x.Id);
            });

        _ = migrationBuilder.CreateTable(
            name: "AdditionalValues",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                ColumnId = table.Column<int>(nullable: true),
                HeadId = table.Column<int>(nullable: true),
                Value = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_AdditionalValues", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_AdditionalValues_AdditionalColumns_ColumnId",
                    column: x => x.ColumnId,
                    principalTable: "AdditionalColumns",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_AdditionalValues_Heads_HeadId",
                    column: x => x.HeadId,
                    principalTable: "Heads",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "JobLogs",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                HeadId = table.Column<int>(nullable: true),
                Start = table.Column<DateTime>(nullable: false),
                Finish = table.Column<DateTime>(nullable: false),
                JobLogs = table.Column<string>(nullable: true),
                WithoutException = table.Column<bool>(nullable: false),
                Exception = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_JobLogs", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_JobLogs_Heads_HeadId",
                    column: x => x.HeadId,
                    principalTable: "Heads",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateIndex(
            name: "IX_AdditionalValues_ColumnId",
            table: "AdditionalValues",
            column: "ColumnId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_AdditionalValues_HeadId",
            table: "AdditionalValues",
            column: "HeadId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_JobLogs_HeadId",
            table: "JobLogs",
            column: "HeadId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "AdditionalValues");

        _ = migrationBuilder.DropTable(
            name: "JobLogs");

        _ = migrationBuilder.DropTable(
            name: "AdditionalColumns");

        _ = migrationBuilder.DropTable(
            name: "Heads");
    }
}
