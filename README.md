## EspCidBackEnd - Projeto de BackEnd do Espaço Cidadão

---

### Arquitetura ONION + Entity Framework (net8.0 LTS)

#### UniBlog.Domain --> Camada de Domínio/Core do produto
#### UniBlog.Infrastructure --> Camada de Infraetrutura/Banco de dados/Repositorios
#### UniBlog.Application --> Camada de Aplicação/Serviços/DTO
#### UniBlog.WebApi --> Camada de Apresentação/Api/Endpoints

---

## Instalar .NET SDK e Runtime (Para desenvolver e rodar projeto, caso não tenha)

```bash
# SDK
winget install Microsoft.DotNet.SDK.8

# Runtime
winget install Microsoft.DotNet.AspNetCore.8
```

---

## Instalar Docker
```bash
winget install -e --id Docker.DockerDesktop
```

---

## Iniciar container do banco

- Ir para a pasta do projeto EspCid.Infrasctructure e rodar

```bash
docker-compose up -d
```

---

## Instalar EntityFramework CommandLine
```bash
dotnet tool install --global dotnet-ef
```

---

## Subir banco ou dropar (EntityFramework)

- Ir para a pasta do projeto EspCid.Infrasctructure e rodar

```bash
# Subir (-s para pegar o projeto de Apresentação/Endpoints)
dotnet-ef database update -s ../EspCid.WebApi

# Dropar (-s para pegar o projeto de Apresentação/Endpoints)
dotnet-ef database drop -s ../EspCid.WebApi
```

