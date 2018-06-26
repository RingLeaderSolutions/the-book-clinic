# The Book Clinic
This is a sample architecture for a distributed workflow built around a service bus (RabbitMQ) and based on microservices. It simulates a simple workflow for a trade which comprises four steps:
1. A new trade is sent to the system.
2. The trade is enriched with data from an external CRM system.
3. An external price engine provides a quote for the trade.
4. The trade is finally completed.

There are three services in charge of executing each step in the workflow:
* TradeService: self-hosted WebAPI. When a new request is received, it creates a new trade in the system with the provided Id.
* CrmService: listens to messages on the service bus and sends a message back to follow up the process.
* PricingService: follows the same mechanism as the CrmService.

## Project structure
The solution contains the following projects:
* TradeManager: runs the three services, hosting the WebAPI and connecting the other two services to RabbitMQ.
* StateSaga: orchestrates the workflow by receving messages from each service, fowarding them to the one according to the workflow configuration and keeping state of each trade.
* Persistence: data access layer in EntityFramework for keeping workflow state.
* Messaging: interface definitions for events and commands for the message queue.
* Common: shared types and libraries.

## Repository structure
This sample architecture also serves as a pattern for breaking up a monolith application into microservices. To illustrate the process, we've created the following branches:

* master: initial version of The Book Clinic.
* first-service: shows the pricing service extracted from the TradeManager project.
* first-service-container: pricing service configured to run in a docker container.

## Runtime setup
The Book Clinic has got three dependencies: RabbitMQ, SQL Server, and SEQ logging (optional). These can be either be installed locally, or from Docker containers (locally or in the cloud). Make sure these are set up before running the solution.