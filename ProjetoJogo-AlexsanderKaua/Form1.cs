namespace ProjetoJogo_AlexsanderKaua
{
    //Comentado por Alexsander.


    public partial class Form1 : Form
    {

        Random randomizer = new Random();


        public Form1()
        {
            InitializeComponent();

            Canvas = new CanvasPanel(this.BackgroundImage);
            this.BackgroundImage = null; //Evita o form de pintar a imagem de fundo desnecessariamente

            //Adicionar o Canvas aos Controles do FORM, o Canvas aparece atrás.
            Controls.Add(Canvas);

            //*Objetos dinâmicos
            //Todo objeto animado que precisa de um evento de Timer é tecnicamente "dinâmico"
            DvD objetoDVD = new DvD(Canvas, randomizer.Next(0, 10)); //Colocamos o DVD no centro do Form, com velocidade inicial levemente aleatória
            Canvas.ObjectList.Add(objetoDVD); //Adiciona o objeto DvD para a lista de objetos do jogo dentro de Canvas, para que possamos desenhá-lo.

            //Chamar o método de Timer
            SetupTimer();

        }


        //Objeto privado de Timer (age como Timer global)
        public readonly System.Windows.Forms.Timer globalTimerFPS = new System.Windows.Forms.Timer();
        // Contador para controlar a frequência das ações (frames)
        private int frameCount = 0;

        //Canvas do Form (para desenhar objetos)
        public CanvasPanel Canvas { get; }





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


            //***DEBUG***
            bool debugPrintFrames = false; //Ligar/Desligar debug de frames (imprime no console a cada frame e a cada compleção de ciclo)
            bool debugPrintCycles = false; //Ligar/Desligar debug de ciclos (imprime no console a cada 32 frames, que é o ciclo completo do Timer)
            //Isso vai causar lag se ligar.
            if (debugPrintFrames) System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: Current frame: {1}.", System.DateTime.Now, frameCount));

            // Código que roda a cada 32 frames (aprox. 1 vez por segundo)
            if (frameCount == 32)
            {
                //**DEBUG**
                if (debugPrintCycles)
                {
                    //Imprimir que o ciclo terminou
                    System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: mainTimerTick terminou um ciclo. frameCount foi resetado para 0.", System.DateTime.Now));
                    //Imprimir a lista de objetos do jogo (GameObject) para debug
                    System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: A lista atual de objetos de jogo (GameObject) é:", System.DateTime.Now));
                }
                //-***-

                int endereco = 0;
                //Para cada objeto dinâmico na lista de objetos...
                foreach (GameObject gameObj in Canvas.ObjectList)
                {
                    //**DEBUG**
                    if (debugPrintCycles)
                    {
                        System.Diagnostics.Debug.WriteLine(String.Format("({0}): GameObject: {1}, Position: ({2},{3}), Size: ({4},{5}), CurrentFrame: {6}", endereco, gameObj.GetType().Name, gameObj.Left, gameObj.Top, gameObj.Width, gameObj.Height, gameObj.CurrentFrame));
                        endereco++;
                    }
                    //-***-

                    //Outros
                    if (gameObj is DvD screensaver)
                    {
                        //Se o objeto for do tipo DvD, checar se ele bateu em algum canto da tela e atualizar o label de contador de cantos.
                        if (screensaver.cornerCounter > 0)
                        {
                            //Se o DVD bateu em pelo menos um canto, e o número de batidas mudou
                            if (lbCornerCounter.Text != $"Batidas de canto: {screensaver.cornerCounter}")
                                lbCornerCounter.Text = $"Batidas de canto: {screensaver.cornerCounter}"; //Achar a propriedade do nosso DvD, e mudar o label.
                        }
                    }

                }





                // Reseta o contador para evitar estouro de memória
                frameCount = 0;
            }

            //**DEBUG**
            //Imprimir lista de 
            //--


            //***Redesenhar Objetos***
            //A cada tick, redesenhamos os objetos.

            Rectangle aRedesenhar = Rectangle.Empty;

            foreach (var objeto in Canvas.ObjectList)
            {
                foreach (var area in objeto.PreviousBounds)
                    aRedesenhar = Rectangle.Union(aRedesenhar, area);

                objeto.PreviousBounds.Clear();

                aRedesenhar = Rectangle.Union(aRedesenhar, objeto.Bounds);
            }

            if (!aRedesenhar.IsEmpty)
                Canvas.Invalidate(aRedesenhar);
        }

        //Método de iniciar o jogo
        private void btn_jogar_Click(object sender, EventArgs e)
        {
            //Parar o Timer
            globalTimerFPS.Stop();

            //Liberar os objetos usados no Form1, já que não estamos usando ele.
            foreach (var objeto in Canvas.ObjectList) objeto.Dispose();
            Canvas.ObjectList.Clear();

            //Criar uma nova instância da classe Game e mudar ela para o novo form ativo.
            Game gameInstance = new Game();
            gameInstance.Show();
            this.Hide(); //Esconder o form original pois não é mais necessário.


        }

        //Método de evento quando o texto do label lbCornerCounter muda.
        private void lbCornerCounter_TextChanged(object sender, EventArgs e)
        {
            //*Quando mudar o texto do label, ver se tem texto, se tiver, deixar o controle visível, caso contrário, invisível
            if (String.IsNullOrWhiteSpace(lbCornerCounter.Text) == false)
                lbCornerCounter.Visible = true;
            else lbCornerCounter.Visible = false;
        }
    }



}
