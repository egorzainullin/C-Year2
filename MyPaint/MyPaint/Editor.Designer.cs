namespace MyPaint
{
    partial class Editor
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.picture = new System.Windows.Forms.PictureBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.RedoButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // picture
            // 
            this.picture.BackColor = System.Drawing.Color.White;
            this.picture.Location = new System.Drawing.Point(1, 126);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(958, 567);
            this.picture.TabIndex = 0;
            this.picture.TabStop = false;
            this.picture.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPicturePaint);
            this.picture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnPictureMouseDown);
            this.picture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnPictureMouseMove);
            this.picture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnPictureMouseUp);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(37, 30);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(104, 41);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "deleteButton";
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // undoButton
            // 
            this.undoButton.Location = new System.Drawing.Point(305, 39);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(75, 23);
            this.undoButton.TabIndex = 2;
            this.undoButton.Text = "Undo";
            this.undoButton.UseVisualStyleBackColor = true;
            // 
            // RedoButton
            // 
            this.RedoButton.Location = new System.Drawing.Point(434, 39);
            this.RedoButton.Name = "RedoButton";
            this.RedoButton.Size = new System.Drawing.Size(75, 23);
            this.RedoButton.TabIndex = 3;
            this.RedoButton.Text = "Redo";
            this.RedoButton.UseVisualStyleBackColor = true;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 697);
            this.Controls.Add(this.RedoButton);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.picture);
            this.MaximumSize = new System.Drawing.Size(975, 744);
            this.MinimumSize = new System.Drawing.Size(975, 744);
            this.Name = "Editor";
            this.Text = "Editor";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Button RedoButton;
    }
}

