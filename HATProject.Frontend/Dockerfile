FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HATProject.Frontend/HATProject.Frontend.csproj", "HATProject.Frontend/"]
RUN dotnet restore "HATProject.Frontend/HATProject.Frontend.csproj"
COPY . .
WORKDIR "/src/HATProject.Frontend"
RUN dotnet build "HATProject.Frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HATProject.Frontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HATProject.Frontend.dll"]
