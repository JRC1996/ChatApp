using System;
using System.Collections.Generic;

namespace ChatApp.Models;

public partial class Message
{
    public long Id { get; set; }

    public string MessageContent { get; set; } = null!;

    public DateTime SentOn { get; set; }

    public int IdChatRoom { get; set; }

    public int IdSender { get; set; }

    public virtual ChatRoom IdChatRoomNavigation { get; set; } = null!;

    public virtual User IdSenderNavigation { get; set; } = null!;
}
