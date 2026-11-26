namespace ProjetoJogo_AlexsanderKaua
{

    public partial class Form1 : Form
    {

        //Comentado por Alexsander.
        public Form1()
        {

            InitializeComponent();

            //Chamar o método de Timer
            SetupTimer();

            //Ativar o Double Buffer, para o objeto DvD e outros não ficarem "piscando" na tela (opções gráficas para o controle do Form)
            this.DoubleBuffered = true;

            this.Controls.Add(estimulation); //Adiciona o objeto DvD na tela do Form
        }



        //Objeto privado de Timer (age como Timer global)
        public readonly System.Windows.Forms.Timer globalTimerFPS = new System.Windows.Forms.Timer();
        // Contador para controlar a frequência das ações (frames)
        private int frameCount = 0;


        public DvD estimulation = new DvD(); //Cria um novo OBJETO da classe avulsa DVD que vem de DVD.cs

        private void SetupTimer()
        {
            globalTimerFPS.Interval = 32; // Repita a ação a cada 'n' milissegundos, nesse caso 32, que equivale à mais ou menos 30 FPS
            globalTimerFPS.Tick += new EventHandler(mainTimer_Tick); //Atribua o método repeatTimer_Tick ao Timer
            globalTimerFPS.Start(); // Faça o Timer iniciar
        }

        //Método que roda a cada 30 FPS, sempre
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            // Chame o método a repetir, nesse caso o método de movimento.

            // Incrementa o contador de frames
            frameCount++;

            // Código que roda a cada 2 frames 
            if (frameCount % 2 == 0)
            {
                //Não precisamos chamar o método do DVD, já que tem um timer interno
            }

            // Código que roda a cada 32 frames (aprox. 1 vez por segundo)
            if (frameCount % 32 == 0)
            {
                System.Diagnostics.Debug.WriteLine("frameCount was reset to 0.");
                // Reseta o contador para evitar estouro de memória
                frameCount = 0;
            }
        }
        //Método de iniciar o jogo
        private void btn_jogar_Click(object sender, EventArgs e)
        {
            //Criar uma nova instância da classe Game e mudar ela para o novo form ativo.
            Game gameInstance = new Game(globalTimerFPS); //Passa o Timer de jogo para o outro form já que não é necessário criar outro
            gameInstance.Show();
            this.Hide(); //Esconder o form original pois não é mais necessário.
        }
    }



}
