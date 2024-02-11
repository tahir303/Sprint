﻿using Car_Management.Models;
using Newtonsoft.Json;
using System.Text;
using Azure;
using Azure.Storage.Blobs;
using SQLitePCL;
using Azure.Messaging.EventGrid;

namespace Car_Management
{
    public class HelperCar
    {
        public static async Task<bool> UploadBlob(IConfiguration config, Car car)
        {
            string blobConnString = config.GetConnectionString("StorAccConnString");
            BlobServiceClient client = new BlobServiceClient(blobConnString);
            string container = config.GetValue<string>("Container");
            var containerClient = client.GetBlobContainerClient(container);

            string fileName = "car" + Guid.NewGuid().ToString() + ".json";
            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            //memorystream
            using (var stream = new MemoryStream())
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings());

                // Use the 'leave open' option to keep the memory stream open after the stream writer is disposed
                using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
                {
                    // Serialize the job to the StreamWriter
                    serializer.Serialize(writer, car);
                }

                // Rewind the stream to the beginning
                stream.Position = 0;

                // Upload the job via the stream
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            await PublishToEventGrid(config, car);
            return true;
        }
        private static async Task PublishToEventGrid(IConfiguration config, Car car)
        {

            var endpoint = config.GetValue<string>("EventGridTopicEndpoint");

            var accessKey = config.GetValue<string>("EventGridAccessKey");


            EventGridPublisherClient client = new EventGridPublisherClient(

            new Uri(endpoint),

            new AzureKeyCredential(accessKey));


            var event1 = new EventGridEvent(

            "CFA",

            "CFA.CategoryEvent",

            "1.0",

            JsonConvert.SerializeObject(car));

            event1.Id = (new Guid()).ToString();

            event1.EventTime = DateTime.Now;

            //resource id

            //event1.Topic = "/subscriptions/73d972cd-c4c3-4ec5-9443-661a57525a5d/resourceGroups/rg-training/providers/Microsoft.EventGrid/topics/omsegt";

            event1.Topic = config.GetValue<string>("EventGridTopic");

            List<EventGridEvent> eventsList = new List<EventGridEvent>

        {

        event1

        };


            // Send the events

            await client.SendEventsAsync(eventsList);

        }
    }
}
