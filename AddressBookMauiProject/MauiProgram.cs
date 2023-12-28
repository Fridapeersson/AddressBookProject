using AddressBookMauiProject.ViewModels;
using AddressBookMauiProject.Views;
using Microsoft.Extensions.Logging;
using Shared.Interfaces;
using Shared.Models;
using Shared.Services;

namespace AddressBookMauiProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            builder.Services.AddSingleton<ContactModel>();
            builder.Services.AddSingleton<ContactService>();
            builder.Services.AddSingleton<IFileService, FileService>();


            builder.Services.AddSingleton<AddViewModel>();
            builder.Services.AddSingleton<AddView>();

            builder.Services.AddSingleton<EditViewModel>();
            builder.Services.AddSingleton<EditView>();

            builder.Services.AddSingleton<ListContactViewModel>();
            builder.Services.AddSingleton<ListContactView>();

            builder.Services.AddSingleton<SpecificContactViewModel>();
            builder.Services.AddSingleton<SpecificContactView>();


            builder.Logging.AddDebug();
            return builder.Build();
        }
    }
}
