FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY Shared/Shared.csproj Shared/Shared.csproj
COPY RecommendationSystem/RecommendationSystem.csproj RecommendationSystem/RecommendationSystem.csproj

RUN dotnet restore RecommendationSystem/RecommendationSystem.csproj

COPY ./RecommendationSystem/ RecommendationSystem/
COPY ./Shared/ Shared/

RUN dotnet publish RecommendationSystem/RecommendationSystem.csproj -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_HTTP_PORTS=4000
ENTRYPOINT ["dotnet", "RecommendationSystem.dll"]