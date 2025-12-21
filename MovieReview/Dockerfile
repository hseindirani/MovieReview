# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# copy the rest of the source code
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "MovieReview.dll"]
