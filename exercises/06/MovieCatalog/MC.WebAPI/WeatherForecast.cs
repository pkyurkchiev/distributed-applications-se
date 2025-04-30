namespace MC.WebAPI
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class WeatherForecast
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public DateOnly Date { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int TemperatureC { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string? Summary { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}