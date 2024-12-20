# Use the official .NET 6.0 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /app

# Copy the csproj file and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the files and build the project
COPY . ./
RUN dotnet build -c Release -o /app/build

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 6.0 ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Expose the port on which the application will run
EXPOSE 80

# Define the entry point for the container
ENTRYPOINT ["dotnet", "login.dll"]
