namespace ProjetoJogo_AlexsanderKaua
{
    //Comentado por Alexsander.

    public class DvD : PictureBox
    {
        /*  Variáveis de Movimento  */
        //Direção
        public string DirecaoX { get; set; } = "dir";
        public string DirecaoY { get; set; } = "bai";
        //Deslocamento
        public int Velocidade { get; set; } = 30; //Pixels que moverá cada vez que o método "mova" for chamado
        public double Aceleracao { get; set; } = 0.05; //inutilizada

        /*  Variáveis de Propriedade  */
        //Tamanho
        public int pixel_Width { get; set; } = 200; //Largura que o controle terá
        public int pixel_Height { get; set; } = 100; //Altura que o controle terá

        public string[] frames { get; set; } = ["dvd_nossa.png", "dvd_nossa2.png", "dvd_nossa3.png"];


        //=Método construtor=
        public DvD()
        {
            /*  Mudar propriedades internas */
            //*Controle
            Size = new System.Drawing.Size(pixel_Width, pixel_Height); //Criar novo Drawing.Size baseado nas variáveis de tamanho. 
                                                                       //*Desenho
            SizeMode = PictureBoxSizeMode.StretchImage;
            string caminhoImagem = Path.Combine(
                Application.StartupPath,
                "Assets/",
                "dvd_nossa.png"
                );
            Image = Image.FromFile(caminhoImagem);
            BackColor = Color.Transparent;

        }

        public void mova(int larguraTela, int alturaTela)
        {

            //=== Movimento Vertical ===
            if (DirecaoY == "cim")
            {
                //Top = Top - Velocidade;
                Top = Top - Velocidade;
                //Verifica se saiu pela parte de cima da tela
                if (Top + Height < 0 + pixel_Height)
                {
                    //Mudar a direção vertical do movimento do DvD para 'baixo'
                    DirecaoY = "bai";
                }
            }
            else if (DirecaoY == "bai")
            {
                Top += Velocidade;
                //Verifica se saiu pela parte de baixo da tela
                if (Top + Height > alturaTela)
                {
                    //Muda a direção vertical do movimento do DvD para 'cima'
                    DirecaoY = "cim";
                }
            }

            //=== Movimento Horizontal ===
            if (DirecaoX == "esq")
            {
                Left -= Velocidade;
                //Verifica se saiu pela parte da esquerda da tela
                if (Left + Width <= 0 + pixel_Width)
                {
                    //Muda a direção horizontal do movimento do DvD para 'direita'

                    DirecaoX = "dir";
                }
            }
            else if (DirecaoX == "dir")
            {
                Left += Velocidade;
                //Verifica se saiu pela parte da direita da tela
                if (Left + Width > larguraTela)
                {
                    //Muda a direção horizontal do movimento do DvD para 'esquerda'
                    DirecaoX = "esq";
                }
            }


        }

    }
}
