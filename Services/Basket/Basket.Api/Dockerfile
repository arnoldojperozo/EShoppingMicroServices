#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Basket/Basket.Api/Basket.Api.csproj", "Services/Basket/Basket.Api/"]
COPY ["Services/Basket/Basket.Application/Basket.Application/Basket.Application.csproj", "Services/Basket/Basket.Application/Basket.Application/"]
COPY ["Services/Basket/Basket.Core/Baket.Core/Basket.Core.csproj", "Services/Basket/Basket.Core/Baket.Core/"]
COPY ["Services/Basket/Basket.Infrastructure/Basket.Infrastructure/Basket.Infrastructure.csproj", "Services/Basket/Basket.Infrastructure/Basket.Infrastructure/"]
COPY ["Infrastructure/EventBus/EventBus.Messages/EventBus.Messages.csproj", "Infrastructure/EventBus.Messages/"]

RUN dotnet restore "Services/Basket/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/Services/Basket/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]