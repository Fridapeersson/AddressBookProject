using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Models;
using Shared.Services;
using System.Collections.ObjectModel;

namespace AddressBookMauiProject.ViewModels;

public partial class ListContactViewModel : ObservableObject
{
    private readonly ContactService _contactService;


    public ListContactViewModel(ContactService contactService)
    {
        _contactService = contactService;

        UpdateMauiList();
    }




    [ObservableProperty]
    private ObservableCollection<ContactModel> _contactList = [];


    public void UpdateMauiList()
    {
        ContactList.Clear();


        ContactList = new ObservableCollection<ContactModel>(_contactService.Contacts.Select(contact => contact).ToList());

        _contactService.LoadContactsFromFile();
    }



    [RelayCommand]
    public async Task GoToSpecificContact(ContactModel editContact)
    {
        _contactService.GetAllContactsFromList();
        UpdateMauiList();
        var parameters = new ShellNavigationQueryParameters
        {
            { "Contact", editContact }
        };

        await Shell.Current.GoToAsync("SpecificContactView", parameters);
    }


    [RelayCommand]
    public async Task GoToAddContact()
    {
        await Shell.Current.GoToAsync("AddView");
    }


    public void UpdateContactlist()
    {
        ContactList = new ObservableCollection<ContactModel>(_contactService.Contacts.Select(contact => contact).ToList());
    }
}
