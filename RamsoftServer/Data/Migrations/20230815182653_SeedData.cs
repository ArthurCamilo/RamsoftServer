using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RamsoftServer.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var columns = new List<string> { "ToDo", "In Progress", "Review", "Done" };

            for (int i = 0; i < columns.Count; i++)
            {
                migrationBuilder.InsertData(table: "Columns", column: "Name", value: columns[i]);

                for (int j = 0; j < 10; j++)
                {
                    migrationBuilder.InsertData(table: "Cards", columns: new[] { "Name", "Index", "ColumnId" }, values: new object[] { RandomString(5), j, i + 1 });
                }
            }
        }

        private static Random random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
