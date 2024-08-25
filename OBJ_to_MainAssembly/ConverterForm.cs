using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace OBJ_to_MainAssembly
{
    public partial class ConverterForm : Form
    {
        string filePath;
        float creationScale = 1;
        bool InvertZY = false, RoundVertexPos = false, Mirror = false, ShowFrameSegments = false;
        char CreationForward = 'X';

        public ConverterForm()
        {
            InitializeComponent();
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Converter converter = new(backgroundWorker1);
            Point[] points;
            Face[] faces;
            switch (Path.GetExtension(filePath))
            {
                case ".obj":
                    Console.WriteLine("Loading OBJ file...");
                    if (!converter.convertOBJ(filePath, out points, out faces)) return;
                    converter.Convert(points, faces, creationScale, InvertZY, RoundVertexPos, Mirror, CreationForward, ShowFrameSegments);
                    break;
                case ".stl":
                    Console.WriteLine("Loading STL file...");
                    if (!converter.convertSTL(filePath, out points, out faces)) return;
                    converter.Convert(points, faces, creationScale, InvertZY, RoundVertexPos, Mirror, CreationForward, ShowFrameSegments);
                    break;
                default:
                    Console.WriteLine("Unsupported file type: " + Path.GetExtension(filePath));
                    MessageBox.Show("Please select a file !");
                    return;
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100)
            {
                progressBar1.Visible = false;
                MessageBox.Show("Done !");
            }
        }
        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "3D files (*.obj, *.stl)|*.obj;*.stl";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                pathInput.Text = filePath;
            }
        }
        private void pathInput_TextChanged(object sender, EventArgs e)
        {
            filePath = pathInput.Text;
        }
        private void scaleInput_TextChanged(object sender, EventArgs e)
        {
            if (!float.TryParse(scaleInput.Text, out creationScale))
            {
                scaleInput.Text = "1";
                creationScale = 1;
            }
        }
        private void forwardComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreationForward = forwardComboBox.Text[0];
        }
        private void invertCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InvertZY = invertCheckBox.Checked;
        }
        private void roundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RoundVertexPos = roundCheckBox.Checked;
        }
        private void mirrorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Mirror = mirrorCheckBox.Checked;
        }
        private void segmentsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ShowFrameSegments = segmentsCheckBox.Checked;
        }
    }
}