﻿using System.ComponentModel;

namespace FF7Scarlet.AIEditor
{
    public partial class ScriptControl : UserControl
    {
        private AIContainer? aiContainer;
        private int selectedScriptIndex = -1;
        private List<Code>? clipboard;

        public event EventHandler? DataChanged, ScriptAdded, ScriptRemoved;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AIContainer? AIContainer
        {
            get { return aiContainer; }
            set
            {
                aiContainer = value;
                DisplayScript(SelectedScriptIndex);
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedScriptIndex
        {
            get { return selectedScriptIndex; }
            set
            {
                selectedScriptIndex = value;
                DisplayScript(SelectedScriptIndex);
            }
        }
        public Script? SelectedScript
        {
            get { return AIContainer?.Scripts[SelectedScriptIndex]; }
        }
        private Code? SelectedCode
        {
            get
            {
                if (AIContainer == null || SelectedScript == null) { return null; }
                return SelectedScript.GetCodeAtPosition(SelectedCodeIndex);
            }
        }
        private int SelectedCodeIndex
        {
            get { return listBoxCurrScript.SelectedIndex; }
        }

        public ScriptControl()
        {
            InitializeComponent();
        }

        private void DisplayScript(int scriptID)
        {
            listBoxCurrScript.BeginUpdate();
            listBoxCurrScript.Items.Clear();
            if (scriptID >= 0 && scriptID < Script.SCRIPT_COUNT)
            {
                if (AIContainer == null)
                {
                    listBoxCurrScript.Enabled = false;
                    listBoxCurrScript.Items.Add("(Enemy doesn't exist.)");
                    toolStripScript.Enabled = false;
                }
                else
                {
                    var script = AIContainer.Scripts[scriptID];
                    toolStripScript.Enabled = true;
                    if (script == null)
                    {
                        listBoxCurrScript.Enabled = false;
                        listBoxCurrScript.Items.Add("(Script is empty)");
                    }
                    else
                    {
                        listBoxCurrScript.Enabled = true;
                        foreach (var line in script.Disassemble())
                        {
                            listBoxCurrScript.Items.Add(line);
                        }
                    }
                }
            }
            listBoxCurrScript.EndUpdate();
        }

        private void ReloadScript(int selected)
        {
            DisplayScript(SelectedScriptIndex);
            listBoxCurrScript.SelectedIndex = selected;
        }

        private void SetClipboard(bool cut)
        {
            if (SelectedScript != null)
            {
                //get indices as ints
                var indices = new List<int> { };
                foreach (int i in listBoxCurrScript.SelectedIndices)
                {
                    indices.Add(i);
                }
                indices.Sort();

                //if code is selected, copy it to the clipboard
                if (indices.Count > 0)
                {
                    clipboard = new List<Code> { };
                    foreach (int i in indices)
                    {
                        clipboard.Add(SelectedScript.GetCodeAtPosition(i));
                    }

                    if (cut) //remove code from the script
                    {
                        RemoveSelectedLines();
                    }
                    toolStripButtonPaste.Enabled = true;
                }
            }
        }

        private void RemoveSelectedLines()
        {
            if (SelectedScript != null)
            {
                //get indices as ints
                var indices = new List<int> { };
                foreach (int i in listBoxCurrScript.SelectedIndices)
                {
                    indices.Add(i);
                }
                indices.Sort();
                indices.Reverse();

                //if code is selected, delete it
                if (indices.Count > 0)
                {
                    foreach (int i in indices)
                    {
                        SelectedScript.RemoveCodeAtPosition(i);
                        listBoxCurrScript.Items.RemoveAt(i);
                    }
                    
                    if (listBoxCurrScript.Items.Count == 0)
                    {
                        InvokeScriptRemoved();
                    }
                }
                InvokeDataChanged();
            }
        }

        private void MoveUp()
        {
            if (SelectedCode != null && SelectedScript != null)
            {
                int temp = SelectedCodeIndex;
                SelectedScript.MoveCodeUp(temp);
                DisplayScript(SelectedScriptIndex);
                listBoxCurrScript.SelectedIndex = temp - 1;
                InvokeDataChanged();
            }
        }

        private void MoveDown()
        {
            if (SelectedCode != null && SelectedScript != null)
            {
                int temp = SelectedCodeIndex;
                SelectedScript.MoveCodeDown(temp);
                DisplayScript(SelectedScriptIndex);
                listBoxCurrScript.SelectedIndex = temp + 1;
                InvokeDataChanged();
            }
        }

        private void CreateLabelAtPosition(byte opcode, FFText label, int pos)
        {
            if (SelectedScript != null)
            {
                if (opcode != (byte)Opcodes.Label) //don't add the label if it's already been added
                {
                    SelectedScript.InsertCodeAtPosition(pos, new CodeLine(SelectedScript,
                        HexParser.NULL_OFFSET_16_BIT, (byte)Opcodes.Label, label));
                }
                SelectedScript.AddLabel(label.ToInt());
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if (AIContainer == null)
            {
                MessageBox.Show("The script container does not exist.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (SelectedScript == null)
            {
                MessageBox.Show("No script selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result;
                Code newCode;
                bool createLabel;

                using (var codeForm = new CodeForm(SelectedScript))
                {
                    result = codeForm.ShowDialog();
                    newCode = codeForm.Code;
                    createLabel = codeForm.CreateNewLabel;
                }

                if (result == DialogResult.OK)
                {
                    int i = 0;
                    if (SelectedScript.IsEmpty)
                    {
                        AIContainer.Scripts[SelectedScriptIndex] = new Script(AIContainer, newCode);
                        listBoxCurrScript.SelectedIndex = 0;
                        InvokeScriptAdded();
                    }
                    else
                    {
                        i = SelectedCodeIndex + 1;
                        SelectedScript.InsertCodeAtPosition(i, newCode);
                    }
                    if (createLabel)
                    {
                        var param = newCode.GetParameter();
                        if (param != null)
                        {
                            CreateLabelAtPosition(newCode.GetPrimaryOpcode(), param, i + 1);
                        }
                    }
                    ReloadScript(i);
                    InvokeDataChanged();
                }
            }
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (AIContainer != null && SelectedScript != null && SelectedCode != null)
            {
                DialogResult result;
                Code newCode;
                bool createLabel;

                using (var codeForm = new CodeForm(SelectedScript, SelectedCode))
                {
                    result = codeForm.ShowDialog();
                    newCode = codeForm.Code;
                    createLabel = codeForm.CreateNewLabel;
                }

                if (result == DialogResult.OK)
                {
                    int i = SelectedCodeIndex;
                    newCode.SetParent(SelectedScript);
                    SelectedScript.ReplaceCodeAtPosition(i, newCode);

                    if (createLabel)
                    {
                        var param = newCode.GetParameter();
                        if (param != null)
                        {
                            CreateLabelAtPosition(newCode.GetPrimaryOpcode(), param, i + 1);
                        }
                    }
                    ReloadScript(i);
                    InvokeDataChanged();
                }
            }
        }

        private void toolStripButtonCut_Click(object sender, EventArgs e)
        {
            SetClipboard(true);
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            SetClipboard(false);
        }

        private void toolStripButtonPaste_Click(object sender, EventArgs e)
        {
            //stuff
        }

        private void toolStripButtonMoveUp_Click(object sender, EventArgs e)
        {
            MoveUp();
        }

        private void toolStripButtonMoveDown_Click(object sender, EventArgs e)
        {
            MoveDown();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            RemoveSelectedLines();
        }

        private void listBoxCurrScript_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        SetClipboard(false);
                        break;
                    case Keys.X:
                        SetClipboard(true);
                        break;
                    case Keys.V:
                        //to add
                        break;
                    case Keys.Up:
                        e.SuppressKeyPress = true;
                        MoveUp();
                        break;
                    case Keys.Down:
                        e.SuppressKeyPress = true;
                        MoveDown();
                        break;
                }
            }
            else if (e.Modifiers == Keys.None)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    RemoveSelectedLines();
                }
            }
        }

        private void InvokeDataChanged()
        {
            DataChanged?.Invoke(this, new EventArgs());
        }

        private void InvokeScriptAdded()
        {
            ScriptAdded?.Invoke(this, new EventArgs());
        }

        private void InvokeScriptRemoved()
        {
            ScriptRemoved?.Invoke(this, new EventArgs());
        }
    }
}
