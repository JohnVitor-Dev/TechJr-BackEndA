
# Define a base image. Use the official .NET SDK 7.0 image to build the application.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

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

# Define the base image for deployment. Use the official .NET runtime 7.0 image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory inside the container.
WORKDIR /app

# Copy the compiled project executable from the previous stage.
COPY --from=build-env /app/out .

# Expose the ports defined for HTTP and HTTPS.
EXPOSE 8080

# Command to start the application when running the container.
ENTRYPOINT ["dotnet", "TasksApi.dll"]
