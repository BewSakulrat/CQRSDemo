# Use the official .NET 8 SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the solution file and project files
COPY *.sln ./
COPY src/WebApi/*.csproj ./src/WebApi/
COPY src/Application/*.csproj ./src/Application/
COPY src/Domain/*.csproj ./src/Domain/
COPY src/Infrastructure/*.csproj ./src/Infrastructure/

# Restore dependencies
RUN dotnet restore ./src/WebApi/WebApi.csproj

# Copy the entire source code and build the application
COPY src/ ./src/
RUN dotnet publish ./src/WebApi/WebApi.csproj -c Release -o out

# Use the runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build /app/out .

# Expose the port for your WebAPI
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "Webapi.dll"]
