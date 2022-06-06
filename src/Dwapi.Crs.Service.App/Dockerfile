FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Dwapi.Crs.Service.App/Dwapi.Crs.Service.App.csproj", "Dwapi.Crs.Service.App/"]
COPY ["src/Dwapi.Crs.Service.Infrastructure/Dwapi.Crs.Service.Infrastructure.csproj", "Dwapi.Crs.Service.Infrastructure/"]
COPY ["src/Dwapi.Crs.Core/Dwapi.Crs.Core.csproj", "Dwapi.Crs.Core/"]
COPY ["src/Dwapi.Contracts/Dwapi.Contracts.csproj", "Dwapi.Contracts/"]
COPY ["src/Dwapi.Crs.SharedKernel/Dwapi.Crs.SharedKernel.csproj", "Dwapi.Crs.SharedKernel/"]
COPY ["src/Dwapi.Crs.Service.Application/Dwapi.Crs.Service.Application.csproj", "Dwapi.Crs.Service.Application/"]
COPY ["src/Dwapi.Crs.SharedKernel.Infrastructure/Dwapi.Crs.SharedKernel.Infrastructure.csproj", "Dwapi.Crs.SharedKernel.Infrastructure/"]
RUN dotnet restore "src/Dwapi.Crs.Service.App/Dwapi.Crs.Service.App.csproj"
COPY . .
WORKDIR "/src/Dwapi.Crs.Service.App"
RUN dotnet build "Dwapi.Crs.Service.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dwapi.Crs.Service.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dwapi.Crs.Service.App.dll"]
