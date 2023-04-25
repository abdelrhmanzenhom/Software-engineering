using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace ODP1_Connected_Start
{
    public partial class ActorsForm : Form
    {
        string ordb = "data source=orcl; user id=scott; password=tiger;";
        OracleConnection conn;
        public ActorsForm()
        {
            InitializeComponent();
        }

        private void ActorsForm_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select UserName,userpassword from Users where ActorID =:id";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("id", cmb_ID.SelectedItem.ToString());
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txt_Name.Text = dr[0].ToString();
                txt_Gender.Text = dr[1].ToString();
            }
            dr.Close();

        }

        private void cmb_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            OracleCommand cmD = new OracleCommand();
            cmD.Connection = conn;
            cmD.CommandText = "insert into USERS  values(:id,:USERNAME,:USERPASSWORD )";
            cmD.Parameters.Add("id", cmb_ID.Text);
            cmD.Parameters.Add("name", txt_Name.Text);
            cmD.Parameters.Add("Password", txt_Gender.Text);
            int r = cmD.ExecuteNonQuery();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand Cmd = new OracleCommand();
            Cmd.Connection = conn;
            Cmd.CommandText = "GetAuctionsrows";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("AucStatus", textBox1.Text);
            Cmd.Parameters.Add("AucName", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader Dr = Cmd.ExecuteReader();
            while (Dr.Read())
            {
                textBox2.Text = Dr[0].ToString();
                textBox3.Text = Dr[1].ToString();
            }
            Dr.Close();

        }

        private void ActorsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
        }

        private void btn_GETUSERSID_Click(object sender, EventArgs e)
        {
            OracleCommand Cmm = new OracleCommand();
            Cmm.Connection = conn;
            Cmm.CommandText = "GetUsersID";
            Cmm.CommandType = CommandType.StoredProcedure;
            Cmm.Parameters.Add("RID", comboBox1.Text);
           
            OracleDataReader DR = Cmm.ExecuteReader();
            while (DR.Read())
            {
                comboBox1.Items.Add (DR[0]);
            }
            DR.Close();
        }
    }
}
