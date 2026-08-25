#!/bin/bash
set -e

echo "Running tests..."

echo "Running unit tests..."
dotnet test tests/Ecommerce.UnitTests --verbosity normal --logger "trx;LogFileName=unit-tests.trx"

echo "Running integration tests..."
dotnet test tests/Ecommerce.IntegrationTests --verbosity normal --logger "trx;LogFileName=integration-tests.trx"

echo "Running architecture tests..."
dotnet test tests/Ecommerce.ArchitectureTests --verbosity normal --logger "trx;LogFileName=architecture-tests.trx"

echo "All tests completed successfully!"
