FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 7154
EXPOSE 5111

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["backend/src/JobSity.Chatroom.API/", "src/JobSity.Chatroom.API/"]
COPY ["backend/src/JobSity.Chatroom.Application/", "src/JobSity.Chatroom.Application/"]
# COPY ["**/*.csproj", "./"]
RUN dotnet restore "src/JobSity.Chatroom.API/JobSity.Chatroom.API.csproj"

# Copy the rest of the source code and build the project
COPY . .
WORKDIR "/src/src/JobSity.Chatroom.API"
RUN dotnet build "JobSity.Chatroom.API.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/src/JobSity.Chatroom.API"
RUN dotnet publish "JobSity.Chatroom.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobSity.Chatroom.API.dll"]
