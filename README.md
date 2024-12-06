# Livrasso - API

**Livrasso - API** é uma aplicação RESTful desenvolvida com .NET Core para fornecer funcionalidades relacionadas à gestão de livros, autores, editoras, categorias, resenhas e avaliações de livros. A API segue a estrutura da aplicação do repositório https://github.com/rodriguesiad/Livrasso, com a diferença de que a autenticação e o login são opcionais e o banco de dados é implementado com o padrão Singleton, armazenando os dados na memória.

## Funcionalidades Principais

- **Cadastro de Livros**: Permite o gerenciamento completo dos livros, incluindo criação, leitura, atualização e exclusão.
  
- **Cadastro de Autores**: Gerencia informações sobre autores, permitindo CRUD (Criar, Ler, Atualizar e Deletar) para autores de livros.

- **Cadastro de Editoras**: Permite o gerenciamento de editoras associadas aos livros.

- **Cadastro de Categorias**: As categorias podem ser atribuídas aos livros, criando um relacionamento muitos-para-muitos.

- **Resenhas de Livros**: Usuários podem cadastrar resenhas para livros, incluindo conteúdo textual e uma nota.

- **Avaliações de Livros**: Avalie livros com estrelas, criando um relacionamento com o livro e o usuário.

- **Cadastro de Usuários**: Cadastre um usuário para ser associado a uma resenha ou uma avaliação.

### API RESTful

A API está completamente documentada no Swagger, garantindo que qualquer usuário consiga consumir e entender os endpoints, mesmo sem o contexto completo da aplicação.

## Estrutura dos Endpoints

A API foi estruturada para fornecer acesso aos seguintes recursos:

- **Livros**
  - `GET /api/livros` - Lista todos os livros cadastrados.
  - `GET /api/livros/{id}` - Retorna os detalhes de um livro específico.
  - `POST /api/livros` - Cria um novo livro.
  - `PUT /api/livros/{id}` - Atualiza os dados de um livro existente.
  - `DELETE /api/livros/{id}` - Deleta um livro.

- **Autores**
  - `GET /api/autores` - Lista todos os autores cadastrados.
  - `GET /api/autores/{id}` - Retorna os detalhes de um autor específico.
  - `POST /api/autores` - Cria um novo autor.
  - `PUT /api/autores/{id}` - Atualiza os dados de um autor existente.
  - `DELETE /api/autores/{id}` - Deleta um autor.

- **Editoras**
  - `GET /api/editoras` - Lista todas as editoras cadastradas.
  - `GET /api/editoras/{id}` - Retorna os detalhes de uma editora específica.
  - `POST /api/editoras` - Cria uma nova editora.
  - `PUT /api/editoras/{id}` - Atualiza os dados de uma editora existente.
  - `DELETE /api/editoras/{id}` - Deleta uma editora.

- **Categorias**
  - `GET /api/categorias` - Lista todas as categorias cadastradas.
  - `GET /api/categorias/{id}` - Retorna os detalhes de uma categoria específica.
  - `POST /api/categorias` - Cria uma nova categoria.
  - `PUT /api/categorias/{id}` - Atualiza os dados de uma categoria existente.
  - `DELETE /api/categorias/{id}` - Deleta uma categoria.

- **Resenhas**
  - `GET /api/resenhas` - Lista todas as resenhas cadastradas.
  - `GET /api/resenhas/{id}` - Retorna os detalhes de uma resenha específica.
  - `POST /api/resenhas` - Cria uma nova resenha para um livro.
  - `PUT /api/resenhas/{id}` - Atualiza os dados de uma resenha existente.
  - `DELETE /api/resenhas/{id}` - Deleta uma resenha.

- **Avaliações de Estrelas**
  - `GET /api/avaliacoesestrelas` - Lista todas as avaliações de estrelas.
  - `GET /api/avaliacoesestrelas/{id}` - Retorna os detalhes de uma avaliação de estrela específica.
  - `GET /api/avaliacoesestrelas/livro/{livroId}` - Retorna todas as avaliação de um livro.
  - `POST /api/avaliacoesestrelas` - Cria uma nova avaliação de estrelas.
 
 - **Usuários**
  - `GET /api/usuarios` - Lista todos os usuários cadastrados.
  - `GET /api/usuarios/{id}` - Retorna os detalhes de um usuário específico.
  - `POST /api/usuarios` - Cria um novo usuário.
  - `PUT /api/usuarios/{id}` - Atualiza os dados de um usuário existente.
  - `DELETE /api/usuarios/{id}` - Deleta um usuário.


## Tecnologias Utilizadas

- **.NET Core 5.0** - Framework utilizado para desenvolver a API.
- **Swagger** - Para documentar os endpoints da API, garantindo que qualquer desenvolvedor consiga consumir a API facilmente.
- **Singleton** - Banco de dados em memória utilizando o padrão Singleton para gerenciar os dados na aplicação.

## Como Rodar a Aplicação

### Pré-requisitos

- **.NET Core SDK 5.0 ou superior**
- **Visual Studio ou VS Code**
  
### Passos

1. Clone o repositórorio na sua IDE
2. Execute o projeto
