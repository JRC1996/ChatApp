using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class UsersParticipant
{
    public int Id { get; set; }

    public int IdUsers { get; set; }

    public int IdChatRoom { get; set; }

    public virtual ChatRoom IdChatRoomNavigation { get; set; } = null!;

    public virtual User IdUsersNavigation { get; set; } = null!;
}
