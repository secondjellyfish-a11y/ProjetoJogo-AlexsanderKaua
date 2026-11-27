using ProjetoJogo_AlexsanderKaua;

public class CanvasPanel : Panel
{
    public List<GameObject> ObjectList { get; } = new List<GameObject>();

    public CanvasPanel(Image? backgroundImage = null)
    {
        DoubleBuffered = true;
        Dock = DockStyle.Fill;
        BackColor = Color.Black;

        BackgroundImageLayout = ImageLayout.Stretch;

        //Opção de adicionar imagem de fundo.
        if (backgroundImage != null) this.BackgroundImage = backgroundImage;
    }

    //Método de desenho do Canvas
    //!! Não chama o método base base.OnPaint(e), então esse painel não desenhará controles atribuídos a Controls.
    protected override void OnPaint(PaintEventArgs e)
    {
        //Desenha objetos primeiro, depois controles.


        /***/
        if (ObjectList.Count > 0)
        {
            // Desenhar os objetos do jogo na tela
            foreach (GameObject sprite in ObjectList)
            {
                //Para cada sprite na lista de objetos,
                //Se o sprite não tem um frame porque o frame atual (CurrentFrame) é nulo, continuar o loop.
                if (string.IsNullOrEmpty(sprite.CurrentFrame)) continue;

                //Senão,
                //*Desenhar o sprite na tela se existir,
                //Se a imagem for do mesmo tamanho que o controle, desenhar como está
                if (sprite.Width == sprite.Image.Width && sprite.Height == sprite.Image.Height)
                    e.Graphics.DrawImage(sprite.Image, sprite.Left, sprite.Top); //Desenhar imagem
                else
                {
                    //Senão, redimensionar a imagem e então desenhar
                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    e.Graphics.DrawImage(sprite.Image, sprite.Left, sprite.Top, sprite.Width, sprite.Height); //Desenhar imagem
                }



            }
        }


    }

}
