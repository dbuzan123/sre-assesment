FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["WebServiceA.csproj", "./"]
RUN dotnet restore "./WebServiceA.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebServiceA.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "WebServiceA.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebServiceA.dll"]