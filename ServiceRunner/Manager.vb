Imports System
Imports System.Diagnostics
Imports Microsoft.VisualBasic

Friend Class Manager
  Public Sub New(Host As Host)
    Me.New(Host, Nothing)
  End Sub



  Public Sub New(Host As Host, Events As ServiceEvents)
    Me.OnStart = Events.OnStart
    Me.OnStop = Events.OnStop
    Me.Host = Host
  End Sub



  Public Sub StartService()
    Try
      Me.OnStart?.Invoke

    Catch ex As Exception
      ' See https://www.jitbit.com/alexblog/266-writing-to-an-event-log-from-net-without-the-description-for-event-id-nonsense/
      EventLog.WriteEntry(".NET Runtime", $"[{Me.Host.DisplayName}] service startup error:{vbCrLf}{vbCrLf}{ex}", EventLogEntryType.Error, 1000)
      Throw

    End Try
  End Sub



  Public Sub StopService()
    Me.OnStop?.Invoke
  End Sub



  Private ReadOnly OnStart As Action
  Private ReadOnly OnStop As Action
  Private ReadOnly Host As Host
End Class
