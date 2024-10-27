@echo off
REM Enable Docker API access via TCP on Windows

echo Configuring Docker to expose TCP API at tcp://0.0.0.0:2375...

REM Check if Docker configuration file exists
set "DOCKER_CONFIG=C:\ProgramData\Docker\config\daemon.json"
if not exist "%DOCKER_CONFIG%" (
    echo Docker configuration file not found. Creating a new one...
    echo { "hosts": ["tcp://0.0.0.0:2375", "npipe://"] } > "%DOCKER_CONFIG%"
) else (
    echo Docker configuration file found. Updating it...
    powershell -Command "(Get-Content '%DOCKER_CONFIG%') -replace '}', ', \"tcp://0.0.0.0:2375\"]}' | Set-Content '%DOCKER_CONFIG%'"
)

echo Restarting Docker service to apply changes...
net stop com.docker.service
net start com.docker.service

echo Docker TCP API has been enabled. You can now access Docker at tcp://host.docker.internal:2375 from containers.
pause
