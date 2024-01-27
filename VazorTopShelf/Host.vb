Imports System
Imports System.Collections.Generic
Imports MoreLinq
Imports Topshelf
Imports Topshelf.HostConfigurators
Imports Topshelf.Runtime

''' <summary>
''' The host class for the TopShelf service. This class
''' is responsible for configuring the service.
''' </summary>
Public Class Host
  ''' <summary>
  ''' Initializes a new instance of the <see cref="Host"/> class.
  ''' </summary>
  ''' <param name="ServiceName">The name of the service.</param>
  ''' <param name="DisplayName">The service's display name.</param>
  ''' <param name="Description">The service's description.</param>
  Public Sub New(
    ServiceName As String,
    DisplayName As String,
    Description As String
  )

    Me.New(ServiceName, DisplayName, Description, RunContexts.LocalService)
  End Sub



  ''' <summary>
  ''' Initializes a new instance of the <see cref="Host"/> class.
  ''' </summary>
  ''' <param name="ServiceName">The name of the service.</param>
  ''' <param name="DisplayName">The service's display name.</param>
  ''' <param name="Description">The service's description.</param>
  ''' <param name="RunContext">The service's run context.</param>
  Public Sub New(
    ServiceName As String,
    DisplayName As String,
    Description As String,
    RunContext As RunContexts
  )

    Me.New(ServiceName, DisplayName, Description, RunContext, Sub(ex) ex = ex)
  End Sub



  ''' <summary>
  ''' Initializes a new instance of the <see cref="Host"/> class.
  ''' </summary>
  ''' <param name="ServiceName">The name of the service.</param>
  ''' <param name="DisplayName">The service's display name.</param>
  ''' <param name="Description">The service's description.</param>
  ''' <param name="RunContext">The service's run context.</param>
  ''' <param name="OnException">The action to execute when an exception is encountered.</param>
  Public Sub New(
    ServiceName As String,
    DisplayName As String,
    Description As String,
    RunContext As RunContexts,
    OnException As Action(Of Exception)
  )

    Me.New(ServiceName, DisplayName, Description, RunContext, OnException, New List(Of String))
  End Sub



  ''' <summary>
  ''' Initializes a new instance of the <see cref="Host"/> class.
  ''' </summary>
  ''' <param name="ServiceName">The name of the service.</param>
  ''' <param name="DisplayName">The service's display name.</param>
  ''' <param name="Description">The service's description.</param>
  ''' <param name="RunContext">The service's run context.</param>
  ''' <param name="OnException">The action to execute when an exception is encountered.</param>
  ''' <param name="DependsOnServices">A list of dependent services.</param>
  Public Sub New(
    ServiceName As String,
    DisplayName As String,
    Description As String,
    RunContext As RunContexts,
    OnException As Action(Of Exception),
    DependsOnServices As List(Of String)
  )

    ServiceName.ThrowIfNothing(NameOf(ServiceName))
    DisplayName.ThrowIfNothing(NameOf(DisplayName))
    Description.ThrowIfNothing(NameOf(Description))
    OnException.ThrowIfNothing(NameOf(OnException))
    DependsOnServices.ThrowIfNothing(NameOf(DependsOnServices))

    Me.DependsOnServices = DependsOnServices
    Me.ServiceName = ServiceName
    Me.DisplayName = DisplayName
    Me.Description = Description
    Me.OnException = OnException
    Me.RunContext = RunContext
  End Sub



  ''' <summary>
  ''' Runs the <see cref="HostFactory"/> for the service.
  ''' </summary>
  ''' <param name="Args">The incoming command line arguments.</param>
  ''' <returns>The TopShelf exit code.</returns>
  Public Function Run(Args As String()) As ExitCodes
    Return Me.Run(Args, Nothing, Nothing)
  End Function



  ''' <summary>
  ''' Runs the <see cref="HostFactory"/> for the service.
  ''' </summary>
  ''' <param name="Args">The incoming command line arguments.</param>
  ''' <param name="HostEvents">A collection of actions to be invoked at various stages of service installation.</param>
  ''' <returns>The TopShelf exit code.</returns>
  Public Function Run(Args As String(), HostEvents As HostEvents) As ExitCodes
    Return Me.Run(Args, HostEvents, Nothing)
  End Function



  ''' <summary>
  ''' Runs the specified arguments.
  ''' </summary>
  ''' <param name="Args">The incoming command line arguments.</param>
  ''' <param name="ServiceEvents">A set of actions to be invoked when the service is started and stopped.</param>
  ''' <returns>The TopShelf exit code.</returns>
  Public Function Run(Args As String(), ServiceEvents As ServiceEvents) As ExitCodes
    Return Me.Run(Args, Nothing, ServiceEvents)
  End Function



  ''' <summary>
  ''' Runs the specified arguments.
  ''' </summary>
  ''' <param name="Args">The incoming command line arguments.</param>
  ''' <param name="HostEvents">A collection of actions to be invoked at various stages of service installation.</param>
  ''' <param name="ServiceEvents">A set of actions to be invoked when the service is started and stopped.</param>
  ''' <returns>The TopShelf exit code.</returns>
  Public Function Run(Args As String(), HostEvents As HostEvents, ServiceEvents As ServiceEvents) As ExitCodes
    Dim oServiceEvents As ServiceEvents
    Dim oConfigurator As Action(Of HostConfigurator)
    Dim oWhenStarted As Action(Of Manager)
    Dim oWhenStopped As Action(Of Manager)
    Dim oHostEvents As HostEvents
    Dim oFactory As ServiceFactory(Of Manager)

    oServiceEvents = If(ServiceEvents, New ServiceEvents)
    oHostEvents = If(HostEvents, New HostEvents)

    oFactory = Function(Settings) New Manager(oServiceEvents)
    oWhenStarted = Sub(Manager) Manager.StartService(Args) ' This action is invoked when the service is started.
    oWhenStopped = Sub(Manager) Manager.StopService() ' This action is invoked when the service is stopped.

    oConfigurator = Sub(HostConfig)
                      HostConfig.Service(Of Manager)(Sub(ServiceConfig)
                                                       ServiceConfig.ConstructUsing(oFactory)
                                                       ServiceConfig.WhenStarted(oWhenStarted)
                                                       ServiceConfig.WhenStopped(oWhenStopped)
                                                     End Sub)

                      HostConfig.StartAutomaticallyDelayed
                      HostConfig.SetServiceName(Me.ServiceName)
                      HostConfig.SetDisplayName(Me.DisplayName)
                      HostConfig.SetDescription(Me.Description)
                      HostConfig.OnException(Me.OnException)

                      Select Case Me.RunContext
                        Case RunContexts.NetworkService : HostConfig.RunAsNetworkService
                        Case RunContexts.LocalService : HostConfig.RunAsLocalService
                        Case RunContexts.LocalSystem : HostConfig.RunAsLocalSystem
                        Case RunContexts.Prompt : HostConfig.RunAsPrompt
                      End Select

                      Me.DependsOnServices.ForEach(Sub(Service)
                                                     If Not String.IsNullOrEmpty(Service) Then
                                                       HostConfig.DependsOn(Service)
                                                     End If
                                                   End Sub)

                      With oHostEvents.Keys
                        .ForEach(Sub(HostEvent)
                                   Select Case HostEvent
                                     Case HostEvents.Events.BeforeInstall : HostConfig.BeforeInstall(oHostEvents.BeforeInstall)
                                     Case HostEvents.Events.AfterInstall : HostConfig.AfterInstall(oHostEvents.AfterInstall)
                                     Case HostEvents.Events.BeforeRollback : HostConfig.BeforeRollback(oHostEvents.BeforeRollback)
                                     Case HostEvents.Events.AfterRollback : HostConfig.AfterRollback(oHostEvents.AfterRollback)
                                     Case HostEvents.Events.BeforeUninstall : HostConfig.BeforeUninstall(oHostEvents.BeforeUninstall)
                                     Case HostEvents.Events.AfterUninstall : HostConfig.AfterUninstall(oHostEvents.AfterUninstall)
                                   End Select
                                 End Sub)
                      End With
                    End Sub

    Return HostFactory.Run(oConfigurator)
  End Function



  Public ReadOnly Property DependsOnServices As List(Of String)
  Public ReadOnly Property OnException As Action(Of Exception)
  Public ReadOnly Property ServiceName As String
  Public ReadOnly Property DisplayName As String
  Public ReadOnly Property Description As String
  Public ReadOnly Property RunContext As RunContexts
End Class
