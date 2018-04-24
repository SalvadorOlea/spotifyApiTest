using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
namespace Spotify
{
    public partial class Form1 : Form
    {
        HttpWebRequest http;
        string URL = "https://api.spotify.com/v1/search?q=muse&type=artist";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            http = (HttpWebRequest)WebRequest.Create("https://api.spotify.com/v1/search?q=muse&type=artist");
            http.Method = "GET";
            http.ContentType = "application/json";
            http.Headers.Add("Authorization", "Bearer API_KEY_HERE");
            using(Stream SpotifyStream = http.GetResponse().GetResponseStream())
            {
                using(StreamReader stream = new StreamReader(SpotifyStream))
                {
                    Newtonsoft.Json.Linq.JObject jObj = Newtonsoft.Json.Linq.JObject.Parse(stream.ReadToEnd());
                    String title = (string)jObj["artists"]["items"][0]["name"];
                    String popularidad = (string)jObj["artists"]["items"][0]["popularity"];
                    String seguidores = (string)jObj["artists"]["items"][1]["followers"]["total"];
                    String imgUrl = (string)jObj["artists"]["items"][0]["images"][0]["url"];
                    var request = WebRequest.Create(imgUrl);

                    using (var response = request.GetResponse())
                    using (var stream1 = response.GetResponseStream())
                    {
                        pictureBox1.Image = Bitmap.FromStream(stream1);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    textBox1.Text = "Artista: " + title + " | Seguidores: "+seguidores+" | Popularidad: " + popularidad;
                }
            }
        }
    }
}
