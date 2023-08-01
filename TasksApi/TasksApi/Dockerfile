
# Define a base image. Use the official .NET SDK image with the version 2.1 to build the application.
FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build-env

# Set the working directory inside the container.
WORKDIR /app

# Copy the project file and csproj file to the working directory.
COPY *.csproj ./

# Restore the project dependencies using the dotnet CLI.
RUN dotnet restore

# Copy all the project code to the working directory.
COPY . ./

# Compile the project to produce the executable file.
RUN dotnet publish -c Release -o out

# Define the base image for deployment. Use the official . runtimeNET image with the version 2.1.
FROM mcr.microsoft.com/dotnet/aspnet:2.1

# Set the working directory inside the container.
WORKDIR /app

# Copy the compiled project executable from the previous stage.
COPY --from=build-env /app/out .

# Expose the port defined for HTTP.
EXPOSE 80

# Command to start the application when running the container.
ENTRYPOINT ["dotnet", "TasksApi.dll"]
