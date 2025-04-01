namespace Microservice.Claims
{
    public class CloudantResponse
    {
        public List<Row>? rows { get; set; }
    }

    public class Row
    {
        public string? id { get; set; }
        public string? key { get; set; }
    }
}
