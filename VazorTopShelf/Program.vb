Imports System
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
    sDisplayName = "Vazor/TopShelf Example"
    sDescription = "This service demonstrates running Vazor under Kestrel in a TopShelf service."

    oHost = New Host(sServiceName, sDisplayName, sDescription)
    oHost.Run(Args)
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

    If Utils.IsService Then
      oContext = CreateHostBuilder.Services.BuildServiceProvider.GetService(Of Context)
      oContext.Database.Migrate
    End If
  End Function
End Module
