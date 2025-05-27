# Service Toggler

This is a simple Windows Forms application that allows you to toggle the status of various Windows services.

## Features

*   Displays a list of services in a ListView control with checkboxes.
*   Allows you to start or stop services by ticking or unticking the checkboxes.
*   Requires administrator privileges to toggle services.
*   Restarts the application with administrator privileges if it's not already running as administrator.

## Services

*   PcaSvc
*   Sysmain
*   DPS
*   CDPU (Random Numbers)
*   Eventlog
*   DcomLaunch
*   BAM
*   Dusmsvc

## Usage

1.  Run the ServiceToggler.exe application.
2.  If the application is not running as administrator, it will prompt you to restart it with administrator privileges.
3.  Tick the checkboxes to start the corresponding services.
4.  Untick the checkboxes to stop the corresponding services.
5.  Click the "Save" button to apply the changes.

## Notes

*   This application requires administrator privileges to function correctly.
*   Toggling some services may have unintended consequences. Use with caution.
