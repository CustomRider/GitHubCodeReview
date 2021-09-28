using GitHubTask.ReactionCalculator;
using System;
using System.Threading.Tasks;

namespace GitHubTask
{
    class Program
    {
        private const string GIT_HUB_API_KEY = "";

        static async Task Main(string[] args)
        {
            var linkToCommit = "https://github.com/andreaskosten/php_examples/commit/19c5500941ec544128962b29ffe6da9eb0ad07d1";
            var linkToPullRequest = "https://github.com/TimurBaldin/BumblebeeGeneratorService/pull/14";
            
            var gitHubClient = new GitHubClient(GIT_HUB_API_KEY);

            Console.WriteLine("- Calculate reactions for Commit -");
            var reactionCalculator = ReactionCalculatorFactory.CreateReactionCalculator(gitHubClient, linkToCommit);
            await reactionCalculator.CalculateAsync();

            Console.WriteLine("- Calculate reactions for PullRequest -");
            reactionCalculator = ReactionCalculatorFactory.CreateReactionCalculator(gitHubClient, linkToPullRequest);
            await reactionCalculator.CalculateAsync();
        }
    }
}
