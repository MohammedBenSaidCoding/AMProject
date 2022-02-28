FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AM.Api/AM.Api.csproj", "AM.Api/"]
COPY ["src/AM.Application/AM.Application.csproj", "AM.Application/"]
COPY ["src/AM.Domain/AM.Domain.csproj", "AM.Domain/"]
COPY ["src/AM.Domain.Shared/AM.Domain.Shared.csproj", "AM.Domain.Shared/"]
RUN dotnet restore "AM.Api/AM.Api.csproj"

COPY ./src .
WORKDIR "/src/AM.Api"
RUN dotnet build "AM.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AM.Api.csproj" -c Release -o  /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AM.Api.dll"]



