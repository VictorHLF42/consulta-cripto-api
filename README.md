# API de Consulta de Preço de Criptomoedas

**Status: Em Desenvolvimento**

[cite_start]Este projeto é uma API RESTful desenvolvida em .NET 8 que simula parte de uma aplicação do domínio de criptoativos[cite: 7, 8]. [cite_start]O objetivo é construir uma API que se integra com a API pública da CoinMarketCap para consultar e armazenar preços de criptomoedas[cite: 8].

---

### Requisitos

Para rodar este projeto, você precisará de:
* .NET 8 SDK
* [cite_start]Uma chave de API da CoinMarketCap [cite: 43]

---

### Estrutura da Arquitetura

[cite_start]A aplicação foi estruturada em camadas para garantir a separação de responsabilidades e a manutenibilidade, conforme exigido no desafio[cite: 4, 21]:

* [cite_start]**Presentation**: Responsável pelos controllers da API e pelo tratamento das respostas HTTP[cite: 24].
* [cite_start]**Application**: Contém os casos de uso (use cases) da aplicação, utilizando o FluentResults para padronizar os retornos[cite: 25, 26].
* [cite_start]**Domain**: Camada para as entidades de negócio (ex: `CryptoCurrency`) e as regras de domínio[cite: 27].
* [cite_start]**Infra**: Inclui os repositórios para acesso a dados, o contexto do Entity Framework e as migrations[cite: 28].

---

### Requisitos Funcionais

[cite_start]A API expõe um endpoint para consulta de preços[cite: 10, 11, 12]:

* [cite_start]**GET `/crypto/{symbol}`**: Retorna o preço da criptomoeda consultada em USD[cite: 12].

**Regras de Negócio:**
1.  [cite_start]Ao receber uma requisição, a aplicação deve primeiro verificar na base de dados SQLite se a cotação já existe[cite: 14].
2.  [cite_start]Se a moeda for encontrada, o valor local é retornado[cite: 15].
3.  [cite_start]Se a moeda não for encontrada, a aplicação chama a API da CoinMarketCap[cite: 16, 17]. [cite_start]Se a cotação for obtida, ela é salva no banco de dados local antes de ser retornada ao usuário[cite: 18].
4.  [cite_start]Caso a moeda não seja encontrada na CoinMarketCap, um erro apropriado é retornado[cite: 19].

---

### Tecnologias Utilizadas

* **ASP.NET Core** em .NET 8
* [cite_start]**Entity Framework Core** com Migrations [cite: 5]
* [cite_start]**SQLite** para persistência local dos dados [cite: 30, 31]
* [cite_start]**FluentResults** para padronização dos retornos [cite: 26]

---

### Instalação e Configuração

1.  Clone este repositório.
2.  Na pasta raiz do projeto, configure sua chave de API da CoinMarketCap no arquivo `appsettings.json` ou como uma variável de ambiente. [cite_start]A chave deve ser adicionada a um cabeçalho de requisição `X-CMC_PRO_API_KEY`[cite: 46].
3.  Execute as migrations para criar o banco de dados localmente:
    ```bash
    dotnet ef database update
    ```
4.  Execute a API:
    ```bash
    dotnet run --project [NomeDoProjetoDaAPI]
    ```

---

### Autor

* Victor H.  - [@VictorHLF42](https://github.com/VictorHLF42)
