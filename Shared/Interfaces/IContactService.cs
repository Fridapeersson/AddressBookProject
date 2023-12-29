using Shared.Models;

namespace Shared.Interfaces;

public interface IContactService
{
    bool AddContactToList(ContactModel contact);

    IEnumerable<ContactModel> GetAllContactsFromList();

    ContactModel GetSpecificContact(string email);

    ContactModel DeleteSpecificContact(string email);
    //public void ExitApplication();

    public void PressAnyKeyToContinue();
}
