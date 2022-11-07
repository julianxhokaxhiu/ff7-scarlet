﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FF7Scarlet
{
    public partial class CodeForm : Form
    {
        public Code Code { get; private set; }
        private List<OpcodeInfo> currList;
        private bool loading = true, unsavedChanges = false;

        public CodeForm(Code code = null)
        {
            InitializeComponent();
            Code = code;
        }

        private void CodeForm_Load(object sender, EventArgs e)
        {
            //get command list
            foreach (var command in CommandInfo.COMMAND_LIST)
            {
                comboBoxCommands.Items.Add(command.Description);
            }
            comboBoxCommands.SelectedIndex = 0;
            comboBoxOpcodeGroups.SelectedIndex = 0;

            //fill form with current code info
            if (Code != null)
            {
                var opcode = OpcodeInfo.GetInfo(Code.GetPrimaryOpcode());
                var command = CommandInfo.GetInfo(opcode.EnumValue);

                if (command != null)
                {
                    comboBoxCommands.SelectedIndex = CommandInfo.COMMAND_LIST.ToList().IndexOf(command);
                    labelParameter1.Text = command.Parameter1;
                    labelParameter2.Text = command.Parameter2;

                    if (Code is CodeLine)
                    {
                        comboBoxOpcodeGroups.SelectedIndex = (int)opcode.Group;
                        textBoxParameter1.Text = Code.GetParameter().ToString();
                        comboBoxOpcodes.SelectedIndex = currList.IndexOf(opcode);
                        comboBoxManualParameter.Text = Code.GetParameter().ToString();
                        SetParameterVisibility(1, true);
                        SetParameterVisibility(2, false);
                    }
                    else
                    {
                        var c = Code as CodeBlock;
                        if (opcode.EnumValue == Opcodes.DebugMessage)
                        {
                            textBoxParameter1.Text = c.GetPopCount().ToString();
                            textBoxParameter2.Text = c.GetParameter().ToString();
                            SetParameterVisibility(1, true);
                            SetParameterVisibility(2, true);
                        }
                        else if (opcode.PopCount > 0)
                        {
                            textBoxParameter1.Text = c.GetCodeAtPosition(0).Disassemble(false);
                            SetParameterVisibility(1, true);
                            if (opcode.PopCount >= 2)
                            {
                                textBoxParameter2.Text = c.GetCodeAtPosition(1).Disassemble(false);
                                SetParameterVisibility(2, true);
                            }
                            else if (opcode.ParameterType != ParameterTypes.None)
                            {
                                textBoxParameter2.Text = c.GetParameter().ToString();
                                SetParameterVisibility(2, true);
                            }
                            else
                            {
                                SetParameterVisibility(2, false);
                            }
                        }
                        else
                        {
                            SetParameterVisibility(1, false);
                        }
                    }
                }
                else if (Code is CodeLine)
                {
                    var c = Code as CodeLine;
                    var o = OpcodeInfo.GetInfo(c.Opcode);
                    tabControlOptions.SelectedTab = tabPageManual;
                    comboBoxOpcodeGroups.SelectedIndex = (int)o.Group;
                    comboBoxOpcodes.SelectedIndex = currList.IndexOf(o);

                    if (o.ParameterType != ParameterTypes.None)
                    {
                        comboBoxManualParameter.Text = c.Parameter.ToString();
                    }
                }
            }
            loading = false;
        }

        private void UpdateOpcodesList()
        {
            int selected = comboBoxOpcodeGroups.SelectedIndex;
            if (Enum.IsDefined(typeof(OpcodeGroups), selected))
            {
                var currGroup = (OpcodeGroups)selected;
                currList =
                    (from o in OpcodeInfo.OPCODE_LIST
                     where o.Group == currGroup
                     select o).ToList();

                comboBoxOpcodes.Items.Clear();
                foreach (var opcode in currList)
                {
                    if (opcode.EnumValue == Opcodes.Label)
                    {
                        comboBoxOpcodes.Items.Add("LABEL");
                    }
                    else
                    {
                        comboBoxOpcodes.Items.Add($"{opcode.Code:X2} -- {opcode.Name}");
                    }
                }
                comboBoxOpcodes.SelectedIndex = 0;
            }
            if (!loading) { unsavedChanges = true; }
        }

        private void UpdateOpcodeParameter()
        {
            int selected = comboBoxOpcodes.SelectedIndex;
            if (selected >= 0 && selected < currList.Count)
            {
                var opcode = currList[selected];
                labelManualParameter.Visible = comboBoxManualParameter.Visible = 
                    (opcode.ParameterType != ParameterTypes.None);
            }
            if (!loading) { unsavedChanges = true; }
        }

        private void SetParameterVisibility(int parameter, bool visible)
        {
            if (parameter == 1)
            {
                labelParameter1.Visible = textBoxParameter1.Visible = buttonParameter1.Visible = visible;
            }
            else if (parameter == 2)
            {
                labelParameter2.Visible = textBoxParameter2.Visible = buttonParameter2.Visible = visible;
            }
        }

        private void EditParameter(int pos)
        {
            Code param;

            //check if parameter is a string or not
            bool isString = false;
            if (Code is CodeBlock)
            {
                if (Code.GetPrimaryOpcode() == (int)Opcodes.DebugMessage)
                {
                    if (pos == 0)
                    {
                        param = new CodeLine(Code.Parent, Code.GetHeader(), (int)Opcodes.PushConst01, new FFText(Code.GetPopCount()));
                    }
                    else
                    {
                        param = (Code as CodeBlock).GetCodeAtEnd();
                        isString = true;
                    }
                }
                else
                {
                    param = (Code as CodeBlock).GetCodeAtPosition(pos);
                }
            }
            else
            {
                param = Code;
                if (Code.GetPrimaryOpcode() == (int)Opcodes.ShowMessage)
                {
                    isString = true;
                }
            }

            //send the parameter to the parameter form for editing
            var temp = new List<Code> { };
            foreach (var p in param.BreakDown())
            {
                temp.Add(p);
            }
            using (var paramForm = new ParameterForm(temp, isString))
            {
                if (paramForm.ShowDialog() == DialogResult.OK)
                {
                    if (isString)
                    {
                        textBoxParameter1.Text = paramForm.Code[0].GetParameter().ToString();
                        comboBoxManualParameter.Text = textBoxParameter1.Text;
                    }
                    else
                    {
                        Code test = null;
                        if (paramForm.Code.Count > 1)
                        {
                            test = new CodeBlock(null, paramForm.Code);
                        }
                        else
                        {
                            test = paramForm.Code[0];
                        }

                        //update parameter text
                        if (tabControlOptions.SelectedTab == tabPageGenerate)
                        {
                            if (pos == 0)
                            {
                                textBoxParameter1.Text = test.Disassemble(false);
                            }
                            else
                            {
                                textBoxParameter2.Text = test.Disassemble(false);
                            }
                        }
                        else
                        {
                            comboBoxManualParameter.Text = test.Disassemble(false);
                        }
                    }
                    unsavedChanges = true;
                }
            }
        }

        private void comboBoxOpcodeGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOpcodesList();
        }

        private void comboBoxOpcodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOpcodeParameter();
        }

        private void buttonParameter1_Click(object sender, EventArgs e)
        {
            EditParameter(0);
        }

        private void buttonParameter2_Click(object sender, EventArgs e)
        {
            EditParameter(1);
        }

        private void comboBoxManualParameter_TextUpdate(object sender, EventArgs e)
        {
            if (!loading)
            {
                unsavedChanges = true;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (unsavedChanges || Code == null)
            {
                var tempList = new List<Code> { };
                if (tabControlOptions.SelectedTab == tabPageGenerate)
                {
                    //stuff
                }
                else
                {
                    var op = currList[comboBoxOpcodes.SelectedIndex];
                    if (op.ParameterType == ParameterTypes.None)
                    {
                        Code = new CodeLine(null, -1, op.Code);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(comboBoxManualParameter.Text))
                        {
                            MessageBox.Show("Parameter cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Code = new CodeLine(null, -1, op.Code, new FFText(comboBoxManualParameter.Text));
                    }
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
