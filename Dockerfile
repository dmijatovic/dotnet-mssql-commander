FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

# copy csproj and restore as distinct layer
# COPY ./commander.csproj ./commander.csproj
# install dependencies
# RUN dotnet restore ./commander.csproj

# copy everything else and build
COPY . .
# build for production
RUN dotnet publish -c Release -o out
# RUN dotnet publish ./commander.csproj \
#   --runtime linux-musl-x64 \
#   -c Release \
#   -o out /p:Version=1.0.0
  #\ -p:PublishTrimmed=true

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

COPY --from=build ./app/out .

EXPOSE 5000

ENTRYPOINT ["./commander" ]