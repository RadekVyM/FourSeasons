namespace FourSeasons.Core.Interfaces
{
    public interface IBasePageViewModel
    {
        Task OnAppearing();
        Task OnDisappearing();
    }
}
