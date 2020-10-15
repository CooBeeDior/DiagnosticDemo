using ProtoBuf;
using System;

namespace DiagnosticApiDemo
{
    [ProtoContract]
    public class WeatherForecast
    {
        [ProtoMember(1)]
        public virtual DateTime Date { get; set; }
        [ProtoMember(2)]
        public virtual int TemperatureC { get; set; }
        //[ProtoMember(3)]
        //public virtual int TemperatureF { get; set; } => 32 + (int)(TemperatureC / 0.5556);
        [ProtoMember(3)]
        public virtual string Summary { get; set; }
    }
}
