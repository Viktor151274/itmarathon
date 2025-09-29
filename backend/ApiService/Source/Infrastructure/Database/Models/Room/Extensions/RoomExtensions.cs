using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room.Extensions
{
    internal static class RoomExtensions
    {
        public static RoomEf SyncRoom(this RoomEf trackedRoom, RoomEf updatedRoom)
        {
            trackedRoom.ClosedOn = updatedRoom.ClosedOn;
            trackedRoom.AdminId = updatedRoom.AdminId;
            trackedRoom.ModifiedOn = updatedRoom.ModifiedOn;
            trackedRoom.MinUsersLimit = updatedRoom.MinUsersLimit;
            trackedRoom.MaxUsersLimit = updatedRoom.MaxUsersLimit;
            trackedRoom.MaxWishesLimit = updatedRoom.MaxWishesLimit;
            trackedRoom.Name = updatedRoom.Name;
            trackedRoom.Description = updatedRoom.Description;
            trackedRoom.InvitationNote = updatedRoom.InvitationNote;
            trackedRoom.GiftExchangeDate = updatedRoom.GiftExchangeDate;
            trackedRoom.GiftMaximumBudget = updatedRoom.GiftMaximumBudget;
            trackedRoom.Users = SyncUsersInRoom(trackedRoom.Users, updatedRoom.Users);
            return trackedRoom;
        }

        private static ICollection<UserEf> SyncUsersInRoom(ICollection<UserEf> trackedUsers, ICollection<UserEf> updatedUsers)
        {
            var trackedDict = trackedUsers.ToDictionary(user => user.Id);
            var updatedDict = updatedUsers.ToDictionary(user => user.Id);

            var toRemove = trackedDict.Keys.Except(updatedDict.Keys).ToList();
            foreach (var id in toRemove)
            {
                trackedUsers.Remove(trackedDict[id]);
            }

            var newUsers = updatedUsers.Where(u => u.Id == 0).ToList();
            foreach (var user in newUsers)
            {
                trackedUsers.Add(user);
            }

            foreach (var updatedUser in updatedUsers.Where(user => user.Id != 0))
            {
                if (trackedDict.TryGetValue(updatedUser.Id, out var trackedUser))
                {
                    trackedUser.ModifiedOn = updatedUser.ModifiedOn;
                    trackedUser.FirstName = updatedUser.FirstName;
                    trackedUser.LastName = updatedUser.LastName;
                    trackedUser.Phone = updatedUser.Phone;
                    trackedUser.DeliveryInfo = updatedUser.DeliveryInfo;
                    trackedUser.GiftToUserId = updatedUser.GiftToUserId;
                    trackedUser.GiftId = updatedUser.GiftId;
                    trackedUser.Email = updatedUser.Email;
                    trackedUser.WantSurprise = updatedUser.WantSurprise;
                    trackedUser.Interests = updatedUser.Interests;
                    trackedUser.Wishes = SyncUserWishes(trackedUser.Wishes, updatedUser.Wishes);
                }
            }

            return trackedUsers;
        }

        private static ICollection<GiftEf> SyncUserWishes(ICollection<GiftEf> trackedWishes, ICollection<GiftEf> updatedWishes)
        {
            var toRemove = trackedWishes
                .Where(trackedWish => !updatedWishes.Any(updatedWish => updatedWish.Name == trackedWish.Name))
                .ToList();

            foreach (var remove in toRemove)
            {
                trackedWishes.Remove(remove);
            }

            var existingNames = new HashSet<string>();

            foreach (var updatedWish in updatedWishes)
            {
                var existing = trackedWishes.FirstOrDefault(ef => ef.Name == updatedWish.Name);
                if (existing != null)
                {
                    existing.ModifiedOn = updatedWish.ModifiedOn;
                    existing.InfoLink = updatedWish.InfoLink;
                    existingNames.Add(updatedWish.Name);
                }
            }

            var toAdd = updatedWishes
                .Where(updatedWish => !existingNames.Contains(updatedWish.Name))
                .ToList();

            foreach (var updatedWish in toAdd)
            {
                trackedWishes.Add(updatedWish);
            }
            
            return trackedWishes;
        }
    }
}
