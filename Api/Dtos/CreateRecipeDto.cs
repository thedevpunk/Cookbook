using System;
using System.Collections.Generic;
using Core.Entities;

namespace Api.Dtos
{
    public class CreateRecipeDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Story { get; set; }

        public Guid CategoryId { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public List<Step> Steps { get; set; }

        public List<Tag> Tags { get; set; }

        public int NumberOfPortions { get; set; }

        public int DurationInMinutes { get; set; }
    }
}