using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Levenshtein_برنامج_يوضح_تطوير_خوارزميــة
{
    public partial class keymn : Form
    {
        OleDbConnection con1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\dell\Documents\Project for improve Levenshtain1.accdb");
        OleDbDataAdapter dp;
        DataTable DT;
        OleDbCommand cmd;

        void filldata_k()
        {
            DT = new DataTable();
            dp = new OleDbDataAdapter("select * from Keyboard_Similarity_EQ", con1);
            dp.Fill(DT);
            dataGridView1.DataSource = DT;
        }
        public keymn()
        {
            InitializeComponent();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void keymn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            filldata_k();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
