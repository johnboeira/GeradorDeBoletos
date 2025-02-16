# GeradorDeBoletos

Configuração do banco Postgre

- Necessário instalar o Postgre e o PgAdmin, vídeo sugestão: https://www.youtube.com/watch?v=_lE-vINotSQ
- Após isso pegue os valores no PgAdmin para substituir a connectionString em: appsettings.json, 
OBS: Para aplicações que não sejam de estudo/teste como é o caso dessa, recomenda-se usar meios de 
proteger as credenciais como: Variáveis de ambiente, gerenciamento de secrets de provedores clouds, .NET User Secrets etc... 