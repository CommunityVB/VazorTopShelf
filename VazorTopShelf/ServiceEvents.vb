Imports System
Imports System.Collections.Generic

#Disable Warning BC42030 ' Variable is passed by reference before it has been assigned a value
''' <summary>
''' This class represents a set of actions to be invoked when the service is started and stopped.
''' </summary>
Public Class ServiceEvents
  Inherits Dictionary(Of Events, Action)

  ''' <summary>
  ''' The action to be invoked before the service starts.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property BeforeStart As Action
    Get
      Me.TryGetValue(Events.BeforeStart, BeforeStart)
    End Get
  End Property



  ''' <summary>
  ''' The action to be invoked before the service stops.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property BeforeStop As Action
    Get
      Me.TryGetValue(Events.BeforeStop, BeforeStop)
    End Get
  End Property



  ''' <summary>
  ''' The action to be invoked after the service starts.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property AfterStart As Action
    Get
      Me.TryGetValue(Events.AfterStart, AfterStart)
    End Get
  End Property



  ''' <summary>
  ''' The action to be invoked after the service stops.
  ''' </summary>
  ''' <value>
  ''' The action.
  ''' </value>
  Public ReadOnly Property AfterStop As Action
    Get
      Me.TryGetValue(Events.AfterStop, AfterStop)
    End Get
  End Property



  ''' <summary>
  ''' The actions that can be invoked.
  ''' </summary>
  Public Enum Events
    BeforeStart
    BeforeStop
    AfterStart
    AfterStop
  End Enum
End Class
