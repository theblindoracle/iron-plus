#/bin/bash

# Find and delete all 'bin' and 'obj' directories recursively from current directory
find . -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +

echo "Cleaned all 'bin' and 'obj' directories."
