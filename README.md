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