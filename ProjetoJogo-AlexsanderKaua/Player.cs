namespace ProjetoJogo_AlexsanderKaua
{
    //Classe Player (comentado por Alexsander)
    public class Player : PictureBox
    {

        public int Velocidade { get; set; } = 30; //Variável para a velocidade de movimento do jogador (com a entrada do jogador)

        //Tamanho em pixels do jogador. Essencialmente o tamanho do controle que será o objeto "jogador".
        public const int pixel_Width = 200;
        public const int pixel_Height = 200;
        public Player()
        {
            //Instruções de gráfico
            Size = new System.Drawing.Size(pixel_Width, pixel_Height); //Fazer o tamanho do controle ser igual a pixel_Width
            SizeMode = PictureBoxSizeMode.StretchImage; //Mudar o modo de redimensionar imagens do PictureBox para "esticar imagem" 
            string caminhoImagem = Path.Combine(
                Application.StartupPath,
                "Assets/",
                "player.png"
                ); //Usa como referência o caminho de imagens do projeto ("Assets/") para uma imagem e cria uma string para o caminho da imagem
            Image = Image.FromFile(caminhoImagem); //Muda a propriedade de Imagem do controle para mostrar o gráfico da imagem na tela. Usa Image.FromFile(#) para puxar o arquivo.
        }

        /*Método de Movimento (Teclas WASD fazem o personagem mover na tela)
         Se o jogador passar da borda, ele aparecerá na borda oposta (pense por exemplo, no jogo Pac-Man.)
        pixel_Width (a escala do personagem) é necessário aqui para posicionar corretamente o personagem na tela em proporção à borda do programa após a reposição.
         */
        public void Mover(Keys tecla, int larguraTela, int alturaTela)
        {
            /*   *Movimentação Vertical*   */
            //Movimentação para cima
            if (tecla == Keys.W || tecla == Keys.Up)
            {
                //Top = Top - Velocidade;
                Top -= Velocidade;
                //Verifica se saiu pela parte de cima da tela
                if (Top + Height < 0 + pixel_Height)
                {
                    Top = alturaTela - Height; //Posição aparece embaixo da tela
                }
            }
            //Movimentação para baixo
            else if (tecla == Keys.S || tecla == Keys.Down)
            {
                Top += Velocidade;
                //Verifica se saiu pela parte de baixo da tela
                if (Top + Height > alturaTela)
                {
                    Top -= alturaTela; //Posição aparece no topo da tela
                }
            }
            /*   *Movimentação Horizontal*   */
            //Movimentação para a esquerda
            else if (tecla == Keys.A || tecla == Keys.Left)
            {
                Left -= Velocidade;
                //Verifica se saiu pela parte da esquerda da tela 
                if (Left + Width <= 0 + pixel_Width)
                {
                    Left = larguraTela - Width; //Posição aparece na direita da tela
                }
            }
            //Movimentação para a direita
            else if (tecla == Keys.D || tecla == Keys.Right)
            {
                Left += Velocidade;
                //Verifica se saiu pela parte da direita da tela
                if (Left + Width > larguraTela)
                {
                    Left -= larguraTela - pixel_Width; //Posição aparece na esquerda da tela 
                }
            }

        }
    }
}
