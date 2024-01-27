Imports System.Collections.Generic
Imports VazorTopShelf.Db.Models

Namespace Db.Data
  Public Class Students
    Public Shared ReadOnly Property All As New List(Of Student) From {
      New Student With {.Id = 1, .Name = "Adam", .Age = 20, .Grade = 69},
      New Student With {.Id = 2, .Name = "Mark", .Age = 21, .Grade = 80},
      New Student With {.Id = 3, .Name = "Tom", .Age = 18, .Grade = 51}
    }
  End Class
End Namespace
