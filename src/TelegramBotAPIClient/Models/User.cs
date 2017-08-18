using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotAPIClient.Models
{
    /// <summary>
    /// Model representing https://core.telegram.org/bots/api#user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Integer, Unique identifier for this user or bot
        /// </summary>
        int id { get; set; }

        /// <summary>
        /// String, User‘s or bot’s first name
        /// </summary>
        string first_name { get; set; }

        /// <summary>
        /// String, Optional.User‘s or bot’s last name
        /// </summary>
        string last_name { get; set; }

        /// <summary>
        /// String, Optional. User‘s or bot’s username
        /// </summary>
        string username { get; set; }

        /// <summary>
        /// String,	Optional. IETF language tag of the user's language
        /// </summary>
        string language_code { get; set; } 
    }
}
