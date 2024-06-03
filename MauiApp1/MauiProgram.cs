using ApiOgrenmeProjesi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MauiApp1
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

#if DEBUG
            builder.Logging.AddDebug();

#endif
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddDbContext<DataContext>(
                options => options.UseSqlite($"Filename={GetDatabasePath()}", x => x.MigrationsAssembly(nameof(ApiOgrenmeProjesi))));

            return builder.Build();
        }
        public static string GetDatabasePath()
        {
            var databasePath = "";
            var databaseName = "KiralamaDb.db3";

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseName);
            }
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                SQLitePCL.Batteries_V2.Init();
                databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName); ;
            }

            return databasePath;

        }
    }
}
