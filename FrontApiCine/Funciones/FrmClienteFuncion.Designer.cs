namespace CineProyectoUTN.Formularios
{
    partial class FrmClienteFuncion
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
            this.dgvClienteFuncion = new System.Windows.Forms.DataGridView();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pelicula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cant_entradas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.funcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClienteFuncion)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(107, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(569, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lista de clientes que mas entradas compraron por funcion";
            // 
            // dgvClienteFuncion
            // 
            this.dgvClienteFuncion.AllowUserToAddRows = false;
            this.dgvClienteFuncion.AllowUserToDeleteRows = false;
            this.dgvClienteFuncion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClienteFuncion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre,
            this.pelicula,
            this.cant_entradas,
            this.funcion});
            this.dgvClienteFuncion.Location = new System.Drawing.Point(111, 66);
            this.dgvClienteFuncion.Name = "dgvClienteFuncion";
            this.dgvClienteFuncion.ReadOnly = true;
            this.dgvClienteFuncion.RowHeadersVisible = false;
            this.dgvClienteFuncion.RowHeadersWidth = 51;
            this.dgvClienteFuncion.RowTemplate.Height = 25;
            this.dgvClienteFuncion.Size = new System.Drawing.Size(595, 343);
            this.dgvClienteFuncion.TabIndex = 1;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "Nombre Cliente";
            this.nombre.MinimumWidth = 6;
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 150;
            // 
            // pelicula
            // 
            this.pelicula.HeaderText = "Pelicula";
            this.pelicula.MinimumWidth = 6;
            this.pelicula.Name = "pelicula";
            this.pelicula.ReadOnly = true;
            this.pelicula.Width = 150;
            // 
            // cant_entradas
            // 
            this.cant_entradas.HeaderText = "Cantidad de entradas";
            this.cant_entradas.MinimumWidth = 6;
            this.cant_entradas.Name = "cant_entradas";
            this.cant_entradas.ReadOnly = true;
            this.cant_entradas.Width = 125;
            // 
            // funcion
            // 
            this.funcion.HeaderText = "Horario funcion";
            this.funcion.MinimumWidth = 6;
            this.funcion.Name = "funcion";
            this.funcion.ReadOnly = true;
            this.funcion.Width = 150;
            // 
            // FrmClienteFuncion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(41)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvClienteFuncion);
            this.Controls.Add(this.label1);
            this.Name = "FrmClienteFuncion";
            this.Text = "Clientes por Funcion";
            this.Load += new System.EventHandler(this.FrmClienteFuncion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClienteFuncion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private DataGridView dgvClienteFuncion;
        private DataGridViewTextBoxColumn nombre;
        private DataGridViewTextBoxColumn pelicula;
        private DataGridViewTextBoxColumn cant_entradas;
        private DataGridViewTextBoxColumn funcion;
    }
}