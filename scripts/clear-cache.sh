#!/bin/bash
set -e

echo "Clearing cache..."

redis-cli -a "$REDIS_PASSWORD" FLUSHALL

echo "Cache cleared successfully!"
