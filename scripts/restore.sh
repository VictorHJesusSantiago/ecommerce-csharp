#!/bin/bash
set -e

echo "Running database restore..."

if [ -z "$1" ]; then
    echo "Usage: $0 <backup_file>"
    exit 1
fi

BACKUP_FILE=$1

if [ ! -f "$BACKUP_FILE" ]; then
    echo "Backup file not found: $BACKUP_FILE"
    exit 1
fi

echo "Restoring from: $BACKUP_FILE"
docker exec -i postgres psql -U ecommerce_user ecommerce < "$BACKUP_FILE"

echo "Database restored successfully!"
