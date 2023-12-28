using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Models;
using Shared.Services;
using System.Diagnostics;

namespace AddressBookMauiProject.ViewModels;

public partial class SpecificContactViewModel : ObservableObject, IQueryAttributable
{
    private readonly ContactService _contactService;
    private readonly ListContactViewModel _listContactViewModel;

    public SpecificContactViewModel(ListContactViewModel contactViewModel, ContactService contactService)
    {
        _listContactViewModel = contactViewModel;
        _contactService = contactService;

        _contactService.LoadContactsFromFile();
    }


    [ObservableProperty]
    private ContactModel _editContact = new();




    [RelayCommand]
    public async Task GoToEdit(ContactModel editContact)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            { "Contact", EditContact }
        };

        await Shell.Current.GoToAsync("EditView", parameters);
    }

    [RelayCommand]
    public async Task DeleteSpecificContact(ContactModel editContact)
    {
        try
        {
            var answer = await Shell.Current.DisplayAlert("Delete Contact", $"Are you sure you want to delete {EditContact.FirstName} {EditContact.LastName}?", "Yes", "No");

            if (answer)
            {
                _contactService.DeleteSpecificContact(EditContact.Email);

                _listContactViewModel.UpdateMauiList();

                await Shell.Current.DisplayAlert("Contact Deleted", $"Contact {EditContact.FirstName} {EditContact.LastName} has been deleted", "Ok");
                await Shell.Current.GoToAsync("//ListContactView");
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex); }

    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("//ListContactView");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        EditContact = (query["Contact"] as ContactModel)!;
    }
}
