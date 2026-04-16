# Pricing Platform API

A robust, scalable pricing engine API built with .NET 8 that provides dynamic quote calculations based on configurable business rules.

## Project Overview

The **Pricing Platform** is an enterprise-grade API service designed to calculate product quotes dynamically. It supports:
- Real-time quote calculations based on various pricing rules
- Bulk quote processing with asynchronous job tracking
- Rule-based pricing engine with weight, location, and time-based calculations
- RESTful API with comprehensive documentation

## Architecture Overview

```
PricingPlatform
├── src/
│   ├── Pricing.API              # ASP.NET Core Web API
│   │   ├── Controllers/         # API endpoints
│   │   ├── Swagger/             # API documentation examples
│   │   └── wwwroot/            # Static files (UI, docs)
│   ├── Pricing.Application      # Business logic layer
│   │   ├── PricingService.cs   # Core pricing service
│   │   └── Rules.cs            # Pricing rule implementations
│   ├── Pricing.Domain           # Domain models & interfaces
│   │   ├── Models/             # Core domain entities
│   │   └── Interfaces/         # Repository patterns
│   └── Pricing.Infrastructure   # Data access layer
│       └── Repositories/       # Repository implementations
├── tests/
│   ├── Pricing.Tests           # Unit tests
│   └── Pricing.Integration.Tests # Integration tests
├── data/
│   ├── rules.json              # Sample pricing rules
│   └── bulk_quotes.csv         # Sample bulk quotes
├── Dockerfile                  # Container configuration
├── docker-compose.yml          # Multi-container setup
└── README.md                   # This file
```

## Architecture Layers

### 1. **Domain Layer** (`Pricing.Domain`)
- **Responsibility**: Core business entities and interfaces
- **Contains**:
  - `Models/`: `QuoteRequest`, `QuoteResponse`, `JobModel`, `PricingRule`
  - `Interfaces/`: Repository contracts (`IJobRepository`, `IRuleRepository`, `IPricingRule`)

### 2. **Application Layer** (`Pricing.Application`)
- **Responsibility**: Business logic and rule execution
- **Contains**:
  - `PricingService`: Orchestrates quote calculations
  - `Rules.cs`: Implements `WeightRule`, `RemoteRule`, `TimeRule`

### 3. **Infrastructure Layer** (`Pricing.Infrastructure`)
- **Responsibility**: Data persistence and external services
- **Contains**:
  - `InMemoryJobRepository`: Job storage (can be extended to use database)
  - `InMemoryRuleRepository`: Rule storage

### 4. **Presentation Layer** (`Pricing.API`)
- **Responsibility**: HTTP endpoints and API documentation
- **Contains**:
  - `QuotesController`: Quote pricing endpoints
  - `JobsController`: Job tracking endpoints
  - `RulesController`: Rule management endpoints
  - `HealthController`: Health check endpoint

## Key Features

- **Dynamic Quote Calculation**: Apply multiple pricing rules in sequence
- **Bulk Processing**: Handle large quote requests asynchronously
- **Job Tracking**: Monitor the status of bulk quote processing
- **Rule Management**: CRUD operations for pricing rules
- **Health Monitoring**: API health check endpoint
- **Swagger/OpenAPI Documentation**: Interactive API documentation
- **Extensible Design**: Easy to add new pricing rules

## Setup Guide

### Prerequisites

- **.NET 8.0 SDK** or later
- **Docker** (optional, for containerized deployment)
- **Git** (for version control)

### Local Development

#### 1. Clone & Navigate
```bash
git clone <repository-url>
cd PricingPlatform
```

#### 2. Restore Dependencies
```bash
dotnet restore
```

#### 3. Build Solution
```bash
dotnet build
```

#### 4. Run Tests
```bash
dotnet test
```

#### 5. Run API
```bash
dotnet run --project src/Pricing.API
```

The API will be available at: `http://localhost:5000`
- **Swagger UI**: `http://localhost:5000/swagger`

### Docker Deployment

#### Build & Run with Docker Compose
```bash
docker-compose up --build
```

The API will be available at: `http://localhost:5000`

#### Manual Docker Build
```bash
docker build -t pricing-platform .
docker run -p 5000:80 pricing-platform
```

## Sample Requests

### 1. Health Check
```bash
curl -X GET http://localhost:5000/health
```

**Response**:
```json
{
  "status": "healthy",
  "timestamp": "2026-04-16T10:00:00Z"
}
```

### 2. Calculate Single Quote
```bash
curl -X POST http://localhost:5000/quotes/price \
  -H "Content-Type: application/json" \
  -d '{
    "basePrice": 100,
    "weight": 5.5,
    "location": "remote",
    "destination": "International",
    "requestTime": "2026-04-16T10:00:00Z"
  }'
```

**Response**:
```json
{
  "basePrice": 100,
  "finalPrice": 127.5,
  "appliedRules": [
    "Weight Rule: +10%",
    "Remote Location: +15%"
  ]
}
```

### 3. Bulk Quote Request
```bash
curl -X POST http://localhost:5000/quotes/bulk \
  -H "Content-Type: application/json" \
  -d '[
    {
      "basePrice": 100,
      "weight": 5.5,
      "location": "remote",
      "destination": "International"
    },
    {
      "basePrice": 200,
      "weight": 2.0,
      "location": "local",
      "destination": "Domestic"
    }
  ]'
```

**Response**:
```json
{
  "job_id": "550e8400-e29b-41d4-a716-446655440000"
}
```

### 4. Get Job Status
```bash
curl -X GET http://localhost:5000/jobs/550e8400-e29b-41d4-a716-446655440000
```

**Response**:
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "status": "Completed",
  "results": [
    {
      "basePrice": 100,
      "finalPrice": 127.5,
      "appliedRules": ["Weight Rule: +10%", "Remote Location: +15%"]
    },
    {
      "basePrice": 200,
      "finalPrice": 200,
      "appliedRules": []
    }
  ]
}
```

### 5. Create Pricing Rule
```bash
curl -X POST http://localhost:5000/rules \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Premium Weight Rule",
    "priority": 1,
    "effectiveFrom": "2026-04-16T00:00:00Z",
    "effectiveTo": "2026-12-31T23:59:59Z",
    "isActive": true
  }'
```

**Response**:
```json
{
  "id": "660f9511-f30c-52e5-b827-557766551111",
  "name": "Premium Weight Rule",
  "priority": 1,
  "effectiveFrom": "2026-04-16T00:00:00Z",
  "effectiveTo": "2026-12-31T23:59:59Z",
  "isActive": true
}
```

### 6. Get All Rules
```bash
curl -X GET http://localhost:5000/rules
```

## API Endpoints

### Quotes Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/quotes/price` | Calculate single quote |
| POST | `/quotes/bulk` | Submit bulk quote request |

### Jobs Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/jobs/{jobId}` | Get job status and results |

### Rules Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/rules` | Get all pricing rules |
| GET | `/rules/{id}` | Get specific rule |
| POST | `/rules` | Create new rule |
| PUT | `/rules/{id}` | Update rule |
| DELETE | `/rules/{id}` | Delete rule |

### Health Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/health` | API health status |

## Interactive API Documentation

Once the API is running, visit:
- **Swagger UI**: `http://localhost:5000/swagger`
- **OpenAPI JSON**: `http://localhost:5000/swagger/v1/swagger.json`

## Sample Data Files

### rules.json
Located in `/data/rules.json` - Contains sample pricing rules:
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "Weight-based Surcharge",
    "priority": 1,
    "effectiveFrom": "2026-01-01T00:00:00Z",
    "effectiveTo": "2026-12-31T23:59:59Z",
    "isActive": true
  },
  {
    "id": "660f9511-f30c-52e5-b827-557766551111",
    "name": "Remote Location Fee",
    "priority": 2,
    "effectiveFrom": "2026-01-01T00:00:00Z",
    "effectiveTo": "2026-12-31T23:59:59Z",
    "isActive": true
  }
]
```

### bulk_quotes.csv
Located in `/data/bulk_quotes.csv` - Sample bulk quote data:
```csv
BasePrice,Weight,Location,Destination
100,5.5,remote,International
200,2.0,local,Domestic
150,3.2,remote,International
300,0.5,local,Domestic
250,8.0,remote,International
```

## Testing

### Run Unit Tests
```bash
dotnet test tests/Pricing.Tests
```

### Run Integration Tests
```bash
dotnet test tests/Pricing.Integration.Tests
```

### Test Coverage
```bash
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

## Pricing Rules

The system supports three built-in pricing rules:

### 1. Weight Rule
- **Logic**: Adds 10% surcharge for every 5kg of weight
- **Formula**: `price += basePrice * (weight / 5) * 0.10`

### 2. Remote Location Rule
- **Logic**: Adds 15% surcharge for remote locations
- **Applicable**: When location = "remote"
- **Surcharge**: 15%

### 3. Time Rule
- **Logic**: Adds 5% surcharge for rush orders (requested before current time)
- **Formula**: `if (requestTime < DateTime.Now) then price += basePrice * 0.05`

## Configuration

### Environment Variables
```bash
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:80
```

### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

## Deployment

### Production Checklist
- [ ] Run full test suite
- [ ] Build in Release mode: `dotnet build -c Release`
- [ ] Configure environment variables
- [ ] Set up database (if not using in-memory storage)
- [ ] Configure logging and monitoring
- [ ] Update Swagger configuration for production
- [ ] Test API endpoints with load testing
- [ ] Set up CI/CD pipeline

### Cloud Deployment (Azure)
```bash
# Deploy to Azure Container Instances
az container create --resource-group <rg> \
  --name pricing-platform \
  --image <registry>/pricing-platform:latest \
  --ports 80
```

## Troubleshooting

### Build Errors
```bash
# Clean build artifacts
dotnet clean
dotnet build
```

### Runtime Issues
```bash
# Check logs
dotnet run --project src/Pricing.API
```

### Docker Issues
```bash
# Rebuild without cache
docker-compose build --no-cache
docker-compose up
```

## Performance Optimization

- **Caching**: Implement rule caching to avoid repeated database calls
- **Async Processing**: Bulk requests are processed asynchronously
- **Database Indexing**: Index JobId and Status fields for faster queries
- **Connection Pooling**: Configure connection pools for database connections

## Future Enhancements

- [ ] Persistent database storage (SQL Server / PostgreSQL)
- [ ] Advanced rule builder UI
- [ ] Rule versioning and rollback
- [ ] Audit logging
- [ ] Rate limiting
- [ ] API authentication (OAuth 2.0)
- [ ] GraphQL support
- [ ] Machine learning-based pricing recommendations
- [ ] Real-time notifications for bulk job completion

## Contributing

1. Create a feature branch: `git checkout -b feature/your-feature`
2. Commit changes: `git commit -am 'Add feature'`
3. Push to branch: `git push origin feature/your-feature`
4. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For issues, questions, or suggestions:
- **Issues**: Create an issue on GitHub
- **Email**: support@pricing-platform.local
- **Documentation**: See `/docs` folder for detailed documentation

---

**Last Updated**: April 16, 2026
**Version**: 1.0.0
**Maintainer**: Development Team
