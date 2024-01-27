Imports Topshelf

Public Module Enums
  ''' <summary>
  ''' The exit codes for the service.
  ''' </summary>
  Public Enum ExitCodes
    ServiceControlRequestFailed = TopshelfExitCode.ServiceControlRequestFailed
    ServiceAlreadyInstalled = TopshelfExitCode.ServiceAlreadyInstalled
    ServiceAlreadyRunning = TopshelfExitCode.ServiceAlreadyRunning
    NotRunningOnWindows = TopshelfExitCode.NotRunningOnWindows
    ServiceNotInstalled = TopshelfExitCode.ServiceNotInstalled
    ServiceNotRunning = TopshelfExitCode.ServiceNotRunning
    SudoRequired = TopshelfExitCode.SudoRequired
    AbnormalExit = TopshelfExitCode.AbnormalExit
    Ok = TopshelfExitCode.Ok
  End Enum



  ''' <summary>
  ''' The run contexts for the service.
  ''' </summary>
  Public Enum RunContexts
    NetworkService
    LocalService
    LocalSystem
    Prompt
  End Enum
End Module
