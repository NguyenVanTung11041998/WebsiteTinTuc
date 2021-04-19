FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.100-buster AS build
WORKDIR /src
COPY ./src .
RUN dotnet restore "WebsiteTinTuc.Admin.Web.Host/WebsiteTinTuc.Admin.Web.Host.csproj" -nowarn:msb3202,msb3277,nu1503

FROM build AS publish
RUN dotnet publish "WebsiteTinTuc.Admin.Web.Host/WebsiteTinTuc.Admin.Web.Host.csproj" --no-restore -nowarn:msb3202,msb3277,nu1503 -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebsiteTinTuc.Admin.Web.Host.dll"]