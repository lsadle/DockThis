FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["StandAlone/StandAlone.csproj", "StandAlone/"]
RUN dotnet restore "StandAlone/StandAlone.csproj"
COPY . .
WORKDIR "/src/StandAlone"
RUN dotnet build "StandAlone.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StandAlone.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StandAlone.dll"]