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
    public partial class Database : Form
    {
       
        OleDbConnection con1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\dell\Documents\Project for improve Levenshtain1.accdb");
        OleDbCommand cmd;
        OleDbDataAdapter dp;
        DataTable DT;
        string source_word, target_word, v_1, v_2;
        double[,] cost;
        int row, col;
        //===========================

        void filldgv2()
        {
            DT = new DataTable();
            dp = new OleDbDataAdapter("select * from DataSet", con1);
            dp.Fill(DT);
            dataGridView1.DataSource = DT;
        }
        
        void add()
        {
          
          
           cmd = new OleDbCommand("insert into DataSet values('" + textBox1 .Text  + "','" + textBox2 .Text  + "')", con1);
            con1.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("تمت الاضافة بنجاح");
            con1.Close();
        }
       
        //=================================================
       
       
        //===========================
        void update(int s)
        {
            cmd = new OleDbCommand("update DataSet set Source_Word='" + textBox1.Text + "', Target_Word='" + textBox2.Text + "'where id=" + s + "", con1);
            con1.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("تم التعديل بنجاح");
            con1.Close();
        }
        //=============================================
        void delete()
        {
            cmd = new OleDbCommand("delete from DataSet  where Target_Word='" + textBox2.Text + "' and Source_Word='" + textBox1.Text + "'", con1);
            con1.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("تم الحذف بنجاح");
            con1.Close();
        }
        //=================
        string updates = " ";
        void save()
        {
            MessageBox.Show(updates);
            if (gg != 0)
                update(gg);
            else add();
            gg = 0;
           
        }
        //==========================
        void filldata()
        {
            dp = new OleDbDataAdapter("select * from DataSet", con1);
            DT = new DataTable();
            dp.Fill(DT);
            dataGridView1.DataSource = DT;
            dataGridView1.Columns[0].HeaderText = "الكلمة المصدر";
            dataGridView1.Columns[1].HeaderText = "الكلمة الهدف";
           

            dataGridView1.Columns[0].Width = 230;
            dataGridView1.Columns[1].Width = 230;
           
        }
        public Database()
        {
            InitializeComponent();
            filldata();
          
            Calculate_Di_button.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Clear();
            //textBox2.Clear();
            Calculate_Di_button.Enabled = true;
            
        }
        int gg = 0;
        private void Database_Load(object sender, EventArgs e)
        {
           
        }

        private void Calculate_Di_button_Click(object sender, EventArgs e)
        {
            save();
            filldata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            delete();
            filldata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gg = Convert.ToInt16(textBox3.Text);
            updates = textBox2.Text;
            Calculate_Di_button.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1[0, i].Value.ToString();
            textBox2.Text = dataGridView1[1, i].Value.ToString();
            //textBox3.Text = dataGridView1[0, 0].Value.ToString();
           // textBox3.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
