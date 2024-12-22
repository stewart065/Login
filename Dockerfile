# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Build image for compiling the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["login.csproj", "./"]
RUN dotnet restore "login.csproj"

# Copy the remaining source code and build the application
COPY . .
RUN dotnet build "login.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "login.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Entry point for the application
ENTRYPOINT ["dotnet", "login.dll"]
