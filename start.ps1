# Останавливаем все процессы dotnet
Write-Host "Останавливаем все процессы dotnet..."
taskkill /F /IM dotnet.exe 2>$null

# Переходим в директорию проекта
Set-Location -Path "TelegramBotLearning"

# Удаляем папки bin и obj
Write-Host "Удаляем папки bin и obj..."
Remove-Item -Path "bin" -Recurse -Force -ErrorAction SilentlyContinue
Remove-Item -Path "obj" -Recurse -Force -ErrorAction SilentlyContinue

# Восстанавливаем зависимости
Write-Host "Восстанавливаем зависимости..."
dotnet restore

# Собираем проект
Write-Host "Собираем проект..."
dotnet build

# Применяем миграции
Write-Host "Применяем миграции..."
dotnet ef database update

# Запускаем проект
Write-Host "Запускаем проект..."
dotnet run 