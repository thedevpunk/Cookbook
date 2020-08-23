using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Group
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<Guid> UserIds { get; set; }

        public List<Guid> AdminIds { get; set; }

        public List<Guid> RecipeIds { get; set; }
    }
}