# Configuração do banco Postgre

- Necessário instalar o Postgre e o PgAdmin, vídeo sugestão: https://www.youtube.com/watch?v=_lE-vINotSQ
- Após isso pegue os valores no PgAdmin para substituir a connectionString em: appsettings.Development.json, 
OBS: Para aplicações que não sejam de estudo/teste como é o caso dessa, recomenda-se usar meios de 
proteger as credenciais como: Variáveis de ambiente, gerenciamento de secrets de provedores clouds, .NET User Secrets etc... 

# Etapas necessárias

Os endpoints de banco e boleto precisam de autenticação, portanto será necessário criar um usuário para posteriormente gerar o token. O Token tem duração de 1000 minutos.

# Scripts

Na pasta helper há script para limpar tabelas, gerar boleto vencido (ontem) e do dia de hoje

# Postman

Na pasta helper há também a coleção do postman usados para testes dos endpoints.
É possível importar essa coleção no postman (botão import)
será necessário também importar o environment para usar a variável global do token jwt

# Autenticação

ao chamar o request "GerarToken" um script js (em scripts) irá pegar o token devolvido e salvar na var global "AuthToken",com isso você estará autenticado para chamar os endpoints de Banco e Boleto.
De forma alternativa, você pode chamar o endpoint "GeradorDeToken" no swagger, pegar o valor e colocar no input do swagger, há mais instruções ao clicar em "Authorize"

OBS: A chave de assinatura está em appsettings.Development.json, podendo ser alterada. Em ambiente de produção essa chave deve ficar ocultar e ser trocada de tempos em tempos.

# Stack/Bibliotecas usadas
C# .net 6
EF Core 
Postgre 
PgAdmin
Visual Studio 2022
Postman

FluentValidation
JWT Token e Bearer
AutoMapper
Serilog (Logando em console e txt)
XUnit
Moq

Handler de exceptions global (Há uma alternativa como padrão Result)
Alguns conceitos do DDD 
Clean Architecture 

Como alternativa aos services da camada de services poderia ser usado o padrão CQRS (um exemplo simples está em: https://github.com/johnboeira/CQRS_Example.git)