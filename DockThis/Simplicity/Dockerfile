FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Simplicity/Simplicity.csproj", "Simplicity/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "Simplicity/Simplicity.csproj"
COPY . .
WORKDIR "/src/Simplicity"
RUN dotnet build "Simplicity.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Simplicity.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Simplicity.dll"]