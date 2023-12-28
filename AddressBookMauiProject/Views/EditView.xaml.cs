using AddressBookMauiProject.ViewModels;

namespace AddressBookMauiProject.Views;

public partial class EditView : ContentPage
{
	public EditView(EditViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}