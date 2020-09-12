using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITagRepository
    {
        Task<IReadOnlyList<Tag>> GetTagsAsync();

        Task CreateTagAsync(Tag tag);

        Task DeleteTagAsync(Guid id);
    }
}