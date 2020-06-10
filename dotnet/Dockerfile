# Build in dotnet core sdk contianer image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o out

# Copy app to Windows Server 2019 dotnet runtime image to run in K8s
FROM mcr.microsoft.com/dotnet/core/runtime:3.1-nanoserver-1809
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["delayed-shutdown.exe"]