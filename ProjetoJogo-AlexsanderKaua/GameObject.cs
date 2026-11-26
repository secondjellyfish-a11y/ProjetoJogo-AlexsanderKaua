namespace ProjetoJogo_AlexsanderKaua
{
    public class GameObject : PictureBox
    {
        //Propriedades do objeto do jogo
        public int speedX { get; set; } = 0; //Velocidade de movimento horizontal
        public int speedY { get; set; } = 0; //Velocidade de movimento vertical

        //Construtor do objeto do jogo
        public GameObject(int sizeX = 0, int sizeY = 0)
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage; //Definir o modo de redimensionamento da imagem
            this.BackColor = Color.Transparent; //Definir a cor de fundo como transparente
            this.Size = new Size(sizeX, sizeY);
            this.Visible = false; //Inicialmente invisível, para que possamos controlar quando ele aparece na tela
        }

        protected string FrameDirectory { get; set; } = "Assets/"; //Diretório onde os frames (sprites) do objeto do jogo estão localizados
        protected string[] Frames { get; set; } = []; //Array de frames (sprites) do objeto do jogo, com todos os frames disponíveis para animação no total.
        public int[] UsedFrames { get; set; } = []; //Array de endereços de frames (números inteiros) do objeto do jogo que estão sendo usados atualmente (para animação)
        private string _currentFrame;

        protected List<IDisposable> UsedTickSubscriptions = new();

        public int FrameTurnSpeed = 30; //Frames até mudar de frame novamente
        public string CurrentFrame
        {
            get => _currentFrame;
            set
            {
                //Quando a propriedade "currentFrame" for mudada, ela automaticamente procurará a imagem e mudará o programa para ela, no diretório especificado na propriedade FrameDirectory.
                string imgPath = GameObjectUtils.FindImageFile(value,FrameDirectory);
                //Checar se arquivo existe
                if (File.Exists(imgPath))
                {
                    //Se existe, atribuir o nome do arquivo (sem toda a estrutura do diretório) a propriedade do frame atual.
                    _currentFrame = value;
                    //Mudar Imagem do controle para o valor
                    Image = Image.FromFile(imgPath);

                    //Mandar mensagem de debug
                    System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: Mudou com sucesso o frame atual de um objeto de jogo para uma imagem de nome '{1}'.", System.DateTime.Now, value));
                }
                else
                {
                    //Senão, não fazer nada e mandar mensagem no console de DEBUG
                    System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: Tentou mudar frame atual de um objeto de jogo para uma imagem inexistente do caminho '{1}'. O frame não foi mudado. Frame atual: {2}", System.DateTime.Now, imgPath, CurrentFrame));
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            //Se livra dos eventos de tick usados por esse objeto do jogo, para que eles não continuem rodando após o objeto ser destruído.
            if (disposing)
            {
                // Remover cada evento de tick usado por esse objeto do jogo
                foreach (var subscription in UsedTickSubscriptions)
                {
                    subscription.Dispose();
                }
                UsedTickSubscriptions.Clear();
            }
            base.Dispose(disposing);
        }

        protected void AddObjectEvent(Action timerAction, int interval)
        {
            var subscription = GameObjectUtils.AddTickEvent(timerAction, interval);
            //Adiciona um evento de tick usado por esse objeto do jogo, para que ele possa ser removido quando o objeto for destruído.
            UsedTickSubscriptions.Add(subscription);
        }

        protected void Animate()
        {
            int currentFrameIndex = Array.IndexOf(Frames, CurrentFrame);
            //Método de animação do objeto do jogo, que muda o frame atual para o próximo frame em UsedFrames
            for (int i = 0; i <= UsedFrames.Length - 1; i++)
            {
                if (currentFrameIndex == UsedFrames[i])
                {
                    //Se o frame atual for o último frame em UsedFrames, mudar para o primeiro frame em UsedFrames, senão mudar para o próximo frame em UsedFrames
                    if (i == UsedFrames.Length - 1)
                    {
                        CurrentFrame = Frames[UsedFrames[0]];
                        break;
                    }
                    else
                    {
                        CurrentFrame = Frames[UsedFrames[i + 1]];
                        break;
                    }
                }
            }
        }

        //Classe simples que representa um evento de tick do Timer que já está rodando, e pode ser cancelado com Dispose() quando não for mais necessário.
        protected sealed class TickSubscription : IDisposable
        {
            private Action _unsubscribe;

            //Construtor
            public TickSubscription(Action unsubscribe) => _unsubscribe = unsubscribe;

            //Remove o evento do Timer, para que ele não seja mais chamado.
            public void Dispose() { _unsubscribe?.Invoke(); _unsubscribe = null; }
        }

        //Classe estática de utilidades para objetos do jogo, que contém métodos úteis para todos os objetos do jogo.
        public static class GameObjectUtils
        {

            //Cria um Timer que pode ser usado por todos os objetos do jogo, para animação e movimentação.
            private static System.Windows.Forms.Timer timer;
            private static readonly int tickInterval = 32; //Intervalo de ticks do Timer, em milissegundos (aproximadamente 30 FPS)

            private static int _tickCounter = 0; //Contador de ticks do Timer, para controlar a frequência das ações (frames)

            static GameObjectUtils()
            {
                timer = new System.Windows.Forms.Timer();
                timer.Interval = tickInterval; // Aproximadamente 30 FPS
                timer.Tick += Base_Tick; //Adiciona o método Base_Tick ao evento Tick do Timer
                timer.Start();
            }

            //Método de encontrar arquivo de imagem no diretório Assets
            public static string FindImageFile(string name, string directory = "Assets/")
            {
                if (string.IsNullOrEmpty(name)) return string.Empty;

                return Path.Combine(Application.StartupPath, directory, name);
            }

            public static IDisposable AddTickEvent(Action timerAction, int interval)
            {
                int elapsed = 0;
                EventHandler handler = null;

                handler = (sender, e) =>
                {
                    elapsed += tickInterval;
                    if (elapsed >= interval)
                    {
                        elapsed -= interval;   // Preserva o resto para o próximo ciclo
                        timerAction?.Invoke();
                    }
                };
                timer.Tick += handler;
                return new TickSubscription(() => timer.Tick -= handler);
            }
           

            

            //Método privado, é o método que roda a cada tick do Timer, e chama todos os eventos adicionados ao Timer.
            static private void Base_Tick(object sender, EventArgs e)
            {
                // Incrementa o contador de ticks
                _tickCounter++;
                // Código que roda a cada 32 ticks (aprox. 1 vez por segundo)
                if (_tickCounter % tickInterval == 0)
                {
                    // Reseta o contador para evitar estouro de memória
                    _tickCounter = 0;
                }
            }


        }
    }

}
