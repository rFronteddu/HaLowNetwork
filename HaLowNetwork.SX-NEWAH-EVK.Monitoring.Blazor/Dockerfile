FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HaLowNetwork.SX-NEWAH-EVK.Monitoring.Blazor/HaLowNetwork.SX-NEWAH-EVK.Monitoring.Blazor.csproj", "HaLowNetwork.SX-NEWAH-EVK.Monitoring.Blazor/"]
COPY ["HaLowNetwork.SX-NEWAH-EVK.Parse/HaLowNetwork.SX-NEWAH-EVK.Parse.csproj", "HaLowNetwork.SX-NEWAH-EVK.Parse/"]
COPY ["HaLow.Raspbian/HaLow.Raspbian.csproj", "HaLow.Raspbian/"]
RUN dotnet restore "HaLowNetwork.SX-NEWAH-EVK.Monitoring.Blazor/HaLowNetwork.SX-NEWAH-EVK.Monitoring.Blazor.csproj"
COPY . .
WORKDIR "/src/HaLowNetwork.SX-NEWAH-EVK.Monitoring.Blazor"
RUN dotnet build "HaLowNetwork.SX-NEWAH-EVK.Monitoring.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HaLowNetwork.SX-NEWAH-EVK.Monitoring.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HaLowNetwork.SX-NEWAH-EVK.Monitoring.Blazor.dll"]
