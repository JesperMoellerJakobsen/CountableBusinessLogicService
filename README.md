# CountableBusinessLogicService

A microservice calling an HTTP integration towards the [countable resource service](https://github.com/JesperMoellerJakobsen/CountableResourceService).  
Solves concurrency issues by using a threadsafe transactional approach with optimistic locking.

## Cluster setup
![Cluster setup](https://github.com/JesperMoellerJakobsen/CountableSwarm/blob/master/ArchitectureDiagram.png)

## Instructions for interacting with microservice using a browser
Hosted in a docker container on &lt;HOSTNAME&gt;:5000/ui/playground and &lt;HOSTNAME&gt;:5000/graphql

GraphQL-playground hosted at:
```
GET <HOSTNAME>:5000/ui/playground
```

GraphQL-endpoint hosted at:
```
GET <HOSTNAME>:5000/graphql
```