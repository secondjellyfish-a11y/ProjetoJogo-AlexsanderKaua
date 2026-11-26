using System.ComponentModel;

namespace ProjetoJogo_AlexsanderKaua
{
    //Comentado por Alexsander.

    public class DvD : GameObject
    {

        
        

        /*  Variáveis de Movimento  */
        //Direção
        public string DirecaoX { get; set; } = "dir";
        public string DirecaoY { get; set; } = "bai";

        //Deslocamento
        public int Velocidade { get; set; } = 30; //Pixels que moverá cada vez que o método "mova" for chamado 

        /*  Variáveis de Propriedade  */
        //Tamanho
        public int PixelWidth { get; set; } = 250; //Largura que o controle terá
        public int PixelHeight { get; set; } = 150; //Altura que o controle terá

        public readonly int TimerInterval = 32;

        public int cornerCounter = 0; //Contador de colisões com os cantos da tela

        //=Método construtor=
        public DvD(Control parent, int velocidadeOffset = 0) : base(250, 150) //Chama o construtor da classe base (GameObject) com os parâmetros de tamanho
        {

            /* Controle "Parente" */
            //O controle parente é o controle que contém o DvD, geralmente o Form
            Parent = parent;

            /*  Mudar propriedades internas */
            //*Desenho

            FrameDirectory = "assets-raw/sprites/"; //Diretório onde os frames (sprites) do DVD estão localizados
            //Adicionar frames totais para a animação do DvD, que são as imagens que serão mostradas na tela
            Frames = new string[] { "dvd.png", "dvd_2.png", "dvd_3.png" };
            //Adicionar endereços de frames usados para a animação do DvD, que são os frames que serão mostrados na tela durante a animação.
            //No caso, são os tres frames totais, então usamos todos eles.
            UsedFrames = new int[] { 0, 1, 2 };

            //Mudar o sprite
            CurrentFrame = Frames[0]; //Mudar o frame atual para o primeiro frame em Frames

            //Mudar posição do DVD após a criação
            //Centralizar o DVD
            Left = Parent.Width / 2 - this.Width;
            Top = Parent.Height / 2 - this.Height;

            Random RandomPosicionamento = new Random();
            //Adicionar direção inicial aleatória
            int r = RandomPosicionamento.Next(0, 4);
            switch (r)
            {
                case 0:
                    DirecaoX = "dir";
                    DirecaoY = "bai";
                    break;
                case 1:
                    DirecaoX = "esq";
                    DirecaoY = "bai";
                    break;
                case 2:
                    DirecaoX = "dir";
                    DirecaoY = "cim";
                    break;
                case 3:
                    DirecaoX = "esq";
                    DirecaoY = "cim";
                    break;
            }   
{
                //Adicionar parâmetro do construtor à velocidade do DVD
                Velocidade += velocidadeOffset;

            }
            /*Não precisamos chamar:
            * Image = Image.FromFile(FindImageFile(CurrentFrame));
            * pois o acessor set de currentFrame já faz isso quando atribuímos para ele, na classe base.
            */


            if (Parent != null)
            {
                //*Checar se o DVD bateu no canto e incrementar um contador
                //Criar um controle de Panel para servir como checador
                Panel p = new Panel();
                int cornerSize = 3; //Tamanho do controle de colisão, não precisa ser grande, apenas um ponto para checar a colisão
                bool cornersAreVisible = true; //Não queremos que o controle seja visível, apenas que sirva como "detector" de colisão, exceto para debug
                Color visibleCornerColor = Color.Aquamarine;
                //Adicionar cantos para uma lista

                // Canto superior esquerdo
                Corners.Add(new Panel
                {
                    Size = new Size(cornerSize, cornerSize),
                    Name = "CantoSuperiorEsquerdo",
                    Visible = cornersAreVisible,
                    BackColor = visibleCornerColor,
                    Left = 0,
                    Top = 0
                }
                );

                // Canto superior direito
                Corners.Add(new Panel
                {
                    Size = new Size(cornerSize, cornerSize),
                    Name = "CantoSuperiorDireito",
                    Visible = cornersAreVisible,
                    BackColor = visibleCornerColor,
                    Left = Parent.Width - cornerSize,
                    Top = 0
                }
                );

                // Canto inferior esquerdo
                Corners.Add(new Panel
                {
                    Size = new Size(cornerSize, cornerSize),
                    Name = "CantoInferiorEsquerdo",
                    Visible = cornersAreVisible,
                    BackColor = visibleCornerColor,
                    Left = 0,
                    Top = Parent.Height - cornerSize
                }
                );

                // Canto inferior direito
                Corners.Add(new Panel
                {
                    Size = new Size(cornerSize, cornerSize),
                    Name = "CantoInferiorDireito",
                    Visible = cornersAreVisible,
                    BackColor = visibleCornerColor,
                    Left = Parent.Width - cornerSize,
                    Top = Parent.Height - cornerSize
                }
                );

                /*Temos de adicionar os cantos como controles filhos do parente do DvD, para que possamos checar colisão com eles.
                 * Só podemos fazer isso depois que o DvD for adicionado a um parente, pois só assim teremos acesso ao Parent do DvD.
                 */
                //Adicionar os cantos como controles filhos do parente do DvD
                foreach (Panel corner in Corners)
                {
                    Parent.Controls.Add(corner); //Adiciona o canto como controle filho do parente do DvD
                }

                //Adicionar o nosso evento com o método AddObjectEvent, para que o método Timer_Tick seja chamado a cada 16 milissegundos
                base.AddObjectEvent(Timer_Tick, TimerInterval);

                //Debug
                System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: O DvD terminou de carregar e foi adicionado ao parente. O método Timer_Tick será chamado a cada {1} milissegundos.", System.DateTime.Now, TimerInterval));
            }
        }

        public List<Panel> Corners = new List<Panel>();

       


        //Método de timer desse objeto
        private void Timer_Tick()
        {
            // Chame o método a repetir, nesse caso o método de movimento.

            
                //Faz o DVD se mover na tela, passando a largura e altura do Form para o método Mova da classe DvD
                Mova(Parent.ClientSize.Width, Parent.ClientSize.Height);

                //Checar se o DVD bateu em algum canto da tela
                foreach (Panel corner in Corners)
                {
                    //Para cada canto, checar se o controle existe no Parent do DvD (o Form) e se é um Panel
                    if (Parent.Controls.Find(corner.Name,true)[0] is Panel canto)
                    {
                        //Controle encontrado, verificar colisão com o canto dentro do parente do DvD (o Form)
                        if (this.Bounds.IntersectsWith(canto.Bounds))
                        {

                            //O DVD bateu no canto
                            base.Animate(); //Chamar o método de animação do DvD, para mudar o frame atual
                            cornerCounter++; //Incrementar o contador de colisões

                            //Mensagem de Debug
                            System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: O DVD bateu no canto da tela.", System.DateTime.Now));
                        }
                    }
                    else
                    {
                        //Não era o controle que esperávamos, mandar mensagem de debug
                        System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: Controle inesperado encontrado.", System.DateTime.Now));
                    }
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


    }


}
