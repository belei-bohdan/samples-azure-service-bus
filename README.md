# Sample of using Azure Service Bus with .NET

## Overview
This sample code demonstrates how to send and receive messages using Azure Service Bus. 
It consists of two projects: QueueReceiver and QueueSenderAPI. 
QueueReceiver is a Console Application that listens for messages, while QueueSenderAPI is an ASP.NET Core Web API that sends messages to a queue.

## Disclaimer
This code is for demonstration purposes and should not be considered production-ready. 
It does not adhere to best practices that you would typically use in a production application.
It's intended to help you understand the basics of working with Azure Service Bus.

## Getting Started

### Prerequisites
- .NET SDK (7.0 or later)
- Azure Service Bus namespace and connection string

### Setup

1. **Clone the Repository:** Clone this repository to your local machine.

2. **Set Up Azure Service Bus:**
   - If you don't already have an Azure subscription, you can sign up for one on the [Azure website](https://azure.com).
   - Once you have an Azure subscription, follow the [Azure portal's guide](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-create-namespace-portal) to create an Azure Service Bus namespace and obtain the necessary connection information.
   
3. **Configure QueueReceiver:**
    - Open your terminal for the `QueueReceiver` project.
    - Use the following commands to configure user secrets:

      ```shell
      dotnet user-secrets set "QueueOptions:Connection" "<your_azure_service_bus_connection>"
      dotnet user-secrets set "QueueOptions:QueueName" "<your_queue_name>"
      ```

4. **Configure QueueSenderAPI:**
    - Open the terminal for the `QueueSenderAPI` project.
    - Use the following commands to configure user secrets:
      
      ```shell
      dotnet user-secrets set "QueueOptions:Connection" "<your_azure_service_bus_connection>"
      dotnet user-secrets set "QueueOptions:QueueName" "<your_queue_name>"
      ```

5. **Run the API:**
    - In the `QueueSenderAPI` project, run the API by pressing F5 or using `dotnet run`.

## Sending Messages

1. **Use an API Client:**
    - Use an API client (e.g., Postman) to send POST requests to the `/messages` endpoint of the running API (`http://localhost:7231/messages`).

2. **Send JSON Data:**
    - Send JSON data with `author` and `message` fields to create messages.

## Receiving Messages

1. **Start QueueReceiver:**
    - Start the `QueueReceiver` console application.

2. **Listening for Messages:**
    - The `QueueReceiver` will listen for incoming messages from the configured Azure Service Bus queue.

3. **Display Messages:**
    - Messages will be displayed in the console as they are received.

## Stopping the Applications
- To stop the `QueueReceiver`, press any key in its console window.
- To stop the `QueueSenderAPI`, press Ctrl+C in its console window.