using AddressBookMauiProject.Views;

namespace AddressBookMauiProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(ListContactView), typeof(ListContactView));
            Routing.RegisterRoute(nameof(AddView), typeof(AddView));
            Routing.RegisterRoute(nameof(EditView), typeof(EditView));
            Routing.RegisterRoute(nameof(SpecificContactView), typeof(SpecificContactView));
        }
    }
}
