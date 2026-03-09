FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["InnoShop.UserService.API/InnoShop.UserService.API.csproj", "InnoShop.UserService.API/"]
COPY ["InnoShop.UserService.Application/InnoShop.UserService.Application.csproj", "InnoShop.UserService.Application/"]
COPY ["InnoShop.UserService.Domain/InnoShop.UserService.Domain.csproj", "InnoShop.UserService.Domain/"]
COPY ["InnoShop.UserService.Infrastructure/InnoShop.UserService.Infrastructure.csproj", "InnoShop.UserService.Infrastructure/"]
RUN dotnet restore "InnoShop.UserService.API/InnoShop.UserService.API.csproj"
COPY . .
RUN dotnet publish "InnoShop.UserService.API/InnoShop.UserService.API.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "InnoShop.UserService.API.dll"]