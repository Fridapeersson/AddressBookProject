using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using System.Diagnostics;

namespace Shared.Services;

public class ContactService : IContactService
{
    public readonly IFileService _fileService;
    public List<ContactModel> Contacts { get; set; } = new List<ContactModel>();

    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
        //Contacts = contacts;

        //Hämtar alla kontakter från filen
        LoadContactsFromFile();

    }


    private readonly string _filePath = @"C:\ProjectsCSharp\AddressBookProject\contacts.json";


    /// <summary>
    ///     Adds a contact to json file if email doesn't already exists
    /// </summary>
    /// <param name="contact">The contact being added to the json file</param>
    /// <returns>True if the contact has been added, else false</returns>
    public bool AddContactToList(ContactModel contact)
    {
        try
        {
            var emailExists = Contacts.Any(x => x.Email == contact.Email);
            if (emailExists)
            {
                return false;
            }
            else
            {
                Contacts.Add(contact);
                var json = JsonConvert.SerializeObject(Contacts, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                });
                _fileService.SaveToFile(_filePath, json);
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return false;
        }
    }

    public IEnumerable<ContactModel> GetAllContactsFromList()
    {
        try
        {
            return Contacts;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    public ContactModel GetSpecificContact(string email)
    {
        try
        {
            var result = Contacts.FirstOrDefault(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase))!;

            if (result != null)
            {
                return result!;
            }
            else
            {
                return null!;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    public ContactModel DeleteSpecificContact(string email)
    {
        try
        {
            var result = Contacts.FirstOrDefault(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));

            if (result != null)
            {
                Contacts.Remove(result);

                var json = JsonConvert.SerializeObject(Contacts, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                });

                _fileService.SaveToFile(_filePath, json);

                return result!;
            }
            return null!;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null!;
        }
    }

    public void PressAnyKeyToContinue()
    {
        Console.WriteLine("\n Press any key to go back to main menu");
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    ///     Gets all contacts from json list and stores it in Contacts
    /// </summary>
    public void LoadContactsFromFile()
    {
        try
        {
            var content = _fileService.GetContentFromFile(_filePath);
            if (!string.IsNullOrEmpty(content))
            {
                Contacts = JsonConvert.DeserializeObject<List<ContactModel>>(content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                })!;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    /// <summary>
    ///     Updates an existing contact's information based on the provided contactmodel
    /// </summary>
    /// <param name="updateContact">is the contactmodel with the updated information</param>
    /// <returns>True if the contact is updated successfully, else false</returns>
    /// 
    public bool UpdateContact(ContactModel updateContact)
    {
        try
        {

            var contactToUpdate = Contacts.FirstOrDefault(x => x.Id == updateContact.Id);

            if (contactToUpdate != null)
            {
                bool contactUpdated = false;

                if (contactToUpdate.FirstName != updateContact.FirstName)
                {
                    contactToUpdate.FirstName = updateContact.FirstName;
                    contactUpdated = true;
                }

                if (contactToUpdate.LastName != updateContact.LastName)
                {
                    contactToUpdate.LastName = updateContact.LastName;
                    contactUpdated = true;
                }

                if (contactToUpdate.Email != updateContact.Email)
                {
                    var emailExists = Contacts.Any(x => x.Email == updateContact.Email && x.Id != updateContact.Id);
                    if (emailExists)
                    {
                        return false;
                    }
                    contactToUpdate.Email = updateContact.Email;
                    contactUpdated = true;
                }

                if (contactToUpdate.Address != updateContact.Address)
                {
                    contactToUpdate.Address = updateContact.Address;
                    contactUpdated = true;
                }

                if (contactToUpdate.Phone != updateContact.Phone)
                {
                    contactToUpdate.Phone = updateContact.Phone;
                    contactUpdated = true;
                }

                if (contactUpdated)
                {
                    var json = JsonConvert.SerializeObject(Contacts, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects
                    });

                    _fileService.SaveToFile(_filePath, json);
                    LoadContactsFromFile();
                }

                return contactUpdated;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
        }

        return false;
    }
}
