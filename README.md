# Desafio

## Instruções

O projeto consiste em uma API desenvolvida em .Net 10 e um front-end desenvolvido com React e MUI (Material-UI);

**Back-end**

O banco de dados é SQL Server, as configurações para acesso estão presentes na API, na pasta “bookcatalog" no arquivo “appsettings.json”

_"DefaultConnection": "Server=...; Database=...; User Id=...; password=...; Trusted_Connection=false;TrustServerCertificate=True;"_

Após substituir os dados de acesso é preciso rodar as migrações (Migrations) para criar as tabelas e também a view (vw_RelatorioAutorAssuntoLivro) usando o seguinte comando dentro da pasta "bookcatalog".

_"dotnet ef database update"_

Depois é só rodar o comando _"dotnet run"_ que a API iniciará no seguinte endereço _http://localhost:5237_


**Front-end**

Agora em outro terminal vamos iniciar o front-end entrando na pasta _front_, mas antes é preciso baixar as dependências usando o comando _"npm install"_ 

Após o processo vamos iniciar a interface usando outro comando _”npm run dev”_, assim como a API o front-end roda em um endereço _http://localhost:5005_

<br>

Pronto, se tudo estiver configurado corretamente a aplicação estará disponível para teste.