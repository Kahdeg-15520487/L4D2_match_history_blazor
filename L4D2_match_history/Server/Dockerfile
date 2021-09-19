#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["L4D2_match_history/Server/L4D2_match_history.Server.csproj", "L4D2_match_history/Server/"]
COPY ["L4D2_match_history/Shared/L4D2_match_history.Shared.csproj", "L4D2_match_history/Shared/"]
COPY ["L4D2_match_history/Client/L4D2_match_history.Client.csproj", "L4D2_match_history/Client/"]
RUN dotnet restore "L4D2_match_history/Server/L4D2_match_history.Server.csproj"
COPY . .
WORKDIR "/src/L4D2_match_history/Server"
RUN dotnet build "L4D2_match_history.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "L4D2_match_history.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "L4D2_match_history.Server.dll"]