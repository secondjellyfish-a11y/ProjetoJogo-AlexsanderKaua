using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoJogo_AlexsanderKaua
{
    //Cria uma classe para servir para desenhar os objetos dinâmicos (controles dinâmicos)
    public class CanvasPanel : Panel
    {
        //É necessário entregar o parente do Painel como parâmetro, e a referência(ref) da lista com os objetos de jogo.
        public CanvasPanel(Control parent)
        {
            this.DoubleBuffered = true;

            Parent = parent;
            //Mudar o tamanho do Canvas para ocupar 100% do form
            Size = Parent.Size;
            BackColor = Color.Transparent;
            Left = 0;
            Top = 0;
            Visible = true; //Não coloque controles dentro de Canvas ao menos que vá desenhá-los manualmente.

        }

        public List<GameObject> ObjectList = new List<GameObject>();

        //Método de desenho do Canvas
        protected override void OnPaint(PaintEventArgs e)
        {
            //NÃO desenha controles.

            if (ObjectList.Count > 0)
            {
                // Desenhar os objetos do jogo na tela
                foreach (GameObject sprite in ObjectList)
                {
                    if (sprite.CurrentFrame != null)
                    {
                        // Desenhar o sprite na tela
                        e.Graphics.DrawImage(sprite.Image, sprite.Left, sprite.Top, sprite.Width, sprite.Height);
                    }
                }
            }
            }
        }
}
