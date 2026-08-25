#!/bin/bash
set -e

echo "Running code formatting..."

dotnet format --verbosity diagnostic

echo "Code formatting completed!"
