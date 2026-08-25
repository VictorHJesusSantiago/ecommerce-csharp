#!/bin/bash
set -e

echo "Starting deployment..."

ENVIRONMENT=${1:-staging}
DOCKER_REGISTRY="ghcr.io/your-org"
API_IMAGE="$DOCKER_REGISTRY/ecommerce-api"
WEB_IMAGE="$DOCKER_REGISTRY/ecommerce-web"

echo "Deploying to $ENVIRONMENT environment..."

echo "Pulling latest images..."
docker pull "$API_IMAGE:latest"
docker pull "$WEB_IMAGE:latest"

echo "Stopping existing containers..."
docker-compose -f docker-compose.prod.yml down

echo "Starting services..."
docker-compose -f docker-compose.prod.yml up -d

echo "Waiting for services to start..."
sleep 30

echo "Running health checks..."
curl -f http://localhost/health || exit 1

echo "Deployment completed successfully!"
