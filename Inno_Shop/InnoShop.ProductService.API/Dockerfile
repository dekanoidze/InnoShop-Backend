FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["InnoShop.ProductService.API/InnoShop.ProductService.API.csproj", "InnoShop.ProductService.API/"]
COPY ["InnoShop.ProductService.Application/InnoShop.ProductService.Application.csproj", "InnoShop.ProductService.Application/"]
COPY ["InnoShop.ProductService.Domain/InnoShop.ProductService.Domain.csproj", "InnoShop.ProductService.Domain/"]
COPY ["InnoShop.ProductService.Infrastructure/InnoShop.ProductService.Infrastructure.csproj", "InnoShop.ProductService.Infrastructure/"]
RUN dotnet restore "InnoShop.ProductService.API/InnoShop.ProductService.API.csproj"
COPY . .
RUN dotnet publish "InnoShop.ProductService.API/InnoShop.ProductService.API.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "InnoShop.ProductService.API.dll"]
- ConnectionStrings__DefaultConnection=Server=sqlserver;Database=InnoShopUserDb;User Id=sa;Password=InnoShop@123456;TrustServerCertificate=True