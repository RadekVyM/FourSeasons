using Foundation;

namespace FourSeasons.Maui;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp()
	{
        SQLitePCL.Batteries_V2.Init();
        return MauiProgram.CreateMauiApp();
    }
}
