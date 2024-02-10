namespace DataSearcher.Domain.Helpers.Data;

public class ResponseUnit<T>
{
    private DateTime _responseDateTime;
    private List<T> _data = new();

    public List<T> Data => new(_data);
    public DateTime ResponseDateTime => _responseDateTime;

    public readonly TimeSpan LifeTime;
    public bool IsOutdated => DateTime.Now.Second > (ResponseDateTime.Second + LifeTime.Seconds);

    public ResponseUnit(List<T> data, TimeSpan? lifeTime)
    {
        _responseDateTime = DateTime.Now;
        _data = data;
        
        LifeTime = lifeTime ?? TimeSpan.FromHours(1);
    }
}