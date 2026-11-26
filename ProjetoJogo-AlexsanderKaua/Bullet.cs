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
            //Move sempre para a direita, aumentando a posição horizontal (Left) da bala
            Left += Velocidade;
        }
        public bool SaiuDaTela(int larguraTela)
        {
            //Retorna um booleano indicando se a bala saiu da tela (true) ou não (false)
            return Left + Width > Parent.ClientSize.Width;
        }


    }
}
