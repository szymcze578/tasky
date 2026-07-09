using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasky.Domain.Entities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Address? Address { get; set; }
        public bool IsActive { get; set; }

        public ICollection<BoardEntity> OwnedBoards { get; set; } = new List<BoardEntity>();
        public ICollection<TaskEntity> AssignedTasks { get; set; } = new List<TaskEntity>();
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
