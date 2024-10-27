# PowerShell Pull Images

$images = @(
    "bitnami/rabbitmq:latest",
    "mcr.microsoft.com/mssql/server",
    "datalust/seq",
	"browserless/chrome"
)

foreach ($image in $images) {
    $result = docker images -q $image
    if (-not $result) {
        Write-Host "Image $image does not exist locally. Pulling the image..."
        docker pull $image
    } else {
        Write-Host "Image $image already exists locally. Skipping pull."
    }
}

docker compose -f docker-compose.yml -f docker-compose.override.yml -p browsely up -d