namespace ProjetoJogo_AlexsanderKaua
{
    public partial class Game : Form
    {
        private Player jogador;
        private List<Enemy> enemyList = new List<Enemy>();
        private Random random = new Random();
        private int contadorSpawn = 0;

        //Mantém o Timer do outro Form
        private System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();

        public Game()
        {
            InitializeComponent();
            this.DoubleBuffered = true; //Ativa o Double Buffer para o Form, evitando que os elementos "piscem" na tela

            IniciarJogo();

            gameTimer.Interval = 32; // Repita a ação a cada 'n' milissegundos, nesse caso 32, que equivale à mais ou menos 30 FPS
            gameTimer.Tick += Timer_Tick; //Atribua o método repeatTimer_Tick ao Timer


            //Iniciar o Timer (começa a contar o tempo e executar os eventos a cada tick)
            gameTimer.Start();
        }


        //Evento Global do Timer
        public void Timer_Tick(object sender, EventArgs e)
        {

            LimparProjeteis(); //Limpa as balas do jogador que saíram da tela


            //Criar novos inimigos
            //Gera um novo inimigo a cada 50 ticks
            contadorSpawn++; //Incrementar a variável do tempo
            if (contadorSpawn >= 50)
            {
                //Esperar um intervalo de tempo (contadorSpawn) até 
                CriarInimigo();
                contadorSpawn = 0;
            }
            //Movimentação e Colisões do Inimigo
            for (int i = enemyList.Count - 1; i >= 0; i--)
            {
                Enemy inimigo = enemyList[i];
                inimigo.Mover();

                //Verificar se saiu da tela
                if (inimigo.SaiuDaTela())
                {
                    RemoverInimigo(inimigo);
                    continue; //Continue por algum motivo até 
                }
                //Colisão disparo x inimigo
                for (int j = jogador.Balas.Count - 1; j >= 0; j--)
                {
                    //Faz o inimigo tomar dano
                    Bullet disparo = jogador.Balas[j];
                    //Se disparo intersecta com inimigo...
                    if (disparo.Bounds.IntersectsWith(inimigo.Bounds))
                    {
                        inimigo.ReceberDano();
                        RemoverBala(disparo);
                        break;
                    }
                }
                //Verificar se inimigo morreu
                if (inimigo.EstaMorto())
                {
                    RemoverInimigo(inimigo);
                    continue;
                }
                //Colisão inimigo x jogador
                if (inimigo.Bounds.IntersectsWith(jogador.Bounds))
                {
                    //O jogador toma dano (perde vida)
                    jogador.Vida--;
                    RemoverInimigo(inimigo);

                    if (jogador.Vida <= 0)
                    {
                        this.BackColor = Color.DarkRed; //Red de SANGUEEEEEEEEEEEEE
                        gameTimer.Stop();
                        MessageBox.Show("GAME OVER (tradução: você perdeu)");
                    }
                }

            }
        }
        public void LimparProjeteis()
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
                    //Chama o método de remover a bala da memória interna do jogo
                    RemoverBala(bala);

                }
            }
        }
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
                if (jogador.Balas.Count < 50)
                {
                    Bullet disparo = jogador.Atirar();
                    Controls.Add(disparo);
                    disparo.BringToFront(); // Garante que a bala fique na frente de outros elementos
                }
            }
        }

        private void CriarInimigo()
        {
            int x = ClientSize.Width;
            int y = random.Next(0, ClientSize.Height - 100);

            Enemy inimigo;
            if (random.Next(0, 100) < 30)
            {
                inimigo = new EnemyQuick(x, y);
            }
            else
            {
                inimigo = new Enemy(x, y);
            }

            enemyList.Add(inimigo); //Adiciona o inimigo em EnemyList
            Controls.Add(inimigo); //Adiciona o inimigo a Controles
        }

        private void RemoverInimigo(Enemy inimigo)
        {
            //Remover o inimigo da memória do jogo (coitado)
            Controls.Remove(inimigo);
            enemyList.Remove(inimigo);
            inimigo.Dispose();
        }

        private void RemoverBala(Bullet bala)
        {
            //Remover a bala da memória do jogo 
            Controls.Remove(bala);
            jogador.Balas.Remove(bala);
            bala.Dispose();
        }


        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
