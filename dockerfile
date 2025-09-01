FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Diary.Api/Diary.Api.csproj", "Diary.Api/"]
COPY ["Diary.Services/Diary.Services.csproj", "Diary.Services/"]
COPY ["Diary.DataContext/Diary.DataContext.csproj", "Diary.DataContext/"]
COPY ["Diary.EntityModels/Diary.EntityModels.csproj", "Diary.EntityModels/"]
RUN dotnet restore "Diary.Api/Diary.Api.csproj"
COPY . .
WORKDIR "/src/Diary.Api"
RUN dotnet build "Diary.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Diary.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Diary.Api.dll"]