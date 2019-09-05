FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["HerokuTest1.csproj", "HerokuTest1/"]
RUN dotnet restore "HerokuTest1/HerokuTest1.csproj"
COPY . "./HerokuTest1"
WORKDIR "/src/HerokuTest1"
RUN dotnet build "HerokuTest1.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HerokuTest1.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD dotnet HerokuTest1.dll