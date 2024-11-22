# Stage 1: Build the Blazor WebAssembly project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the solution file
COPY ["*.sln", "./"]

# Copy the PWA project directory specifically
COPY ["src/MMS.PWA", "src/MMS.PWA/"]

# Restore dependencies
WORKDIR /app/src/MMS.PWA
RUN dotnet restore

# Build and publish the PWA project
RUN dotnet publish -c Release -o /app/dist

# Stage 2: Serve the application using Nginx
FROM nginx:1.25.2-alpine
WORKDIR /usr/share/nginx/html

# Remove the default Nginx static files
RUN rm -rf ./*

# Copy the published output from the build stage
COPY --from=build /app/dist/wwwroot .

# Expose port 80 for the server
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
