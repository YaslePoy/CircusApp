using System.Windows;
using CircusApp.DB;

namespace CircusApp
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static circus_dbEntities DB = new circus_dbEntities();
        public static User User;
    }
}