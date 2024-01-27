Imports System.Diagnostics
Imports Microsoft.AspNetCore.Mvc
Imports VazorTopShelf.Models

Namespace Controllers
  Partial Public Class HomeController
    <ResponseCache(Duration:=0, Location:=ResponseCacheLocation.None, NoStore:=True)>
    Public Function [Error]() As IActionResult
      Return Me.View(New ErrorViewModel With {.RequestId = If(Activity.Current?.Id, Me.HttpContext.TraceIdentifier)})
    End Function
  End Class
End Namespace
