#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["MSTutorial.Domain/MSTutorial.Domain.csproj", "MSTutorial.Domain/"]
COPY ["MSTutorial.CommandService/MSTutorial.CommandService.csproj", "MSTutorial.CommandService/"]

RUN dotnet restore "MSTutorial.CommandService/MSTutorial.CommandService.csproj"
COPY . .
WORKDIR "/src/MSTutorial.CommandService"
RUN dotnet build "MSTutorial.CommandService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSTutorial.CommandService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSTutorial.CommandService.dll"]