FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DotNetCoreRabbitMqQueue/DotNetCoreRabbitMqQueue.csproj", "DotNetCoreRabbitMqQueue/"]
RUN dotnet restore "DotNetCoreRabbitMqQueue/DotNetCoreRabbitMqQueue.csproj"
COPY . .
WORKDIR "/src/DotNetCoreRabbitMqQueue"
RUN dotnet build "DotNetCoreRabbitMqQueue.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetCoreRabbitMqQueue.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetCoreRabbitMqQueue.dll"]
