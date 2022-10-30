FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Mullayon.Api/Mullayon.Api.csproj", "Mullayon.Api/"]
RUN dotnet restore "Mullayon.Api/Mullayon.Api.csproj"
COPY . .
WORKDIR "/src/Mullayon.Api"
RUN dotnet build "Mullayon.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mullayon.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mullayon.Api.dll"]
