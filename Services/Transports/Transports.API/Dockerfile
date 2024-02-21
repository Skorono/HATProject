FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/Transports/Transports.API/Transports.API.csproj", "Services/Transports/Transports.API/"]
RUN dotnet restore "Services/Transports/Transports.API/Transports.API.csproj"
COPY . .
WORKDIR "/src/Services/Transports/Transports.API"
RUN dotnet build "Transports.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transports.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transports.API.dll"]
