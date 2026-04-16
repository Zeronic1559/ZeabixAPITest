# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and project files
COPY . .

# Restore dependencies
RUN dotnet restore

# Build solution
RUN dotnet build -c Release --no-restore

# Publish
RUN dotnet publish src/Pricing.API -c Release -o /out --no-build

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published files from build stage
COPY --from=build /out .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD dotnet /app/Pricing.API.dll || exit 1

# Expose port
EXPOSE 80

# Entry point
ENTRYPOINT ["dotnet", "Pricing.API.dll"]
