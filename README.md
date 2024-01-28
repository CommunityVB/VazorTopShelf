# VazorTopShelf

This sample demonstrates running a Vazor VB.NET Web application under Kestrel, hosted in a Windows Service using TopShelf. It started as a simple .NET Core VB.NET Console application. The Project SDK value was updated from `Microsoft.NET.Sdk` to `Microsoft.NET.Sdk.Web` and the Vazor and TopShelf packages were added. Essential files were copied from a new Vazor-template-sourced project.

To install the service, `dotnet publish` to your desired output directory, and then run the following command from an elevated command prompt in that directory:

`VazorTopShelf.exe install`

This will install the service. You can then start it using `net start VazorTopShelf` or *services.msc*. With the service running, navigate to [http://localhost:5000/](http://localhost:5000/) to view the application and access its pages. At service start, a simple SQLite database will automatically be created in a new `Db\Data` folder alongside `wwwroot` (if they don't exist already).

The installation is configured in the `Host.Run()` method to set the service's startup type to *Automatic (Delayed Start)*. You can change this setting to your liking by calling `HostConfig.Disabled()`, `HostConfig.StartManually()`, `HostConfig.StartAutomatically()` or `HostConfig.StartAutomaticallyDelayed()`.

To uninstall the service, run the following command from an elevated command prompt:

`VazorTopShelf.exe uninstall`

Here's a high-level view of how it works:

1. The `Program` class is the entry point for the application. It initializes a `Host` instance, and then calls the `Host.Run()` method.
2. `Host.Run()` sets the stage for the service, including specifying what happens when the service starts and stops.
3. The `Manager.StartService()` method configures and runs the Web Application when the service starts. Note that the `WebApplication` class provides a `Run()` method, which blocks and listens on a port and which we normally use, but since TopShelf handles the blocking all we need here is the listening component, `StartAsync()`.

## Notes

Current date: Jan 27, 2024

- EF Core 7.0.15 is used here, pending support for EF Core 8 in [EntityFrameworkCore.VisualBasic](https://www.nuget.org/packages/EntityFrameworkCore.VisualBasic/). This results in a design-time RID-related warning:

  > Found version-specific or distribution-specific runtime identifier(s): alpine-arm, alpine-arm64, alpine-x64. Affected libraries: SQLitePCLRaw.lib.e_sqlite3. In .NET 8.0 and higher, assets for version-specific and distribution-specific runtime identifiers will not be found by default. See https://aka.ms/dotnet/rid-usage for details.

  See [https://github.com/sebastienros/yessql/issues/510](https://github.com/sebastienros/yessql/issues/510) for more information. The temporary workaround is to update the normally-transient `SQLitePCLRaw.bundle_e_sqlite3` package to `2.1.7`. Once the VB.NET EF Core design-time tools are updated to support EF Core 8, this interim package update won't be necessary.

- The Vazor repository is located at [https://github.com/VBAndCs/Vazor](https://github.com/VBAndCs/Vazor). The latest version (v2.1) is available at [https://marketplace.visualstudio.com/items?itemName=ModernVBNET.Vazor](https://marketplace.visualstudio.com/items?itemName=ModernVBNET.Vazor).
- The TopShelf repository is located at [https://github.com/Topshelf/Topshelf](https://github.com/Topshelf/Topshelf).
