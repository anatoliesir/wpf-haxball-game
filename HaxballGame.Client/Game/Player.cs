using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace HaxballGame.Client.Game
{
    public class Player
    {
        public float X { get; set; } = 100;
        public float Y { get; set; } = 100;
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public Ellipse Ellipse { get; set; }
        public Player(Brush brush, int width, int height)
        {
            Ellipse = new Ellipse
            {
                Width = width,
                Height = height,
                Fill = brush
            };
        }
    }
}
