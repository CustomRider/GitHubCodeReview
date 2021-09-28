using GitHubTask.Constants;
using GitHubTask.Extensions;
using GitHubTask.Models;
using GitHubTask.Queries;
using GitHubTask.ReactionCalculator.Abstractions;
using GitHubTask.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubTask.ReactionCalculator
{
    public abstract class BaseReactionCalculator : IReactionCalculator
    {
        protected GitHubClient GitHubClient;

        public BaseReactionCalculator(GitHubClient gitHubClient)
        {
            GitHubClient = gitHubClient;
        }

        public abstract Task CalculateAsync();
        protected abstract Func<string, string, int, PaginationQuery, Task<IEnumerable<Reaction>>> GetReactions { get; }

        protected async Task CalculateReaction(string owner, string repo, IEnumerable<Comment> comments)
        {
            foreach (var userComments in comments.GroupBy(x => new { x.User.Id, x.User.Login }))
            {
                var userReactions = await userComments.SelectManyAsync(x =>
                    GitHubUtils.GetAllAsync(query => GetReactions(owner, repo, x.Id, query)
                ));
                var plusCount = userReactions.Count(x => x.Content == Reactions.PLUS);
                var minusCount = userReactions.Count(x => x.Content == Reactions.MINUS);

                Console.WriteLine($"{userComments.Key.Login} - {userComments.Count()} - {plusCount} - {minusCount}");
            }
        }
    }
}
