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
        public int PixelWidth { get; set; } = 200; //Largura que o controle terá
        public int PixelHeight { get; set; } = 100; //Altura que o controle terá

        private System.Windows.Forms.Timer thisTimer = new System.Windows.Forms.Timer();
        private int frameCount = 0;
        private int TimerInterval = 32;


        public string[] Frames { get; set; } = ["dvd_nossa.png", "dvd_nossa2.png", "dvd_nossa3.png"];
        public string currentFrame { get; set; }
        public int FrameTurnSpeed = 30; //Frames até mudar de frame novamente




        //=Método construtor=
        public DvD()
        {
            /*  Mudar propriedades internas */

            //Ativar o timer
            thisTimer.Interval = TimerInterval;
            thisTimer.Tick += internalTimer_Tick;
           

            currentFrame = Frames[0]; //Mudar o frame atual para o primeiro frame em Frames

            //*Controle
            Size = new System.Drawing.Size(PixelWidth, PixelHeight); //Criar novo Drawing.Size baseado nas variáveis de tamanho. 
                                                                     //*Desenho
            SizeMode = PictureBoxSizeMode.StretchImage;

            Image = Image.FromFile(FindImageFile(currentFrame));
            BackColor = Color.Transparent;

 thisTimer.Start();
        }

        

        private string FindImageFile(string name)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;

            return Path.Combine(Application.StartupPath, "Assets/", name);
        }
        //Método que roda a cada 30 FPS, sempre
        private void internalTimer_Tick(object sender, EventArgs e)
        {
            // Chame o método a repetir, nesse caso o método de movimento.

            // Incrementa o contador de frames
            frameCount++;

            // Código que roda a cada 2 frames 
            if (frameCount % 2 == 0)
            {
                //Faz o DVD se mover na tela, passando a largura e altura do Form para o método Mova da classe DvD
                Mova(Parent.ClientSize.Width, Parent.ClientSize.Height);
            }

            //Código que roda a cada 8 frames 
            if (frameCount % 8 == 0)
            {
                animate();
            }

            // Código que roda a cada 32 frames (aprox. 1 vez por segundo)
            if (frameCount % 32 == 0)
            {
                // Reseta o contador para evitar estouro de memória
                frameCount = 0;
            }
        }

        public void Mova(int larguraTela, int alturaTela)
        {

            //=== Movimento Vertical ===
            if (DirecaoY == "cim")
            {
                //Top = Top - Velocidade;
                Top = Top - Velocidade;
                //Verifica se saiu pela parte de cima da tela
                if (Top + Height < 0 + PixelHeight)
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
                if (Left + Width <= 0 + PixelWidth)
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

            this.Refresh(); //Atualiza o controle, para que o movimento seja visível na tela

        }

        private void animate()
        {

            for (int i = Frames.Length - 1; i <= 0; i--)
            {
                if (currentFrame == Frames[i])
                {
                    if (Array.IndexOf(Frames, currentFrame) == 2)
                    {
                        currentFrame = Frames[0];
                    }
                    else
                    {
                        currentFrame = Frames[i + 1];
                    }
                }
            }
        }
    }


}
