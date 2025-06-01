@echo off
echo Останавливаем все процессы dotnet...
taskkill /F /IM dotnet.exe 2>nul

echo Переходим в директорию проекта...
cd TelegramBotLearning

echo Удаляем папки bin и obj...
if exist bin rmdir /s /q bin
if exist obj rmdir /s /q obj

echo Восстанавливаем зависимости...
dotnet restore

echo Собираем проект...
dotnet build

echo Проверяем наличие EF Core tools...
dotnet tool list --global | findstr "dotnet-ef" >nul
if errorlevel 1 (
    echo Устанавливаем EF Core tools...
    dotnet tool install --global dotnet-ef
)

echo Применяем миграции...
dotnet ef database update

echo Запускаем проект...
start http://localhost:5102
dotnet run 