using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using Microsoft.Win32;

using ImageSplitter.Objects;
using ImageSplitter.Algorithms;

namespace ImageSplitter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myDataContext = new MyDataContext();
            this.DataContext = myDataContext;
        }

        private MyDataContext myDataContext;

        private void createDirectory()
        {
            // Specify the directory you want to manipulate.
            string path = "cards";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    //MessageBox.Show("That path exists already");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

            }
            catch (Exception e)
            {
                MessageBox.Show("The process failed: {0}", e.ToString());
            }
            finally { }
        }

        private void ButtonFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OpenDialog1 = new OpenFileDialog();
            OpenDialog1.Filter = "Файли зображень(*.BMP;*.JPG;*.GIF; *.JPEG; *.PNG)|*.BMP;*.JPG;*.GIF;*.JPEG;*.PNG|Всі файли (*.*)|*.*";
            if (OpenDialog1.ShowDialog() == true)
            {
                myDataContext.FileName = OpenDialog1.FileName;
            }
        }

        private void ButtonSplit_Click(object sender, RoutedEventArgs e)
        {
            createDirectory();
            ImageCropper.cropImage(myDataContext.FileName);
        }
    }
}
