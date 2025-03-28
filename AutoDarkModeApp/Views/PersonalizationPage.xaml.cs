using AutoDarkModeApp.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace AutoDarkModeApp.Views;

public sealed partial class PersonalizationPage : Page
{
    public PersonalizationViewModel ViewModel
    {
        get;
    }

    public PersonalizationPage()
    {
        ViewModel = App.GetService<PersonalizationViewModel>();
        InitializeComponent();
    }
}
