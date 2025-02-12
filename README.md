# Trabalho de SD

## OrderSystem.Order.API
API de pedidos feita com C# + .NET, banco de dados SQLite, Entity Framework como ORM e autenticação por token JWT.

### Rotas:

AUTH:
POST Auth/login -> Autenticar usuário

ORDER:
POST Order -> Cadastrar novo pedido
GET Order/userId -> Obter listagem de pedidos associados a algum usuário

USER:
POST User -> Cadastrar novo usuário
GET User/userId -> Obter informações do usuário
PUT User -> Atualizar informações do usuário
DELETE User -> Excluir conta do usuário

Passo a passo para acessar rotas seguras:
1. Cadastrar usuário com e-mail e senha
2. Autenticar na rota login
3. Passar o Token no cabeçalho das requisições subsequentes seguindo o formato "Bearer <TOKEN>"(no Swagger tem o botão "Authorize" para isso)

## OrderSystem.Payment.API
API de pagamentos feita com Python, recebe um Id de pedido via RPC e chama método para atualizar no banco o status do pedido para "Pagamento Realizado" após alguns segundos.

Basta rodar "python main.py" que ao criar uma nova Order na API de pedidos ela irá fazer a chamada RPC e atualizar o status automaticamente.