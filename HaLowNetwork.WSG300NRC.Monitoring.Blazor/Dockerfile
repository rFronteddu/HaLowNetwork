FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HaLowNetwork.Monitoring.Blazor.WSG300NRC/HaLowNetwork.Monitoring.Blazor.WSG300NRC.csproj", "HaLowNetwork.Monitoring.Blazor.WSG300NRC/"]
RUN dotnet restore "HaLowNetwork.Monitoring.Blazor.WSG300NRC/HaLowNetwork.Monitoring.Blazor.WSG300NRC.csproj"
COPY . .
WORKDIR "/src/HaLowNetwork.Monitoring.Blazor.WSG300NRC"
RUN dotnet build "HaLowNetwork.Monitoring.Blazor.WSG300NRC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HaLowNetwork.Monitoring.Blazor.WSG300NRC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HaLowNetwork.Monitoring.Blazor.WSG300NRC.dll"]
