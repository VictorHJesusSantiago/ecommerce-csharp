#!/bin/bash
set -e

echo "Running database migrations..."

ENVIRONMENT=${1:-Development}

if [ "$ENVIRONMENT" = "Production" ]; then
    echo "WARNING: Running migrations in production!"
    read -p "Are you sure? (yes/no): " confirm
    if [ "$confirm" != "yes" ]; then
        echo "Migration cancelled."
        exit 1
    fi
fi

echo "Applying migrations..."
dotnet ef database update --project src/Ecommerce.Infrastructure --startup-project src/Ecommerce.Api

echo "Migrations completed successfully!"
