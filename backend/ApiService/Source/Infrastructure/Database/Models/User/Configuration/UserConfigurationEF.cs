using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User.Configuration
{
    internal class UserConfigurationEf : IEntityTypeConfiguration<UserEf>
    {
        public void Configure(EntityTypeBuilder<UserEf> builder)
        {
            #region Relations

            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.TargetUser)
                .WithOne(u => u.GotGiftFromUser)
                .HasForeignKey<UserEf>(u => u.GiftToUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.TargetGift)
                .WithOne(g => g.UserChose)
                .HasForeignKey<UserEf>(u => u.GiftId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Room)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Wishes)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);

            #endregion

            #region Values Configuration

            builder.Property(user => user.Id).HasColumnType("bigint")
                .ValueGeneratedOnAdd();

            builder.Property(user => user.CreatedOn).IsRequired();

            builder.Property(user => user.ModifiedOn).IsRequired();

            builder.Property(user => user.RoomId).IsRequired();

            builder.HasIndex(user => user.AuthCode).IsUnique();

            builder.Property(user => user.AuthCode).IsRequired();

            builder.Property(user => user.FirstName).HasMaxLength(40).IsRequired();

            builder.Property(user => user.LastName).HasMaxLength(40).IsRequired();

            builder.Property(user => user.Phone).IsRequired();

            builder.Property(user => user.DeliveryInfo).HasMaxLength(500).IsRequired();

            builder.Property(user => user.WantSurprise).IsRequired().HasDefaultValue(true);

            builder.Property(user => user.Interests).HasMaxLength(1000);

            #endregion
        }
    }
}
