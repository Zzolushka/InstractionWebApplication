using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InstructionsWebApplication.Data.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_AspNetUsers_UserId",
                table: "Instruction");

            migrationBuilder.DropForeignKey(
                name: "FK_Page_Instruction_InstructionId",
                table: "Page");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Page",
                table: "Page");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruction",
                table: "Instruction");

            migrationBuilder.RenameTable(
                name: "Page",
                newName: "Pages");

            migrationBuilder.RenameTable(
                name: "Instruction",
                newName: "Instructions");

            migrationBuilder.RenameIndex(
                name: "IX_Page_InstructionId",
                table: "Pages",
                newName: "IX_Pages_InstructionId");

            migrationBuilder.RenameIndex(
                name: "IX_Instruction_UserId",
                table: "Instructions",
                newName: "IX_Instructions_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pages",
                table: "Pages",
                column: "PageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructions",
                table: "Instructions",
                column: "InstructionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_AspNetUsers_UserId",
                table: "Instructions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Instructions_InstructionId",
                table: "Pages",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "InstructionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_AspNetUsers_UserId",
                table: "Instructions");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Instructions_InstructionId",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructions",
                table: "Instructions");

            migrationBuilder.RenameTable(
                name: "Pages",
                newName: "Page");

            migrationBuilder.RenameTable(
                name: "Instructions",
                newName: "Instruction");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_InstructionId",
                table: "Page",
                newName: "IX_Page_InstructionId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructions_UserId",
                table: "Instruction",
                newName: "IX_Instruction_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Page",
                table: "Page",
                column: "PageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruction",
                table: "Instruction",
                column: "InstructionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruction_AspNetUsers_UserId",
                table: "Instruction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Page_Instruction_InstructionId",
                table: "Page",
                column: "InstructionId",
                principalTable: "Instruction",
                principalColumn: "InstructionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
