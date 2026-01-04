# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# FIX: Added ".Api" to folder and project name to match your solution
COPY ["HealthcareAppointment.Api/HealthcareAppointment.Api.csproj", "HealthcareAppointment.Api/"]
RUN dotnet restore "./HealthcareAppointment.Api/HealthcareAppointment.Api.csproj"
COPY . .
# FIX: Navigate to the correct folder
WORKDIR "/src/HealthcareAppointment.Api"
# FIX: Build the correct project
RUN dotnet build "./HealthcareAppointment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
# FIX: Publish the correct project
RUN dotnet publish "./HealthcareAppointment.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# FIX: Entrypoint dll likely includes .Api if the project does. 
# Check your bin/Debug/net9.0 folder. If the file is HealthcareAppointment.Api.dll, use that.
# If it is just HealthcareAppointment.dll, keep it as is.
# Safest bet based on project name is .Api.dll:
ENTRYPOINT ["dotnet", "HealthcareAppointment.Api.dll"]