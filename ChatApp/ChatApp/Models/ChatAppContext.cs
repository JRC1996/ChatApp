using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Models;

public partial class ChatAppContext : DbContext
{
    public ChatAppContext()
    {
    }

    public ChatAppContext(DbContextOptions<ChatAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersParticipant> UsersParticipants { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table_2");

            entity.HasIndex(e => new { e.SentOn, e.IdChatRoom }, "IDX_MESSAGES_SentOn_IdChatRoom");

            entity.Property(e => e.MessageContent)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.SentOn).HasColumnType("datetime");

            entity.HasOne(d => d.IdChatRoomNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdChatRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_ChatRooms");

            entity.HasOne(d => d.IdSenderNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdSender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IDX_USERS_EMAIL").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsersParticipant>(entity =>
        {
            entity.HasIndex(e => new { e.IdUsers, e.IdChatRoom }, "IDX_USERSPARTICIPANTS_IdUsers_IdChatRoom").IsUnique();

            entity.HasOne(d => d.IdChatRoomNavigation).WithMany(p => p.UsersParticipants)
                .HasForeignKey(d => d.IdChatRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersParticipants_ChatRooms");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.UsersParticipants)
                .HasForeignKey(d => d.IdUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersParticipants_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
