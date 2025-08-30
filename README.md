# 🚀 API de Consulta de Preço de Criptomoedas

Este projeto é uma **API RESTful** desenvolvida em **.NET 8**, projetada para consultar e gerenciar o preço de criptomoedas em tempo real.  
A API utiliza **SQLite** para cache local e integra-se com a **API pública da CoinMarketCap** para garantir dados sempre atualizados.  

**Status do Projeto:** 🚧 Em desenvolvimento  

---

## 🏛 Arquitetura da Solução

A aplicação foi construída seguindo os princípios da **Arquitetura Limpa**, organizada em quatro camadas independentes para garantir **separação de responsabilidades**, **testabilidade** e **fácil manutenção**:

- **Presentation** → Contém os `Controllers` da API e o tratamento das respostas HTTP.  
- **Application** → Reúne a lógica de negócio, casos de uso e utiliza **FluentResults** para padronizar os retornos.  
- **Domain** → Define as entidades de negócio (`CryptoCurrency`) e contratos como `ICryptoRepository`.  
- **Infra** → Gerencia o acesso a dados, incluindo `DbContext` e implementações de repositórios.  

---

## ⚙️ Funcionalidades (Parciais)

### Endpoint planejado:
- **`GET /api/Crypto/{symbol}`** → Retorna o preço da criptomoeda em **USD**.  

### Regras de Negócio:
1. A aplicação busca primeiro a cotação em cache no **banco de dados local (SQLite)**.  
2. Caso não encontre, consulta a **CoinMarketCap API**.  
3. Se a consulta externa for bem-sucedida, os dados são **armazenados no cache local**.  
4. Se a moeda não existir em nenhuma das fontes, retorna um erro **`404 Not Found`**.  

---

## 🛠 Tecnologias Utilizadas

- **.NET 8 (ASP.NET Core)**  
- **Entity Framework Core + Migrations**  
- **SQLite** para persistência local  
- **FluentResults** para padronização de retornos  

---

## 📥 Instalação e Configuração (em andamento)

1. Clone este repositório:
   ```bash
   git clone https://github.com/VictorHLF42/crypto-api.git
