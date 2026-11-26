namespace ProjetoJogo_AlexsanderKaua
{
    //Comentado por Alexsander.
    

    public partial class Form1 : Form
    {

        Random randomizer = new Random();

       
        public Form1()
        {
            InitializeComponent();

            Canvas = new CanvasPanel(this);
            Controls.Add(Canvas);

            //Objetos dinâmicos
            DvD objetoDVD = new DvD(Canvas, randomizer.Next(0,30)); //Colocamos o DVD no centro do Form, com velocidade inicial aleatória
            Canvas.ObjectList.Add(objetoDVD); //Adiciona o objeto DvD para a lista de objetos do jogo, para que possamos manipular ele mais facilmente.

            //Chamar o método de Timer
            SetupTimer();

            //Ativar o Double Buffer, para o objeto DvD e outros não ficarem "piscando" na tela (opções gráficas para o controle do Form)
            this.DoubleBuffered = true;

            


        }

        

        //Objeto privado de Timer (age como Timer global)
        public readonly System.Windows.Forms.Timer globalTimerFPS = new System.Windows.Forms.Timer();
        // Contador para controlar a frequência das ações (frames)
        private int frameCount = 0;

        //Canvas do Form (para desenhar objetos)
        public CanvasPanel Canvas;

        public List<GameObject> GetGameObjects { 
            get {
                return Canvas.ObjectList;
            }  
        } 
        
       

        private void SetupTimer()
        {
            globalTimerFPS.Interval = 32; // Repita a ação a cada 'n' milissegundos, nesse caso 32, que equivale à mais ou menos 30 FPS
            globalTimerFPS.Tick += mainTimer_Tick; //Atribua o método repeatTimer_Tick ao Timer
            globalTimerFPS.Start(); // Faça o Timer iniciar
        }

        //Método que roda a cada 30 FPS, sempre
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            // Chame o método a repetir, nesse caso o método de movimento.

            // Incrementa o contador de frames
            frameCount++;

            bool debugPrintFrames = false;
            if (debugPrintFrames) System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: Current frame: {1}.", System.DateTime.Now,frameCount));

            // Código que roda a cada 32 frames (aprox. 1 vez por segundo)
            if (frameCount % 32 == 0)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: mainTimerTick terminou um ciclo. frameCount foi resetado para 0.", System.DateTime.Now));

                //Imprimir a lista de objetos do jogo (GameObject) para debug
                System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: A lista atual de objetos de jogo (GameObject) é:",System.DateTime.Now));
                int endereco = 0;
                foreach (GameObject obj in GetGameObjects)
                {
                    System.Diagnostics.Debug.WriteLine(String.Format("({0}): GameObject: {1}, Position: ({2},{3}), Size: ({4},{5}), CurrentFrame: {6}",endereco, obj.GetType().Name, obj.Left, obj.Top, obj.Width, obj.Height, obj.CurrentFrame));
                    endereco++;
                }

                
                foreach (GameObject DynamicObj in GetGameObjects) {
                    if (DynamicObj is DvD screensaver) {
                        if(screensaver.cornerCounter > 0) { 
                            //Se o DVD bateu em pelo menos um canto
                        //Chamar a lista de objetos dinâmicos e achar o nosso DvD
                        lbCornerCounter.Text = String.Format("Batidas de canto: {0}", screensaver.cornerCounter);
                        }
                    }
                }


                // Reseta o contador para evitar estouro de memória
                frameCount = 0;
            }

            Canvas.Invalidate(); // Redesenha o Canvas para atualizar APENAS os objetos (se pedir para o Form inteiro redesenhar, gera lag)
        }
        //Método de iniciar o jogo
        private void btn_jogar_Click(object sender, EventArgs e)
        {
            
            //Liberar os objetos usados no Form1, já que não estamos usando ele.
            foreach (var objeto in Canvas.ObjectList)
            {
                //Remove o objeto da memória
                objeto.Dispose();
            }

            //Criar uma nova instância da classe Game e mudar ela para o novo form ativo.
            Game gameInstance = new Game(globalTimerFPS); //Passa o Timer de jogo para o outro form já que não é necessário criar outro
            gameInstance.Show();
            this.Hide(); //Esconder o form original pois não é mais necessário.

            
        }
    }



}
