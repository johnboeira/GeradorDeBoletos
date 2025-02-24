# GeradorDeBoletos

Configuração do banco Postgre

- Necessário instalar o Postgre e o PgAdmin, vídeo sugestão: https://www.youtube.com/watch?v=_lE-vINotSQ
- Após isso pegue os valores no PgAdmin para substituir a connectionString em: appsettings.json, 
OBS: Para aplicações que não sejam de estudo/teste como é o caso dessa, recomenda-se usar meios de 
proteger as credenciais como: Variáveis de ambiente, gerenciamento de secrets de provedores clouds, .NET User Secrets etc... 

# Script

Na pasta helper há script para limpar tabelas, gerar boleto vencido (ontem) e do dia de hoje

# Postman e Autenticação

Na pasta helper há também a coleção do postman usados para testes dos endpoints.
É possível importar essa coleção no postman (botão import)
será necessário também importa o environment para usar a variável global do token jwt (botão import)
ao chamar o request "GerarToken" um script js (em scripts) irá pegar o token devolvido e salvar na var global "AuthToken",com isso você estará autenticado para chamar os endpoints de Banco e Boleto.

De forma alternativa, você pode chamar o endpoint "GeradorDeToken" no swagger, pegar o valor e colocar no input do swagger, há mais instruções ao clicar em "Authorize"