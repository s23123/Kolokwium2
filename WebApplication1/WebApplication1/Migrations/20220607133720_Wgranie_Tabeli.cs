using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Wgranie_Tabeli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Musicans",
                columns: table => new
                {
                    IdMusican = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicans", x => x.IdMusican);
                });

            migrationBuilder.CreateTable(
                name: "MusicLabels",
                columns: table => new
                {
                    IdMusicLabel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicLabels", x => x.IdMusicLabel);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    IdAlbum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbumName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdMusicLabel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.IdAlbum);
                    table.ForeignKey(
                        name: "FK_Albums_MusicLabels_IdMusicLabel",
                        column: x => x.IdMusicLabel,
                        principalTable: "MusicLabels",
                        principalColumn: "IdMusicLabel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    IdTrack = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Duration = table.Column<float>(type: "real", nullable: false),
                    IdMusicAlbum = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.IdTrack);
                    table.ForeignKey(
                        name: "FK_Tracks_Albums_IdMusicAlbum",
                        column: x => x.IdMusicAlbum,
                        principalTable: "Albums",
                        principalColumn: "IdAlbum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Musican_Tracks",
                columns: table => new
                {
                    IdTrack = table.Column<int>(type: "int", nullable: false),
                    IdMusican = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musican_Tracks", x => new { x.IdMusican, x.IdTrack });
                    table.ForeignKey(
                        name: "FK_Musican_Tracks_Musicans_IdMusican",
                        column: x => x.IdMusican,
                        principalTable: "Musicans",
                        principalColumn: "IdMusican",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Musican_Tracks_Tracks_IdTrack",
                        column: x => x.IdTrack,
                        principalTable: "Tracks",
                        principalColumn: "IdTrack",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MusicLabels",
                columns: new[] { "IdMusicLabel", "Name" },
                values: new object[,]
                {
                    { 1, "NAzwaWytorwni" },
                    { 2, "Wytwornia" }
                });

            migrationBuilder.InsertData(
                table: "Musicans",
                columns: new[] { "IdMusican", "FirstName", "LastName", "NickName" },
                values: new object[,]
                {
                    { 1, "Czarek", "Rozwadowski", "abc" },
                    { 2, "Jan", "Kowalski", null }
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "IdAlbum", "AlbumName", "IdMusicLabel", "PublishDate" },
                values: new object[] { 1, "NazwaAlbumu", 1, new DateTime(2000, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "IdAlbum", "AlbumName", "IdMusicLabel", "PublishDate" },
                values: new object[] { 2, "Album", 2, new DateTime(2020, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "IdTrack", "Duration", "IdMusicAlbum", "TrackName" },
                values: new object[] { 1, 3f, 1, "NazwaKawalka" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "IdTrack", "Duration", "IdMusicAlbum", "TrackName" },
                values: new object[] { 2, 5f, 2, "NameOfTrack" });

            migrationBuilder.InsertData(
                table: "Musican_Tracks",
                columns: new[] { "IdMusican", "IdTrack" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Musican_Tracks",
                columns: new[] { "IdMusican", "IdTrack" },
                values: new object[] { 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_IdMusicLabel",
                table: "Albums",
                column: "IdMusicLabel");

            migrationBuilder.CreateIndex(
                name: "IX_Musican_Tracks_IdTrack",
                table: "Musican_Tracks",
                column: "IdTrack");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_IdMusicAlbum",
                table: "Tracks",
                column: "IdMusicAlbum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Musican_Tracks");

            migrationBuilder.DropTable(
                name: "Musicans");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "MusicLabels");
        }
    }
}
