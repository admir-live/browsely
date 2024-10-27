FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Modules/Node/Browsely.Modules.Node.Api/Browsely.Modules.Node.Api.csproj", "src/Modules/Node/Browsely.Modules.Node.Api/"]
COPY ["src/Common/Browsely.Common.Infrastructure/Browsely.Common.Infrastructure.csproj", "src/Common/Browsely.Common.Infrastructure/"]
COPY ["src/Common/Browsely.Common.Application/Browsely.Common.Application.csproj", "src/Common/Browsely.Common.Application/"]
COPY ["src/Common/Browsely.Common.Domain/Browsely.Common.Domain.csproj", "src/Common/Browsely.Common.Domain/"]
COPY ["src/Modules/Node/Browsely.Modules.Node.Infrastructure/Browsely.Modules.Node.Infrastructure.csproj", "src/Modules/Node/Browsely.Modules.Node.Infrastructure/"]
COPY ["src/Modules/Node/Browsely.Modules.Node.Presentation/Browsely.Modules.Node.Presentation.csproj", "src/Modules/Node/Browsely.Modules.Node.Presentation/"]
COPY ["src/Modules/Dispatcher/Browsely.Modules.Dispatcher.Events/Browsely.Modules.Dispatcher.Events.csproj", "src/Modules/Dispatcher/Browsely.Modules.Dispatcher.Events/"]
COPY ["src/Modules/Node/Browsely.Modules.Node.Application/Browsely.Modules.Node.Application.csproj", "src/Modules/Node/Browsely.Modules.Node.Application/"]
RUN dotnet restore "src/Modules/Node/Browsely.Modules.Node.Api/Browsely.Modules.Node.Api.csproj"
COPY . .
WORKDIR "/src/src/Modules/Node/Browsely.Modules.Node.Api"
RUN dotnet build "Browsely.Modules.Node.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Browsely.Modules.Node.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Browsely.Modules.Node.Api.dll"]
