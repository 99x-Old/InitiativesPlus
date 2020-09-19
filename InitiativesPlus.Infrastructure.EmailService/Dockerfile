#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["InitiativesPlus.Infrastructure.EmailService/InitiativesPlus.Infrastructure.EmailService.csproj", "InitiativesPlus.Infrastructure.EmailService/"]
RUN dotnet restore "InitiativesPlus.Infrastructure.EmailService/InitiativesPlus.Infrastructure.EmailService.csproj"
COPY . .
WORKDIR "/src/InitiativesPlus.Infrastructure.EmailService"
RUN dotnet build "InitiativesPlus.Infrastructure.EmailService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InitiativesPlus.Infrastructure.EmailService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InitiativesPlus.Infrastructure.EmailService.dll"]