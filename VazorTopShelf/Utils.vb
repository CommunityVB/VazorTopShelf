Imports System.IO

Public Class Utils
  ''' <summary>
  ''' Gets the project's content root folder.
  ''' </summary>
  ''' <value>
  ''' A <see cref="DirectoryInfo"/> instance representing the project's content root folder.
  ''' </value>
  ''' <remarks>
  ''' Used in this project for the database connection string only.
  ''' However, knowledge of this folder is necessary for API applications.<br/>
  ''' See <see href="https://nick.barrett.org.nz/an-update-on-hosting-an-asp-net-core-full-framework-app-in-a-windows-service-using-topshelf-c6d39e8ef827"/> for more information.</remarks>
  Public Shared ReadOnly Property ContentRootFolder As DirectoryInfo
    Get
      With New FileInfo(GetType(Utils).Assembly.Location)
        ContentRootFolder = .Directory
      End With

      Do While ContentRootFolder.GetDirectories.NotAny(Function(Folder) Folder.Name = "wwwroot")
        ContentRootFolder = ContentRootFolder.Parent
      Loop
    End Get
  End Property



  ''' <summary>
  ''' The database connection string.
  ''' </summary>
  ''' <returns>A connection string.</returns>
  Public Shared ReadOnly Property ConnectionString As String
    Get
      Return $"Data Source={DbFile.FullName}"
    End Get
  End Property



  ''' <summary>
  ''' The SQLite database file.
  ''' </summary>
  ''' <returns>A <see cref="FileInfo"/> instance representing the database.</returns>
  Public Shared ReadOnly Property DbFile As FileInfo
    Get
      Dim oDbFolder As DirectoryInfo
      Dim sDbFolder As String
      Dim sDbFile As String

      sDbFolder = Path.Combine(ContentRootFolder.FullName, "Db", "Data")
      oDbFolder = New DirectoryInfo(sDbFolder)
      sDbFile = Path.Combine(oDbFolder.FullName, "Contacts.db")

      oDbFolder.Create()

      Return New FileInfo(sDbFile)
    End Get
  End Property
End Class
