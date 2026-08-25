#!/bin/bash
set -e

echo "Building Docker images..."

DOCKER_REGISTRY="ghcr.io/your-org"
API_IMAGE="$DOCKER_REGISTRY/ecommerce-api"
WEB_IMAGE="$DOCKER_REGISTRY/ecommerce-web"

echo "Building API image..."
docker build -f docker/Dockerfile.api -t "$API_IMAGE:latest" .

echo "Building Web image..."
docker build -f docker/Dockerfile.web -t "$WEB_IMAGE:latest" .

echo "Docker images built successfully!"
