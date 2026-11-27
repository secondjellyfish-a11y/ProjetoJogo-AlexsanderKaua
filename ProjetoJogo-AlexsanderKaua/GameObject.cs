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

            _images = new Dictionary<string, Image>();
        }

        //***DEBUG***
        protected bool DebugAlertImageChangeAttempts = true; //Se ativado, toda tentativa de mudança de CurrentFrame será alertada no console.
        //***

        protected string FrameDirectory { get; set; } = "Assets/"; //Diretório onde os frames (sprites) do objeto do jogo estão localizados
        public string[] FrameSheet //Array de frames (strings de imagem) do objeto do jogo, com todos os frames disponíveis para animação no total.
        {
            get => _frameSheet;
            set
            {
                if (ReferenceEquals(_frameSheet, value)) return;   // Se o Array for igual a nova array, retornar.

                foreach (var img in _images.Values) img.Dispose(); //*Descarta imagens antigas (se este objeto é dono delas)
                _images.Clear(); //Limpa o dicionário

                _frameSheet = value; //Atribui à propriedade privada
                if (value == null) return; //*Se o intuito era apagar frameSheet, interromper o código.

                //*Senão,
                //Para cada nome distinto na lista, 
                foreach (var nome in value.Distinct())
                {
                    //*Achar uma Imagem baseado nas entradas de string, e adicioná-las ao dicionário _images.
                    string path = GameObjectUtils.FindImageFile(nome, FrameDirectory);
                    if (File.Exists(path)) //**Se arquivo existe,
                        _images[nome] = Image.FromFile(path); //Adicionar imagem à _images
                    else //**
                        System.Diagnostics.Debug.WriteLine($"Frame não encontrado: '{path}'"); //Mensagem de DEBUG
                }
            }
        }
        public string[] AnimationFrames { get; set; } = []; //Array de frames (strings de imagem) do objeto do jogo que estão sendo usados atualmente (para animação)

        private string[] _frameSheet;
        private string _currentFrame;
        protected Dictionary<string, Image> _images;   // Dicionário com: nome → Image carregada


        protected List<IDisposable> UsedTickSubscriptions = new();

        public int FrameTurnSpeed = 30; //Frames até mudar de frame novamente
        public string CurrentFrame
        {
            get => _currentFrame;
            set
            {
                //*Se o frame atual for diferente do frame que queremos mudar, mudar o frame atual para o novo frame, e mudar a imagem do objeto do jogo para a imagem correspondente ao novo frame.

                if (_currentFrame == value) return; //Se o frame atual for o mesmo que o frame que queremos mudar, não fazer nada
                if (_images.TryGetValue(value, out var img) == false) //*Se o frame que queremos mudar não existe no dicionário de imagens, não fazer nada
                {
                    // Frame inexistente — log no console e return
                    //**DEBUG**
                    if (DebugAlertImageChangeAttempts) System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: Tentativa de mudar para frame inexistente '{1}' em GameObject do tipo {2}. Frame atual: '{3}'.", System.DateTime.Now, value, GetType().Name, _currentFrame));
                    //-***-
                    return;
                }
                //*Senão, atribuir valor para o frame atual, mudando a imagem, e editar a propriedade privada respectiva.
                _currentFrame = value;
                Image = img;

                //**DEBUG**
                if (DebugAlertImageChangeAttempts) System.Diagnostics.Debug.WriteLine(String.Format("[{0}]: Mudando frame de GameObject do tipo {1} para '{2}'.", System.DateTime.Now, GetType().Name, _currentFrame));
                //-***-
            }
        }

        // Guarda os retângulos da posição ANTES do último movimento
        public List<Rectangle> PreviousBounds { get; } = new List<Rectangle>();

        public Rectangle Bounds => new Rectangle((int)Left, (int)Top, Width, Height);

        private float _left, _top;

        public float Left
        {
            get => _left;
            set { PreviousBounds.Add(Bounds); _left = value; }
        }
        public float Top
        {
            get => _top;
            set { PreviousBounds.Add(Bounds); _top = value; }
        }
        public float Right
        {
            get => _left + Width;
            set => Left = value - Width;
        }
        public float Bottom
        {
            get => _top + Height;
            set => Top = value - Height;
        }

        //Sobrescreve o método Dispose para remover eventos de tick usados por esse objeto do jogo,
        //para que eles não continuem rodando após o objeto ser destruído, e fiquem gastando memória.
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



        protected IDisposable AddObjectEvent(Action timerAction, int intervalMs)
        {
            //Chama o método estático AddTickEvent da classe GameObjectUtils para adicionar um evento de tick usado por esse objeto do jogo,
            //e retorna a assinatura do evento para que ele possa ser removido quando o objeto for destruído.
            var subscription = GameObjectUtils.AddTickEvent(timerAction, intervalMs);
            //Adiciona um evento de tick usado por esse objeto do jogo, para que ele possa ser removido quando o objeto for destruído.
            UsedTickSubscriptions.Add(subscription);
            return subscription;
        }

        //Método de animação do objeto do jogo, que muda o frame atual para o próximo frame em AnimationFrames (frames usados no momento para animação)
        protected void TurnFrameForward()
        {
            //Método de animação do objeto do jogo, que muda o frame atual para o próximo frame em AnimationFrames
            for (int i = 0; i <= AnimationFrames.Length - 1; i++)
            {
                if (AnimationFrames.Length > 0)
                {
                    //Se o frame atual for o último frame em AnimationFrames, mudar para o primeiro frame em AnimationFrames, senão mudar para o próximo frame em AnimationFrames
                    if (i == AnimationFrames.Length - 1)
                    {
                        CurrentFrame = AnimationFrames[0];
                        break;
                    }
                    else
                    {
                        //Se o frame atual for um frame em AnimationFrames, e não for o último,
                        if (CurrentFrame == AnimationFrames[i])
                        {
                            //Avançar uma casa nos frames de animação
                            CurrentFrame = AnimationFrames[i + 1];
                            break;
                        }
                    }
                }
            }
        }



        //Classe estática de utilidades para objetos do jogo, que contém métodos úteis para todos os objetos do jogo.
        public static class GameObjectUtils
        {

            //Classe simples que representa um evento de tick do Timer que já está rodando, e pode ser cancelado com Dispose() quando não for mais necessário.
            private sealed class TickSubscription(Action unsubscribe) : IDisposable
            {
                //unsubscribe deve ser criado com um método de "() => timer.Tick -= handler",
                //então quando chamamos Dispose(), ele remove o evento do Timer, para que ele não seja mais chamado.
                private Action? _unsubscribe = unsubscribe;

                //Remove o evento do Timer, para que ele não seja mais chamado.
                public void Dispose()
                {
                    _unsubscribe?.Invoke(); //Invoca o método de unsubscribe, que remove o evento do Timer
                    _unsubscribe = null; //Remove o método, caso seja chamado de novo.
                }
            }

            //Cria um Timer que pode ser usado por todos os objetos do jogo, para animação e movimentação.
            private static System.Windows.Forms.Timer timer;
            private static readonly int tickInterval = 32; //Intervalo de ticks do Timer, em milissegundos (aproximadamente 30 FPS)


            //Construtor estático da classe GameObjectUtils, que inicializa o Timer global para objetos do jogo.
            static GameObjectUtils()
            {
                timer = new System.Windows.Forms.Timer();
                timer.Interval = tickInterval; // Aproximadamente 30 FPS
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


        }
    }

}
