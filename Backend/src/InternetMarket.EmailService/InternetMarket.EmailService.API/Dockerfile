FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Backend/src/InternetMarket.EmailService/InternetMarket.EmailService.API/InternetMarket.EmailService.API.csproj"
RUN dotnet publish "Backend/src/InternetMarket.EmailService/InternetMarket.EmailService.API/InternetMarket.EmailService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5192
EXPOSE 5192
ENTRYPOINT ["dotnet", "InternetMarket.EmailService.API.dll"]
