#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV DB_SERVER="mongodb://0.0.0.0:27017"
ENV DB_DATABASE="dbtest"
ENV DB_COLECTION="dev"

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["mongotest/mongotest.csproj", "mongotest/"]
RUN dotnet restore "./mongotest/mongotest.csproj"
COPY . .
WORKDIR "/src/mongotest"
RUN dotnet build "./mongotest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./mongotest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mongotest.dll"]