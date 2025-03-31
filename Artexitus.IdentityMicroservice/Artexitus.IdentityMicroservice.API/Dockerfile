FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Artexitus.IdentityMicroservice/Artexitus.IdentityMicroservice.API/Artexitus.IdentityMicroservice.API.csproj", "Artexitus.IdentityMicroservice/Artexitus.IdentityMicroservice.API/"]
COPY ["Artexitus.IdentityMicroservice/Artexitus.IdentityMicroservice.Application/Artexitus.IdentityMicroservice.Application.csproj", "Artexitus.IdentityMicroservice/Artexitus.IdentityMicroservice.Application/"]
COPY ["Artexitus.IdentityMicroservice/Artexitus.IdentityMicroservice.Contracts/Artexitus.IdentityMicroservice.Contracts.csproj", "Artexitus.IdentityMicroservice/Artexitus.IdentityMicroservice.Contracts/"]
COPY ["Artexitus.IdentityMicroservice/Artexitus.IdentityMicroservice.Domain/Artexitus.IdentityMicroservice.Domain.csproj", "Artexitus.IdentityMicroservice/Artexitus.IdentityMicroservice.Domain/"]
RUN dotnet restore "./Artexitus.IdentityMicroservice/Artexitus.IdentityMicroservice.API/Artexitus.IdentityMicroservice.API.csproj"
COPY . .
WORKDIR "/src/Artexitus.IdentityMicroservice/Artexitus.IdentityMicroservice.API"
RUN dotnet build "./Artexitus.IdentityMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Artexitus.IdentityMicroservice.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Artexitus.IdentityMicroservice.API.dll"]