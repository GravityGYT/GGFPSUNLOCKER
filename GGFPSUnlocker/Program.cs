using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

public class Program
{
    static Dictionary<string, string> GetInstalledBrowsers()
    {
        Dictionary<string, string> browsers = new Dictionary<string, string>
        {
            { "Google Chrome", @"C:\Program Files\Google\Chrome\Application\chrome.exe" },
            { "Opera GX", $@"C:\Users\{Environment.UserName}\AppData\Local\Programs\Opera GX\opera.exe" },
            { "Opera_all", $@"C:\Program Files\Opera GX" },
            { "Brave", @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe" },
        };

        List<string> keysToRemove = new List<string>();

        foreach (var browser in browsers)
        {
            if (!File.Exists(browser.Value))
            {
                keysToRemove.Add(browser.Key);
            }
        }

        foreach (string key in keysToRemove)
        {
            browsers.Remove(key);
        }

        return browsers;
    }

    public static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("GG_FPS_UNLOCKER");
        Console.WriteLine("By GravityG");
        Console.WriteLine("Version: 1.1");
        Console.WriteLine("Snay.io's optimization is shit");
        Console.WriteLine(" ");

        var browsers = GetInstalledBrowsers();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Select a browser: e.g. [1.] for Google Chrome");
        int i = 1;
        foreach (var browser in browsers)
        {
            Console.WriteLine($"{i}. {browser.Key}");
            i++;
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        int choice;
        while (true)
        {
            Console.Write("Input Option: ");
            bool isNumeric = int.TryParse(Console.ReadLine(), out choice);
            if (isNumeric && choice > 0 && choice <= browsers.Count)
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
        }

        string selectedBrowser = browsers.ElementAt(choice - 1).Value;

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Select an option:");
        Console.WriteLine("1. Launch the browser with the FPS cap removed");
        Console.WriteLine("2. Create a desktop shortcut for the browser with FPS cap removed arguments");

        Console.ForegroundColor = ConsoleColor.Magenta;
        int option;
        while (true)
        {
            Console.Write("Input Option: ");
            bool isNumeric = int.TryParse(Console.ReadLine(), out option);
            if (isNumeric && (option == 1 || option == 2))
            {
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
        }

        foreach (var process in Process.GetProcesses())
        {
            if (process.ProcessName.Contains(Path.GetFileNameWithoutExtension(selectedBrowser)))
            {
                process.Kill();
            }
        }

        if (option == 1)
        {
            Process.Start(selectedBrowser, "--disable-gpu-vsync --disable-frame-rate-limit --enable-zero-copy --disable-accelerated-video-decode --process-per-site");
        }
        else if (option == 2)
        {
            IWshRuntimeLibrary.WshShell wsh = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = wsh.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Browser.lnk") as IWshRuntimeLibrary.IWshShortcut;
            shortcut.TargetPath = selectedBrowser;
            shortcut.Arguments = "--disable-gpu-vsync --disable-frame-rate-limit --enable-zero-copy --disable-accelerated-video-decode --process-per-site";
            shortcut.Save();
        }
    }
}
