namespace CineProyectoUTN.Formularios
{
    partial class FrmRecaudacionPelicula
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
            this.label1 = new System.Windows.Forms.Label();
            this.dvgRecaudacion = new System.Windows.Forms.DataGridView();
            this.pelicula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_recaudado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dvgRecaudacion)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(176, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(567, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "TOTAL RECAUDADO POR PELICULA";
            // 
            // dvgRecaudacion
            // 
            this.dvgRecaudacion.AllowUserToAddRows = false;
            this.dvgRecaudacion.AllowUserToDeleteRows = false;
            this.dvgRecaudacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgRecaudacion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pelicula,
            this.genero,
            this.total_recaudado});
            this.dvgRecaudacion.Location = new System.Drawing.Point(155, 109);
            this.dvgRecaudacion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dvgRecaudacion.Name = "dvgRecaudacion";
            this.dvgRecaudacion.ReadOnly = true;
            this.dvgRecaudacion.RowHeadersVisible = false;
            this.dvgRecaudacion.RowHeadersWidth = 51;
            this.dvgRecaudacion.RowTemplate.Height = 25;
            this.dvgRecaudacion.Size = new System.Drawing.Size(577, 392);
            this.dvgRecaudacion.TabIndex = 1;
            this.dvgRecaudacion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgRecaudacion_CellContentClick);
            // 
            // pelicula
            // 
            this.pelicula.HeaderText = "Pelicula";
            this.pelicula.MinimumWidth = 6;
            this.pelicula.Name = "pelicula";
            this.pelicula.ReadOnly = true;
            this.pelicula.Width = 200;
            // 
            // genero
            // 
            this.genero.HeaderText = "Genero";
            this.genero.MinimumWidth = 6;
            this.genero.Name = "genero";
            this.genero.ReadOnly = true;
            this.genero.Width = 150;
            // 
            // total_recaudado
            // 
            this.total_recaudado.HeaderText = "Total Recaudado en Pesos";
            this.total_recaudado.MinimumWidth = 6;
            this.total_recaudado.Name = "total_recaudado";
            this.total_recaudado.ReadOnly = true;
            this.total_recaudado.Width = 150;
            // 
            // FrmRecaudacionPelicula
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.dvgRecaudacion);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmRecaudacionPelicula";
            this.Text = "FrmRecaudacionPelicula";
            this.Load += new System.EventHandler(this.FrmRecaudacionPelicula_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dvgRecaudacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private DataGridView dvgRecaudacion;
        private DataGridViewTextBoxColumn pelicula;
        private DataGridViewTextBoxColumn genero;
        private DataGridViewTextBoxColumn total_recaudado;
    }
}