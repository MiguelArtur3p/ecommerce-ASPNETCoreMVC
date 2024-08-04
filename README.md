Ecommerce em ASP.NET Core MVC
Este repositório contém o código-fonte de uma loja virtual desenvolvida com C# e ASP.NET Core MVC. O projeto utiliza uma série de tecnologias e práticas para fornecer uma plataforma robusta e segura para comércio eletrônico.

## Funcionalidades
Autenticação e Segurança:

Cookies e Sessões: Gerenciamento de sessões de usuário e armazenamento seguro de dados de sessão com cookies.
Segurança: Implementação de medidas de segurança, incluindo proteção contra CSRF (Cross-Site Request Forgery), uso de tokens de segurança, e criptografia de dados sensíveis.
Filtros e Middleware:

Filtros: Aplicação de filtros para validação e manipulação de dados de entrada e saída.
Middleware: Configuração de middleware para gerenciar o pipeline de solicitação e resposta do aplicativo.
Validações Customizadas:

Implementação de validações customizadas para garantir a integridade e a conformidade dos dados de entrada.
Áreas:

Organização do projeto em áreas distintas para modularização e melhor gerenciamento do código.

## EF Core (Entity Framework Core)

CRUD: Operações básicas de CRUD (Criar, Consultar, Atualizar e Deletar) para gerenciar os dados da loja.
Migrations: Criação automática de scripts para alteração e atualização da estrutura do banco de dados com base nas alterações no modelo de dados.

## Razor (Motor de Template do ASP.NET Core)

Tag Helper: Utilização de tag helpers para criar componentes HTML personalizados e simplificar a sintaxe no Razor.
HTML Helper: Uso de HTML helpers para gerar elementos HTML dinâmicos de forma mais eficiente.
ViewComponent: Implementação de view components para criar componentes reutilizáveis e encapsulados.
Layout e Importações: Definição de layouts e importações para uma estrutura de página consistente e reutilização de código.

## Padrões de Projeto

MVC (Model-View-Controller): Estrutura padrão para separar a lógica de aplicação, apresentação e controle.
Repository Pattern: Implementação do padrão Repository para abstrair o acesso aos dados e promover uma melhor organização.
Unit of Work: Uso do padrão Unit of Work para gerenciar transações e operações no banco de dados.
Injeção de Dependência: Aplicação de injeção de dependência para promover a modularidade e facilitar o teste e manutenção do código.

## Como Rodar o Projeto
Clone o repositório:
Copiar código
git clone https://github.com/MiguelArtur3p/ecommerce-ASPNETCoreMVC.git
Navegue até o diretório do projeto:

Copiar código
cd nome-do-repositorio
Restaure as dependências e execute o projeto
Acesse a aplicação no navegador em http://localhost:5000
