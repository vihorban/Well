using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Well
{
    public class CustomCommands
    {
        public static RoutedCommand Exit = new RoutedCommand();
        public static RoutedCommand Cancel_Step = new RoutedCommand();
        public static RoutedCommand New_Game = new RoutedCommand();
        public static RoutedCommand Settings = new RoutedCommand();
        public static RoutedCommand About = new RoutedCommand();
        public static RoutedCommand RestoreDefault = new RoutedCommand();
    }
}
