using GitHubTask.Models;
using GitHubTask.Queries;
using GitHubTask.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GitHubTask.ReactionCalculator
{
    public sealed class PullRequestReactionCalculator : BaseReactionCalculator
    {
        private static readonly Regex PULL_REQUEST_REGEX = new Regex(@"(?:https:\/\/github.com\/)(\w+)\/(\w+)(?:\/pull\/)(\d+)", RegexOptions.Compiled);
        private readonly GroupCollection _groupCollection;

        public PullRequestReactionCalculator(GitHubClient gitHubClient, string url): base(gitHubClient)
        {
            _groupCollection = PULL_REQUEST_REGEX.Match(url).Groups;
        }

        protected override Func<string, string, int, PaginationQuery, Task<IEnumerable<Reaction>>> GetReactions => GitHubClient.GetReactionForPullRequestAsync;
        
        public override async Task CalculateAsync()
        {
            var comments = await GitHubUtils.GetAllAsync(query =>
                GitHubClient.GetCommentsForPullRequestAsync(_groupCollection[1].Value, _groupCollection[2].Value, Convert.ToInt32(_groupCollection[3].Value), query));

            await CalculateReaction(_groupCollection[1].Value, _groupCollection[2].Value, comments);
        }

        public static bool IsCanExecute(string url) =>
            PULL_REQUEST_REGEX.IsMatch(url);
    }
}
