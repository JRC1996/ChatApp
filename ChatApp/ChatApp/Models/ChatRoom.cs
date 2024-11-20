using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class ChatRoom
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<UsersParticipant> UsersParticipants { get; set; } = new List<UsersParticipant>();
}
