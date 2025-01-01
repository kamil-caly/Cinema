using System.Text.Json.Serialization;

namespace CinemaApiDomain.Entities.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TicketState
    {
        Valid,
        Invalid,
        Used,
        NotExist
    }
}
