using System;
using System.Collections.Generic;
using System.Text;

namespace FRSCInventory.Models.DataObjects.Notification
{
    public class FireBaseModel
    {
        public class FCMResponse
        {
            public double Multicast_id { get; set; }
            public int Success { get; set; }
            public int Failure { get; set; }
            public long Canonical_ids { get; set; }
            public Result[] Results { get; set; }
        }

        public class Result
        {
            public string Error { get; set; }
        }

        public class Message
        {
            //public string[] registration_ids { get; set; }
            public Notification notification { get; set; }
            public object data { get; set; }
            public string to { get; set; }
            //public string operation { get; set; } = "create";
            //public string notification_key_name { get; set; } = "sample";
        }

        public class Notification
        {
            public string title { get; set; }
            public string body { get; set; }
        }

    }
}
