using GitHubTask.Models;
using GitHubTask.Queries;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitHubTask
{
    public class GitHubClient
    {
        private readonly string _token;
        private readonly RestApiClient _apiClient;

        private const string BASE_URL = "https://api.github.com/";
        private const string USER_AGENT = "node.js";
        private const string PREVIEW_ACCEPT = "application/vnd.github.squirrel-girl-preview";

        public GitHubClient(string token)
        {
            _token = token;

            _apiClient = new RestApiClient(new Uri(BASE_URL));
            _apiClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue(USER_AGENT)));
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", _token);
        }

        public Task<IEnumerable<Comment>> GetCommentsForCommitAsync(string owner, string repo, string commitSha, PaginationQuery query) =>
             _apiClient.Get<IEnumerable<Comment>>(new Uri($"repos/{owner}/{repo}/commits/{commitSha}/comments{query}", UriKind.Relative));

        public Task<IEnumerable<Comment>> GetCommentsForPullRequestAsync(string owner, string repo, int pullNumber, PaginationQuery query) =>
             _apiClient.Get<IEnumerable<Comment>>(new Uri($"repos/{owner}/{repo}/pulls/{pullNumber}/comments{query}", UriKind.Relative));

        public Task<IEnumerable<Reaction>> GetReactionForCommitAsync(string owner, string repo, int commentId, PaginationQuery query)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"repos/{owner}/{repo}/comments/{commentId}/reactions{query}", UriKind.Relative));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(PREVIEW_ACCEPT));
            return _apiClient.Get<IEnumerable<Reaction>>(request);
        }

        public Task<IEnumerable<Reaction>> GetReactionForPullRequestAsync(string owner, string repo, int commentId, PaginationQuery query = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"repos/{owner}/{repo}/pulls/comments/{commentId}/reactions{query}", UriKind.Relative));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(PREVIEW_ACCEPT));
            return _apiClient.Get<IEnumerable<Reaction>>(request);
        }
    }
}
