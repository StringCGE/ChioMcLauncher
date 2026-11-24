namespace ChioLauncher
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
            btnIniciar = new Button();
            btnParar = new Button();
            rtbOutConsole = new RichTextBox();
            SuspendLayout();
            // 
            // btnIniciar
            // 
            btnIniciar.Location = new Point(12, 12);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(75, 23);
            btnIniciar.TabIndex = 0;
            btnIniciar.Text = "Iniciar";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // btnParar
            // 
            btnParar.Location = new Point(93, 12);
            btnParar.Name = "btnParar";
            btnParar.Size = new Size(75, 23);
            btnParar.TabIndex = 1;
            btnParar.Text = "Parar";
            btnParar.UseVisualStyleBackColor = true;
            btnParar.Click += btnParar_Click;
            // 
            // rtbOutConsole
            // 
            rtbOutConsole.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbOutConsole.Location = new Point(12, 59);
            rtbOutConsole.Name = "rtbOutConsole";
            rtbOutConsole.Size = new Size(260, 190);
            rtbOutConsole.TabIndex = 2;
            rtbOutConsole.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 261);
            Controls.Add(rtbOutConsole);
            Controls.Add(btnParar);
            Controls.Add(btnIniciar);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnIniciar;
        private Button btnParar;
        private RichTextBox rtbOutConsole;
    }
}
