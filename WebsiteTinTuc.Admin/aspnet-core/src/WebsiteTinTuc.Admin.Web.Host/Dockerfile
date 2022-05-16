FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

WORKDIR /app
EXPOSE 21021

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.100-buster AS build
WORKDIR /src
COPY ["WebsiteTinTuc.Admin.Web.Host/WebsiteTinTuc.Admin.Web.Host.csproj", "WebsiteTinTuc.Admin.Web.Host/"]
COPY ["WebsiteTinTuc.Admin.Application/WebsiteTinTuc.Admin.Application.csproj", "WebsiteTinTuc.Admin.Application/"]
COPY ["WebsiteTinTuc.Admin.Core/WebsiteTinTuc.Admin.Core.csproj", "WebsiteTinTuc.Admin.Core/"]
COPY ["WebsiteTinTuc.Admin.EntityFrameworkCore/WebsiteTinTuc.Admin.EntityFrameworkCore.csproj", "WebsiteTinTuc.Admin.EntityFrameworkCore/"]
COPY ["WebsiteTinTuc.Admin.Migrator/WebsiteTinTuc.Admin.Migrator.csproj", "WebsiteTinTuc.Admin.Migrator/"]
COPY ["WebsiteTinTuc.Admin.Web.Core/WebsiteTinTuc.Admin.Web.Core.csproj", "WebsiteTinTuc.Admin.Web.Core/"]

RUN dotnet restore "WebsiteTinTuc.Admin.Web.Host/WebsiteTinTuc.Admin.Web.Host.csproj"
COPY . .

WORKDIR "/src/WebsiteTinTuc.Admin.Web.Host"
RUN dotnet build "WebsiteTinTuc.Admin.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebsiteTinTuc.Admin.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebsiteTinTuc.Admin.Web.Host.dll"]
