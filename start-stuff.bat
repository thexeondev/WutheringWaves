@rem This batch file starts the SDKServer and GameServer in the background
@rem just after the build process is done. (dotnet build)

cd SDKServer\bin\Debug\net8.0\
start "" SDKServer.exe 

cd ..\..\..\..\GameServer\bin\Debug\net8.0\
start "" GameServer.exe