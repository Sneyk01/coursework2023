using System.Text;

namespace myPongGame
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            foreach (var f in new DirectoryInfo(@"...").GetFiles("*.cs", SearchOption.AllDirectories))
            {
                string s = File.ReadAllText(f.FullName);
                File.WriteAllText(f.FullName, s, Encoding.UTF8);
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new mainForm());
        }
    }
}