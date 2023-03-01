﻿namespace FF7Scarlet.SceneEditor
{
    public partial class SceneExportForm : Form
    {
        private readonly Scene[] scenes;
        private int selectedScene;
        private bool processing = false;

        public SceneExportForm(Scene[] sceneList, int selected)
        {
            InitializeComponent();
            scenes = sceneList;
            selectedScene = selected;

            for (int i = 0; i < DataManager.SCENE_COUNT; ++i)
            {
                listBoxSceneList.Items.Add($"{i}: {scenes[i].GetEnemyNames()}");
            }
            if (selected == -1)
            {
                radioButtonMultiple.Checked = true;
                radioButtonSelected.Enabled = false;
            }
            else
            {
                radioButtonSelected.Checked = true;
                listBoxSceneList.SelectedIndices.Add(selected);
            }
        }

        private void radioButtonMultiple_CheckedChanged(object sender, EventArgs e)
        {
            listBoxSceneList.Enabled = radioButtonMultiple.Checked;
            buttonSelectAll.Enabled = radioButtonMultiple.Checked;
            buttonUnselectAll.Enabled = radioButtonMultiple.Checked;
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            listBoxSceneList.SelectedIndices.Clear();
            for (int i = 0; i < DataManager.SCENE_COUNT; ++i)
            {
                listBoxSceneList.SelectedIndices.Add(i);
            }
        }

        private void buttonUnselectAll_Click(object sender, EventArgs e)
        {
            listBoxSceneList.SelectedIndices.Clear();
        }

        private async void buttonExport_Click(object sender, EventArgs e)
        {
            if (listBoxSceneList.SelectedIndices.Count == 0)
            {
                MessageBox.Show("No scenes selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    DialogResult result;
                    string path;
                    bool success = false;

                    if (radioButtonSelected.Checked) //single scene
                    {
                        using (var save = new SaveFileDialog())
                        {
                            save.FileName = $"scene.{selectedScene}.bin";
                            save.Filter = "Scene file|*.bin";
                            result = save.ShowDialog();
                            path = save.FileName;
                        }

                        if (result == DialogResult.OK)
                        {
                            groupBoxExport.Enabled = false;
                            buttonExport.Enabled = false;
                            ExportScene(selectedScene, path);
                            progressBarSaving.Value = 100;
                            success = true;
                        }
                    }
                    else //multiple scenes
                    {
                        using (var save = new FolderBrowserDialog())
                        {
                            result = save.ShowDialog();
                            path = save.SelectedPath;
                        }

                        if (result == DialogResult.OK)
                        {
                            if (!Directory.Exists(path))
                            {
                                MessageBox.Show("Invalid path.", "Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
                            else
                            {
                                groupBoxExport.Enabled = false;
                                buttonExport.Enabled = false;
                                processing = true;
                                success = await ExportMulti(path);
                            }
                        }
                    }
                    if (success)
                    {
                        MessageBox.Show("Scene(s) exported successfully.", "Done!", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        processing = false;
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBarSaving.Value = 0;
                    processing = false;
                }
            }
        }

        private void ExportScene(int scene, string path)
        {
            try
            {
                scenes[scene].UpdateRawData();
                File.WriteAllBytes(path, scenes[scene].GetRawData());
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception was thrown in scene {scene}:\n\n{ex.Message}", ex);
            }
        }

        private Task ExportSceneAsync(int scene, string path)
        {
            return Task.Run(() =>
            {
                try
                {
                    ExportScene(scene, path);
                }
                catch (Exception ex)
                {
                    throw new Exception($"An exception was thrown in scene {scene}:\n\n{ex.Message}", ex);
                }
            });
        }

        private async Task<bool> ExportMulti(string folderPath)
        {
            int index = 0;
            try
            {
                int count = listBoxSceneList.SelectedIndices.Count;
                for (int i = 0; i < count; ++i)
                {
                    index = listBoxSceneList.SelectedIndices[i];
                    string filePath = folderPath + $"\\scene.{index}.bin";
                    await ExportSceneAsync(index, filePath);
                    progressBarSaving.Value = ((i + 1) / count) * 100;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void SceneExportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (processing) { e.Cancel = true; }
        }
    }
}