using AddressBookMauiProject.ViewModels;

namespace AddressBookMauiProject.Views;

public partial class SpecificContactView : ContentPage
{
	public SpecificContactView(SpecificContactViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}