# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY src/MMS.Api/*.csproj ./src/MMS.Api/
RUN dotnet restore ./src/MMS.Api/MMSApi.csproj

# Copy the source files and build the application
COPY src ./src
WORKDIR /app/src/MMS.Api
RUN dotnet publish -c Release -o /app/out

# Stage 2: Create the runtime container
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the build output
COPY --from=build /app/out ./

# Copy the certificate
COPY src/MMS.Api/certificates/localhost.pfx /app/certificates/localhost.pfx

# Configure environment variables for Kestrel
ENV Kestrel__Certificates__Default__Path=/app/certificates/localhost.pfx
ENV Kestrel__Certificates__Default__Password=test_password

# Expose HTTP and HTTPS ports
EXPOSE 80
EXPOSE 443

# Set the entry point
ENTRYPOINT ["dotnet", "MMS.Api.dll"]
