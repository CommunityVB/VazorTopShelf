Imports System
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.DependencyInjection
Imports Serilog
Imports ServiceRunner
Imports Vazor
Imports VazorTopShelf.Db

Friend Module Program
  Friend Sub Main(Args As String())
    Dim oServiceEvents As ServiceEvents
    Dim sServiceName As String
    Dim sDisplayName As String
    Dim sDescription As String
    Dim oOnStart As Action
    Dim oOnStop As Action
    Dim oHost As Host

    VazorSharedView.CreateAll()

    Log.Logger = (New LoggerConfiguration).
      MinimumLevel.
      Information.
      WriteTo.
      Console.
      CreateLogger

    oOnStart = Sub()
                 Website = CreateHostBuilder(Args).Build
                 Website.UseHttpsRedirection
                 Website.UseStaticFiles
                 Website.UseRouting
                 Website.UseAuthorization
                 Website.UseEndpoints(Sub(Routes) Routes.MapControllerRoute(name:=NAME, pattern:=PATTERN))

                 Using oScope As IServiceScope = Website.Services.CreateScope
                   oScope.ServiceProvider.GetRequiredService(Of Context).Database.Migrate
                 End Using

                 Website.StartAsync()
               End Sub

    oOnStop = Sub() Website.StopAsync()

    oServiceEvents = New ServiceEvents From {
      {ServiceEvents.Events.OnStart, oOnStart},
      {ServiceEvents.Events.OnStop, oOnStop}
    }

    sServiceName = "VazorTopShelf"
    sDisplayName = "Vazor/TopShelf Example"
    sDescription = "This service demonstrates running Vazor under Kestrel in a TopShelf service."

    oHost = New Host(sServiceName, sDisplayName, sDescription)
    oHost.Run(oServiceEvents)
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
  End Function



  Private Website As WebApplication

  Private Const PATTERN As String = "{controller=Home}/{action=Index}/{id?}"
  Private Const NAME As String = "default"
End Module
