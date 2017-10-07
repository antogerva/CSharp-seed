FROM microsoft/dotnet:2-sdk
WORKDIR /dotnet/LHGames

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out -r linux-x64

ENTRYPOINT ["dotnet", "LHGames/out/LHGames.dll"]
EXPOSE 8080
