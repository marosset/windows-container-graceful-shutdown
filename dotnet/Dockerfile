# Build in dotnet core sdk contianer image
FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o out

# Copy app to Windows Server 2019 dotnet runtime image to run in K8s
FROM mcr.microsoft.com/dotnet/runtime:5.0-nanoserver-1809
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["delayed-shutdown.exe"]