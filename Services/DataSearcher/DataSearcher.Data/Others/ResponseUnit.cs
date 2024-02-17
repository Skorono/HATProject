namespace DataSearcher.Data.Others;

[Serializable]
public class ResponseUnit<T>
{
    public List<T> Data { get; set; }

    public DateTime ResponseDateTime { get; } = DateTime.Now;

    public TimeSpan LifeTime { get; set; }
    public bool IsOutdated => DateTime.Now.Second > ResponseDateTime.Second + LifeTime.Seconds;
}