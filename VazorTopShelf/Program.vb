Imports System
Imports System.Linq
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.DependencyInjection
Imports Serilog
Imports Vazor
Imports VazorTopShelf.Db

Friend Module Program
  Friend Sub Main(Args As String())
    Dim sServiceName As String
    Dim sDisplayName As String
    Dim sDescription As String
    Dim oHost As Host

    sServiceName = "VazorTopShelf"
    sDisplayName = "Vazor TopShelf Sample"
    sDescription = "This service demonstrates running Vazor under Kestrel in a TopShelf service."

    oHost = New Host(sServiceName, sDisplayName, sDescription)

    If Args.Length = 0 Then
      oHost.Run(Args)
    Else
      ' EF Core design-time commands send an argument --applicationName
      ' If we see that argument, we don't want to run the service
      If Args.First <> "--applicationName" Then
        oHost.Run(Args)
      End If
    End If
  End Sub



  ''' <summary>
  ''' Creates the host builder for the website.
  ''' </summary>
  ''' <param name="Args">The incoming command line arguments.</param>
  ''' <returns>A <see cref="WebApplicationBuilder"/> instance.</returns>
  ''' <remarks>This function supports EF Core's design-time
  ''' commands (Add-Migration, Update-Database, etc.).<br/>
  ''' See <see href="https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation"/> 
  ''' for more information.</remarks>
  ''' 
  Public Function CreateHostBuilder(Args As String()) As WebApplicationBuilder
    Dim oMvcOptions As Action(Of MvcRazorRuntimeCompilationOptions)
    Dim oWebOptions As WebApplicationOptions
    Dim oProvider As VazorViewProvider
    Dim oContext As Context

    oMvcOptions = Sub(Options)
                    oProvider = New VazorViewProvider
                    Options.FileProviders.Add(oProvider)
                  End Sub

    oWebOptions = New WebApplicationOptions With {
      .ContentRootPath = Utils.ContentRootFolder.FullName,
      .Args = Args
    }

    CreateHostBuilder = WebApplication.CreateBuilder(oWebOptions)
    CreateHostBuilder.Host.UseSerilog
    CreateHostBuilder.Services.AddControllersWithViews.AddRazorRuntimeCompilation(oMvcOptions)
    CreateHostBuilder.Services.AddDbContext(Of Context)(Sub(Builder)
                                                          Builder.UseSqlite(Utils.ConnectionString)
                                                        End Sub)

    oContext = CreateHostBuilder.Services.BuildServiceProvider.GetService(Of Context)
    oContext.Database.Migrate
  End Function
End Module
