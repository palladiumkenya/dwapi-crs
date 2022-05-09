FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Dwapi.Crs/Dwapi.Crs.csproj", "Dwapi.Crs/"]
RUN dotnet restore "src/Dwapi.Crs/Dwapi.Crs.csproj"
COPY . .
WORKDIR "/src/Dwapi.Crs"
RUN dotnet build "Dwapi.Crs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dwapi.Crs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dwapi.Crs.dll"]
