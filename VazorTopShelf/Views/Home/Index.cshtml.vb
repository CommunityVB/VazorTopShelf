Imports System.Collections.Generic
Imports Microsoft.AspNetCore.Mvc.ViewFeatures
Imports Vazor
Imports VazorTopShelf.Db.Models

' To add a new Vazor view, right-click the folder in Solution Explorer,
' click Add/New item, and choose the "VazorView" item from the dialog.
' This will add both the vazor.vb and vbxml.vb files to the folder.

Namespace Home
  Public Class IndexView
    Inherits VazorView

    ' Supply your actual view name, path, and title to the base constructor.
    ' By default, UTF-8 encoding is used to render the view. 
    ' You can send another encoding as the fourth parameter of MyBase.New().
    Public Sub New(Students As List(Of Student), ViewData As ViewDataDictionary)
      MyBase.New("Index", "Views\Home", "Home")

      Me.Students = Students
      Me.ViewData = ViewData

      ViewData("Title") = Me.Title
    End Sub



    ' This function is called in the "Index" action method in the HomeController:
    ' View(IndexView.CreateNew(Students, ViewData))
    Public Shared Function CreateNew(Students As List(Of Student), ViewData As ViewDataDictionary) As String
      Return VazorViewMapper.Add(New IndexView(Students, ViewData))
    End Function



    ' GetVbXml() is defiend in the Index.cshtml.vbxml.vb file, 
    ' and it contains the view design


    Public ReadOnly Property Students As List(Of Student)
    Public ReadOnly Property ViewData As ViewDataDictionary
  End Class
End Namespace
