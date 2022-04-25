using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace praktikumweek8
{
    public partial class FormHasil : Form
    {
        public FormHasil()
        {
            InitializeComponent();
        }
        public static string sqlConnection = "server=localhost; uid=root; pwd=; database=premier_league";
        public MySqlConnection sqlConnect = new MySqlConnection(sqlConnection); // sebagai data koneksi ke dbms nya
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter;
        public string sqlQuery;
        //week 8
        private void cBoxKiri_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                 DataTable dtTim= new DataTable();
            sqlQuery = "select t.team_name as 'namaTim' , p.player_name , m.manager_name, concat (t.home_stadium, ',' , t.city) ,t.capacity from team t, manager m, player p WHERE t.manager_id = m.manager_id and t.captain_id = p.player_id and t.team_id= '" + cBoxKiri.SelectedValue + "'";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTim);
            lJwbKiri1.Text = dtTim.Rows[0][2].ToString();
            lJwbKiri2.Text = dtTim.Rows[0][1].ToString();
            lBawah1.Text = dtTim.Rows[0][3].ToString();
            lBawah2.Text = dtTim.Rows[0][4].ToString();
            }
            catch (Exception)
            {

                
            }
          
            


        }

        private void FormHasil_Load(object sender, EventArgs e)
        {
            sqlConnect.Open();
            DataTable dtTim = new DataTable();
            DataTable dtTim2 = new DataTable();
            sqlQuery = "select t.team_name as 'namaTim' ,t.team_id as 'timid', p.player_name , m.manager_name from team t,manager m, player p WHERE t.manager_id = m.manager_id and t.captain_id = p.player_id ";
            sqlCommand = new MySqlCommand(sqlQuery,sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTim);
            sqlQuery = "select t.team_name as 'namaTim' , p.player_name ,t.team_id as 'timid', m.manager_name from team t,manager m, player p WHERE t.manager_id = m.manager_id and t.captain_id = p.player_id ";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtTim2);
            cBoxKiri.DataSource = dtTim;
            cBoxKiri.DisplayMember = "namaTim";
            cBoxKiri.ValueMember = "timid";
            cBoxKanan.DataSource = dtTim2;
            cBoxKanan.DisplayMember = "namaTim";
            cBoxKanan.ValueMember = "timid";



        }

        private void cBoxKanan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTim= new DataTable();
                sqlQuery = "select t.team_name as 'namaTim' , p.player_name , m.manager_name from team t, manager m, player p WHERE t.manager_id = m.manager_id and t.captain_id = p.player_id and t.team_id = '" + cBoxKanan.SelectedValue + "'";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtTim);
                lJwbKanan1.Text = dtTim.Rows[0][2].ToString();
                lJwbKanan2.Text = dtTim.Rows[0][1].ToString();
            }
            catch (Exception)
            {


            }
        }
        //week 10

        private void btnCheck_Click(object sender, EventArgs e)
        {
           
                //tanggal
                DataTable dtTgl = new DataTable();
                sqlQuery = "select date_format(match.match_date, '%e %M %Y') as 'tgl',concat(match.goal_home,' - ',match.goal_away)  from player p, dmatch d,team t, team t2, `match` where d.match_id = match.match_id and p.player_id = d.player_id and (((t.team_id = '" + cBoxKiri.SelectedValue.ToString() + "'and t2.team_id = '" + cBoxKanan.SelectedValue.ToString() + "')or (t2.team_id = '" + cBoxKiri.SelectedValue.ToString() + "' and t.team_id = '" + cBoxKanan.SelectedValue.ToString() + "')) and ((t.team_id = match.team_home and t2.team_id = match.team_away) or (t.team_id = match.team_away and t2.team_id = match.team_home) )); ";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dtTgl);
                lTgll.Text = dtTgl.Rows[0][0].ToString();
                lSkorr.Text = dtTgl.Rows[0][1].ToString();

                //hasil match
                DataTable match = new DataTable();
                sqlQuery = "select d.minute as 'Minute' , if (p.team_id = match.team_home,p.player_name,' ') as 'Player Name 1',if(p.team_id = match.team_home,if(d.type = 'CY' ,'Yellow Card',if(d.type = 'CR','Red Card',if(d.type = 'GO','Goal',if(d.type = 'GP','Goal Penalty',if(d.type = 'GW','Own Goal','Penalty Miss'))))),' ') as 'Type 1',if(p.team_id = match.team_away,p.player_name,' ') as 'Player Name 2',if(p.team_id = match.team_away,if(d.type = 'CY' ,'Yellow Card',if(d.type = 'CR','Red Card',if(d.type = 'GO','Goal',if(d.type = 'GP','Goal Penalty',if(d.type = 'GW','Own Goal','Penalty Miss'))))),' ') as 'Type 2' from player p, dmatch d,team t, team t2, `match` where d.match_id = match.match_id and p.player_id = d.player_id and (((t.team_id = '" + cBoxKiri.SelectedValue.ToString() + "'and t2.team_id = '" + cBoxKanan.SelectedValue.ToString() + "')or (t2.team_id = '" + cBoxKiri.SelectedValue.ToString() + "' and t.team_id = '" + cBoxKanan.SelectedValue.ToString() + "')) and ((t.team_id = match.team_home and t2.team_id = match.team_away) or (t.team_id = match.team_away and t2.team_id = match.team_home) )) group by 1 order by 1; ";
                sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
                sqlAdapter = new MySqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(match);
                dGridMatch.DataSource = match;
          
        }
    }
}
