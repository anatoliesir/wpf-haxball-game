using HaxballGame.Client.Game;
using System.Diagnostics;
using System.Text;
using System.Threading;
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
        private int _frameTime = 1000 / 60;
        double moveX = 0;
        double moveY = 0;
        double speed = 300;
        private double shootSlowness = 0;
        private double deltaTime = 0;
        private Stopwatch _timer = new Stopwatch();

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
                _timer.Start();
                await Task.Delay(_frameTime);

                if (Keyboard.IsKeyDown(Key.Space)) shootSlowness = 0.3;
                else shootSlowness = 0;

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

                // Diferenta intre primul cadru si al doilea cadru
                deltaTime = _timer.Elapsed.TotalSeconds;

                if (lenght > 0)
                {
                    // Normalizarea vitezei diagonale si inmultirea cu viteza adevarata constanta prin deltaTime
                    moveX = (moveX / lenght * deltaTime * speed) * (double)(1 - shootSlowness);
                    moveY = (moveY / lenght * deltaTime * speed) * (double)(1 - shootSlowness);

                    if ((moveX + Canvas.GetLeft(Player.Ellipse)) <= 0 ||
                        (moveX + Canvas.GetLeft(Player.Ellipse) + Player.Ellipse.ActualWidth) >= cnvGameScreen.ActualWidth)                        
                    {
                        if (chkDebug.IsChecked == true) txtStare.Text = "Wall hit!";
                        moveX = 0;                                                
                    }
                    if ((moveY + Canvas.GetTop(Player.Ellipse)) <= 0 ||
                        (moveY + Canvas.GetTop(Player.Ellipse) + Player.Ellipse.ActualHeight) >= cnvGameScreen.ActualHeight)
                    {
                        if (chkDebug.IsChecked == true) txtStare.Text = "Wall hit!";
                        moveY = 0;
                    }

                    Canvas.SetLeft(Player.Ellipse, Canvas.GetLeft(Player.Ellipse) + moveX);
                    Canvas.SetTop(Player.Ellipse, Canvas.GetTop(Player.Ellipse) + moveY);
                    moveY = 0;
                    moveX = 0;
                }
                _timer.Reset();
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