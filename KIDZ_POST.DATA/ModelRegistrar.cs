

namespace KIDZ_POST.DATA
{
    using KIDZ_POST.DATA.CONTRACT;
    using KIDZ_POST.DATA.MODEL;
    using Microsoft.EntityFrameworkCore;

    public class ModelRegistrar : IModelRegistrar
    {
        public void RegisterModels(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Security");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.FirstName).HasColumnName("FirstName");
                entity.Property(e => e.LastName).HasColumnName("LastName");
                entity.Property(e => e.UserName).HasColumnName("UserName");
                entity.Property(e => e.Password).HasColumnName("Password");
                entity.Property(e => e.Description).HasColumnName("Description");
                entity.Property(e => e.IsActivated).HasColumnName("IsActivated");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message", "Media");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Body).HasColumnName("Body");
                entity.Property(e => e.Title).HasColumnName("Title");
                entity.Property(e => e.CreatedTime).HasColumnName("CreatedTime");
                entity.HasOne(x => x.CreatedBy).WithMany(x => x.Messages).HasForeignKey(x => x.CreatedById);
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.ToTable("UserMessage", "Media");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.ViewedTime).HasColumnName("ViewedTime");
                entity.HasOne(x => x.User).WithMany(x => x.UserMessages).HasForeignKey(x => x.UserId);
                entity.HasOne(x => x.Message).WithMany(x => x.UserMessages).HasForeignKey(x => x.MessageId);
            });

        }
    }
}

