﻿namespace FF7Scarlet
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            groupBoxBattleLgp = new GroupBox();
            labelBattleLgp = new Label();
            textBoxBattleLgp = new TextBox();
            buttonBattleLgpBrowse = new Button();
            buttonOK = new Button();
            buttonCancel = new Button();
            groupBoxVanillaExe = new GroupBox();
            labelVanillaExe = new Label();
            textBoxVanillaExe = new TextBox();
            buttonVanillaExeBrowse = new Button();
            groupBoxPS3Tweaks = new GroupBox();
            checkBoxPS3Tweaks = new CheckBox();
            labelPS3Tweaks = new Label();
            groupBoxRememberLastOpened = new GroupBox();
            checkBoxRemeberLastOpened = new CheckBox();
            groupBoxBattleLgp.SuspendLayout();
            groupBoxVanillaExe.SuspendLayout();
            groupBoxPS3Tweaks.SuspendLayout();
            groupBoxRememberLastOpened.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxBattleLgp
            // 
            groupBoxBattleLgp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxBattleLgp.Controls.Add(labelBattleLgp);
            groupBoxBattleLgp.Controls.Add(textBoxBattleLgp);
            groupBoxBattleLgp.Controls.Add(buttonBattleLgpBrowse);
            groupBoxBattleLgp.Location = new Point(15, 173);
            groupBoxBattleLgp.Margin = new Padding(4, 3, 4, 3);
            groupBoxBattleLgp.Name = "groupBoxBattleLgp";
            groupBoxBattleLgp.Padding = new Padding(4, 3, 4, 3);
            groupBoxBattleLgp.Size = new Size(438, 98);
            groupBoxBattleLgp.TabIndex = 1;
            groupBoxBattleLgp.TabStop = false;
            groupBoxBattleLgp.Text = "battle.lgp";
            // 
            // labelBattleLgp
            // 
            labelBattleLgp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelBattleLgp.Location = new Point(8, 19);
            labelBattleLgp.Name = "labelBattleLgp";
            labelBattleLgp.Size = new Size(422, 45);
            labelBattleLgp.TabIndex = 6;
            labelBattleLgp.Text = "This file contains the battle models, and will increase accuracy when dealing with model data.";
            // 
            // textBoxBattleLgp
            // 
            textBoxBattleLgp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxBattleLgp.Location = new Point(10, 67);
            textBoxBattleLgp.Margin = new Padding(4, 3, 4, 3);
            textBoxBattleLgp.Name = "textBoxBattleLgp";
            textBoxBattleLgp.Size = new Size(324, 23);
            textBoxBattleLgp.TabIndex = 5;
            // 
            // buttonBattleLgpBrowse
            // 
            buttonBattleLgpBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonBattleLgpBrowse.Location = new Point(342, 67);
            buttonBattleLgpBrowse.Margin = new Padding(4, 3, 4, 3);
            buttonBattleLgpBrowse.Name = "buttonBattleLgpBrowse";
            buttonBattleLgpBrowse.Size = new Size(88, 23);
            buttonBattleLgpBrowse.TabIndex = 3;
            buttonBattleLgpBrowse.Text = "Browse...";
            buttonBattleLgpBrowse.UseVisualStyleBackColor = true;
            buttonBattleLgpBrowse.Click += buttonBattleLgpBrowse_Click;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.Location = new Point(377, 356);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 3;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(296, 356);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // groupBoxVanillaExe
            // 
            groupBoxVanillaExe.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxVanillaExe.Controls.Add(labelVanillaExe);
            groupBoxVanillaExe.Controls.Add(textBoxVanillaExe);
            groupBoxVanillaExe.Controls.Add(buttonVanillaExeBrowse);
            groupBoxVanillaExe.Location = new Point(15, 69);
            groupBoxVanillaExe.Margin = new Padding(4, 3, 4, 3);
            groupBoxVanillaExe.Name = "groupBoxVanillaExe";
            groupBoxVanillaExe.Padding = new Padding(4, 3, 4, 3);
            groupBoxVanillaExe.Size = new Size(438, 98);
            groupBoxVanillaExe.TabIndex = 0;
            groupBoxVanillaExe.TabStop = false;
            groupBoxVanillaExe.Text = "Unedited ff7.exe";
            // 
            // labelVanillaExe
            // 
            labelVanillaExe.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelVanillaExe.Location = new Point(9, 19);
            labelVanillaExe.Name = "labelVanillaExe";
            labelVanillaExe.Size = new Size(420, 43);
            labelVanillaExe.TabIndex = 6;
            labelVanillaExe.Text = "This should be a completely unedited ff7.exe file, to be referenced when creating Hext files. Currently only English EXEs are supported.";
            // 
            // textBoxVanillaExe
            // 
            textBoxVanillaExe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxVanillaExe.Location = new Point(9, 65);
            textBoxVanillaExe.Margin = new Padding(4, 3, 4, 3);
            textBoxVanillaExe.Name = "textBoxVanillaExe";
            textBoxVanillaExe.Size = new Size(324, 23);
            textBoxVanillaExe.TabIndex = 5;
            // 
            // buttonVanillaExeBrowse
            // 
            buttonVanillaExeBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonVanillaExeBrowse.Location = new Point(341, 65);
            buttonVanillaExeBrowse.Margin = new Padding(4, 3, 4, 3);
            buttonVanillaExeBrowse.Name = "buttonVanillaExeBrowse";
            buttonVanillaExeBrowse.Size = new Size(88, 23);
            buttonVanillaExeBrowse.TabIndex = 3;
            buttonVanillaExeBrowse.Text = "Browse...";
            buttonVanillaExeBrowse.UseVisualStyleBackColor = true;
            buttonVanillaExeBrowse.Click += buttonVanillaExeBrowse_Click;
            // 
            // groupBoxPS3Tweaks
            // 
            groupBoxPS3Tweaks.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxPS3Tweaks.Controls.Add(checkBoxPS3Tweaks);
            groupBoxPS3Tweaks.Controls.Add(labelPS3Tweaks);
            groupBoxPS3Tweaks.Location = new Point(15, 277);
            groupBoxPS3Tweaks.Name = "groupBoxPS3Tweaks";
            groupBoxPS3Tweaks.Size = new Size(438, 73);
            groupBoxPS3Tweaks.TabIndex = 4;
            groupBoxPS3Tweaks.TabStop = false;
            groupBoxPS3Tweaks.Text = "Postscriptthree tweaks";
            // 
            // checkBoxPS3Tweaks
            // 
            checkBoxPS3Tweaks.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxPS3Tweaks.AutoSize = true;
            checkBoxPS3Tweaks.Location = new Point(10, 48);
            checkBoxPS3Tweaks.Name = "checkBoxPS3Tweaks";
            checkBoxPS3Tweaks.Size = new Size(68, 19);
            checkBoxPS3Tweaks.TabIndex = 1;
            checkBoxPS3Tweaks.Text = "Enabled";
            checkBoxPS3Tweaks.UseVisualStyleBackColor = true;
            // 
            // labelPS3Tweaks
            // 
            labelPS3Tweaks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelPS3Tweaks.Location = new Point(10, 19);
            labelPS3Tweaks.Name = "labelPS3Tweaks";
            labelPS3Tweaks.Size = new Size(422, 20);
            labelPS3Tweaks.TabIndex = 0;
            labelPS3Tweaks.Text = "Include additional features enabled by the Postscriptthree Tweaks mod.";
            // 
            // groupBoxRememberLastOpened
            // 
            groupBoxRememberLastOpened.Controls.Add(checkBoxRemeberLastOpened);
            groupBoxRememberLastOpened.Location = new Point(15, 12);
            groupBoxRememberLastOpened.Name = "groupBoxRememberLastOpened";
            groupBoxRememberLastOpened.Size = new Size(437, 51);
            groupBoxRememberLastOpened.TabIndex = 5;
            groupBoxRememberLastOpened.TabStop = false;
            groupBoxRememberLastOpened.Text = "Remember last opened files";
            // 
            // checkBoxRemeberLastOpened
            // 
            checkBoxRemeberLastOpened.AutoSize = true;
            checkBoxRemeberLastOpened.Location = new Point(9, 22);
            checkBoxRemeberLastOpened.Name = "checkBoxRemeberLastOpened";
            checkBoxRemeberLastOpened.Size = new Size(68, 19);
            checkBoxRemeberLastOpened.TabIndex = 0;
            checkBoxRemeberLastOpened.Text = "Enabled";
            checkBoxRemeberLastOpened.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(464, 391);
            Controls.Add(groupBoxRememberLastOpened);
            Controls.Add(groupBoxPS3Tweaks);
            Controls.Add(groupBoxVanillaExe);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(groupBoxBattleLgp);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(440, 430);
            Name = "SettingsForm";
            Text = "Scarlet - Settings";
            groupBoxBattleLgp.ResumeLayout(false);
            groupBoxBattleLgp.PerformLayout();
            groupBoxVanillaExe.ResumeLayout(false);
            groupBoxVanillaExe.PerformLayout();
            groupBoxPS3Tweaks.ResumeLayout(false);
            groupBoxPS3Tweaks.PerformLayout();
            groupBoxRememberLastOpened.ResumeLayout(false);
            groupBoxRememberLastOpened.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxBattleLgp;
        private TextBox textBoxBattleLgp;
        private Button buttonBattleLgpBrowse;
        private Label labelBattleLgp;
        private Button buttonOK;
        private Button buttonCancel;
        private GroupBox groupBoxVanillaExe;
        private Label labelVanillaExe;
        private TextBox textBoxVanillaExe;
        private Button buttonVanillaExeBrowse;
        private GroupBox groupBoxPS3Tweaks;
        private Label labelPS3Tweaks;
        private CheckBox checkBoxPS3Tweaks;
        private GroupBox groupBoxRememberLastOpened;
        private CheckBox checkBoxRemeberLastOpened;
    }
}