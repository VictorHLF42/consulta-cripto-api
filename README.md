# 🚀 API de Consulta de Preço de Criptomoedas

Este projeto é uma **API RESTful** desenvolvida em **.NET 8**, criada para consultar e gerenciar o preço de criptomoedas em tempo real.  
A API utiliza **SQLite** como cache local e integra-se à **CoinMarketCap API** para garantir dados sempre atualizados.  

**Status do Projeto:** ✅ Concluído  

## 🏛 Arquitetura da Solução

A aplicação segue os princípios da **Arquitetura Limpa**, organizada em quatro camadas independentes, garantindo **separação de responsabilidades**, **testabilidade** e **fácil manutenção**:

- **Domain** → Define as entidades de negócio (`CryptoCurrency`) e contratos (`ICryptoRepository`).  
- **Application** → Contém os casos de uso e lógica de negócio, utilizando **FluentResults** para padronização de retornos.  
- **Infra** → Implementa acesso a dados (`DbContext`, repositórios) e integração com a API externa.  
- **Presentation** → Contém os `Controllers` da API e o tratamento das respostas HTTP.  

## ⚙️ Funcionalidades

- **`GET /api/Crypto/{symbol}`** → Retorna o preço da criptomoeda em **USD**.  

### Fluxo de Regras de Negócio
1. A aplicação busca a cotação primeiro no **cache local (SQLite)**.  
2. Caso não encontre, consulta a **CoinMarketCap API**.  
3. Se a resposta externa for válida, o dado é salvo no cache local.  
4. Se a moeda não existir em nenhuma das fontes, retorna **`404 Not Found`**.  

## 🛠 Tecnologias Utilizadas

- **.NET 8 (ASP.NET Core)**  
- **Entity Framework Core + Migrations**  
- **SQLite**  
- **FluentResults**  

## 📦 Instalação e Execução

1. Clone este repositório:
   ```bash
   git clone https://github.com/VictorHLF42/consulta-cripto-api.git
