using System.Threading.Tasks;

namespace GitHubTask.ReactionCalculator.Abstractions
{
    public interface IReactionCalculator
    {
        public Task CalculateAsync();
    }
}
