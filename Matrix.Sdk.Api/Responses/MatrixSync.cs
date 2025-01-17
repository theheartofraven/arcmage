﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Matrix.Sdk.Api.Helpers;
using Newtonsoft.Json;

namespace Matrix.Sdk.Api.Responses
{
    [DataContract]
    public class MatrixSync
    {
        [DataMember(Name = "next_batch")]
        public string NextBatch { get; set; }
        [DataMember(Name = "account_data")]
        public MatrixSyncEvents AccountData { get; set; }
        [DataMember(Name = "presence")]
        public MatrixSyncEvents Presense { get; set; }
        [DataMember(Name = "rooms")]
        public MatrixSyncRooms Rooms { get; set; }
    }

    [DataContract]
    public class MatrixSyncEvents
    {
        [DataMember(Name = "events")]
        [JsonConverter(typeof(MatrixEventsItemConverter))]
        public List<MatrixEvents> Events { get; set; }
    }

    [DataContract]
    public class MatrixSyncTimelineEvents : MatrixSyncEvents
    {
        [DataMember(Name = "limited")]
        public bool Limited { get; set; }
        [DataMember(Name = "prev_batch")]
        public string PreviousBatch { get; set; }
    }

    [DataContract]
    public class MatrixSyncRooms
    {
        [DataMember(Name = "invite")]
        public Dictionary<string, MatrixEventRoomInvited> Invite { get; set; }
        [DataMember(Name = "join")]
        public Dictionary<string, MatrixEventRoomJoined> Join { get; set; }
        [DataMember(Name = "leave")]
        public Dictionary<string, MatrixEventRoomLeft> Leave { get; set; }
    }
}
