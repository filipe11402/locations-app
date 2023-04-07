FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Locations.API/Locations.API.csproj", "src/Locations.API/"]
RUN dotnet restore "src/Locations.API/Locations.API.csproj"
COPY . .
WORKDIR "/src/src/Locations.API"
VOLUME [ "/src/Locations.API/Locations/" ]
RUN dotnet build "Locations.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Locations.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Locations.API.dll"]