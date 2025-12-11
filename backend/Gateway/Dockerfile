FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY Shared/Shared.csproj Shared/Shared.csproj
COPY Gateway/Gateway.csproj Gateway/Gateway.csproj

RUN dotnet restore Gateway/Gateway.csproj

COPY ./Gateway/ Gateway/
COPY ./Shared/ Shared/

RUN dotnet publish Gateway/Gateway.csproj -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_HTTP_PORTS=3000
ENTRYPOINT ["dotnet", "Gateway.dll"]