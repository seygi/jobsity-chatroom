docker-compose up -d
@echo off
cd backend/src/JobSity.Chatroom.Consumer.BotStockSearch
dotnet build
echo "Waiting for 10 seconds..."
timeout /t 10
echo "Done!"
dotnet run
pause