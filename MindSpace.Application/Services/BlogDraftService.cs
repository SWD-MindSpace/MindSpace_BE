﻿using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Interfaces.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace MindSpace.Application.Services
{
    public class BlogDraftService : IBlogDraftService
    {
        // ====================================
        // === Props & Fields
        // ====================================

        private readonly IDatabase _database;

        // ====================================
        // === Constructors
        // ====================================

        public BlogDraftService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        // ====================================
        // === Methods
        // ====================================

        /// <summary>
        /// Delete blog draft by id
        /// </summary>
        /// <param name="blogDraftId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBlogDraftAsync(string blogDraftId)
        {
            return await _database.KeyDeleteAsync(blogDraftId);
        }

        /// <summary>
        /// Get Blog Draft from redis database
        /// </summary>
        /// <param name="blogDraftId"></param>
        /// <returns></returns>
        public async Task<BlogDraft?> GetBlogDraftAsync(string blogDraftId)
        {
            var blogDraft = await _database.StringGetAsync(blogDraftId);
            return blogDraft.IsNullOrEmpty ? null : JsonSerializer.Deserialize<BlogDraft>(blogDraft);
        }

        /// <summary>
        /// Set new or updated the blog draft
        /// </summary>
        /// <param name="blogDraft"></param>
        /// <returns></returns>
        public async Task<BlogDraft?> SetBlogDraftAsync(BlogDraft blogDraft)
        {
            var IsSetSuccessful = await _database.StringSetAsync(blogDraft.Id,
                JsonSerializer.Serialize(blogDraft),
                TimeSpan.FromHours(2));

            return !IsSetSuccessful ? null : await GetBlogDraftAsync(blogDraft.Id);
        }
    }
}
