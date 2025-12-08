namespace GameStore.WinFormsApp
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
            this.gamesDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBoxEditor = new System.Windows.Forms.GroupBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtGenre = new System.Windows.Forms.TextBox();
            this.lblGenre = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBoxBusiness = new System.Windows.Forms.GroupBox();
            this.btnShowDiscounted = new System.Windows.Forms.Button();
            this.resultsTextBox = new System.Windows.Forms.RichTextBox();
            this.btnGroup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gamesDataGridView)).BeginInit();
            this.groupBoxEditor.SuspendLayout();
            this.groupBoxBusiness.SuspendLayout();
            this.SuspendLayout();

            this.gamesDataGridView.AllowUserToAddRows = false;
            this.gamesDataGridView.AllowUserToDeleteRows = false;
            this.gamesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gamesDataGridView.Location = new System.Drawing.Point(12, 12);
            this.gamesDataGridView.Name = "gamesDataGridView";
            this.gamesDataGridView.ReadOnly = true;
            this.gamesDataGridView.Size = new System.Drawing.Size(550, 426);
            this.gamesDataGridView.TabIndex = 0;
            this.gamesDataGridView.SelectionChanged += new System.EventHandler(this.gamesDataGridView_SelectionChanged);

            this.groupBoxEditor.Controls.Add(this.txtDiscount);
            this.groupBoxEditor.Controls.Add(this.lblDiscount);
            this.groupBoxEditor.Controls.Add(this.btnClearSelection);
            this.groupBoxEditor.Controls.Add(this.btnDelete);
            this.groupBoxEditor.Controls.Add(this.btnUpdate);
            this.groupBoxEditor.Controls.Add(this.btnAdd);
            this.groupBoxEditor.Controls.Add(this.txtPrice);
            this.groupBoxEditor.Controls.Add(this.lblPrice);
            this.groupBoxEditor.Controls.Add(this.txtGenre);
            this.groupBoxEditor.Controls.Add(this.lblGenre);
            this.groupBoxEditor.Controls.Add(this.txtTitle);
            this.groupBoxEditor.Controls.Add(this.lblTitle);
            this.groupBoxEditor.Location = new System.Drawing.Point(578, 12);
            this.groupBoxEditor.Name = "groupBoxEditor";
            this.groupBoxEditor.Size = new System.Drawing.Size(280, 230);
            this.groupBoxEditor.TabIndex = 1;
            this.groupBoxEditor.TabStop = false;
            this.groupBoxEditor.Text = "Редактор";

            this.txtDiscount.Location = new System.Drawing.Point(90, 113);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(173, 23);
            this.txtDiscount.TabIndex = 11;

            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(18, 116);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(63, 15);
            this.lblDiscount.TabIndex = 10;
            this.lblDiscount.Text = "Скидка, %";

            this.btnClearSelection.Location = new System.Drawing.Point(18, 190);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(120, 23);
            this.btnClearSelection.TabIndex = 9;
            this.btnClearSelection.Text = "Снять выделение";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);

            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(188, 152);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnUpdate.Enabled = false;
            this.btnUpdate.Location = new System.Drawing.Point(102, 152);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Изменить";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnAdd.Location = new System.Drawing.Point(18, 152);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.txtPrice.Location = new System.Drawing.Point(90, 84);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(173, 23);
            this.txtPrice.TabIndex = 5;

            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(18, 87);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(35, 15);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "Цена";

            this.txtGenre.Location = new System.Drawing.Point(90, 55);
            this.txtGenre.Name = "txtGenre";
            this.txtGenre.Size = new System.Drawing.Size(173, 23);
            this.txtGenre.TabIndex = 3;

            this.lblGenre.AutoSize = true;
            this.lblGenre.Location = new System.Drawing.Point(18, 58);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(38, 15);
            this.lblGenre.TabIndex = 2;
            this.lblGenre.Text = "Жанр";

            this.txtTitle.Location = new System.Drawing.Point(90, 26);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(173, 23);
            this.txtTitle.TabIndex = 1;

            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(18, 29);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(59, 15);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Название";

            this.groupBoxBusiness.Controls.Add(this.btnShowDiscounted);
            this.groupBoxBusiness.Controls.Add(this.resultsTextBox);
            this.groupBoxBusiness.Controls.Add(this.btnGroup);
            this.groupBoxBusiness.Location = new System.Drawing.Point(578, 248);
            this.groupBoxBusiness.Name = "groupBoxBusiness";
            this.groupBoxBusiness.Size = new System.Drawing.Size(280, 190);
            this.groupBoxBusiness.TabIndex = 2;
            this.groupBoxBusiness.TabStop = false;
            this.groupBoxBusiness.Text = "Функции";

            this.btnShowDiscounted.Location = new System.Drawing.Point(18, 51);
            this.btnShowDiscounted.Name = "btnShowDiscounted";
            this.btnShowDiscounted.Size = new System.Drawing.Size(245, 23);
            this.btnShowDiscounted.TabIndex = 2;
            this.btnShowDiscounted.Text = "Показать все игры со скидкой";
            this.btnShowDiscounted.UseVisualStyleBackColor = true;
            this.btnShowDiscounted.Click += new System.EventHandler(this.btnShowDiscounted_Click);

            this.resultsTextBox.Location = new System.Drawing.Point(18, 80);
            this.resultsTextBox.Name = "resultsTextBox";
            this.resultsTextBox.ReadOnly = true;
            this.resultsTextBox.Size = new System.Drawing.Size(245, 95);
            this.resultsTextBox.TabIndex = 1;
            this.resultsTextBox.Text = "";

            this.btnGroup.Location = new System.Drawing.Point(18, 22);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(245, 23);
            this.btnGroup.TabIndex = 0;
            this.btnGroup.Text = "Сгруппировать по жанру";
            this.btnGroup.UseVisualStyleBackColor = true;
            this.btnGroup.Click += new System.EventHandler(this.btnGroup_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 450);
            this.Controls.Add(this.groupBoxBusiness);
            this.Controls.Add(this.groupBoxEditor);
            this.Controls.Add(this.gamesDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steam";
            ((System.ComponentModel.ISupportInitialize)(this.gamesDataGridView)).EndInit();
            this.groupBoxEditor.ResumeLayout(false);
            this.groupBoxEditor.PerformLayout();
            this.groupBoxBusiness.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private DataGridView gamesDataGridView;
        private GroupBox groupBoxEditor;
        private TextBox txtTitle;
        private Label lblTitle;
        private TextBox txtPrice;
        private Label lblPrice;
        private TextBox txtGenre;
        private Label lblGenre;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private GroupBox groupBoxBusiness;
        private RichTextBox resultsTextBox;
        private Button btnGroup;
        private Button btnClearSelection;
        private Button btnShowDiscounted;
        private TextBox txtDiscount;
        private Label lblDiscount;
    }
}