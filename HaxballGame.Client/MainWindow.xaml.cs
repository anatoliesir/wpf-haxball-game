using HaxballGame.Client.Game;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

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

        public static readonly RoutedUICommand StopCommand = new RoutedUICommand(
            "Stop Game",
            "Stop",
            typeof(MainWindow)
        );
        

        bool IsGameOn = false;
        Player Player = new Player(Brushes.Red, 50, 50);
        private int _frameTime = 17;
        double moveX = 0;
        double moveY = 0;
        double speed = 5;

        public MainWindow()
        {
            InitializeComponent();

        }
        
        private async void Start_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (IsGameOn) return;
            IsGameOn = true;
            Canvas.SetLeft(Player.Ellipse, Player.X);
            Canvas.SetTop(Player.Ellipse, Player.Y);
            cnvGameScreen.Children.Add(Player.Ellipse);
            if (chkDebug.IsChecked == true)
                txtStare.Text = "Jocul a fost pornit!";

            await GameLoop();
            
        }

        private async Task GameLoop()
        {
            while (IsGameOn)
            {
                await Task.Delay(_frameTime);
                if (Keyboard.IsKeyDown(Key.W))
                {
                    moveY -= 1;
                }
                if (Keyboard.IsKeyDown(Key.D))
                {
                    moveX += 1;
                }
                if (Keyboard.IsKeyDown(Key.S))
                {
                     moveY += 1;
                }
                if (Keyboard.IsKeyDown(Key.A))
                {
                    moveX -= 1;
                }

                double lenght = Math.Sqrt(moveX * moveX + moveY * moveY);

                if (lenght > 0)
                {
                    // Normalizarea vitezei diagonale
                    moveX = moveX / lenght * speed;
                    moveY = moveY / lenght * speed;

                    Canvas.SetLeft(Player.Ellipse, Canvas.GetLeft(Player.Ellipse) + moveX);
                    Canvas.SetTop(Player.Ellipse, Canvas.GetTop(Player.Ellipse) + moveY);
                    moveY = 0;
                    moveX = 0;

                }
            }
        }
        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!IsGameOn) return;
            IsGameOn = false;
            cnvGameScreen.Children.Remove(Player.Ellipse);
            if (chkDebug.IsChecked == true)
                txtStare.Text = "Jocul a fost oprit!";
        }        

        
    }
}