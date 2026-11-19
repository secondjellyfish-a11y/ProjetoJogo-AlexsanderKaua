namespace ProjetoJogo_AlexsanderKaua
{
    public partial class Game : Form
    {
        private Player jogador;
        public Game(System.Windows.Forms.Timer gameTimer)
        {
            InitializeComponent();
            IniciarJogo();

            globalTimerFPS = gameTimer;
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
        }
        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
