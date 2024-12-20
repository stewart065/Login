FROM mcr.microsoft.com/dotnet/aspnet:6.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0-nanoserver-1809 AS build
ARG configuration=Release
WORKDIR /src
COPY ["login.csproj", "./"]
RUN dotnet restore "login.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "login.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "login.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "login.dll"]
