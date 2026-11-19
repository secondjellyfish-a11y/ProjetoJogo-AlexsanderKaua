namespace ProjetoJogo_AlexsanderKaua
{
    public class Bullet : PictureBox
    {
        public int Velocidade { get; set; } = 10;

        public Bullet(int x, int y)
        {
            Left = x;
            Top = y;
            Size = new System.Drawing.Size(100, 100);
            SizeMode = PictureBoxSizeMode.StretchImage;
            BackColor = Color.Transparent; //Faz a bala ter fundo transparente
            string caminhoImagem = Path.Combine(
                Application.StartupPath,
                "Assets/",
                "bananaPurple_0.png"
                );
            Image = Image.FromFile(caminhoImagem);
        }

        public void Mover()
        {
            Left += Velocidade;
        }
        public bool SaiuDaTela(int larguraTela)
        {
            return Left + Width > 1920;
        }


    }
}
