using FourSeasons.Core.Interfaces;

namespace FourSeasons.Core.ViewModels
{
    public class BasePageViewModel : ObservableObject, IBasePageViewModel
    {
        public virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnDisappearing()
        {
            return Task.CompletedTask;
        }
    }
}
