# WiredBrainCoffeeMachine

# Project Details

1)  A WPF application that simulates a coffee machine and sends events to the Event Hub.
2)  A NET Core application that consumes events from the Event Hub 
3)  One can Create real-time dashboard in Power BI to display life data and much more. 

# What are Event Hubs
An Event Hub can be used to collect telemetry data like this log data.
If you haven't heard the word telemetry before, telemetry consists of the two parts tele and metry. 
Tele stands for remote and metry for metering, or measuring. The word telemetry stands for remote metering, or remote measuring. 
And this is actually what you want to do when you send log data from your application to a remote destination like an Event Hub. 
But now you might think instead of using an Event Hub here, you could also build a web API with ASP. NET Core that takes the log data 
from your application and stores it in a database. So, what's exactly the advantage of using an Event Hub instead of a classic 
ASP. NET Core Web API? An advantage is that you don't have to write any code to set up an Azure Event Hub. 
You just go to the Azure portal and there you can create and configure an Event Hub very easily.
It's a platform as a service. But there are other advantages, and a really big advantage is scaling. 
Azure Event Hubs can be used to collect more than one million events per second. 
That's a massive number of events. The total data volume of these events can be more than one gigabyte per second. 
So, when you think again about your application that logs some user events to the Event Hub, this means that there can be
massive amounts of events logged at the same time, and the Event Hub can handle that huge amount of incoming data. 
But also at small scale, an Azure Event Hub is a great choice, because another advantage is that an event hub is quite cheap.
In the smallest configuration, you get an Event Hub for around $11. 00 per month. On top of that, you have to pay for the incoming events,
and the price there is $0. 028 per million of incoming events. That means you can set up an Event Hub that enters on average more than
100 million events per month for less than $15. 00. Beside classic applications,
Event Hubs are also used quite often to collect telemetry data that comes from devices and gateways. 
For example, GPS records could send position data periodically to an Event Hub, or a weather station could send 
temperatures every 5 seconds to an Event Hub. Quite often there are devices that can't connect to the internet on their own.
Then usually an application grabs sensor data from these devices and pushes that sensor data to an Event Hub. 
Such an application grabbing sensor data from devices and pushing it to an Event Hub is called a gateway. 
So you see, besides sending log data from a classic application, you could use Azure Event Hubs for typical Internet of Things 
scenarios where you send sensor data from devices and gateways to an Event Hub. All these different senders, an application, 
a device, or a gateway, have in common that it's always a piece of code that sends an event to an Event Hub.
And the Event Hub itself is easy to set up and scales very well. Now let's assume that events are sent to your Event Hub. 
Then you can consume and analyze these events. 
