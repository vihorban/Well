using System.Windows.Input;

namespace Well
{
    public class CustomCommands
    {
        public static RoutedCommand Exit = new RoutedCommand();
        public static RoutedCommand CancelStep = new RoutedCommand();
        public static RoutedCommand NewGame = new RoutedCommand();
        public static RoutedCommand Settings = new RoutedCommand();
        public static RoutedCommand About = new RoutedCommand();
        public static RoutedCommand RestoreDefault = new RoutedCommand();
    }
}