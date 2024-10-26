FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Modules/Dispatcher/Browsely.Modules.Dispatcher.Api/Browsely.Modules.Dispatcher.Api.csproj", "src/Modules/Dispatcher/Browsely.Modules.Dispatcher.Api/"]
RUN dotnet restore "src/Modules/Dispatcher/Browsely.Modules.Dispatcher.Api/Browsely.Modules.Dispatcher.Api.csproj"
COPY . .
WORKDIR "/src/src/Modules/Dispatcher/Browsely.Modules.Dispatcher.Api"
RUN dotnet build "Browsely.Modules.Dispatcher.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Browsely.Modules.Dispatcher.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Browsely.Modules.Dispatcher.Api.dll"]
