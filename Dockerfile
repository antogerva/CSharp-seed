FROM microsoft/dotnet:2-sdk as dotnet-build
WORKDIR /dotnet/LHGames

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out -r linux-x64

FROM microsoft/dotnet:2-runtime-jessie
WORKDIR /Volumes/LHGames
COPY --from=dotnet-build /dotnet/LHGames/LHGames/out .
ENTRYPOINT ["dotnet", "LHGames.dll"]
EXPOSE 8080
