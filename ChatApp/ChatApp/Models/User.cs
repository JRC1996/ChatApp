using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UsersParticipant> UsersParticipants { get; set; } = new List<UsersParticipant>();
}
