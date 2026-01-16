# =========================
# ETAPA 1: BUILD
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar solución y proyectos
COPY *.sln .
COPY Api.WebApi/*.csproj Api.WebApi/
COPY Api.DAO/*.csproj Api.DAO/
COPY Api.Models/*.csproj Api.Models/
COPY Api.Handlers/*.csproj Api.Handlers/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código
COPY . .

# Publicar la Web API
RUN dotnet publish Api.WebApi/Api.WebApi.csproj -c Release -o /out

# =========================
# ETAPA 2: RUNTIME
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

# Railway usa el puerto 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Api.WebApi.dll"]
