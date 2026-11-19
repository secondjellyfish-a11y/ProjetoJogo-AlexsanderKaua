namespace ProjetoJogo_AlexsanderKaua
{
    public partial class Game : Form
    {
        private Player jogador;
        public Game(System.Windows.Forms.Timer gameTimer)
        {
            InitializeComponent();
            this.DoubleBuffered = true; //Ativa o Double Buffer para o Form, evitando que os elementos "piscem" na tela

            IniciarJogo();


            globalTimerFPS = gameTimer;


            //Adiciona os eventos do Timer
            gameTimer.Tick += (sender, e) =>
            {

                // Remove as balas que saíram da tela do formulário
                for (int i = jogador.Balas.Count - 1; i >= 0; i--)
                {
                    Bullet bala = jogador.Balas[i];
                    //Atualizar posição da bala
                    bala.Mover();
                    //Checar se a bala saiu da tela
                    if (bala.SaiuDaTela(ClientSize.Width))
                    {
                        jogador.Balas.RemoveAt(i);
                        Controls.Remove(bala);
                        bala.Dispose();
                    }
                }

            };

            gameTimer.Start();

        }
        private System.Windows.Forms.Timer globalTimerFPS; //Mantém o Timer do outro Form
        private void IniciarJogo()
        {
            //Instanciar um objeto Player;
            jogador = new Player();
            //Definir uma posição para o jogador.
            jogador.Location = new Point(50, 340);
            //Adicionar o Player no formulário;
            Controls.Add(jogador);

            //Ativa a verificação de teclas pressionadas
            KeyPreview = true;
            //Quando a tecla for pressionada, executa o método Game_KeyDown
            KeyDown += Game_KeyDown;
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            //Executa o método mover passando a chave (tecla) pressionada
            jogador.Mover(e.KeyCode, ClientSize.Width, ClientSize.Height);

            if (e.KeyCode == Keys.Space)
            {
                //Colocar um limite de balas do jogador na tela, para não sobrecarregar o jogo
                if (jogador.Balas.Count < 50) { 
                    Bullet disparo = jogador.Atirar();
                Controls.Add(disparo);
                disparo.BringToFront(); // Garante que a bala fique na frente de outros elementos
               }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            /* Desenhar as balas na tela para adicionar camadas próprias 
            * Normalmente, o Windows Forms desenha os controles automaticamente, mas,
            * eles não apresentam transparência, então para as balas não ficarem uma em cima da outra, precisamos desenhá-las manualmente.
            */

            //Plano de fundo (quando tiver fundo)

            //Jogador
            e.Graphics.DrawImage(jogador.Image, jogador.Left, jogador.Top, jogador.Width, jogador.Height);
            //Balas do jogador
            jogador.Balas.ForEach(bala =>
            {
                //Desenha a bala na tela
                e.Graphics.DrawImage(bala.Image, bala.Left, bala.Top, bala.Width, bala.Height);
            });
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
