FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SignalRDemo3ytEFC.csproj", "."]
RUN dotnet restore "./SignalRDemo3ytEFC.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SignalRDemo3ytEFC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SignalRDemo3ytEFC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignalRDemo3ytEFC.dll"]