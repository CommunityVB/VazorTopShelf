Imports System.Linq
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Logging
Imports VazorTopShelf.Db.Models

Namespace Controllers
  ''' <summary>
  ''' These actions call the native ASP.NET C# Razor engine
  ''' to render the view. To use Vazor views instead, create a
  ''' Vazor view instance here. See the HomeController's Index
  ''' action for an example.
  ''' </summary>
  Partial Public Class ContactsController
    Inherits Controller

    Public Function Index() As IActionResult
      Return Me.View(Me.Context.Contacts.ToList)
    End Function




    <HttpPost>
    Public Function Index(
      FirstName As String,
      LastName As String,
      PhoneArea As String,
      PhonePrefix As String,
      PhoneSuffix As String,
      Email As String
    ) As IActionResult

      Dim oContact As Contact

      oContact = New Contact With {
        .FirstName = FirstName,
        .LastName = LastName,
        .PhoneArea = PhoneArea,
        .PhonePrefix = PhonePrefix,
        .PhoneSuffix = PhoneSuffix,
        .Email = Email
      }

      Me.Context.Contacts.Add(oContact)
      Me.Context.SaveChanges()

      Me.Logger.LogInformation("New contact added: {oContact}", oContact)

      Return Me.View(Me.Context.Contacts.ToList)
    End Function
  End Class
End Namespace
