#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SoftwareEngineering/SoftwareEngineering.csproj", "SoftwareEngineering/"]
RUN dotnet restore "SoftwareEngineering/SoftwareEngineering.csproj"
COPY . .
WORKDIR "/src/SoftwareEngineering"
RUN dotnet build "SoftwareEngineering.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SoftwareEngineering.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoftwareEngineering.dll"]