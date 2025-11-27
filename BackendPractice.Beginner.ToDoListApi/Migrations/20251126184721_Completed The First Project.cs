using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPractice.Beginner.TodoListAPI.Migrations
{
    /// <inheritdoc />
    public partial class CompletedTheFirstProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_User_UserId",
                table: "ToDoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItem",
                table: "ToDoItem");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "ToDoItem",
                newName: "ToDoItems");

            migrationBuilder.RenameIndex(
                name: "IX_User_Name",
                table: "Users",
                newName: "IX_Users_Name");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItem_UserId",
                table: "ToDoItems",
                newName: "IX_ToDoItems_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_Users_UserId",
                table: "ToDoItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_Users_UserId",
                table: "ToDoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "ToDoItems",
                newName: "ToDoItem");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Name",
                table: "User",
                newName: "IX_User_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItems_UserId",
                table: "ToDoItem",
                newName: "IX_ToDoItem_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItem",
                table: "ToDoItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_User_UserId",
                table: "ToDoItem",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
