Write-Host "Removing migrations..." -ForegroundColor Green
Remove-Item -LiteralPath "Migrations" -Force -Recurse

Write-Host "Create initial migration..." -ForegroundColor Green
dotnet ef migrations add InitialCreate

Write-Host "Dropping database..." -ForegroundColor Green
dotnet ef database drop --force

Write-Host "Updating database..." -ForegroundColor Green
dotnet ef database update