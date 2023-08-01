
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layer
COPY TasksApi/*.csproj .
RUN dotnet restore

# copy everything and else build app
COPY TasksApi/. .
RUN dotnet publish -c Release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .

# Expose port 80
EXPOSE 80

# Update the entrypoint to the correct DLL filename and its path
ENTRYPOINT ["dotnet", "TasksApi.dll"]
