namespace ProjetoJogo_AlexsanderKaua
{

    public class EnemyQuick : Enemy
    {
        private int direcaoVertical = 1;
        private int direcaoHorizontal = 2;

        public EnemyQuick(int x, int y) : base(x, y, "enemy-spider.png")
        {
            Velocidade = 16;
            Vida = 1;
            Size = new Size(70, 72);
        }

        public override void Mover()
        {
            base.Mover();

            Top += direcaoVertical * 3;
            if (Top <= 0 || Top + Height >= Parent.ClientSize.Height)
            {
                direcaoVertical *= -1;
            }
        }
    }
}
