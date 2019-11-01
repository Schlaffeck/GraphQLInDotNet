namespace GraphQlInDotNet.Common.Queries
{
    public interface IQuery<TInputType,TResultType>
    {
        TResultType Execute(TInputType inputType);
    }
}
