namespace ChioLauncher.ItemSelected
{
    partial class UC_ItemFolder
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            lb_Name = new Label();
            btn_Get = new Button();
            SuspendLayout();
            // 
            // lb_Name
            // 
            lb_Name.AutoSize = true;
            lb_Name.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lb_Name.Location = new Point(35, 6);
            lb_Name.Name = "lb_Name";
            lb_Name.Size = new Size(53, 17);
            lb_Name.TabIndex = 0;
            lb_Name.Text = "-Name-";
            lb_Name.MouseClick += UC_ItemFolder_MouseClick;
            // 
            // btn_Get
            // 
            btn_Get.Location = new Point(3, 4);
            btn_Get.Name = "btn_Get";
            btn_Get.Size = new Size(26, 23);
            btn_Get.TabIndex = 1;
            btn_Get.Text = "G";
            btn_Get.UseVisualStyleBackColor = true;
            btn_Get.Click += btn_Get_Click;
            // 
            // UC_ItemFolder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            Controls.Add(btn_Get);
            Controls.Add(lb_Name);
            Name = "UC_ItemFolder";
            Size = new Size(362, 30);
            MouseClick += UC_ItemFolder_MouseClick;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lb_Name;
        private Button btn_Get;
    }
}
