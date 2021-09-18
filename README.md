# CountableBusinessLogicService

A microservice using restful HTTP to integrate against the [countable resource service](https://github.com/JesperMoellerJakobsen/CountableResourceService)).
Solves concurrency issues by using a threadsafe transactional approach with optimistic locking.

Hosted in a docker container on &lt;HOSTNAME&gt;:5000/graphql

## Instructions for interacting with microservice

```
GET <HOSTNAME>:5000/playground
```
