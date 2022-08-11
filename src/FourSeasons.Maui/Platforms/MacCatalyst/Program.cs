using ObjCRuntime;
using UIKit;

namespace FourSeasons.Maui;

public class Program
{
	// This is the main entry point of the application.
	static void Main(string[] args)
	{
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.

        SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
        SQLitePCL.Batteries_V2.Init();

        UIApplication.Main(args, null, typeof(AppDelegate));
	}
}
