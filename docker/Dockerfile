FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Ecommerce.Api/Ecommerce.Api.csproj", "Ecommerce.Api/"]
COPY ["src/Ecommerce.Application/Ecommerce.Application.csproj", "Ecommerce.Application/"]
COPY ["src/Ecommerce.Domain/Ecommerce.Domain.csproj", "Ecommerce.Domain/"]
COPY ["src/Ecommerce.Infrastructure/Ecommerce.Infrastructure.csproj", "Ecommerce.Infrastructure/"]
RUN dotnet restore "Ecommerce.Api/Ecommerce.Api.csproj"
COPY src/ .
WORKDIR "/src/Ecommerce.Api"
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.dll"]
