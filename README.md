# ServiceToggler

ServiceToggler is a simple C# Windows Forms application that allows you to enable or disable specific Windows services (pcasvc, sysmain, dps, eventlog, dcomlaunch) through a graphical interface.

## How to Build

1. Make sure you have the .NET SDK installed (https://dotnet.microsoft.com/download).
2. Open this folder in Visual Studio Code or your preferred IDE.
3. Build the project using the command:
   ```
   dotnet build
   ```

## How to Run

1. After building, run the application with:
   ```
   dotnet run
   ```

## Features
- Enable or disable the following Windows services:
  - pcasvc
  - sysmain
  - dps
  - eventlog
  - dcomlaunch

## Note
- You may need to run the application as Administrator to control Windows services.
