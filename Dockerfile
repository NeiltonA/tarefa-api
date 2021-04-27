# Dockerfile

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
# We create the folder inside the container
WORKDIR /local-project

# We are coping all project executables that we created with dotnet build and dotnet publish 
COPY ./bin/Release/net5.0/publish/* ./

EXPOSE 8000
EXPOSE 8081

# We indicate to execute the program in the executable of the project
ENTRYPOINT ["dotnet", "tarefas-api.dll"]