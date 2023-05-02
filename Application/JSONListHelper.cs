using Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class JSONListHelper
    {
        //public static string GetEventListJSONString(List<Event> events)
        //{
        //    var eventList = new List<Event>();
        //    foreach (var model in events)
        //    {
        //        var myEvent = new Event()
        //        {
        //            EventId = model.EventId,
        //            Start = model.Start,
        //            End = model.End,
        //            Description = model.Description
        //        };
        //        eventList.Add(myEvent);
        //    }
        //    return System.Text.Json.JsonSerializer.Serialize(eventList);
        //}

        //public static string GetCardEventListJSONString(List<Card> cardEvents)
        //{
        //    var cardEventList = new List<Event>();
        //    foreach (var cardEvent in cardEvents)
        //    {
        //        var card = new Event()
        //        {
        //            EventId = cardEvent.CardId,
        //       //     Start = cardEvent.DueDate,
        //      //      End = cardEvent.DueDate,
        //            Description = cardEvent.Description
        //        };
        //        cardEventList.Add(card);
        //    }
        //    return System.Text.Json.JsonSerializer.Serialize(cardEventList);
        //}
    }
}
