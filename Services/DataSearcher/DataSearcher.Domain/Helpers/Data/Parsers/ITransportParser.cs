namespace DataSearcher.Domain.Helpers.Data.Parsers;

public interface ITransportParser<out TOut, in TInput>
{
    public TOut? Parse(TInput data);
}