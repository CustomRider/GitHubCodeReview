using GitHubTask.Models;
using GitHubTask.Queries;
using GitHubTask.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GitHubTask.ReactionCalculator
{
    public sealed class CommitReactionCalculator : BaseReactionCalculator
    {
        private static Regex COMMIT_URL_PATTERN => new Regex(@"(?:https:\/\/github.com\/)(\w+)\/(\w+)(?:\/commit\/)(\w+)", RegexOptions.Compiled);
        private readonly GroupCollection _groupCollection;

        public CommitReactionCalculator(GitHubClient gitHubClient, string url) : base(gitHubClient)
        {
            _groupCollection = COMMIT_URL_PATTERN.Match(url).Groups;
        }

        protected override Func<string, string, int, PaginationQuery, Task<IEnumerable<Reaction>>> GetReactions => GitHubClient.GetReactionForCommitAsync;

        public override async Task CalculateAsync()
        {
            var comments = await GitHubUtils.GetAllAsync(query =>
              GitHubClient.GetCommentsForCommitAsync(_groupCollection[1].Value, _groupCollection[2].Value, _groupCollection[3].Value, query));

            await CalculateReaction(_groupCollection[1].Value, _groupCollection[2].Value, comments);
        }

        public static bool IsCanExecute(string url) =>
            COMMIT_URL_PATTERN.IsMatch(url);
    }
}
