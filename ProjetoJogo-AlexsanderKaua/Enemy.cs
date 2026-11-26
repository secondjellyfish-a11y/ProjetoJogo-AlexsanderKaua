namespace ProjetoJogo_AlexsanderKaua
{
    public class Enemy : PictureBox
    {
        public int Velocidade { get; set; } = 8;
        public int Vida { get; set; } = 3;

        private string currentFrame = "enemy.png"; //Sprite base para inimigos

        private int pixelWidth = 64;
        private int pixelHeight = 112;

        public Enemy(int x, int y)
        {
            Size = new Size(pixelWidth, pixelHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;
            string caminhoImagem = Path.Combine(
                Application.StartupPath,
                "Assets/",
                "enemy.png"
                );
            if (File.Exists(caminhoImagem))
            {
                Image = Image.FromFile(caminhoImagem);
            }
            else
            {
                BackColor = Color.Red;
            }

            BackColor = Color.Transparent;

            //Mudar a posição do Enemy com os parâmetros de posição
            Left = x;
            Top = y;
        }

        public virtual void Mover()
        {
            Left -= Velocidade;
        }

        public void ReceberDano(int dano = 1)
        {
            Vida -= dano;
        }
        public bool EstaMorto()
        {
            return Vida <= 0;
        }
        public bool SaiuDaTela()
        {
            return Left + Width < 0;
        }
    }
}
