#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 5006

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/SC.API.CleanArchitecture.Web/SC.API.CleanArchitecture.Web.csproj", "src/SC.API.CleanArchitecture.Web/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
RUN dotnet restore "src/SC.API.CleanArchitecture.Web/SC.API.CleanArchitecture.Web.csproj"
COPY . .
WORKDIR "/src/SC.API.CleanArchitecture.Web"
RUN dotnet build "SC.API.CleanArchitecture.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SC.API.CleanArchitecture.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SC.API.CleanArchitecture.Web.dll"]
