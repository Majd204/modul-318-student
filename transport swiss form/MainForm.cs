using SwissTransport;
using System;
using System.Windows.Forms;

namespace transport_swiss_form
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString();
            label2.Text = DateTime.Now.ToLongTimeString();
            TB_From.AutoCompleteCustomSource = new AutoCompleteStringCollection();

        }

        Connections connections = new Connections();
        Transport transport = new Transport();
        StationBoardRoot stationBoardRoot = new StationBoardRoot();
        Stations stations = new Stations();
        Station station = new Station();


        private void TB_From_TextChanged(object sender, EventArgs e)
        {
          
         
                stations = transport.GetStations(TB_From.Text);
                listBox1.DataSource = stations.StationList;
                listBox1.DisplayMember = "Name";
       

        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            TB_From.Text = listBox1.GetItemText(listBox1.SelectedItem);
        }
       
        private void TB_To_TextChanged(object sender, EventArgs e)
        {
            stations = transport.GetStations(TB_To.Text);
            listBox2.DataSource = stations.StationList;
            listBox2.DisplayMember = "Name";
            
         }
        private void listBox2_Click(object sender, EventArgs e)
        {
            TB_To.Text = listBox2.GetItemText(listBox2.SelectedItem);
        }



        private void button1_Click(object sender, EventArgs e)
        {

            string date = dateTimePicker1.Value.ToString();
            string time = dateTimePicker2.Value.ToString();
            int cb = 0;
            if (checkBox2.Checked)
            {
                cb = 1;
            }
            var connections = transport.GetConnections(TB_From.Text, TB_To.Text, date, time, cb);

            foreach (Connection connection in connections.ConnectionList)
            {
                DateTime d2 = DateTime.Parse(connection.From.Departure);
                DateTime d1 = DateTime.Parse(connection.To.Arrival);
                listBox4.Items.Add(connection.From.Station.Name + "  |  " + connection.To.Station.Name
                     + "  |  " + d2 + "  |  " + d1);
            }
           

        }

        //Destination Departure 
        private void button2_Click(object sender, EventArgs e)
        {
            string time = dateTimePicker2.Value.ToString();
            var i = transport.GetStationBoard(TB_From.Text, time);
            listBox3.Items.Clear();
            foreach (var station in i.Entries)
            {
                string s = station.Category + station.Number + "  |  " + station.To;
                listBox3.Items.Add(s + "  |  " + station.Stop.Departure);
            }

        }

  
    }
 


}
