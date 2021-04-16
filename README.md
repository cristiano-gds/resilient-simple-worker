# Resilient Simple Worker

A simple example of **worker** that looking for new content wrote in a text file.

This example also apply resilience methods such as Retry and Fallback using Polly library.

## How To Use

### Best Usage

Use VSCode to run and Postman to call Web APIs. The files launch.json and task.json already configured to run in paralell in VSCode. 

### Web APIs

Web APIs can be called by endpoint "http://localhost:5000/api/file". Use these HTTP methods to handle file:

* POST - Create the file if not exists
* DELETE - Remove the file
* PUT - Update the content with a new message


# Worker Simples Resiliente (PT-Br)

Um simples exemplo de **worker** que procura por novo conteúdo escrito em um arquivo de texto.

Este exemplo também aplica métodos de resiliência como Retry e Fallback usando a biblioteca Polly.

## Como Usar

### Melhor uso

Use VSCode para executar o projeto e o Postman para chamar as APIs para gerenciar as ações no arquivo. Os arquivos launch.json e task.json já estão configurados para executar as WebAPIs e worker paralelamente pelo VSCode.

### Web APIs

Web APIs podem ser chamadas através do endpoint "http://localhost:5000/api/file". Use estes métodos HTTP para manipular o arquivo de texto:

* POST - Cria o arquivo se não existir
* DELETE - Remove o arquivo
* PUT - Atualiza o conteúdo do arquivo com uma mensagem enviada por parâmetro
