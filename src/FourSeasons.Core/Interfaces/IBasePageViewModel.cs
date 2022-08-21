using System.ComponentModel;

namespace FourSeasons.Core.Interfaces
{
    public interface IBasePageViewModel : INotifyPropertyChanged
    {
        Task OnAppearing();
        Task OnDisappearing();
    }
}
