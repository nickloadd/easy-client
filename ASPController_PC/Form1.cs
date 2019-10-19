using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace ASPController_PC
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

        private async void button1_Click(object sender, EventArgs e)
        {
            string JSONData = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(textBox1.Text));
            WebRequest request = WebRequest.Create("https://localhost:44305/Home/Hello");

            request.Method = "POST";

            string quary = $"name={JSONData}";

            byte[] byteMsg = Encoding.UTF8.GetBytes(quary);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteMsg.Length;

            using (Stream stream = await request.GetRequestStreamAsync())
            {
                await stream.WriteAsync(byteMsg, 0, byteMsg.Length);
            }

            WebResponse response = await request.GetResponseAsync();

            string answer = null;

            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader sR = new StreamReader(s))
                {
                    answer = await sR.ReadToEndAsync();
                }
            }
            response.Close();

            string hellowStr = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<string>(answer));

            MessageBox.Show(hellowStr);
        }
    }
}
