
Create a self-signed certificate (https://learn.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide#with-dotnet-dev-certs)

dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\grpc-server.pfx -p grpc
dotnet dev-certs https --trust