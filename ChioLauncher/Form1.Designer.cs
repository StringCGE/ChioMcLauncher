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
            rtb_OutConsole = new RichTextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabControl2 = new TabControl();
            tabP_Llm = new TabPage();
            btn_ForceStop = new Button();
            btnn_StartModel = new Button();
            btn_GoToWeb = new Button();
            tb_LlmUrl = new TextBox();
            tabControl3 = new TabControl();
            tabP_InferenceEngine = new TabPage();
            flp_InferenceEngines = new FlowLayoutPanel();
            lb_InferenceEngineSelected = new Label();
            label3 = new Label();
            label2 = new Label();
            btnFindInferenceEnginesFolder = new Button();
            tb_InferenceEnginesFolder = new TextBox();
            tabP__Models = new TabPage();
            flp_Models = new FlowLayoutPanel();
            lb_ModelSelected = new Label();
            label4 = new Label();
            tb_ModelsFolder = new TextBox();
            label1 = new Label();
            btnFindModelsFolder = new Button();
            tabP_Minecraft = new TabPage();
            tabP_Console = new TabPage();
            fbd_ = new FolderBrowserDialog();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabControl2.SuspendLayout();
            tabP_Llm.SuspendLayout();
            tabControl3.SuspendLayout();
            tabP_InferenceEngine.SuspendLayout();
            tabP__Models.SuspendLayout();
            tabP_Minecraft.SuspendLayout();
            tabP_Console.SuspendLayout();
            SuspendLayout();
            // 
            // btnIniciar
            // 
            btnIniciar.Location = new Point(6, 53);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(75, 23);
            btnIniciar.TabIndex = 0;
            btnIniciar.Text = "Iniciar";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // btnParar
            // 
            btnParar.Location = new Point(87, 53);
            btnParar.Name = "btnParar";
            btnParar.Size = new Size(75, 23);
            btnParar.TabIndex = 1;
            btnParar.Text = "Parar";
            btnParar.UseVisualStyleBackColor = true;
            btnParar.Click += btnParar_Click;
            // 
            // rtb_OutConsole
            // 
            rtb_OutConsole.Dock = DockStyle.Fill;
            rtb_OutConsole.Location = new Point(3, 3);
            rtb_OutConsole.Name = "rtb_OutConsole";
            rtb_OutConsole.Size = new Size(495, 234);
            rtb_OutConsole.TabIndex = 2;
            rtb_OutConsole.Text = "";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(28, 7);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(354, 203);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = SystemColors.Control;
            tabPage1.Controls.Add(btnIniciar);
            tabPage1.Controls.Add(btnParar);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(346, 175);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = SystemColors.Control;
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(346, 175);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            // 
            // tabControl2
            // 
            tabControl2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl2.Controls.Add(tabP_Llm);
            tabControl2.Controls.Add(tabP_Minecraft);
            tabControl2.Controls.Add(tabP_Console);
            tabControl2.Location = new Point(1, 12);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(509, 268);
            tabControl2.TabIndex = 4;
            // 
            // tabP_Llm
            // 
            tabP_Llm.BackColor = SystemColors.Control;
            tabP_Llm.Controls.Add(btn_ForceStop);
            tabP_Llm.Controls.Add(btnn_StartModel);
            tabP_Llm.Controls.Add(btn_GoToWeb);
            tabP_Llm.Controls.Add(tb_LlmUrl);
            tabP_Llm.Controls.Add(tabControl3);
            tabP_Llm.Location = new Point(4, 24);
            tabP_Llm.Name = "tabP_Llm";
            tabP_Llm.Padding = new Padding(3);
            tabP_Llm.Size = new Size(501, 240);
            tabP_Llm.TabIndex = 0;
            tabP_Llm.Text = "Llm";
            // 
            // btn_ForceStop
            // 
            btn_ForceStop.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_ForceStop.Location = new Point(415, 3);
            btn_ForceStop.Name = "btn_ForceStop";
            btn_ForceStop.Size = new Size(79, 23);
            btn_ForceStop.TabIndex = 10;
            btn_ForceStop.Text = "Force Stop";
            btn_ForceStop.UseVisualStyleBackColor = true;
            btn_ForceStop.MouseClick += btn_ForceStop_MouseClick;
            // 
            // btnn_StartModel
            // 
            btnn_StartModel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnn_StartModel.Location = new Point(368, 3);
            btnn_StartModel.Name = "btnn_StartModel";
            btnn_StartModel.Size = new Size(41, 23);
            btnn_StartModel.TabIndex = 9;
            btnn_StartModel.Text = "Start";
            btnn_StartModel.UseVisualStyleBackColor = true;
            btnn_StartModel.Click += btnn_StartModel_Click;
            // 
            // btn_GoToWeb
            // 
            btn_GoToWeb.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_GoToWeb.Location = new Point(287, 3);
            btn_GoToWeb.Name = "btn_GoToWeb";
            btn_GoToWeb.Size = new Size(75, 23);
            btn_GoToWeb.TabIndex = 8;
            btn_GoToWeb.Text = "Go to Web";
            btn_GoToWeb.UseVisualStyleBackColor = true;
            btn_GoToWeb.Click += btn_GoToWeb_Click;
            // 
            // tb_LlmUrl
            // 
            tb_LlmUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_LlmUrl.Location = new Point(7, 3);
            tb_LlmUrl.Name = "tb_LlmUrl";
            tb_LlmUrl.Size = new Size(274, 23);
            tb_LlmUrl.TabIndex = 7;
            // 
            // tabControl3
            // 
            tabControl3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl3.Controls.Add(tabP_InferenceEngine);
            tabControl3.Controls.Add(tabP__Models);
            tabControl3.Location = new Point(3, 27);
            tabControl3.Name = "tabControl3";
            tabControl3.SelectedIndex = 0;
            tabControl3.Size = new Size(495, 210);
            tabControl3.TabIndex = 6;
            // 
            // tabP_InferenceEngine
            // 
            tabP_InferenceEngine.BackColor = SystemColors.Control;
            tabP_InferenceEngine.Controls.Add(flp_InferenceEngines);
            tabP_InferenceEngine.Controls.Add(lb_InferenceEngineSelected);
            tabP_InferenceEngine.Controls.Add(label3);
            tabP_InferenceEngine.Controls.Add(label2);
            tabP_InferenceEngine.Controls.Add(btnFindInferenceEnginesFolder);
            tabP_InferenceEngine.Controls.Add(tb_InferenceEnginesFolder);
            tabP_InferenceEngine.Location = new Point(4, 24);
            tabP_InferenceEngine.Name = "tabP_InferenceEngine";
            tabP_InferenceEngine.Padding = new Padding(3);
            tabP_InferenceEngine.Size = new Size(487, 182);
            tabP_InferenceEngine.TabIndex = 0;
            tabP_InferenceEngine.Text = "Inference Engine";
            // 
            // flp_InferenceEngines
            // 
            flp_InferenceEngines.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flp_InferenceEngines.AutoScroll = true;
            flp_InferenceEngines.FlowDirection = FlowDirection.TopDown;
            flp_InferenceEngines.Location = new Point(6, 53);
            flp_InferenceEngines.Name = "flp_InferenceEngines";
            flp_InferenceEngines.Size = new Size(475, 126);
            flp_InferenceEngines.TabIndex = 8;
            flp_InferenceEngines.WrapContents = false;
            // 
            // lb_InferenceEngineSelected
            // 
            lb_InferenceEngineSelected.AutoSize = true;
            lb_InferenceEngineSelected.Location = new Point(64, 35);
            lb_InferenceEngineSelected.Name = "lb_InferenceEngineSelected";
            lb_InferenceEngineSelected.Size = new Size(102, 15);
            lb_InferenceEngineSelected.TabIndex = 7;
            lb_InferenceEngineSelected.Text = "-InferenceEngine-";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 35);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 6;
            label3.Text = "Selected:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 9);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 4;
            label2.Text = "Folder:";
            // 
            // btnFindInferenceEnginesFolder
            // 
            btnFindInferenceEnginesFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFindInferenceEnginesFolder.Location = new Point(408, 6);
            btnFindInferenceEnginesFolder.Name = "btnFindInferenceEnginesFolder";
            btnFindInferenceEnginesFolder.Size = new Size(75, 23);
            btnFindInferenceEnginesFolder.TabIndex = 5;
            btnFindInferenceEnginesFolder.Text = "Find Dir";
            btnFindInferenceEnginesFolder.UseVisualStyleBackColor = true;
            btnFindInferenceEnginesFolder.Click += btnFindInferenceEnginesFolder_Click;
            // 
            // tb_InferenceEnginesFolder
            // 
            tb_InferenceEnginesFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_InferenceEnginesFolder.Location = new Point(53, 6);
            tb_InferenceEnginesFolder.Name = "tb_InferenceEnginesFolder";
            tb_InferenceEnginesFolder.Size = new Size(349, 23);
            tb_InferenceEnginesFolder.TabIndex = 3;
            tb_InferenceEnginesFolder.Text = "C:\\InferenceEngines";
            // 
            // tabP__Models
            // 
            tabP__Models.BackColor = SystemColors.Control;
            tabP__Models.Controls.Add(flp_Models);
            tabP__Models.Controls.Add(lb_ModelSelected);
            tabP__Models.Controls.Add(label4);
            tabP__Models.Controls.Add(tb_ModelsFolder);
            tabP__Models.Controls.Add(label1);
            tabP__Models.Controls.Add(btnFindModelsFolder);
            tabP__Models.Location = new Point(4, 24);
            tabP__Models.Name = "tabP__Models";
            tabP__Models.Padding = new Padding(3);
            tabP__Models.Size = new Size(487, 182);
            tabP__Models.TabIndex = 1;
            tabP__Models.Text = "Models";
            // 
            // flp_Models
            // 
            flp_Models.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flp_Models.AutoScroll = true;
            flp_Models.FlowDirection = FlowDirection.TopDown;
            flp_Models.Location = new Point(6, 53);
            flp_Models.Name = "flp_Models";
            flp_Models.Size = new Size(475, 126);
            flp_Models.TabIndex = 9;
            flp_Models.WrapContents = false;
            // 
            // lb_ModelSelected
            // 
            lb_ModelSelected.AutoSize = true;
            lb_ModelSelected.Location = new Point(64, 35);
            lb_ModelSelected.Name = "lb_ModelSelected";
            lb_ModelSelected.Size = new Size(51, 15);
            lb_ModelSelected.TabIndex = 8;
            lb_ModelSelected.Text = "-Model-";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(4, 35);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 7;
            label4.Text = "Selected:";
            // 
            // tb_ModelsFolder
            // 
            tb_ModelsFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tb_ModelsFolder.Location = new Point(53, 6);
            tb_ModelsFolder.Name = "tb_ModelsFolder";
            tb_ModelsFolder.Size = new Size(349, 23);
            tb_ModelsFolder.TabIndex = 0;
            tb_ModelsFolder.Text = "H:\\llm\\models";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 9);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 1;
            label1.Text = "Folder:";
            // 
            // btnFindModelsFolder
            // 
            btnFindModelsFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFindModelsFolder.Location = new Point(408, 6);
            btnFindModelsFolder.Name = "btnFindModelsFolder";
            btnFindModelsFolder.Size = new Size(75, 23);
            btnFindModelsFolder.TabIndex = 2;
            btnFindModelsFolder.Text = "Find Dir";
            btnFindModelsFolder.UseVisualStyleBackColor = true;
            btnFindModelsFolder.Click += btnFindModelsFolder_Click;
            // 
            // tabP_Minecraft
            // 
            tabP_Minecraft.BackColor = SystemColors.Control;
            tabP_Minecraft.Controls.Add(tabControl1);
            tabP_Minecraft.Location = new Point(4, 24);
            tabP_Minecraft.Name = "tabP_Minecraft";
            tabP_Minecraft.Padding = new Padding(3);
            tabP_Minecraft.Size = new Size(501, 240);
            tabP_Minecraft.TabIndex = 1;
            tabP_Minecraft.Text = "Minecraft";
            // 
            // tabP_Console
            // 
            tabP_Console.BackColor = SystemColors.Control;
            tabP_Console.Controls.Add(rtb_OutConsole);
            tabP_Console.Location = new Point(4, 24);
            tabP_Console.Name = "tabP_Console";
            tabP_Console.Padding = new Padding(3);
            tabP_Console.Size = new Size(501, 240);
            tabP_Console.TabIndex = 2;
            tabP_Console.Text = "Console";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(511, 284);
            Controls.Add(tabControl2);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabControl2.ResumeLayout(false);
            tabP_Llm.ResumeLayout(false);
            tabP_Llm.PerformLayout();
            tabControl3.ResumeLayout(false);
            tabP_InferenceEngine.ResumeLayout(false);
            tabP_InferenceEngine.PerformLayout();
            tabP__Models.ResumeLayout(false);
            tabP__Models.PerformLayout();
            tabP_Minecraft.ResumeLayout(false);
            tabP_Console.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnIniciar;
        private Button btnParar;
        private RichTextBox rtb_OutConsole;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabControl tabControl2;
        private TabPage tabP_Llm;
        private TabPage tabP_Minecraft;
        private Button btnFindModelsFolder;
        private Label label1;
        private TextBox tb_ModelsFolder;
        private FolderBrowserDialog fbd_;
        private Button btnFindInferenceEnginesFolder;
        private Label label2;
        private TextBox tb_InferenceEnginesFolder;
        private TabControl tabControl3;
        private TabPage tabP_InferenceEngine;
        private TabPage tabP__Models;
        private Label label3;
        private Label lb_InferenceEngineSelected;
        private Label lb_ModelSelected;
        private Label label4;
        private FlowLayoutPanel flp_InferenceEngines;
        private FlowLayoutPanel flp_Models;
        private Button btn_GoToWeb;
        private TextBox tb_LlmUrl;
        private Button btnn_StartModel;
        private TabPage tabP_Console;
        private Button btn_ForceStop;
    }
}
