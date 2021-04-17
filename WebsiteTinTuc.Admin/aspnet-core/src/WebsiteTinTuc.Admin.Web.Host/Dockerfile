FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.100-buster AS build
WORKDIR /src
COPY ["src/WebsiteTinTuc.Admin.Web.Host/WebsiteTinTuc.Admin.Web.Host.csproj", "src/WebsiteTinTuc.Admin.Web.Host/"]
COPY ["src/WebsiteTinTuc.Admin.Application/WebsiteTinTuc.Admin.Application.csproj", "src/WebsiteTinTuc.Admin.Application/"]
COPY ["src/WebsiteTinTuc.Admin.Core/WebsiteTinTuc.Admin.Core.csproj", "src/WebsiteTinTuc.Admin.Core/"]
COPY ["src/WebsiteTinTuc.Admin.EntityFrameworkCore/WebsiteTinTuc.Admin.EntityFrameworkCore.csproj", "src/WebsiteTinTuc.Admin.EntityFrameworkCore/"]
COPY ["src/WebsiteTinTuc.Admin.Migrator/WebsiteTinTuc.Admin.Migrator.csproj", "src/WebsiteTinTuc.Admin.Migrator/"]
COPY ["src/WebsiteTinTuc.Admin.Web.Core/WebsiteTinTuc.Admin.Web.Core.csproj", "src/WebsiteTinTuc.Admin.Web.Core/"]

RUN dotnet restore "src/WebsiteTinTuc.Admin.Web.Host/WebsiteTinTuc.Admin.Web.Host.csproj" -nowarn:msb3202,msb3277,nu1503
COPY . .

WORKDIR "/src/src/WebsiteTinTuc.Admin.Web.Host"
RUN dotnet build "WebsiteTinTuc.Admin.Web.Host.csproj" --no-restore -nowarn:msb3202,msb3277,nu1503 -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebsiteTinTuc.Admin.Web.Host" --no-restore -nowarn:msb3202,msb3277,nu1503 -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebsiteTinTuc.Admin.Web.Host.dll"]
