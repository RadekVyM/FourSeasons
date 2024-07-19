using ObjCRuntime;
using UIKit;
using Foundation;

[assembly: Preserve(typeof(System.Linq.Queryable), AllMembers = true)]

namespace FourSeasons.Maui;

public class Program
{
    // This is the main entry point of the application.
    static void Main(string[] args)
    {
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.

        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}