using Newtonsoft.Json;
using System;
using AppWeb.TelegramBotClient.Converters;

namespace AppWeb.TelegramBotClient.Models
{
    /// <summary>
    /// This object represents a message.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Message
    {
        /// <summary>
        /// Unique message identifier
        /// </summary>
        [JsonProperty("message_id", Required = Required.Always)]
        public int MessageId { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        [JsonProperty("from", Required = Required.Default)]
        public User From { get; set; }

        /// <summary>
        /// Conversation the message belongs to
        /// </summary>
        [JsonProperty("chat", Required = Required.Always)]
        public Chat Chat { get; set; }

        /// <summary>
        /// Date the message was sent
        /// </summary>
        [JsonProperty("date", Required = Required.Always)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Optional. Title, for channels and group chats
        /// </summary>
        [JsonProperty(PropertyName = "text", Required = Required.Default)]
        public string Text { get; set; }
    }
}
