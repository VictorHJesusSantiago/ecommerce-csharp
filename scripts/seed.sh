#!/bin/bash
set -e

echo "Seeding database..."

ENVIRONMENT=${1:-Development}

echo "Running seed data for $ENVIRONMENT..."
dotnet run --project src/Ecommerce.Api -- --seed

echo "Database seeded successfully!"
