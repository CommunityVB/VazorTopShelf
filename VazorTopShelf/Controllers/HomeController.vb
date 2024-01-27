Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Logging

Namespace Controllers
  Partial Public Class HomeController
    Inherits Controller

    Public Sub New(Logger As ILogger(Of HomeController))
      Me.Logger = Logger
    End Sub



    Private ReadOnly Logger As ILogger(Of HomeController)
  End Class
End Namespace
