FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Expose ports
EXPOSE 5001/tcp
ENV ASPNETCORE_URLS http://*:5001

# Start
ENTRYPOINT ["dotnet", "out/BeerDotApi.dll"]
