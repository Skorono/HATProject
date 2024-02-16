namespace DataSearcher.Domain.Helpers.Data;

[Serializable]
public class ResponseUnit<T>
{
    private DateTime _responseDateTime = DateTime.Now;
    public List<T> Data { get; set; }

    public DateTime ResponseDateTime => _responseDateTime;

    public TimeSpan LifeTime { get; set; }
    public bool IsOutdated => DateTime.Now.Second > (ResponseDateTime.Second + LifeTime.Seconds);
}