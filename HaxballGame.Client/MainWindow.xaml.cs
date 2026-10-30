using HaxballGame.Client.Game;
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

namespace HaxballGame.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>        
   
    public partial class MainWindow : Window
    {
        // Crearea comenzilor costumizate pentru binding usor si intuitiv
        public static readonly RoutedUICommand StartCommand = new RoutedUICommand(
            "Start Game",
            "Start",
            typeof(MainWindow)
        );
        public static readonly RoutedUICommand MoveUpCommand = new RoutedUICommand(
            "Move up",
            "MoveUp",
            typeof(MainWindow)
        );
        public static readonly RoutedUICommand MoveLeftCommand = new RoutedUICommand(
            "Move left",
            "MoveLeft",
            typeof(MainWindow)
        );
        public static readonly RoutedUICommand MoveDownCommand = new RoutedUICommand(
            "Move down",
            "MoveDown",
            typeof(MainWindow)
        );
        public static readonly RoutedUICommand MoveRightCommand = new RoutedUICommand(
            "Move rigth",
            "MoveRigth",
            typeof(MainWindow)
        );

        public static readonly RoutedUICommand StopCommand = new RoutedUICommand(
            "Stop Game",
            "Stop",
            typeof(MainWindow)
        );
        

        bool IsGameOn = false;

        Player Player = new Player(Brushes.Red, 50, 50);

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Start_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (IsGameOn) return;
            IsGameOn = true;
            Canvas.SetLeft(Player.Ellipse, Player.X);
            Canvas.SetTop(Player.Ellipse, Player.Y);
            cnvGameScreen.Children.Add(Player.Ellipse);
            if (chkDebug.IsChecked == true)
                txtStare.Text = "Jocul a fost pornit!";
        }
        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!IsGameOn) return;
            IsGameOn = false;
            cnvGameScreen.Children.Remove(Player.Ellipse);
            if (chkDebug.IsChecked == true)
                txtStare.Text = "Jocul a fost oprit!";
        }

        private void UpMove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (chkDebug.IsChecked == true && IsGameOn) 
                txtStare.Text = "Merge sus!";
        }

        private void RightMove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (chkDebug.IsChecked == true && IsGameOn)
                txtStare.Text = "Merge dreapta!";
        }

        private void DownMove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (chkDebug.IsChecked == true && IsGameOn)
                txtStare.Text = "Merge jos!";
        }

        private void LeftMove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (chkDebug.IsChecked == true && IsGameOn)
                txtStare.Text = "Merge stanga!";
        }

        
    }
}