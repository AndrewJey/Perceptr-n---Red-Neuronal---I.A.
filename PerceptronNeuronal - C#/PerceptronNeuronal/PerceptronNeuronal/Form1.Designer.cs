namespace PerceptronNeuronal
{
    partial class Form_Perceptron
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Entrenar = new System.Windows.Forms.Button();
            this.btn_Salir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Entrenar
            // 
            this.btn_Entrenar.BackColor = System.Drawing.Color.ForestGreen;
            this.btn_Entrenar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Entrenar.Location = new System.Drawing.Point(12, 211);
            this.btn_Entrenar.Name = "btn_Entrenar";
            this.btn_Entrenar.Size = new System.Drawing.Size(93, 37);
            this.btn_Entrenar.TabIndex = 0;
            this.btn_Entrenar.Text = "Entrenamiento";
            this.btn_Entrenar.UseVisualStyleBackColor = false;
            this.btn_Entrenar.Click += new System.EventHandler(this.btn_Entrenar_Click);
            // 
            // btn_Salir
            // 
            this.btn_Salir.BackColor = System.Drawing.Color.ForestGreen;
            this.btn_Salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Salir.Location = new System.Drawing.Point(352, 211);
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(93, 37);
            this.btn_Salir.TabIndex = 1;
            this.btn_Salir.Text = "Salir";
            this.btn_Salir.UseVisualStyleBackColor = false;
            this.btn_Salir.Click += new System.EventHandler(this.btn_Salir_Click);
            // 
            // Form_Perceptron
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(457, 260);
            this.Controls.Add(this.btn_Salir);
            this.Controls.Add(this.btn_Entrenar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form_Perceptron";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Percentron Multicapa";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Entrenar;
        private System.Windows.Forms.Button btn_Salir;
    }
}

