using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Models;
using Shared.Services;
using System.Diagnostics;

namespace AddressBookMauiProject.ViewModels;

public partial class AddViewModel : ObservableObject
{
    private readonly ContactService _contactService;
    private readonly ListContactViewModel _listContactViewModel;


    public AddViewModel(ContactService contactService, ListContactViewModel listContactViewModel)
    {
        _contactService = contactService;
        _listContactViewModel = listContactViewModel;
    }


    [ObservableProperty]
    private ContactModel _regForm = new();


    [RelayCommand]
    public async Task Add()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(RegForm.Email))
            {
                await Shell.Current.DisplayAlert("Email Missing", "You have to enter an email", "Ok");
                return;
            }
            else
            {
                var contactAdded = _contactService.AddContactToList(RegForm);

                if (contactAdded)
                {
                    _listContactViewModel.UpdateMauiList();

                    await Shell.Current.DisplayAlert("Success", "You have added a new Contact!", "Ok");

                    RegForm = new();
                    await Shell.Current.GoToAsync("//ListContactView");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "A contact with that email already exists", "Ok");
                    return;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex); }

    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("//ListContactView");
    }
}
