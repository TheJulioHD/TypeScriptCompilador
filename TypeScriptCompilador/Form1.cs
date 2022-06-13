using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeScriptCompilador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lexicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lexico = new lexico(codetext.Text);
            lexico.runlexicon();
            List<errores> listaErroresLexico = lexico.listaErroresLexicio;

            var Lista = new BindingList<token>(lexico.listaToken);
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = Lista;
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = listaErroresLexico;

        }

        private void console_Click(object sender, EventArgs e)
        {
            var lexico = new lexico(codetext.Text);
            List<errores> listaErroresLexico = lexico.listaErroresLexicio;
            dataGridView2.DataSource = listaErroresLexico;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmabout = new FrmAbout();
            frmabout.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                codetext.Text = openFileDialog1.FileName;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                codetext.Text= saveFileDialog1.FileName;
            }
        }
    }
}
