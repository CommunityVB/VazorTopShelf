Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Logging
Imports VazorTopShelf.Db

Namespace Controllers
  Partial Public Class ContactsController
    Inherits Controller

    Public Sub New(Context As Context, Logger As ILogger(Of HomeController))
      Me.Context = Context
      Me.Logger = Logger
    End Sub



    Private ReadOnly Context As Context
    Private ReadOnly Logger As ILogger(Of HomeController)
  End Class
End Namespace
