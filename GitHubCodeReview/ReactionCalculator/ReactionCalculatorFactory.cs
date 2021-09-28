using GitHubTask.ReactionCalculator.Abstractions;
using System;

namespace GitHubTask.ReactionCalculator
{
    public static class ReactionCalculatorFactory
    {
        public static IReactionCalculator CreateReactionCalculator(GitHubClient gitHubClient, string url) =>
            url switch
            {
                var x when CommitReactionCalculator.IsCanExecute(x) => new CommitReactionCalculator(gitHubClient, url),
                var x when PullRequestReactionCalculator.IsCanExecute(x) => new PullRequestReactionCalculator(gitHubClient, url),
                _ => throw new InvalidOperationException("Cannot find calculator for url")
            };
    }
}
