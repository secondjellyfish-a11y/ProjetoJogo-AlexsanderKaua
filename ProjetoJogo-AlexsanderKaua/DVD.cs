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
        public int PixelWidth { get; set; } = 250; //Largura que o controle terá
        public int PixelHeight { get; set; } = 150; //Altura que o controle terá

        private System.Windows.Forms.Timer thisTimer = new System.Windows.Forms.Timer();
        private int frameCount = 0;
        private int TimerInterval = 32;


        public string[] Frames { get; set; } = ["dvd_nossa.png", "dvd_nossa2.png", "dvd_nossa3.png"];
        private string _currentFrame; 
        public string CurrentFrame { 
            get => _currentFrame; 
            set { 
                //Quando a propriedade "currentFrame" for mudada, ela automaticamente procurará a imagem e mudará o programa para ela.
                string imgPath = FindImageFile(value);
                //Checar se arquivo existe
                if (File.Exists(imgPath)) { 
                    //Se existe, atribuir o nome do arquivo (sem toda a estrutura do diretório) a propriedade do frame atual.
                    _currentFrame = value;
                    //Mudar Imagem do controle para o valor
                    Image = Image.FromFile(imgPath);

                    //Mandar mensagem de debug
                    System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: Mudou com sucesso o frame atual de um objeto DVD para uma imagem inexistente de nome '{1}'.", System.DateTime.Now, value));
                } 
                else
                {
                    //Senão, não fazer nada e mandar mensagem no console de DEBUG
                    System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: Tentou mudar frame atual de um objeto DVD para uma imagem inexistente de nome '{1}'. O frame não foi mudado. Frame atual: {2}", System.DateTime.Now, value, CurrentFrame));
                }
            } 
        }
        public int FrameTurnSpeed = 30; //Frames até mudar de frame novamente




        //=Método construtor=
        public DvD()
        {
            /*  Mudar propriedades internas */

            //Ativar o timer
            thisTimer.Interval = TimerInterval;
            thisTimer.Tick += Timer_Tick;
           
            

            //*Controle
            Size = new System.Drawing.Size(PixelWidth, PixelHeight); //Criar novo Drawing.Size baseado nas variáveis de tamanho. 
            //*Desenho
            //Mudar o sprite
            CurrentFrame = Frames[0]; //Mudar o frame atual para o primeiro frame em Frames
            SizeMode = PictureBoxSizeMode.StretchImage;

            /*Não precisamos chamar:
            * Image = Image.FromFile(FindImageFile(CurrentFrame));
            * pois o acessor set de currentFrame já faz isso quando atribuímos para ele.
            */
            BackColor = Color.Transparent;

 thisTimer.Start();
        }

        

        private string FindImageFile(string name)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;

            return Path.Combine(Application.StartupPath, "Assets/", name);
        }

        //Método de timer desse objeto
        private void Timer_Tick(object sender, EventArgs e)
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

            for (int i = 0; i <= Frames.Length - 1; i++)
            {
                if (CurrentFrame == Frames[i])
                {
                    if (i == 2)
                    {
                        CurrentFrame = Frames[0];
                        break;
                    }
                    else
                    {
                        CurrentFrame = Frames[i + 1];
                        break;
                    }
                }
            }
        }
    }


}
