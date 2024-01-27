Imports Microsoft.AspNetCore.Mvc
Imports VazorTopShelf.Db.Data
Imports VazorTopShelf.Home

Namespace Controllers
  Partial Public Class HomeController
    ''' <summary>
    ''' This action loads a Vazor view, hooking into the ASP.NET
    ''' C# Razor view engine. To use the native Razor view engine
    ''' instead, and standard *.cshtml files with C# code, don't
    ''' create a Vazor view instance here. Simply call Me.View().
    ''' </summary>
    ''' <returns>The view to send to the browser</returns>
    Public Function Index() As IActionResult
      Return Me.View(IndexView.CreateNew(Students.All, Me.ViewData), Students.All)
    End Function
  End Class
End Namespace
