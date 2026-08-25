#!/bin/bash
set -e

echo "Generating code coverage report..."

dotnet test tests/Ecommerce.UnitTests --collect:"XPlat Code Coverage" --results-directory ./coverage

echo "Coverage report generated in ./coverage directory"
