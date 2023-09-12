FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TinyCRM/Services/IAM/IAM.API/IAM.API.csproj", "TinyCRM/Services/IAM/IAM.API/"]
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
