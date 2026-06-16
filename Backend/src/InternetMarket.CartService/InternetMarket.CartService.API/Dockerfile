FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/InternetMarket.CartService/InternetMarket.CartService.API/InternetMarket.CartService.API.csproj"
RUN dotnet publish "src/InternetMarket.CartService/InternetMarket.CartService.API/InternetMarket.CartService.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5203
EXPOSE 5203
ENTRYPOINT ["dotnet", "InternetMarket.CartService.API.dll"]
