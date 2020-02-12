namespace BeerDotApi.Configuration
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int ExpTime { get; set; } 
    }
    
    public class RabbitMQConfig
    {
        public string Host {get;set;}
        public string Username {get; set;}
        public string Password {get; set;}
    }

    public class SeqConfig
    {
        public string ServerUrl {get; set;}
        public string MinimumLevel {get; set;}
    }

    public class PostgresConfig
    {
        public string Host { get; set; }
        public string Db { get; set; }
        public uint Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
    
    public class AppConfig
    {
        public RabbitMQConfig RabbitMQ {get; set;}
        public SeqConfig Seq {get; set;}   
        public PostgresConfig Postgres { get; set; }
        public JwtConfig JWT { get; set; }
    }
}