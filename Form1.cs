using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace DBMaiden {
    // 0 id +
    // 1 title +
    // 2 credits +
    // 3 icon0 +
    // 4 icon0_mirror +
    // 5 url + 
    // 6 url_mirror +
    // 7 readme +
    // 8 readme_mirror +
    // 9 src +
    // 10 download_src_mirror + 
    // 11 time_added + (automated)
    // 12 config_type +
    // 13 options + 
    // 14 type + 
    // 15 depends +
    // 16 visible +
    public partial class Form1 : Form {
        const string magicHeader = "id,title,credits,download_icon0,download_icon0_mirror,download_url,download_url_mirror,download_readme,download_readme_mirror,download_src,download_src_mirror,time_added,config_type,options,type,depends,visible";
        string[] lines;
        string[] info;
        public Form1() {
            InitializeComponent();
        }
        public static T[] RemoveAt<T>(T[] array, int index) {
            var foos = new List<T>(array);
            foos.RemoveAt(index);
            return foos.ToArray();
        }

        private void refreshTable() {
            listBox1.Items.Clear();
            string[] linesNames = new string[lines.Length];
            for(int i = 0; i < lines.Length; i++) {
                string[] item = lines[i].Split(','); // array.Length == 17
                linesNames[i] = item[1] + " / " + item[0];
            }
            listBox1.Items.AddRange(linesNames);
            listBox1.Items.RemoveAt(0); // magicHeader
        }

        private void refreshInfo() {
            string outputLine = "";
            info[11] = ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
            for (int i = 0; i < 17; i++) {
                if (i != 0 && i != 17) outputLine += ",";
                outputLine += info[i];
            }
            lines[listBox1.SelectedIndex + 1] = outputLine;
        }

        private void clearLines() {
            lines = new string[] { magicHeader };
        }

        private void Form1_Load(object sender, EventArgs e) {
            clearLines();
            refreshTable();
        }

        private void Button1_Click(object sender, EventArgs e) {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Beware!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) {
                clearLines();
                refreshTable();
            } 
        }

        private void Button3_Click(object sender, EventArgs e) {
            SaveFileDialog path = new SaveFileDialog();
            path.DefaultExt = "csv";
            path.RestoreDirectory = true;
            path.ShowDialog();
            File.WriteAllLines(path.FileName, lines);
        }

        private void Button2_Click(object sender, EventArgs e) {
            OpenFileDialog path = new OpenFileDialog();
            path.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            path.RestoreDirectory = true;
            path.ShowDialog();
            string[] tempLines = File.ReadAllLines(path.FileName);
            if(tempLines[0] != magicHeader) {
                MessageBox.Show("File is not valid.");
            } else {
                lines = tempLines;
                refreshTable();
            }
            
        }

        private void Button4_Click(object sender, EventArgs e) {
            Array.Resize(ref lines, lines.Length + 1);
            lines[lines.Length - 1] = magicHeader;
            int select = listBox1.SelectedIndex;
            refreshTable();
            listBox1.SelectedIndex = select;
        }

        private void Enable() { // yandev called he wants his code back
            checkBox1.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            textBox11.Enabled = true;
            textBox12.Enabled = true;
            textBox13.Enabled = true;
            textBox14.Enabled = true;
            textBox15.Enabled = true;
        }
        
        private void Disable() {
            checkBox1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            textBox13.Enabled = false;
            textBox14.Enabled = false;
            textBox15.Enabled = false;
        }

        private void Button5_Click(object sender, EventArgs e) {
            if(listBox1.SelectedIndex >= 0) {
                lines = RemoveAt(lines, listBox1.SelectedIndex + 1);
                int select = listBox1.SelectedIndex;
                refreshTable();
                if (select - 1 >= 0) {
                    listBox1.SelectedIndex = select - 1;
                } else {
                    Disable();
                }
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (listBox1.SelectedIndex < 0) {
                Disable();
            } else {
                Enable();
            }
            info = lines[listBox1.SelectedIndex + 1].Split(','); // array.Length == 17
            if (info[16] == "True") checkBox1.Checked = true;
            else checkBox1.Checked = false;
            textBox1.Text = info[0];
            textBox2.Text = info[1];
            textBox3.Text = info[2];
            textBox5.Text = info[3];
            textBox4.Text = info[4];
            textBox7.Text = info[5];
            textBox6.Text = info[6];
            textBox9.Text = info[7];
            textBox8.Text = info[8];
            textBox11.Text = info[9];
            textBox10.Text = info[10];
            textBox15.Text = info[12];
            textBox14.Text = info[13];
            textBox13.Text = info[14];
            textBox12.Text = info[15];
            textBox16.Text = info[11]; // unix time read only
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e) {
            if (checkBox1.Checked) info[16] = "True";
            else info[16] = "False";
            refreshInfo();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e) {
            info[0] = textBox1.Text;
            refreshInfo();
        }

        private void Label1_Click(object sender, EventArgs e) {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e) {
            info[1] = textBox2.Text;
            refreshInfo();
        }

        private void TextBox3_TextChanged(object sender, EventArgs e) {
            info[2] = textBox3.Text;
            refreshInfo();
        }

        private void TextBox5_TextChanged(object sender, EventArgs e) {
            info[3] = textBox5.Text;
            refreshInfo();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e) {
            info[4] = textBox4.Text;
            refreshInfo();
        }

        private void TextBox7_TextChanged(object sender, EventArgs e) {
            info[5] = textBox7.Text;
            refreshInfo();
        }

        private void TextBox6_TextChanged(object sender, EventArgs e) {
            info[6] = textBox6.Text;
            refreshInfo();
        }

        private void TextBox9_TextChanged(object sender, EventArgs e) {
            info[7] = textBox9.Text;
            refreshInfo();
        }

        private void TextBox8_TextChanged(object sender, EventArgs e) {
            info[8] = textBox8.Text;
            refreshInfo();
        }

        private void TextBox11_TextChanged(object sender, EventArgs e) {
            info[9] = textBox11.Text;
            refreshInfo();
        }

        private void TextBox10_TextChanged(object sender, EventArgs e) {
            info[10] = textBox10.Text;
            refreshInfo();
        }

        private void TextBox15_TextChanged(object sender, EventArgs e) {
            info[12] = textBox15.Text;
            refreshInfo();
        }

        private void TextBox14_TextChanged(object sender, EventArgs e) {
            info[13] = textBox14.Text;
            refreshInfo();
        }

        private void TextBox13_TextChanged(object sender, EventArgs e) {
            info[14] = textBox13.Text;
            refreshInfo();
        }

        private void TextBox12_TextChanged(object sender, EventArgs e) {
            info[15] = textBox12.Text;
            refreshInfo();
        }
    }
}
