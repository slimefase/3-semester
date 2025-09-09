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
            gamesDataGridView = new DataGridView();
            groupBoxEditor = new GroupBox();
            btnClearSelection = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtPrice = new TextBox();
            lblPrice = new Label();
            txtGenre = new TextBox();
            lblGenre = new Label();
            txtTitle = new TextBox();
            lblTitle = new Label();
            groupBoxBusiness = new GroupBox();
            btnShowCheaper = new Button();
            resultsTextBox = new RichTextBox();
            btnGroup = new Button();
            ((System.ComponentModel.ISupportInitialize)gamesDataGridView).BeginInit();
            groupBoxEditor.SuspendLayout();
            groupBoxBusiness.SuspendLayout();
            SuspendLayout();

            gamesDataGridView.AllowUserToAddRows = false;
            gamesDataGridView.AllowUserToDeleteRows = false;
            gamesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gamesDataGridView.Location = new Point(12, 12);
            gamesDataGridView.Name = "gamesDataGridView";
            gamesDataGridView.ReadOnly = true;
            gamesDataGridView.Size = new Size(480, 426);
            gamesDataGridView.TabIndex = 0;
            gamesDataGridView.SelectionChanged += gamesDataGridView_SelectionChanged;

            groupBoxEditor.Controls.Add(btnClearSelection);
            groupBoxEditor.Controls.Add(btnDelete);
            groupBoxEditor.Controls.Add(btnUpdate);
            groupBoxEditor.Controls.Add(btnAdd);
            groupBoxEditor.Controls.Add(txtPrice);
            groupBoxEditor.Controls.Add(lblPrice);
            groupBoxEditor.Controls.Add(txtGenre);
            groupBoxEditor.Controls.Add(lblGenre);
            groupBoxEditor.Controls.Add(txtTitle);
            groupBoxEditor.Controls.Add(lblTitle);
            groupBoxEditor.Location = new Point(508, 12);
            groupBoxEditor.Name = "groupBoxEditor";
            groupBoxEditor.Size = new Size(280, 200);
            groupBoxEditor.TabIndex = 1;
            groupBoxEditor.TabStop = false;
            groupBoxEditor.Text = "Редактор";

            btnClearSelection.Location = new Point(18, 160);
            btnClearSelection.Name = "btnClearSelection";
            btnClearSelection.Size = new Size(120, 23);
            btnClearSelection.TabIndex = 9;
            btnClearSelection.Text = "Снять выделение";
            btnClearSelection.UseVisualStyleBackColor = true;
            btnClearSelection.Click += btnClearSelection_Click;

            btnDelete.Enabled = false;
            btnDelete.Location = new Point(188, 122);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            btnUpdate.Enabled = false;
            btnUpdate.Location = new Point(102, 122);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Изменить";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            btnAdd.Location = new Point(18, 122);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Добавить";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            txtPrice.Location = new Point(74, 84);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(189, 23);
            txtPrice.TabIndex = 5;

            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(18, 87);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(35, 15);
            lblPrice.TabIndex = 4;
            lblPrice.Text = "Цена";

            txtGenre.Location = new Point(74, 55);
            txtGenre.Name = "txtGenre";
            txtGenre.Size = new Size(189, 23);
            txtGenre.TabIndex = 3;

            lblGenre.AutoSize = true;
            lblGenre.Location = new Point(18, 58);
            lblGenre.Name = "lblGenre";
            lblGenre.Size = new Size(38, 15);
            lblGenre.TabIndex = 2;
            lblGenre.Text = "Жанр";

            txtTitle.Location = new Point(74, 26);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(189, 23);
            txtTitle.TabIndex = 1;

            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(18, 29);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(59, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Название";

            groupBoxBusiness.Controls.Add(btnShowCheaper);
            groupBoxBusiness.Controls.Add(resultsTextBox);
            groupBoxBusiness.Controls.Add(btnGroup);
            groupBoxBusiness.Location = new Point(508, 218);
            groupBoxBusiness.Name = "groupBoxBusiness";
            groupBoxBusiness.Size = new Size(280, 220);
            groupBoxBusiness.TabIndex = 2;
            groupBoxBusiness.TabStop = false;
            groupBoxBusiness.Text = "Бизнес-функции";

            btnShowCheaper.Location = new Point(18, 51);
            btnShowCheaper.Name = "btnShowCheaper";
            btnShowCheaper.Size = new Size(245, 23);
            btnShowCheaper.TabIndex = 2;
            btnShowCheaper.Text = "Показать игры дешевле 500 ₽";
            btnShowCheaper.UseVisualStyleBackColor = true;
            btnShowCheaper.Click += btnShowCheaper_Click;

            resultsTextBox.Location = new Point(18, 80);
            resultsTextBox.Name = "resultsTextBox";
            resultsTextBox.ReadOnly = true;
            resultsTextBox.Size = new Size(245, 125);
            resultsTextBox.TabIndex = 1;
            resultsTextBox.Text = "";

            btnGroup.Location = new Point(18, 22);
            btnGroup.Name = "btnGroup";
            btnGroup.Size = new Size(245, 23);
            btnGroup.TabIndex = 0;
            btnGroup.Text = "Сгруппировать по жанру";
            btnGroup.UseVisualStyleBackColor = true;
            btnGroup.Click += btnGroup_Click;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBoxBusiness);
            Controls.Add(groupBoxEditor);
            Controls.Add(gamesDataGridView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Магазин Игр";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)gamesDataGridView).EndInit();
            groupBoxEditor.ResumeLayout(false);
            groupBoxEditor.PerformLayout();
            groupBoxBusiness.ResumeLayout(false);
            ResumeLayout(false);
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
        private Button btnShowCheaper;
    }
}