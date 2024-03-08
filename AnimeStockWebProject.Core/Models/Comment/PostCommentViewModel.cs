﻿

namespace AnimeStockWebProject.Core.Models.Comment
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidations.CommentEntity;
    public class PostCommentViewModel
    {
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        [Required]
        public string Description { get; set; } = null!;
        public int BookId { get; set; }
    }
}
