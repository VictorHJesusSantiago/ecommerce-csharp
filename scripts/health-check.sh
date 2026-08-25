#!/bin/bash
set -e

echo "Checking application health..."

echo "Checking API health..."
curl -f http://localhost/health || echo "API health check failed"

echo "Checking Web health..."
curl -f http://localhost:5000/health || echo "Web health check failed"

echo "Health checks completed!"
