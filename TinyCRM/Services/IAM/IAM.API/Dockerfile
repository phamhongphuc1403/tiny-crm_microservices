FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TinyCRM/Services/IAM/IAM.API/IAM.API.csproj", "TinyCRM/Services/IAM/IAM.API/"]
COPY ["TinyCRM/BuildingBlocks/BuildingBlock.Presentation/BuildingBlock.Presentation.csproj", "TinyCRM/BuildingBlocks/BuildingBlock.Presentation/"]
COPY ["TinyCRM/BuildingBlocks/BuildingBlock.Application/BuildingBlock.Application.csproj", "TinyCRM/BuildingBlocks/BuildingBlock.Application/"]
COPY ["TinyCRM/BuildingBlocks/BuildingBlock.Domain/BuildingBlock.Domain.csproj", "TinyCRM/BuildingBlocks/BuildingBlock.Domain/"]
COPY ["TinyCRM/BuildingBlocks/BuildingBlock.Infrastructure.EFCore/BuildingBlock.Infrastructure.EFCore.csproj", "TinyCRM/BuildingBlocks/BuildingBlock.Infrastructure.EFCore/"]
COPY ["TinyCRM/BuildingBlocks/BuildingBlock.RabbitMQ/BuildingBlock.RabbitMQ.csproj", "TinyCRM/BuildingBlocks/BuildingBlock.RabbitMQ/"]
COPY ["TinyCRM/Services/IAM/IAM.Business/IAM.Business.csproj", "TinyCRM/Services/IAM/IAM.Business/"]
COPY ["TinyCRM/Services/IAM/IAM.Domain/IAM.Domain.csproj", "TinyCRM/Services/IAM/IAM.Domain/"]
COPY ["TinyCRM/Services/IAM/IAM.Infrastructure/IAM.Infrastructure.csproj", "TinyCRM/Services/IAM/IAM.Infrastructure/"]
COPY ["TinyCRM/BuildingBlocks/BuildingBlock.Infrastructure.RedisCache/BuildingBlock.Infrastructure.RedisCache.csproj", "TinyCRM/BuildingBlocks/BuildingBlock.Infrastructure.RedisCache/"]
RUN dotnet restore "TinyCRM/Services/IAM/IAM.API/IAM.API.csproj"
COPY . .
WORKDIR "/src/TinyCRM/Services/IAM/IAM.API"
RUN dotnet build "IAM.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IAM.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IAM.API.dll"]
