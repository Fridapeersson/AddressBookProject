using AddressBookMauiProject.ViewModels;

namespace AddressBookMauiProject.Views;

public partial class AddView : ContentPage
{
	public AddView(AddViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}