namespace GraphQlInDotNet.Common.Commands
{
    public interface ICommand<TInputType,TResultType>
    {
        TResultType Execute(TInputType input);
    }
}
