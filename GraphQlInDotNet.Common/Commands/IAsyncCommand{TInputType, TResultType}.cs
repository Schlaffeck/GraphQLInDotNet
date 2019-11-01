using System.Threading.Tasks;

namespace GraphQlInDotNet.Common.Commands
{
    public interface IAsyncCommand<TInputType,TResultType>
    {
        Task<TResultType> ExecuteAsync(TInputType input);
    }
}
