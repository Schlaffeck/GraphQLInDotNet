namespace GraphQlInDotNet.Common.Queries
{
    public interface IQuery<TResultType>
    {
        TResultType Execute();
    }
}
