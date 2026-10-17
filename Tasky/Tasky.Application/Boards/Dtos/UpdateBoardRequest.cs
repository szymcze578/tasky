using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tasky.Domain.Entities;

namespace Tasky.Application.Boards.Dtos;

public class UpdateBoardRequest
{
    [Required]
    public required string Name { get; set; }
    public string? Description { get; set; }
}
