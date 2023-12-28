using AddressBookMauiProject.ViewModels;

namespace AddressBookMauiProject.Views;

public partial class ListContactView : ContentPage
{
	public ListContactView(ListContactViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}