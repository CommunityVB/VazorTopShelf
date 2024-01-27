Imports Microsoft.EntityFrameworkCore.Migrations

Namespace Global.VazorTopShelf.Db.Migrations
    ''' <inheritdoc />
    Partial Public Class _001
        Inherits Migration

        ''' <inheritdoc />
        Protected Overrides Sub Up(migrationBuilder As MigrationBuilder)
            migrationBuilder.CreateTable(
                name:="Contacts",
                columns:=Function(table) New With {
                    .Id = table.Column(Of Integer)(type:="INTEGER", nullable:=False).
                        Annotation("Sqlite:Autoincrement", True),
                    .FirstName = table.Column(Of String)(type:="TEXT", maxLength:=10, nullable:=False, defaultValue:=""),
                    .LastName = table.Column(Of String)(type:="TEXT", maxLength:=10, nullable:=False, defaultValue:=""),
                    .Email = table.Column(Of String)(type:="TEXT", maxLength:=50, nullable:=False, defaultValue:=""),
                    .PhoneArea = table.Column(Of String)(type:="TEXT", maxLength:=3, nullable:=False, defaultValue:=""),
                    .PhonePrefix = table.Column(Of String)(type:="TEXT", maxLength:=3, nullable:=False, defaultValue:=""),
                    .PhoneSuffix = table.Column(Of String)(type:="TEXT", maxLength:=4, nullable:=False, defaultValue:=""),
                    .Instructions = table.Column(Of String)(type:="TEXT", nullable:=False, defaultValue:=""),
                    .Relation = table.Column(Of String)(type:="TEXT", maxLength:=10, nullable:=False, defaultValue:="")
                },
                constraints:=Sub(table)
                    table.PrimaryKey("PK_Contacts", Function(x) x.Id)
                End Sub)

            migrationBuilder.CreateIndex(
                name:="IX_Contacts_FirstName",
                table:="Contacts",
                column:="FirstName")

            migrationBuilder.CreateIndex(
                name:="IX_Contacts_FirstName_LastName",
                table:="Contacts",
                columns:={"FirstName", "LastName"})

            migrationBuilder.CreateIndex(
                name:="IX_Contacts_LastName",
                table:="Contacts",
                column:="LastName")
        End Sub

        ''' <inheritdoc />
        Protected Overrides Sub Down(migrationBuilder As MigrationBuilder)
            migrationBuilder.DropTable(
                name:="Contacts")
        End Sub
    End Class
End Namespace
