using Moq;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using Shared.Services;

namespace AddressBook_Test.Services;

public class ContactService_Tests
{
    [Fact]
    public void AddContactToList_Should_AddAContactToContactList_ThenReturnTrue()
    {
        // Arrange
        var contact = new ContactModel()
        {
            FirstName = "Nisse",
            LastName = "Hult",
            Email = "nh@example.com",
            Address = "gatan 1",
            Phone = "123456576"
        };

        // Skapa en mock av IFileService
        var mockFileService = new Mock<IFileService>();

        // Setup mocken för att returnera önskat värde
        mockFileService.Setup(x => x.SaveToFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        var contactService = new ContactService(mockFileService.Object);

        // Act
        bool result = contactService.AddContactToList(contact);

        // Assert
        Assert.True(result);
    }



    [Fact]
    public void GetAllContactsFromList_Should_GetAllContactsFromList_Then_ReturnContactList()
    {
        //Arrange
        var mockFileService = new Mock<IFileService>();
        var contactsTestData = new List<ContactModel>
        {
            new ContactModel { FirstName = "Nisse", LastName = "Hult", Address = "Gatan 1", Email = "mail@gmail.com", Phone = "1234567890" },

             new ContactModel { FirstName = "Test", LastName = "Test", Address = "Test 1", Email = "test@gmail.com", Phone = "1234567890" }
        };

        mockFileService.Setup(x => x.GetContentFromFile(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(contactsTestData));

        IContactService contactService = new ContactService(mockFileService.Object);

        //Act
        var result = contactService.GetAllContactsFromList();

        //Assert
        Assert.NotNull(result);
        Assert.True(((IEnumerable<ContactModel>)result).Any());
    }


    [Fact]
    public void GetSpecificContact_Should_GetASpecificContactFromListBasedOnEmail_Then_ReturnTheContactWithTheSpecificEmail()
    {
        //Mock
        var mockFileService = new Mock<IFileService>();
        FileService fileService = new FileService();

        //Arrange
        IContactService contactService = new ContactService(mockFileService.Object);
        ContactModel contact = new ContactModel { FirstName = "Nisse", LastName = "Hult", Address = "Gatan 1", Email = "mail@gmail.com", Phone = "1234567890" };

        contactService.AddContactToList(contact);

        string emailToSearch = "mail@gmail.com";


        //Act
        IContact specificContact = contactService.GetSpecificContact(emailToSearch);

        //Assert
        Assert.NotNull(specificContact);
        Assert.Equal(emailToSearch, specificContact.Email);
        Assert.Equal(contact.FirstName, specificContact.FirstName);
    }


    [Fact]
    public void DeleteSpecificContact_Should_DeleteASpecificContactBasedOnEmail_Then_DeleteSpecificContactFromList()
    {
        //Arrange
        //Mock
        var mockFileService = new Mock<IFileService>();

        List<ContactModel> contacts = new List<ContactModel>
        {
            new ContactModel { FirstName = "Nisse", LastName = "Hult", Address = "Gatan 8", Email = "mail@gmail.com", Phone = "1234567890" }
        };

        mockFileService.Setup(m => m.GetContentFromFile(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(contacts));

        IContactService contactService = new ContactService(mockFileService.Object);
        //kontakten som ska tas bort
        string emailToDelete = "mail@gmail.com";


        //Act
        IContact deletedContact = contactService.DeleteSpecificContact(emailToDelete);


        //Assert
        Assert.Null(contactService.GetSpecificContact(emailToDelete));
        Assert.Equal(emailToDelete, deletedContact.Email);
    }
}
