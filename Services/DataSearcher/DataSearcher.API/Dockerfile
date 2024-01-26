FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/DataSearcher/DataSearcher.API/DataSearcher.API.csproj", "Services/DataSearcher/DataSearcher.API/"]
RUN dotnet restore "Services/DataSearcher/DataSearcher.API/DataSearcher.API.csproj"
COPY . .
WORKDIR "/src/Services/DataSearcher/DataSearcher.API"
RUN dotnet build "DataSearcher.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataSearcher.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataSearcher.API.dll"]
