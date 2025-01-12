set -eou pipefail

rm -r ./build || true

mkdir ./build
mkdir ./build/out

cp ./utilities/FileMode.bat ./build/out

dotnet build -o ./build/out

cp -r ./mix-it-up-commands ./build/mix-it-up-commands

cp -r ./README.md ./build