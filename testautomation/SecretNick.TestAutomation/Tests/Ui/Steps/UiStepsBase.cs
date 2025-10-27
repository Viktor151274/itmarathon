using Microsoft.Playwright;
using Tests.Ui.Pages;

namespace Tests.Ui.Steps
{
    public abstract class UiStepsBase(IPage page)
    {
        internal BasePage GetBasePage() => new(page);
        internal BaseSuccessPage GetSuccessPage() => new(page);
        internal CreateRoomSuccessPage GetRoomSuccessPage() => new(page);
        internal RoomPage GetRoomPage() => new(page);
        internal JoinPage GetJoinPage() => new(page);
        internal CreateRoomPage GetCreateRoomPage() => new(page);
        internal InvalidPage GetInvalidPage() => new(page);
        internal AddDetailsPage GetAddDetailsPage() => new(page);
        internal AddWishesPage GetAddWishesPage() => new(page);
    }
}
