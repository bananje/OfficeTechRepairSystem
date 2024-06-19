# Этап 1: Используем официальный образ .NET Core Runtime для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этап 2: Используем официальный образ .NET Core SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем файл csproj и восстанавливаем зависимости
COPY ["OfficeTechRepairSystem/OfficeTechRepairSystem.csproj", "OfficeTechRepairSystem/"]
RUN dotnet restore "./OfficeTechRepairSystem/OfficeTechRepairSystem.csproj"

# Копируем все остальные файлы проекта
COPY . .

# Устанавливаем рабочую директорию и компилируем проект
WORKDIR "/src/OfficeTechRepairSystem"
RUN dotnet build "./OfficeTechRepairSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап 3: Публикуем приложение
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OfficeTechRepairSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этап 4: Финальный образ
FROM base AS final
WORKDIR /app

# Копируем опубликованные файлы в финальный образ
COPY --from=publish /app/publish .

# Убедитесь, что папка с изображениями существует и копируется
COPY --from=build /src/OfficeTechRepairSystem/wwwroot/static/SpecializationImageStorage /app/wwwroot/static/SpecializationImageStorage

# Указываем команду для запуска приложения
ENTRYPOINT ["dotnet", "OfficeTechRepairSystem.dll"]