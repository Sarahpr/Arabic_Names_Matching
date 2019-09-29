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
    public partial class Similarity : Form
    {
        OleDbConnection con1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\dell\Documents\Project for improve Levenshtain1.accdb");
        OleDbDataAdapter dp;
        DataTable DT;
        OleDbCommand cmd;

        void filldata_f()
        {
            DT = new DataTable();
            dp = new OleDbDataAdapter("select * from Form_Similarity", con1);
            dp.Fill(DT);
            dataGridView1.DataSource = DT;
        }

        public Similarity()
        {
            InitializeComponent();
            filldata_f();
        }

        private void Similarity_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommandBuilder cmdb = new OleDbCommandBuilder(dp);
            dp.Update(DT);
            MessageBox.Show("تم حفظ التغيرات");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
