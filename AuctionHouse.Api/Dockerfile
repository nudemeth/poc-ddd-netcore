#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AuctionHouse.Api/AuctionHouse.Api.csproj", "AuctionHouse.Api/"]
COPY ["AuctionHouse.Infrastructure/AuctionHouse.Infrastructure.csproj", "AuctionHouse.Infrastructure/"]
COPY ["AuctionHouse.Application/AuctionHouse.Application.csproj", "AuctionHouse.Application/"]
COPY ["AuctionHouse.Domain/AuctionHouse.Domain.csproj", "AuctionHouse.Domain/"]
RUN dotnet restore "AuctionHouse.Api/AuctionHouse.Api.csproj"
RUN dotnet restore "AuctionHouse.Infrastructure/AuctionHouse.Infrastructure.csproj"
COPY . .
WORKDIR "/src/AuctionHouse.Infrastructure"
RUN dotnet build "AuctionHouse.Infrastructure.csproj" -c Release -o /app/build
WORKDIR "/src/AuctionHouse.Api"
RUN dotnet build "AuctionHouse.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuctionHouse.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuctionHouse.Api.dll"]
