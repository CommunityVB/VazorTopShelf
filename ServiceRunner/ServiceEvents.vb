Imports System
Imports System.Collections.Generic

#Disable Warning BC42030 ' Variable is passed by reference before it has been assigned a value
''' <summary>
''' This class represents a set of actions to be invoked when the service is started and stopped.
''' </summary>
Public Class ServiceEvents
  Inherits Dictionary(Of Events, Action)

  ''' <summary>
  ''' The action to be invoked when the service starts.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property OnStart As Action
    Get
      Me.TryGetValue(Events.OnStart, OnStart)
    End Get
  End Property



  ''' <summary>
  ''' The action to be invoked when the service stops.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property OnStop As Action
    Get
      Me.TryGetValue(Events.OnStop, OnStop)
    End Get
  End Property



  ''' <summary>
  ''' The actions that can be invoked.
  ''' </summary>
  Public Enum Events
    OnStart
    OnStop
  End Enum
End Class
