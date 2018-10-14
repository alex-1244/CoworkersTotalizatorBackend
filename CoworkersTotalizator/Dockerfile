FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 44363
EXPOSE 44364

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CoworkersTotalizator/CoworkersTotalizator.csproj", "CoworkersTotalizator/"]
COPY ["CoworkersTotalizator.Services/CoworkersTotalizator.Services.csproj", "CoworkersTotalizator.Services/"]
COPY ["CoworkersTotalizator.Dal/CoworkersTotalizator.Dal.csproj", "CoworkersTotalizator.Dal/"]
COPY ["CoworkersTotalizator.Models/CoworkersTotalizator.Models.csproj", "CoworkersTotalizator.Models/"]
RUN dotnet restore "CoworkersTotalizator/CoworkersTotalizator.csproj"
COPY . .
WORKDIR "/src/CoworkersTotalizator"
RUN dotnet build "CoworkersTotalizator.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoworkersTotalizator.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoworkersTotalizator.dll"]