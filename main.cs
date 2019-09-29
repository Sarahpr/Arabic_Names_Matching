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
    public partial class main : Form
    {  /*==============Data Members==============*/
        string source_word, target_word, v_1, v_2;
        double[,] cost;
        int row, col;
        //================================================

        OleDbConnection con1 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\dell\Documents\Project for improve Levenshtain1.accdb");
        OleDbDataAdapter dp;
        DataTable DT;
        OleDbCommand cmd;
        //=================================================
        /*=================function Members====================*/
        void filldat_1()
        {
            cmd = new OleDbCommand("select v_value from varibles", con1);
            con1.Open();
            OleDbDataReader dr = cmd.ExecuteReader();
            dr.Read();

            v_1 = dr[0].ToString();
            dr.Read();
            v_2 = dr[0].ToString();
            con1.Close();
        }
        /*__________function for ____________________*/
        //=============================================
        //this function for return the minimum element
        double minimumel(double x, double y, double z)
        {
            if (x < y && x < z)
                return x;
            else
                if (y < x && y < z)
                    return y;
                else return z;
        }
        //=================================================
        /*this function for compute sumtion for two number*/
        float sum(double x, double y)
        {
            float z = (float)x + (float)y; return z;
        }
        //====================================================
        //this function for return cost of insert & delete 
        double ins_del_cost(char a, char b)
        {
            double cost;
            if (a == ' ' || (a != b && a == 'ء') && (b == 'ا' || b == 'و' || b == 'ي'))
                cost = 0;
            else if (a == b)
                cost = Convert.ToDouble(v_2);
            else if (a != b && (a == 'ا' || a == 'و' || a == 'ي'))
                cost = Convert.ToDouble(v_1);
            else
                cost = 1;
            return cost;
        }
        //=============================================
        int max_L(string s_w, string T_w)
        {
            if (s_w.Length > T_w.Length)
                return s_w.Length;
            return T_w.Length;
        }
        //============================================================
        double similarity_R(string source_W, string Target_W, double cost)
        { return ((1 - (cost / (float)max_L(source_W, Target_W))) * 100); }
        //====================================================
        //=============================================

        /*______________functions used to compute replacement cost____________________*/
        //this function for return the keyboard similarity value
        double keyboard_s(char a, char b)
        {
            try
            {
                if (a == 'ئ') row = 36;
                else
                {
                    for (int i = 1; i < dataGridView1.RowCount - 1; i++)
                        if (Convert.ToChar(dataGridView1[i, 0].Value) == a)
                        { row = i; break; }
                }
                if (b == 'ئ') row = 36;
                else
                {
                    for (int i = 1; i < dataGridView1.ColumnCount; i++)
                        if (Convert.ToChar(dataGridView1[0, i].Value) == b)
                        { col = i; break; }
                }
                return 1 - Convert.ToDouble(dataGridView1[row, col].Value);
            }
            catch (Exception e) { return 1; }
        }
        //======================================
        //this function for return the sound similarity value
        double sound_s(char a, char b)
        {
            try
            {
                if (a == 'ئ') row = 36;
                else
                {
                    for (int i = 1; i < dataGridView4.RowCount - 1; i++)
                        if (Convert.ToChar(dataGridView4[i, 0].Value) == a)
                        { row = i; break; }
                }
                if (a == 'ئ') row = 36;
                else
                {
                    for (int i = 1; i < dataGridView4.ColumnCount; i++)
                        if (Convert.ToChar(dataGridView4[0, i].Value) == b)
                        { col = i; break; }
                }
                return 1 - Convert.ToDouble(dataGridView4[row, col].Value);
            }
            catch (Exception e) { return 1; }
        }
        //=================================================
        //this function for return the form similarity value
        double form_s(char a, char b)
        {
            try
            {
                if (a == 'ئ') row = 36;
                else
                {
                    for (int i = 1; i < dataGridView3.RowCount - 1; i++)
                        if (Convert.ToChar(dataGridView3[i, 0].Value) == a)
                        { row = i; break; }
                }
                if (b == 'ئ') col = 36;
                else
                {
                    for (int i = 1; i < dataGridView3.ColumnCount; i++)
                        if (Convert.ToChar(dataGridView3[0, i].Value) == b)
                        { col = i; break; }
                }
                return 1 - Convert.ToDouble(dataGridView3[row, col].Value);
            }
            catch (Exception e) { return 1; }
        }
        //========================================
        //this function for return the keyboard similarity value
        double minimum_s(char a, char b)
        {
            double value1, value2, value3, result;
            value1 = keyboard_s(a, b);
            value2 = sound_s(a, b);
            value3 = form_s(a, b);
            result = minimumel(value1, value2, value3);
            // MessageBox.Show(result.ToString());
            return result;

        }
        //==========================================
        double average_s(char a, char b)
        {
            double value1, value2, value3, result;
            value1 = keyboard_s(a, b);
            value2 = sound_s(a, b);
            value3 = form_s(a, b);
            //result = minimumel(1-value1, 1-value2,1-value3);
            result = (value1 + value2 + value3) / 3;
            return result;

        }
        //==================================
        /*____________________fill dgv with data from sound &form&keyboard &replacement cost________*/
        void filldgv5()
        {

            DT = new DataTable();
            dp = new OleDbDataAdapter("select * from replacement_cost", con1);
            dp.Fill(DT);
            dataGridView5.DataSource = DT;

        }
        void filldgv6()
        {

            DT = new DataTable();
            dp = new OleDbDataAdapter("select * from Keyboard_Similarity", con1);
            dp.Fill(DT);
            dataGridView1.DataSource = DT;

        }
        void filldgv4()
        {

            DT = new DataTable();
            dp = new OleDbDataAdapter("select * from Sound_Similarity", con1);
            dp.Fill(DT);
            dataGridView4.DataSource = DT;

        }
        void filldgv1()
        {

            DT = new DataTable();
            dp = new OleDbDataAdapter("select * from Keyboard_Similarity_EQ", con1);
            dp.Fill(DT);
            dataGridView1.DataSource = DT;

        }
        void filldgv3()
        {

            DT = new DataTable();
            dp = new OleDbDataAdapter("select * from Form_Similarity", con1);
            dp.Fill(DT);
            dataGridView3.DataSource = DT;

        }

        void filldgv2()
        {
            DT = new DataTable();
            dp = new OleDbDataAdapter("select * from DataSet", con1);
            dp.Fill(DT);
            dataGridView2.DataSource = DT;
        }

        //==================Edit levenstein keyboard&sond &form cost for replacement===========================

        double cul_destance(string source, string target)
        {
            source_word = source;
            target_word = target;
            char ch1, ch2;
            cost = new double[source_word.Length + 1, target_word.Length + 1];
            for (int i = 0; i <= source_word.Length; i++) cost[i, 0] = i;
            for (int i = 0; i <= target_word.Length; i++) cost[0, i] = i;
            for (int j = 1; j <= target_word.Length; j++)
                for (int i = 1; i <= source_word.Length; i++)
                {
                    if (i != 1)
                        ch1 = source_word[i - 2];
                    else ch1 = ' ';
                    if (j != 1)
                        ch2 = target_word[j - 2];
                    else ch2 = ' ';
                    cost[i, j] = minimumel(sum(cost[i - 1, j], ins_del_cost(source_word[i - 1], ch1)), sum(cost[i, j - 1], ins_del_cost(target_word[j - 1], ch2)), sum(cost[i - 1, j - 1], average_s(source_word[i - 1], target_word[j - 1])));

                }
            double Distance = cost[source_word.Length, target_word.Length];
            return Distance;


        }
        //=============================

        double cul_destance_s(string source, string target)
        {
            source_word = source;
            target_word = target;
            char ch1, ch2;
            cost = new double[source_word.Length + 1, target_word.Length + 1];
            for (int i = 0; i <= source_word.Length; i++) cost[i, 0] = i;
            for (int i = 0; i <= target_word.Length; i++) cost[0, i] = i;
            for (int j = 1; j <= target_word.Length; j++)
                for (int i = 1; i <= source_word.Length; i++)
                {
                    if (i != 1)
                        ch1 = source_word[i - 2];
                    else ch1 = ' ';
                    if (j != 1)
                        ch2 = target_word[j - 2];
                    else ch2 = ' ';
                    cost[i, j] = minimumel(sum(cost[i - 1, j], ins_del_cost(source_word[i - 1], ch1)), sum(cost[i, j - 1], ins_del_cost(target_word[j - 1], ch2)), sum(cost[i - 1, j - 1], sound_s(source_word[i - 1], target_word[j - 1])));

                }
            double Distance = cost[source_word.Length, target_word.Length];
            return Distance;


        }
        //=======================================
        //==================Edit levenstein keyboard cost for replacement===========================

        double cul_destance_k(string source, string target)
        {
            source_word = source;
            target_word = target;
            char ch1, ch2;
            cost = new double[source_word.Length + 1, target_word.Length + 1];
            for (int i = 0; i <= source_word.Length; i++) cost[i, 0] = i;
            for (int i = 0; i <= target_word.Length; i++) cost[0, i] = i;
            for (int j = 1; j <= target_word.Length; j++)
                for (int i = 1; i <= source_word.Length; i++)
                {
                    if (i != 1)
                        ch1 = source_word[i - 2];
                    else ch1 = ' ';
                    if (j != 1)
                        ch2 = target_word[j - 2];
                    else ch2 = ' ';
                    cost[i, j] = minimumel(sum(cost[i - 1, j], ins_del_cost(source_word[i - 1], ch1)), sum(cost[i, j - 1], ins_del_cost(target_word[j - 1], ch2)), sum(cost[i - 1, j - 1], keyboard_s(source_word[i - 1], target_word[j - 1])));

                }
            double Distance = cost[source_word.Length, target_word.Length];
            return Distance;


        }
        //=======================================
        //==================Edit levenstein use form cost for replacement===========================

        double cul_destance_f(string source, string target)
        {
            source_word = source;
            target_word = target;
            char ch1, ch2;
            cost = new double[source_word.Length + 1, target_word.Length + 1];
            for (int i = 0; i <= source_word.Length; i++) cost[i, 0] = i;
            for (int i = 0; i <= target_word.Length; i++) cost[0, i] = i;
            for (int j = 1; j <= target_word.Length; j++)
                for (int i = 1; i <= source_word.Length; i++)
                {
                    if (i != 1)
                        ch1 = source_word[i - 2];
                    else ch1 = ' ';
                    if (j != 1)
                        ch2 = target_word[j - 2];
                    else ch2 = ' ';
                    cost[i, j] = minimumel(sum(cost[i - 1, j], ins_del_cost(source_word[i - 1], ch1)), sum(cost[i, j - 1], ins_del_cost(target_word[j - 1], ch2)), sum(cost[i - 1, j - 1], form_s(source_word[i - 1], target_word[j - 1])));

                }
            double Distance = cost[source_word.Length, target_word.Length];
            return Distance;
        }
        //==================Edit levenstein minimum element cost for replacement===========================

        //===============================================
        double cul_destance_m(string source, string target)
        {
            source_word = source;
            target_word = target;
            char ch1, ch2;
            cost = new double[source_word.Length + 1, target_word.Length + 1];
            for (int i = 0; i <= source_word.Length; i++) cost[i, 0] = i;
            for (int i = 0; i <= target_word.Length; i++) cost[0, i] = i;
            for (int j = 1; j <= target_word.Length; j++)
                for (int i = 1; i <= source_word.Length; i++)
                {
                    if (i != 1)
                        ch1 = source_word[i - 2];
                    else ch1 = ' ';
                    if (j != 1)
                        ch2 = target_word[j - 2];
                    else ch2 = ' ';
                    cost[i, j] = minimumel(sum(cost[i - 1, j], ins_del_cost(source_word[i - 1], ch1)), sum(cost[i, j - 1], ins_del_cost(target_word[j - 1], ch2)), sum(cost[i - 1, j - 1], minimum_s(source_word[i - 1], target_word[j - 1])));

                }
            double Distance = cost[source_word.Length, target_word.Length];
            return Distance;

        }
        //=================Levenshtein Distance=========================
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
        //===========================================
        void filldata()
        {
            cmd = new OleDbCommand("select * from Dataset", con1);
            con1.Open();
            int row = 0;
            double avr_1 = 0, avr_2 = 0, avr_3 = 0, avr_4 = 0, avr_5 = 0, avr_6 = 0;
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
                    avr_1 += similarity;
                    int s = Convert.ToInt16(similarity);
                    int dd = (int)(Distance * 100);
                    float d1 = (float)dd / 100;

                    dataGridView2[2, row].Value = d1.ToString();
                    dataGridView2[3, row].Value = s.ToString() + "%";
                    Distance = cul_destance_k(dr[0].ToString(), dr[1].ToString());
                    similarity = similarity_R(source_word, target_word, Distance);
                    avr_2 += similarity;
                    dd = (int)(Distance * 100);
                    d1 = (float)dd / 100;
                    s = Convert.ToInt16(similarity);
                    dataGridView2[4, row].Value = d1.ToString();
                    dataGridView2[5, row].Value = s.ToString() + "%";
                    Distance = cul_destance_s(dr[0].ToString(), dr[1].ToString());
                    similarity = similarity_R(source_word, target_word, Distance);
                    avr_3 += similarity;
                    s = Convert.ToInt16(similarity);
                    dd = (int)(Distance * 100);
                    d1 = (float)dd / 100;
                    dataGridView2[6, row].Value = d1.ToString();
                    dataGridView2[7, row].Value = s.ToString() + "%";
                    Distance = cul_destance_f(dr[0].ToString(), dr[1].ToString());
                    similarity = similarity_R(source_word, target_word, Distance);
                    avr_4 += similarity;
                    s = Convert.ToInt16(similarity);
                    dd = (int)(Distance * 100);
                    d1 = (float)dd / 100;
                    dataGridView2[8, row].Value = d1.ToString();
                    dataGridView2[9, row].Value = s.ToString() + "%";
                    Distance = cul_destance_m(dr[0].ToString(), dr[1].ToString());
                    similarity = similarity_R(source_word, target_word, Distance);
                    avr_5 += similarity;
                    s = Convert.ToInt16(similarity);
                    dd = (int)(Distance * 100);
                    d1 = (float)dd / 100;
                    dataGridView2[10, row].Value = d1.ToString();
                    dataGridView2[11, row].Value = s.ToString() + "%";
                    Distance = cul_destance(dr[0].ToString(), dr[1].ToString());
                    similarity = similarity_R(source_word, target_word, Distance);
                    avr_6 += similarity;
                    s = Convert.ToInt16(similarity);
                    dd = (int)(Distance * 100);
                    d1 = (float)dd / 100;
                    dataGridView2[12, row].Value = d1.ToString();
                    dataGridView2[13, row].Value = s.ToString() + "%";

                    row++;
                }
                dataGridView2[0, row].Value = "avrage";
                dataGridView2[3, row].Value = (avr_1 / row).ToString() + "%";
                dataGridView2[5, row].Value = (avr_2 / row).ToString() + "%";
                dataGridView2[7, row].Value = (avr_3 / row).ToString() + "%";
                dataGridView2[9, row].Value = (avr_4 / row).ToString() + "%";
                dataGridView2[11, row].Value = (avr_5 / row).ToString() + "%";
                dataGridView2[13, row].Value = (avr_6 / row).ToString() + "%";
            }
            con1.Close();
        }
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
                    int dd = (int)(Distance * 100);
                    float d1 = (float)dd / 100;
                    dataGridView2[2, row].Value = d1.ToString();
                    dataGridView2[3, row].Value = s.ToString() + "%";

                    row++;
                }

            }
            con1.Close();
        }
        /*___________________events in program__________________________________*/
        public main()
        {
            InitializeComponent();
            filldgv1();
            filldat_1();
            filldgv3();
            filldgv4();
            filldgv5();
        }
        public main(int i)
        {
            InitializeComponent();

            filldat_1();
            filldgv3();
            filldgv4();
            filldgv5();
            if (i == 1)
                filldgv1();
            else filldgv6();
            filldata();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void main_Load(object sender, EventArgs e)
        {
            filldata();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }
    }
}
