# ���� 1: ���������� ����������� ����� .NET Core Runtime ��� ����������
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ���� 2: ���������� ����������� ����� .NET Core SDK ��� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# �������� ���� csproj � ��������������� �����������
COPY ["OfficeTechRepairSystem/OfficeTechRepairSystem.csproj", "OfficeTechRepairSystem/"]
RUN dotnet restore "./OfficeTechRepairSystem/OfficeTechRepairSystem.csproj"

# �������� ��� ��������� ����� �������
COPY . .

# ������������� ������� ���������� � ����������� ������
WORKDIR "/src/OfficeTechRepairSystem"
RUN dotnet build "./OfficeTechRepairSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ���� 3: ��������� ����������
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OfficeTechRepairSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ���� 4: ��������� �����
FROM base AS final
WORKDIR /app

# �������� �������������� ����� � ��������� �����
COPY --from=publish /app/publish .

# ���������, ��� ����� � ������������� ���������� � ����������
COPY --from=build /src/OfficeTechRepairSystem/wwwroot/static/SpecializationImageStorage /app/wwwroot/static/SpecializationImageStorage

# ��������� ������� ��� ������� ����������
ENTRYPOINT ["dotnet", "OfficeTechRepairSystem.dll"]