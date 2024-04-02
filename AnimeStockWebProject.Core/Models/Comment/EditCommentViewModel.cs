
namespace AnimeStockWebProject.Core.Models.Comment
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidations.CommentEntity;
    public class EditCommentViewModel
    {
        public int Id { get; set; }

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Cannot post empty comment.")]
        public string Description { get; set; } = null!;
    }
}
