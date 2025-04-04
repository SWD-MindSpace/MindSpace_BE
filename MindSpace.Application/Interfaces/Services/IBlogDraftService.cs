﻿using MindSpace.Domain.Entities.Drafts.Blogs;

namespace MindSpace.Application.Interfaces.Services
{
    public interface IBlogDraftService
    {
        Task<BlogDraft?> GetBlogDraftAsync(string blogDraftId);
        Task<BlogDraft?> SetBlogDraftAsync(BlogDraft blogDraft);
        Task<bool> DeleteBlogDraftAsync(string blogDraftId);
    }
}
