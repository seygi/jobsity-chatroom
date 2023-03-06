# jobsity-chatroom

### Assignment
The goal of this exercise is to create a simple browser-based chat application using .NET.

This application should allow several users to talk in a chatroom and also to get stock quotes
from an API using a specific command.

## Installation and execution
### Requirements

* Must have docker installed
* Must love to see yml and DockerFile :P

### How-to
* Download the repo https://github.com/seygi/jobsity-chatroom
* Open root folder in terminal
* in terminal call type the command `docker-compose up --build`
* wait until all containers stay running
* access the url http://localhost:4200/public/login
* create 1 or more accounts
* create 1 or more rooms
* have fun

## Mandatory Features
- [x] Allow registered users to log in and talk with other users in a chatroom.
- [x] Allow users to post messages as commands into the chatroom with the following format
/stock=stock_code
- [x] Create a decoupled bot that will call an API using the stock_code as a parameter
(https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv, here aapl.us is the
stock_code)
- [x] The bot should parse the received CSV file and then it should send a message back into
the chatroom using a message broker like RabbitMQ. The message will be a stock quote
using the following format: “APPL.US quote is $93.42 per share”. The post owner will be
the bot.
- [x] Have the chat messages ordered by their timestamps and show only the last 50
messages.
- [x] Unit test the functionality you prefer.

## Bonus assignments made
* [x] Several chatrooms
* [x] .NET Identity
* [x] Handle Bot errors
* [x] Build an installer


## Built With

- [.NET](https://dotnet.microsoft.com/en-us/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Postgres](https://www.postgresql.org/)
- [Angular](https://angular.io/)
- [Docker](https://www.docker.com/)
