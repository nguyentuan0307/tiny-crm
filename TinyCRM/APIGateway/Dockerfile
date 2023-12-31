﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TinyCRM/APIGateway/APIGateway.csproj", "TinyCRM/APIGateway/"]
COPY ["TinyCRM/BuildingBlocks/BuildingBlock.Presentation/BuildingBlock.Presentation.csproj", "TinyCRM/BuildingBlocks/BuildingBlock.Presentation/"]
COPY ["TinyCRM/BuildingBlocks/BuildingBlock.Domain/BuildingBlock.Domain.csproj", "TinyCRM/BuildingBlocks/BuildingBlock.Domain/"]
COPY ["TinyCRM/BuildingBlocks/BuildingBlock.Application/BuildingBlock.Application.csproj", "TinyCRM/BuildingBlocks/BuildingBlock.Application/"]
RUN dotnet restore "TinyCRM/APIGateway/APIGateway.csproj"
COPY . .
WORKDIR "/src/TinyCRM/APIGateway"
RUN dotnet build "APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGateway.dll"]
