FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG BUILDPLATFORM
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILDPLATFORM
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "/src"]
COPY ["Directory.Packages.props", "/src"]
COPY ["Server.Presentation.Grpc/Server.Presentation.Grpc.csproj", "Server.Presentation.Grpc/"]
RUN dotnet restore "Server.Presentation.Grpc/Server.Presentation.Grpc.csproj"
COPY . .
WORKDIR "/src/Server.Presentation.Grpc"
RUN dotnet build "Server.Presentation.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Server.Presentation.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.Presentation.Grpc.dll"]
