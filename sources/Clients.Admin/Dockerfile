FROM --platform=$BUILDPLATFORM  mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG BUILDPLATFORM
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM  mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILDPLATFORM
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "/src"]
COPY ["Directory.Packages.props", "/src"]
COPY ["Clients.Admin/Clients.Admin.csproj", "Clients.Admin/"]
RUN dotnet restore "Clients.Admin/Clients.Admin.csproj"
COPY . .
WORKDIR "/src/Clients.Admin"
RUN dotnet build "Clients.Admin.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Clients.Admin.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clients.Admin.dll"]
