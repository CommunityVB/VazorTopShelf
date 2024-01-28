Imports System
Imports Microsoft.AspNetCore.Builder
Imports Serilog
Imports Vazor

''' <summary>
''' This class manages what occurs when the service is started or stopped.
''' </summary>
Friend Class Manager
  ''' <summary>
  ''' Initializes a new instance of the <see cref="Manager"/> class.
  ''' </summary>
  Public Sub New()
    Me.New(Nothing)
  End Sub



  ''' <summary>
  ''' Initializes a new instance of the <see cref="Manager"/> class.
  ''' </summary>
  ''' <param name="Events">A set of actions to be invoked when the service is started and stopped.</param>
  Public Sub New(Events As ServiceEvents)
    MyBase.New()

    Me.BeforeStart = Events.BeforeStart
    Me.BeforeStop = Events.BeforeStop
    Me.AfterStart = Events.AfterStart
    Me.AfterStop = Events.AfterStop
  End Sub



  ''' <summary>
  ''' Configures the web application, invokes the <see cref="BeforeStart"/> action, starts the website, and then invokes the <see cref="AfterStart"/> action.
  ''' </summary>
  ''' <param name="Args">The incoming command line arguments.</param>
  Public Sub StartService(Args As String())
    VazorSharedView.CreateAll()

    Log.Logger = (New LoggerConfiguration).
      MinimumLevel.
      Information.
      WriteTo.
      Console.
      CreateLogger

    Me.App = CreateHostBuilder(Args).Build
    Me.App.UseHttpsRedirection
    Me.App.UseStaticFiles
    Me.App.UseRouting
    Me.App.UseAuthorization
    Me.App.UseEndpoints(Sub(Routes) Routes.MapControllerRoute(name:=NAME, pattern:=PATTERN))

    Me.BeforeStart?.Invoke
    Me.App.StartAsync()
    Me.AfterStart?.Invoke
  End Sub



  ''' <summary>
  ''' Invokes the <see cref="BeforeStop"/> action, stops the website, and then invokes the <see cref="AfterStop"/> action.
  ''' </summary>
  Public Sub StopService()
    Me.BeforeStop?.Invoke
    Me.App.StopAsync()
    Me.AfterStop?.Invoke
  End Sub



  Private ReadOnly BeforeStart As Action
  Private ReadOnly BeforeStop As Action
  Private ReadOnly AfterStart As Action
  Private ReadOnly AfterStop As Action
  Private App As WebApplication

  Private Const PATTERN As String = "{controller=Home}/{action=Index}/{id?}"
  Private Const NAME As String = "default"
End Class
