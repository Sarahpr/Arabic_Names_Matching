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
    public partial class Frvarible : Form
    {
        OleDbConnection con1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\dell\Documents\Project for improve Levenshtain1.accdb");
        OleDbCommand cmd;
        void filldata()
        {
            cmd = new OleDbCommand("select v_value from varibles", con1);
            con1.Open();
            OleDbDataReader dr = cmd.ExecuteReader();
            dr.Read();
            textBox2.Text = dr[0].ToString();
            dr.Read();
            textBox1.Text = dr[0].ToString();
            con1.Close();
        }
        //==============================
        void updatedata()
        {
            con1.Open();

            cmd = new OleDbCommand("update varibles set v_value='" + textBox1.Text + "'where name_v='∅'  ", con1);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("update varibles set v_value='" + textBox2.Text + "'where name_v='μ' ", con1);
            cmd.ExecuteNonQuery();
            MessageBox.Show("تم التعديل بنجاح");
            con1.Close();
        }
        public Frvarible()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Frvarible_Load(object sender, EventArgs e)
        {
            filldata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updatedata();
        }
    }
}
