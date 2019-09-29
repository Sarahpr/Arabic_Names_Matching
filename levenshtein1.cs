using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.OleDb;

using System.Windows.Forms;

namespace Levenshtein_برنامج_يوضح_تطوير_خوارزميــة
{
    public partial class levenshtein1 : Form
    {/*==============Data Members==============*/
        string source_word, target_word;


        //================================================
        OleDbConnection con1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\dell\Documents\Project for improve Levenshtain1.accdb");

        OleDbCommand cmd;
        //=============================================
        int max_L(string s_w, string T_w)
        {
            if (s_w.Length > T_w.Length)
                return s_w.Length;
            return T_w.Length;
        }
        //==================================================
        double minimumel(double x, double y, double z)
        {
            if (x < y && x < z)
                return x;
            else
                if (y < x && y < z)
                    return y;
                else return z;
        }
        //========================================
        double cul_destance_l(string source, string target)
        {
            source_word = source;
            target_word = target;
            int c;
            double[,] cost;
            cost = new double[source_word.Length + 1, target_word.Length + 1];
            for (int i = 0; i <= source_word.Length; i++) cost[i, 0] = i;
            for (int i = 0; i <= target_word.Length; i++) cost[0, i] = i;
            for (int j = 1; j <= target_word.Length; j++)
                for (int i = 1; i <= source_word.Length; i++)
                {

                    if (source_word[i - 1] == target_word[j - 1]) c = 0; else c = 1;
                    cost[i, j] = minimumel(cost[i - 1, j] + 1, cost[i, j - 1] + 1, cost[i - 1, j - 1] + c);

                } return (cost[source_word.Length, target_word.Length]);

        }
        double similarity_R(string source_W, string Target_W, double cost)
        { return ((1 - (cost / (float)max_L(source_W, Target_W))) * 100); }
        //=================================
        void filldata2()
        {
            cmd = new OleDbCommand("select * from DataSet", con1);
            con1.Open();
            int row = 0;
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    dataGridView2.Rows.Add();
                    dataGridView2[0, row].Value = dr[0].ToString();
                    dataGridView2[1, row].Value = dr[1].ToString();
                    double Distance = cul_destance_l(dr[0].ToString(), dr[1].ToString());
                    double similarity = similarity_R(source_word, target_word, Distance);
                    int s = Convert.ToInt16(similarity);
                    dataGridView2[2, row].Value = Distance.ToString();
                    dataGridView2[3, row].Value = s.ToString() + "%";

                    row++;
                }

            }
            con1.Close();
        }
        public levenshtein1()
        {
            InitializeComponent();
            filldata2();
        }

        private void levenshtein1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
