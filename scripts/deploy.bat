@echo off
setlocal

set ENVIRONMENT=%1
if "%ENVIRONMENT%"=="" set ENVIRONMENT=staging

echo Deploying to %ENVIRONMENT% environment...

echo Pulling latest images...
docker pull ghcr.io/your-org/ecommerce-api:latest
docker pull ghcr.io/your-org/ecommerce-web:latest

echo Stopping existing containers...
docker-compose -f docker-compose.prod.yml down

echo Starting services...
docker-compose -f docker-compose.prod.yml up -d

echo Waiting for services to start...
timeout /t 30 /nobreak

echo Running health checks...
curl -f http://localhost/health
if %errorlevel% neq 0 (
    echo Health check failed!
    exit /b 1
)

echo Deployment completed successfully!
