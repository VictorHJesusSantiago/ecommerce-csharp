#!/bin/bash
set -e

echo "Running database backup..."

BACKUP_DIR="./backups"
TIMESTAMP=$(date +%Y%m%d_%H%M%S)
BACKUP_FILE="$BACKUP_DIR/ecommerce_backup_$TIMESTAMP.sql"

mkdir -p "$BACKUP_DIR"

echo "Creating backup: $BACKUP_FILE"
docker exec postgres pg_dump -U ecommerce_user ecommerce > "$BACKUP_FILE"

echo "Backup completed: $BACKUP_FILE"
