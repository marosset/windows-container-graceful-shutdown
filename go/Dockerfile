FROM golang:latest as build
ENV GOOS=windows
ENV GOARCH=amd64
WORKDIR /app
COPY . ./
RUN go build -o delayed-shutdown.exe

FROM mcr.microsoft.com/windows/nanoserver:1809
WORKDIR /app
COPY --from=build /app/delayed-shutdown.exe .
ENTRYPOINT ["delayed-shutdown.exe"]