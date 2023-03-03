#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/JobSity.Chatroom.API/JobSity.Chatroom.API.csproj", "src/JobSity.Chatroom.API/"]
RUN dotnet restore "src/JobSity.Chatroom.API/JobSity.Chatroom.API.csproj"
COPY . .
WORKDIR "/src/src/JobSity.Chatroom.API"
RUN dotnet build "JobSity.Chatroom.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobSity.Chatroom.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobSity.Chatroom.API.dll"]