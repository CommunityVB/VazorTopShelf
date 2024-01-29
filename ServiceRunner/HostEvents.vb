Imports System
Imports System.Collections.Generic

#Disable Warning BC42030 ' Variable is passed by reference before it has been assigned a value
''' <summary>
''' This class represents a collection of actions to be invoked at various stages of service installation.
''' </summary>
Public Class HostEvents
  Inherits Dictionary(Of Events, Action)

  ''' <summary>
  ''' The action to be invoked before the service is installed.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property BeforeInstall As Action
    Get
      Me.TryGetValue(Events.BeforeInstall, BeforeInstall)
    End Get
  End Property



  ''' <summary>
  ''' The action to be invoked before the service installation is rolled back, in the event of installation failure.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property BeforeRollback As Action
    Get
      Me.TryGetValue(Events.BeforeRollback, BeforeRollback)
    End Get
  End Property



  ''' <summary>
  ''' The action to be invoked before the service is uninstalled.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property BeforeUninstall As Action
    Get
      Me.TryGetValue(Events.BeforeUninstall, BeforeUninstall)
    End Get
  End Property



  ''' <summary>
  ''' The action to be invoked after the service is installed.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property AfterInstall As Action
    Get
      Me.TryGetValue(Events.AfterInstall, AfterInstall)
    End Get
  End Property



  ''' <summary>
  ''' The action to be invoked after the service installation is rolled back, in the event of installation failure.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property AfterRollback As Action
    Get
      Me.TryGetValue(Events.AfterRollback, AfterRollback)
    End Get
  End Property



  ''' <summary>
  ''' The action to be invoked after the service is uninstalled.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property AfterUninstall As Action
    Get
      Me.TryGetValue(Events.AfterUninstall, AfterUninstall)
    End Get
  End Property



  ''' <summary>
  ''' The actions that can be invoked.
  ''' </summary>
  Public Enum Events
    BeforeInstall
    BeforeRollback
    BeforeUninstall
    AfterInstall
    AfterRollback
    AfterUninstall
  End Enum
End Class
