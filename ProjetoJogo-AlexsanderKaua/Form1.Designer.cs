namespace ProjetoJogo_AlexsanderKaua
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lbl_titulo = new Label();
            btn_jogar = new Button();
            lbCornerCounter = new Label();
            SuspendLayout();
            // 
            // lbl_titulo
            // 
            lbl_titulo.AutoSize = true;
            lbl_titulo.BackColor = Color.White;
            lbl_titulo.Font = new Font("Courier New", 67F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_titulo.ForeColor = Color.Orchid;
            lbl_titulo.Location = new Point(319, 163);
            lbl_titulo.Name = "lbl_titulo";
            lbl_titulo.Size = new Size(1260, 144);
            lbl_titulo.TabIndex = 0;
            lbl_titulo.Text = "Bonzi VS. Vírus";
            // 
            // btn_jogar
            // 
            btn_jogar.BackColor = Color.FromArgb(190, 255, 190);
            btn_jogar.Font = new Font("Courier New", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_jogar.ForeColor = Color.MediumSeaGreen;
            btn_jogar.Location = new Point(579, 402);
            btn_jogar.Name = "btn_jogar";
            btn_jogar.Size = new Size(740, 181);
            btn_jogar.TabIndex = 1;
            btn_jogar.Text = "Iniciar Limpeza";
            btn_jogar.UseVisualStyleBackColor = false;
            btn_jogar.Click += btn_jogar_Click;
            // 
            // lbCornerCounter
            // 
            lbCornerCounter.AutoSize = true;
            lbCornerCounter.BackColor = Color.Transparent;
            lbCornerCounter.Font = new Font("Segoe UI", 12F);
            lbCornerCounter.ForeColor = SystemColors.ButtonFace;
            lbCornerCounter.Location = new Point(579, 638);
            lbCornerCounter.Name = "lbCornerCounter";
            lbCornerCounter.Size = new Size(0, 32);
            lbCornerCounter.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Thistle;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1898, 1024);
            Controls.Add(lbCornerCounter);
            Controls.Add(btn_jogar);
            Controls.Add(lbl_titulo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Jogo (?)";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_titulo;
        private Button btn_jogar;
        private Label lbCornerCounter;
    }
}
