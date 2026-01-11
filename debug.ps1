dotnet publish Flow.Launcher.Plugin.ChatGPT -c Debug -r win-x64 --no-self-contained

$AppDataFolder = "E:\"
$flowLauncherExe = "$AppDataFolder\FlowLauncher\Flow.Launcher.exe"

if (Test-Path $flowLauncherExe) {
    Stop-Process -Name "Flow.Launcher" -Force -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2

    if (Test-Path "$AppDataFolder\FlowLauncher\app-2.0.3\Plugins\ChatGPT") {
        Remove-Item -Recurse -Force "$AppDataFolder\FlowLauncher\app-2.0.3\Plugins\ChatGPT"
    }

    Copy-Item "Flow.Launcher.Plugin.ChatGPT\bin\Debug\win-x64\publish" "$AppDataFolder\FlowLauncher\app-2.0.3\Plugins\" -Recurse -Force
    Rename-Item -Path "$AppDataFolder\FlowLauncher\app-2.0.3\Plugins\publish" -NewName "ChatGPT"

    Start-Sleep -Seconds 2
    Start-Process $flowLauncherExe
}
else {
    Write-Host "Flow.Launcher.exe not found. Please install Flow Launcher first"
}
