using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Models;
using Shared.Services;
using System.Diagnostics;

namespace AddressBookMauiProject.ViewModels;

public partial class EditViewModel : ObservableObject, IQueryAttributable
{
    private readonly ContactService _contactService;
    private readonly ListContactViewModel _listContactViewModel;



    public EditViewModel(ContactService contactService, ListContactViewModel listContactViewModel)
    {
        _contactService = contactService;
        _listContactViewModel = listContactViewModel;


        _contactService.LoadContactsFromFile();
        _listContactViewModel.UpdateMauiList();

    }

    [ObservableProperty]
    private ContactModel _editContact = new();



    //Updating contact
    [RelayCommand]
    public async Task Update()
    {
        //_contactService.RemoveAndLoadContactsFromFile();

        try
        {
            var result = await Shell.Current.DisplayAlert("Save Changes", "Do you want to save the changes?", "Yes", "No");

            if (result)
            {
                if (string.IsNullOrEmpty(EditContact.Email))
                {
                    await Shell.Current.DisplayAlert("Email Missing", "You need to enter a email", "Ok");
                    return;
                }

                var contactUpdated = _contactService.UpdateContact(EditContact);

                if (!contactUpdated)
                {
                    await Shell.Current.DisplayAlert("Email Already Exists", "The email already exists", "Ok");
                    return;
                }

                _contactService.UpdateContact(EditContact);
                _contactService.LoadContactsFromFile();
                _listContactViewModel.UpdateMauiList();



                await Shell.Current.GoToAsync("//ListContactView");
            }

            else
            {
                await Shell.Current.DisplayAlert("Nothing Saved", "Nothing got saved", "Go Back to Contact List");
                await Shell.Current.GoToAsync("//ListContactView");

            }
        }
        catch (Exception e) { Debug.WriteLine(e); }
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
