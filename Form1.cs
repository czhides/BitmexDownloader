using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BitmexAssistant
{
    public partial class Form1 : Form
    {
        string APIKey = "";
        string APISecret = "";
        WebSocket Ws;
        Dictionary<string, decimal> Prices = new Dictionary<string, decimal>();
        List<Instruments> ActiveInstruments = new List<Instruments>();
        Instrument ActiveInstrument = new Instrument();
        public Form1()
        {
            InitializeComponent();
            
        }
        private void InitializeWebsocket()
        {
            Ws = new WebSocket("wss://www.bitmex.com/realtime");
            Ws.OnMessage += (sender, e) =>
            {
                try
                {
                    JObject Message = JObject.Parse(e.Data);
                    if (Message.ContainsKey("table"))
                    {
                        if ((string)Message["table"] == "trade")
                        {
                            if (Message.ContainsKey("data"))
                            {
                                JArray TD = (JArray)Message["data"];
                                if (TD.Any())
                                {
                                    decimal Price = (decimal)TD.Children().LastOrDefault()["price"];
                                    string Symbol = (string)TD.Children().LastOrDefault()["symbol"];
                                    Prices[Symbol] = Price;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            Ws.Connect();

            foreach ( in collection)
            {

            }
        }
    }
}
