﻿using System.Text.Json.Serialization;

namespace Microservice.Claims
{
    public class ApiToken
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        [JsonPropertyName("expiration")]
        public int Expiration { get; set; }
    }
}
