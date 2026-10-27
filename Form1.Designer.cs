namespace Grafos_app
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.picGrafo = new System.Windows.Forms.PictureBox();
            this.txtRes = new System.Windows.Forms.TextBox();
            this.panelIzq = new System.Windows.Forms.Panel();
            this.lblAristas = new System.Windows.Forms.Label();
            this.txtAristas = new System.Windows.Forms.TextBox();
            this.chkDirigido = new System.Windows.Forms.CheckBox();
            this.lblRaiz = new System.Windows.Forms.Label();
            this.txtRaiz = new System.Windows.Forms.TextBox();
            this.lblDe = new System.Windows.Forms.Label();
            this.txtOrigen = new System.Windows.Forms.TextBox();
            this.lblA = new System.Windows.Forms.Label();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.btnMatriz = new System.Windows.Forms.Button();
            this.btnEuler = new System.Windows.Forms.Button();
            this.btnArbol = new System.Windows.Forms.Button();
            this.btnKruskal = new System.Windows.Forms.Button();
            this.btnDijkstra = new System.Windows.Forms.Button();
            this.lblGrafo2 = new System.Windows.Forms.Label();
            this.txtGrafo2 = new System.Windows.Forms.TextBox();
            this.btnIso = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.picRed = new System.Windows.Forms.PictureBox();
            this.txtResRed = new System.Windows.Forms.TextBox();
            this.panelIzq2 = new System.Windows.Forms.Panel();
            this.lblActividades = new System.Windows.Forms.Label();
            this.txtActividades = new System.Windows.Forms.TextBox();
            this.btnRed = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGrafo)).BeginInit();
            this.panelIzq.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRed)).BeginInit();
            this.panelIzq2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1264, 741);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.picGrafo);
            this.tabPage1.Controls.Add(this.txtRes);
            this.tabPage1.Controls.Add(this.panelIzq);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1256, 712);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Grafos y dígrafos";
            // 
            // picGrafo
            // 
            this.picGrafo.BackColor = System.Drawing.Color.White;
            this.picGrafo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picGrafo.Location = new System.Drawing.Point(300, 0);
            this.picGrafo.Name = "picGrafo";
            this.picGrafo.Size = new System.Drawing.Size(536, 712);
            this.picGrafo.TabIndex = 2;
            this.picGrafo.TabStop = false;
            this.picGrafo.Paint += new System.Windows.Forms.PaintEventHandler(this.picGrafo_Paint);
            // 
            // txtRes
            // 
            this.txtRes.BackColor = System.Drawing.Color.White;
            this.txtRes.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtRes.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtRes.Location = new System.Drawing.Point(836, 0);
            this.txtRes.Multiline = true;
            this.txtRes.Name = "txtRes";
            this.txtRes.ReadOnly = true;
            this.txtRes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRes.Size = new System.Drawing.Size(420, 712);
            this.txtRes.TabIndex = 1;
            // 
            // panelIzq
            // 
            this.panelIzq.AutoScroll = true;
            this.panelIzq.Controls.Add(this.lblAristas);
            this.panelIzq.Controls.Add(this.txtAristas);
            this.panelIzq.Controls.Add(this.chkDirigido);
            this.panelIzq.Controls.Add(this.lblRaiz);
            this.panelIzq.Controls.Add(this.txtRaiz);
            this.panelIzq.Controls.Add(this.lblDe);
            this.panelIzq.Controls.Add(this.txtOrigen);
            this.panelIzq.Controls.Add(this.lblA);
            this.panelIzq.Controls.Add(this.txtDestino);
            this.panelIzq.Controls.Add(this.btnMatriz);
            this.panelIzq.Controls.Add(this.btnEuler);
            this.panelIzq.Controls.Add(this.btnArbol);
            this.panelIzq.Controls.Add(this.btnKruskal);
            this.panelIzq.Controls.Add(this.btnDijkstra);
            this.panelIzq.Controls.Add(this.lblGrafo2);
            this.panelIzq.Controls.Add(this.txtGrafo2);
            this.panelIzq.Controls.Add(this.btnIso);
            this.panelIzq.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzq.Location = new System.Drawing.Point(0, 0);
            this.panelIzq.Name = "panelIzq";
            this.panelIzq.Size = new System.Drawing.Size(300, 712);
            this.panelIzq.TabIndex = 0;
            // 
            // lblAristas
            // 
            this.lblAristas.AutoSize = true;
            this.lblAristas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAristas.Location = new System.Drawing.Point(10, 10);
            this.lblAristas.Name = "lblAristas";
            this.lblAristas.Size = new System.Drawing.Size(200, 15);
            this.lblAristas.TabIndex = 0;
            this.lblAristas.Text = "Aristas (origen destino [peso])";
            // 
            // txtAristas
            // 
            this.txtAristas.AcceptsReturn = true;
            this.txtAristas.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtAristas.Location = new System.Drawing.Point(10, 32);
            this.txtAristas.Multiline = true;
            this.txtAristas.Name = "txtAristas";
            this.txtAristas.Size = new System.Drawing.Size(270, 120);
            this.txtAristas.TabIndex = 1;
            this.txtAristas.Text = "A B\r\nA C\r\nB C\r\nC D\r\nC E\r\nD E";
            // 
            // chkDirigido
            // 
            this.chkDirigido.AutoSize = true;
            this.chkDirigido.Location = new System.Drawing.Point(10, 160);
            this.chkDirigido.Name = "chkDirigido";
            this.chkDirigido.Size = new System.Drawing.Size(200, 20);
            this.chkDirigido.TabIndex = 2;
            this.chkDirigido.Text = "Dígrafo (aristas dirigidas)";
            this.chkDirigido.UseVisualStyleBackColor = true;
            // 
            // lblRaiz
            // 
            this.lblRaiz.AutoSize = true;
            this.lblRaiz.Location = new System.Drawing.Point(10, 192);
            this.lblRaiz.Name = "lblRaiz";
            this.lblRaiz.Size = new System.Drawing.Size(36, 16);
            this.lblRaiz.TabIndex = 3;
            this.lblRaiz.Text = "Raíz:";
            // 
            // txtRaiz
            // 
            this.txtRaiz.Location = new System.Drawing.Point(50, 189);
            this.txtRaiz.Name = "txtRaiz";
            this.txtRaiz.Size = new System.Drawing.Size(45, 22);
            this.txtRaiz.TabIndex = 4;
            // 
            // lblDe
            // 
            this.lblDe.AutoSize = true;
            this.lblDe.Location = new System.Drawing.Point(105, 192);
            this.lblDe.Name = "lblDe";
            this.lblDe.Size = new System.Drawing.Size(27, 16);
            this.lblDe.TabIndex = 5;
            this.lblDe.Text = "De:";
            // 
            // txtOrigen
            // 
            this.txtOrigen.Location = new System.Drawing.Point(135, 189);
            this.txtOrigen.Name = "txtOrigen";
            this.txtOrigen.Size = new System.Drawing.Size(45, 22);
            this.txtOrigen.TabIndex = 6;
            this.txtOrigen.Text = "A";
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(190, 192);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(21, 16);
            this.lblA.TabIndex = 7;
            this.lblA.Text = "A:";
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(215, 189);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(45, 22);
            this.txtDestino.TabIndex = 8;
            this.txtDestino.Text = "E";
            // 
            // btnMatriz
            // 
            this.btnMatriz.Location = new System.Drawing.Point(10, 222);
            this.btnMatriz.Name = "btnMatriz";
            this.btnMatriz.Size = new System.Drawing.Size(270, 30);
            this.btnMatriz.TabIndex = 9;
            this.btnMatriz.Text = "Dibujar + matriz + grados";
            this.btnMatriz.UseVisualStyleBackColor = true;
            this.btnMatriz.Click += new System.EventHandler(this.btnMatriz_Click);
            // 
            // btnEuler
            // 
            this.btnEuler.Location = new System.Drawing.Point(10, 258);
            this.btnEuler.Name = "btnEuler";
            this.btnEuler.Size = new System.Drawing.Size(270, 30);
            this.btnEuler.TabIndex = 10;
            this.btnEuler.Text = "Euler y Hamilton";
            this.btnEuler.UseVisualStyleBackColor = true;
            this.btnEuler.Click += new System.EventHandler(this.btnEuler_Click);
            // 
            // btnArbol
            // 
            this.btnArbol.Location = new System.Drawing.Point(10, 294);
            this.btnArbol.Name = "btnArbol";
            this.btnArbol.Size = new System.Drawing.Size(270, 30);
            this.btnArbol.TabIndex = 11;
            this.btnArbol.Text = "Árbol: hojas, altura, pre/in/postorden";
            this.btnArbol.UseVisualStyleBackColor = true;
            this.btnArbol.Click += new System.EventHandler(this.btnArbol_Click);
            // 
            // btnKruskal
            // 
            this.btnKruskal.Location = new System.Drawing.Point(10, 330);
            this.btnKruskal.Name = "btnKruskal";
            this.btnKruskal.Size = new System.Drawing.Size(270, 30);
            this.btnKruskal.TabIndex = 12;
            this.btnKruskal.Text = "Kruskal (árbol generador mínimo)";
            this.btnKruskal.UseVisualStyleBackColor = true;
            this.btnKruskal.Click += new System.EventHandler(this.btnKruskal_Click);
            // 
            // btnDijkstra
            // 
            this.btnDijkstra.Location = new System.Drawing.Point(10, 366);
            this.btnDijkstra.Name = "btnDijkstra";
            this.btnDijkstra.Size = new System.Drawing.Size(270, 30);
            this.btnDijkstra.TabIndex = 13;
            this.btnDijkstra.Text = "Dijkstra (camino más corto)";
            this.btnDijkstra.UseVisualStyleBackColor = true;
            this.btnDijkstra.Click += new System.EventHandler(this.btnDijkstra_Click);
            // 
            // lblGrafo2
            // 
            this.lblGrafo2.AutoSize = true;
            this.lblGrafo2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGrafo2.Location = new System.Drawing.Point(10, 412);
            this.lblGrafo2.Name = "lblGrafo2";
            this.lblGrafo2.Size = new System.Drawing.Size(200, 15);
            this.lblGrafo2.TabIndex = 14;
            this.lblGrafo2.Text = "Segundo grafo (isomorfismo)";
            // 
            // txtGrafo2
            // 
            this.txtGrafo2.AcceptsReturn = true;
            this.txtGrafo2.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtGrafo2.Location = new System.Drawing.Point(10, 432);
            this.txtGrafo2.Multiline = true;
            this.txtGrafo2.Name = "txtGrafo2";
            this.txtGrafo2.Size = new System.Drawing.Size(270, 90);
            this.txtGrafo2.TabIndex = 15;
            this.txtGrafo2.Text = "1 2\r\n2 3\r\n3 4\r\n4 5\r\n5 3\r\n1 3";
            // 
            // btnIso
            // 
            this.btnIso.Location = new System.Drawing.Point(10, 530);
            this.btnIso.Name = "btnIso";
            this.btnIso.Size = new System.Drawing.Size(270, 30);
            this.btnIso.TabIndex = 16;
            this.btnIso.Text = "¿Son isomorfos?";
            this.btnIso.UseVisualStyleBackColor = true;
            this.btnIso.Click += new System.EventHandler(this.btnIso_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.picRed);
            this.tabPage2.Controls.Add(this.txtResRed);
            this.tabPage2.Controls.Add(this.panelIzq2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1256, 712);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Red de actividades (camino crítico)";
            // 
            // picRed
            // 
            this.picRed.BackColor = System.Drawing.Color.White;
            this.picRed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picRed.Location = new System.Drawing.Point(300, 0);
            this.picRed.Name = "picRed";
            this.picRed.Size = new System.Drawing.Size(536, 712);
            this.picRed.TabIndex = 2;
            this.picRed.TabStop = false;
            this.picRed.Paint += new System.Windows.Forms.PaintEventHandler(this.picRed_Paint);
            // 
            // txtResRed
            // 
            this.txtResRed.BackColor = System.Drawing.Color.White;
            this.txtResRed.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtResRed.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtResRed.Location = new System.Drawing.Point(836, 0);
            this.txtResRed.Multiline = true;
            this.txtResRed.Name = "txtResRed";
            this.txtResRed.ReadOnly = true;
            this.txtResRed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResRed.Size = new System.Drawing.Size(420, 712);
            this.txtResRed.TabIndex = 1;
            // 
            // panelIzq2
            // 
            this.panelIzq2.Controls.Add(this.lblActividades);
            this.panelIzq2.Controls.Add(this.txtActividades);
            this.panelIzq2.Controls.Add(this.btnRed);
            this.panelIzq2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIzq2.Location = new System.Drawing.Point(0, 0);
            this.panelIzq2.Name = "panelIzq2";
            this.panelIzq2.Size = new System.Drawing.Size(300, 712);
            this.panelIzq2.TabIndex = 0;
            // 
            // lblActividades
            // 
            this.lblActividades.AutoSize = true;
            this.lblActividades.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblActividades.Location = new System.Drawing.Point(10, 10);
            this.lblActividades.Name = "lblActividades";
            this.lblActividades.Size = new System.Drawing.Size(250, 15);
            this.lblActividades.TabIndex = 0;
            this.lblActividades.Text = "Actividades: nombre duración predecesoras";
            // 
            // txtActividades
            // 
            this.txtActividades.AcceptsReturn = true;
            this.txtActividades.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtActividades.Location = new System.Drawing.Point(10, 32);
            this.txtActividades.Multiline = true;
            this.txtActividades.Name = "txtActividades";
            this.txtActividades.Size = new System.Drawing.Size(270, 150);
            this.txtActividades.TabIndex = 1;
            this.txtActividades.Text = "A 3 -\r\nB 2 -\r\nC 4 A\r\nD 3 A,B\r\nE 2 C,D";
            // 
            // btnRed
            // 
            this.btnRed.Location = new System.Drawing.Point(10, 192);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(270, 30);
            this.btnRed.TabIndex = 2;
            this.btnRed.Text = "Dibujar red y calcular caminos";
            this.btnRed.UseVisualStyleBackColor = true;
            this.btnRed.Click += new System.EventHandler(this.btnRed_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 741);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laboratorio de Grafos — Matemática Discreta (UNI)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGrafo)).EndInit();
            this.panelIzq.ResumeLayout(false);
            this.panelIzq.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRed)).EndInit();
            this.panelIzq2.ResumeLayout(false);
            this.panelIzq2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox picGrafo;
        private System.Windows.Forms.TextBox txtRes;
        private System.Windows.Forms.Panel panelIzq;
        private System.Windows.Forms.Label lblAristas;
        private System.Windows.Forms.TextBox txtAristas;
        private System.Windows.Forms.CheckBox chkDirigido;
        private System.Windows.Forms.Label lblRaiz;
        private System.Windows.Forms.TextBox txtRaiz;
        private System.Windows.Forms.Label lblDe;
        private System.Windows.Forms.TextBox txtOrigen;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.Button btnMatriz;
        private System.Windows.Forms.Button btnEuler;
        private System.Windows.Forms.Button btnArbol;
        private System.Windows.Forms.Button btnKruskal;
        private System.Windows.Forms.Button btnDijkstra;
        private System.Windows.Forms.Label lblGrafo2;
        private System.Windows.Forms.TextBox txtGrafo2;
        private System.Windows.Forms.Button btnIso;
        private System.Windows.Forms.PictureBox picRed;
        private System.Windows.Forms.TextBox txtResRed;
        private System.Windows.Forms.Panel panelIzq2;
        private System.Windows.Forms.Label lblActividades;
        private System.Windows.Forms.TextBox txtActividades;
        private System.Windows.Forms.Button btnRed;
    }
}