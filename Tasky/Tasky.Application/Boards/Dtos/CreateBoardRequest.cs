using System.ComponentModel.DataAnnotations;
using Tasky.Domain.Enums;

namespace Tasky.Application.Boards.Dtos;

public class CreateBoardRequest
{
    [Required(ErrorMessage = "Name is required!")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Board name must be between 6 and 50 characters")]
    public required string Name { get; set; }

    [StringLength(200, ErrorMessage = "Board description must not be longer than 200 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Type of board is required!")]
    [Range(0,1)]
    public required BoardType Type { get; set; }
}
