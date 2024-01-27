Imports Microsoft.EntityFrameworkCore
Imports VazorTopShelf.Db.Models

Namespace Db
  Public Class Context
    Inherits DbContext

    Public Sub New()
    End Sub



    Public Sub New(Options As DbContextOptions)
      MyBase.New(Options)
    End Sub



    Protected Overrides Sub OnConfiguring(Builder As DbContextOptionsBuilder)
      If Not Builder.IsConfigured Then
        Builder.UseSqlite(Utils.ConnectionString)
      End If
    End Sub



    Protected Overrides Sub OnModelCreating(Builder As ModelBuilder)
      Builder.ApplyConfigurationsFromAssembly(Me.GetType.Assembly)
    End Sub



    Public Overridable Property Contacts As DbSet(Of Contact)
  End Class
End Namespace
