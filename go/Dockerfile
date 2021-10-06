FROM golang:latest as build
ENV GOOS=windows
ENV GOARCH=amd64
ENV GO111MODULE=off
WORKDIR /app
COPY . ./
RUN go build -o delayed-shutdown.exe

FROM mcr.microsoft.com/windows/servercore:1809
WORKDIR /app
COPY --from=build /app/delayed-shutdown.exe .
ENTRYPOINT ["delayed-shutdown.exe"]