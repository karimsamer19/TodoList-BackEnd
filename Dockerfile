#? first-stage base image
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

#? Copy csproj and restore dependencies
COPY *.csproj .
RUN dotnet restore


#? Copy and publish application files
COPY . .
RUN dotnet publish -c release -o /app

#? Final stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT [ "dotnet", "TodoList.dll" ]