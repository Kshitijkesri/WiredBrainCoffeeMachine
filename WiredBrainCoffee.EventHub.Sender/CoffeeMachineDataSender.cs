using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.EventHub.Sender.Model;

namespace WiredBrainCoffee.EventHub.Sender
{
    public interface ICoffeeMachineDataSender
    {
        Task SendDataAsync(CoffeeMachineData data);
        Task SendDataAsync(IEnumerable<CoffeeMachineData> datas);
    }
    public class CoffeeMachineDataSender:ICoffeeMachineDataSender
    {
        private EventHubClient _eventHubClient;

        public CoffeeMachineDataSender( string eventHubConnString)
        {
             _eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnString); 
        }

          public async Task SendDataAsync(CoffeeMachineData data)
        {
            EventData eventData = CreateEventData(data);

            await _eventHubClient.SendAsync(eventData);
        }

     

        public async Task SendDataAsync(IEnumerable<CoffeeMachineData> datas)
        {
            var eventdataas = datas.Select(CoffeeMachineData => CreateEventData(CoffeeMachineData));
          await  _eventHubClient.SendAsync(eventdataas);
        }
        private static EventData CreateEventData(CoffeeMachineData data)
        {
            var dataAsJson = JsonConvert.SerializeObject(data);
            var eventData = new EventData(Encoding.UTF8.GetBytes(dataAsJson));
            return eventData;
        }
    }
}
