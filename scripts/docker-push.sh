#!/bin/bash
set -e

echo "Pushing Docker images..."

DOCKER_REGISTRY="ghcr.io/your-org"
API_IMAGE="$DOCKER_REGISTRY/ecommerce-api"
WEB_IMAGE="$DOCKER_REGISTRY/ecommerce-web"

echo "Logging in to container registry..."
docker login $DOCKER_REGISTRY

echo "Pushing API image..."
docker push "$API_IMAGE:latest"

echo "Pushing Web image..."
docker push "$WEB_IMAGE:latest"

echo "Docker images pushed successfully!"
