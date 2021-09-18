# CountableBusinessLogicService

A microservice calling an HTTP integration towards the [countable resource service](https://github.com/JesperMoellerJakobsen/CountableResourceService).  
Solves concurrency issues by using a threadsafe transactional approach with optimistic locking.

Hosted in a docker container on &lt;HOSTNAME&gt;:5000/graphql

## Instructions for interacting with microservice using a browser

GraphQL-playground hosted at:
```
GET <HOSTNAME>:5000/ui/playground
```
