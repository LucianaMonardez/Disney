﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Disney.Migrations.ApplicationDb
{
    public partial class initialDisney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    IdGenero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreGenero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagenGenero = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.IdGenero);
                });

            migrationBuilder.CreateTable(
                name: "MovieOrSeries",
                columns: table => new
                {
                    IdPelicula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagenPelicula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TituloPelicula = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaDeCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Calificacion = table.Column<int>(type: "int", nullable: false),
                    IdGenero = table.Column<int>(type: "int", nullable: false),
                    CharacterIdPersonaje = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieOrSeries", x => x.IdPelicula);
                    table.ForeignKey(
                        name: "FK_MovieOrSeries_Genres_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Genres",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    IdPersonaje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagenPersonaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombrePersonaje = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Peso = table.Column<double>(type: "float", nullable: false),
                    Historia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPelicula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.IdPersonaje);
                    table.ForeignKey(
                        name: "FK_Characters_MovieOrSeries_IdPelicula",
                        column: x => x.IdPelicula,
                        principalTable: "MovieOrSeries",
                        principalColumn: "IdPelicula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_IdPelicula",
                table: "Characters",
                column: "IdPelicula");

            migrationBuilder.CreateIndex(
                name: "IX_MovieOrSeries_CharacterIdPersonaje",
                table: "MovieOrSeries",
                column: "CharacterIdPersonaje");

            migrationBuilder.CreateIndex(
                name: "IX_MovieOrSeries_IdGenero",
                table: "MovieOrSeries",
                column: "IdGenero");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieOrSeries_Characters_CharacterIdPersonaje",
                table: "MovieOrSeries",
                column: "CharacterIdPersonaje",
                principalTable: "Characters",
                principalColumn: "IdPersonaje",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_MovieOrSeries_IdPelicula",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "MovieOrSeries");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
