using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFDownloader
{
    public partial class Form1 : Form
    {
        readonly PDFDownloader dL;
        public Form1()
        {
            InitializeComponent();
            dL = new PDFDownloader();
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(InputBox.Text))
            {
                MessageBox.Show("Error: No input file chosen", "InputError",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(OutputBox.Text))
            {
                MessageBox.Show("Error: No output folder chosen", "OutputError",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            IProgress<int> progress = new Progress<int>(value => {

                if(value > 0)
                {
                    CopyProgressBar.Value = 0;
                    CopyProgressBar.Maximum = value+1;

                }
                else
                {
                    CopyProgressBar.Value += 1;
                }
                
            });

            await Task.Run(() => dL.Run(InputBox.Text, OutputBox.Text, progress));
            Debug.WriteLine("Worked");
        }

        private void PathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog v1 = new FolderBrowserDialog();
            var result = v1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(v1.SelectedPath))
            {
                OutputBox.Text = v1.SelectedPath +"\\";
            }
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog v1 = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx|*.xlsx|All files (*.*)|*,*"
            };

            if(v1.ShowDialog() == DialogResult.OK)
            {
                InputBox.Text = v1.FileName;
            }
        }
    }
}
