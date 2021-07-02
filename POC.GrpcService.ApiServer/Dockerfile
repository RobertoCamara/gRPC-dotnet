FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["POC.GrpcService.ApiServer/POC.GrpcService.ApiServer.csproj", "POC.GrpcService.ApiServer/"]
COPY ["POC.GrpcService.SharedProtos/POC.GrpcService.SharedProtos.csproj", "POC.GrpcService.SharedProtos/"]
RUN dotnet restore "POC.GrpcService.ApiServer/POC.GrpcService.ApiServer.csproj"
COPY . .
WORKDIR "/src/POC.GrpcService.ApiServer"
RUN dotnet build "POC.GrpcService.ApiServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "POC.GrpcService.ApiServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POC.GrpcService.ApiServer.dll"]