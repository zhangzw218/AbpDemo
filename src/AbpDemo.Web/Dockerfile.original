#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["NuGet.Config", "."]
COPY ["src/AbpDemo.Web/AbpDemo.Web.csproj", "src/AbpDemo.Web/"]
COPY ["src/AbpDemo.Application/AbpDemo.Application.csproj", "src/AbpDemo.Application/"]
COPY ["src/AbpDemo.Domain/AbpDemo.Domain.csproj", "src/AbpDemo.Domain/"]
COPY ["src/AbpDemo.Domain.Shared/AbpDemo.Domain.Shared.csproj", "src/AbpDemo.Domain.Shared/"]
COPY ["src/AbpDemo.Application.Contracts/AbpDemo.Application.Contracts.csproj", "src/AbpDemo.Application.Contracts/"]
COPY ["src/AbpDemo.HttpApi/AbpDemo.HttpApi.csproj", "src/AbpDemo.HttpApi/"]
COPY ["src/AbpDemo.EntityFrameworkCore/AbpDemo.EntityFrameworkCore.csproj", "src/AbpDemo.EntityFrameworkCore/"]
RUN dotnet restore "src/AbpDemo.Web/AbpDemo.Web.csproj"
COPY . .
WORKDIR "/src/src/AbpDemo.Web"
RUN dotnet build "AbpDemo.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AbpDemo.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AbpDemo.Web.dll"]