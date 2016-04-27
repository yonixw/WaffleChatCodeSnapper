using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        string reverse (string t)
        {
            string res = "";
            foreach (char c in t)
                res = c + res;
            return res;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }




        private static byte[] data;
        Queue<string> alldata = new Queue<string>();

        void Main2()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Int32 port = 6667;
            TcpClient client = new TcpClient("irc.twitch.tv", port);
            // Enter in channel (the username of the stream chat you wish to connect to) without the #
            string channel = "giantwaffle";

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer. 

            string loginstring = "PASS " + txtAuth.Text  + "\r\nNICK yonixw\r\n";
            Byte[] login = System.Text.Encoding.ASCII.GetBytes(loginstring);
            stream.Write(login, 0, login.Length);
            Debug.Print("Sent login.\r\n");
            Debug.Print(loginstring);

            // Receive the TcpServer.response.
            // Buffer to store the response bytes.
            data = new Byte[512];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Debug.Print("Received WELCOME: \r\n\r\n{0}", responseData);

            // send message to join channel

            string joinstring = "JOIN " + "#" + channel + "\r\n";
            Byte[] join = System.Text.Encoding.ASCII.GetBytes(joinstring);
            stream.Write(join, 0, join.Length);
            Debug.Print("Sent channel join.\r\n");
            Debug.Print(joinstring);

            // PMs the channel to announce that it's joined and listening
            // These three lines are the example for how to send something to the channel

            string announcestring = channel + "!" + channel + "@" + channel + ".tmi.twitch.tv PRIVMSG " + channel + " BOT ENABLED\r\n";
            Byte[] announce = System.Text.Encoding.ASCII.GetBytes(announcestring);
            stream.Write(announce, 0, announce.Length);

            // Lets you know its working

            Debug.Print("TWITCH CHAT HAS BEGUN.\r\n\r\nr.");
            Debug.Print("\r\nBE CAREFUL.");

            while (true)
            {

                // build a buffer to read the incoming TCP stream to, convert to a string

                byte[] myReadBuffer = new byte[1024];
                StringBuilder myCompleteMessage = new StringBuilder();
                int numberOfBytesRead = 0;

                // Incoming message may be larger than the buffer size.
                do
                {
                    try { numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length); }
                    catch (Exception e)
                    {
                        Debug.Print("OH SHIT SOMETHING WENT WRONG\r\n", e);
                    }

                    myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                }

                // when we've received data, do Things

                while (stream.DataAvailable);

                // Print out the received message to the console.
                Debug.Print(myCompleteMessage.ToString());
                switch (myCompleteMessage.ToString())
                {
                    // Every 5 minutes the Twitch server will send a PING, this is to respond with a PONG to keepalive

                    case "PING :tmi.twitch.tv\r\n":
                        try
                        {
                            Byte[] say = System.Text.Encoding.ASCII.GetBytes("PONG :tmi.twitch.tv\r\n");
                            stream.Write(say, 0, say.Length);
                            Debug.Print("Ping? Pong!");
                        }
                        catch (Exception e)
                        {
                            Debug.Print("OH SHIT SOMETHING WENT WRONG\r\n", e);
                        }
                        break;

                    // If it's not a ping, it's probably something we care about.  Try to parse it for a message.
                    default:
                        try
                        {
                            string messageParser = myCompleteMessage.ToString();
                            string[] message = messageParser.Split(':');
                            string[] preamble = message[1].Split(' ');
                            string tochat;

                            // This means it's a message to the channel.  Yes, PRIVMSG is IRC for messaging a channel too
                            if (preamble[1] == "PRIVMSG")
                            {
                                string[] sendingUser = preamble[0].Split('!');
                                tochat = sendingUser[0] + ": " + message[2];

                                // sometimes the carriage returns get lost (??)
                                if (tochat.Contains("\n") == false)
                                {
                                    tochat = tochat + "\n";
                                }

                                // Ignore some well known bots
                                if (sendingUser[0] != "moobot" && sendingUser[0] != "whale_bot")
                                {
                                    alldata.Enqueue(tochat.TrimEnd('\n'));
                                }
                            }
                            // A user joined.
                            else if (preamble[1] == "JOIN")
                            {
                                string[] sendingUser = preamble[0].Split('!');
                                tochat = "JOINED: " + sendingUser[0];
                                //    Debug.Print(tochat);
                                alldata.Enqueue(tochat.TrimEnd('\n'));
                            }
                        }
                        // This is a disgusting catch for something going wrong that keeps it all running.  I'm sorry.
                        catch (Exception e)
                        {
                            Debug.Print("OH SHIT SOMETHING WENT WRONG\r\n", e);
                        }

                        // Uncomment the following for raw message output for debugging
                        //
                        // Debug.Print("Raw output: " + message[0] + "::" + message[1] + "::" + message[2]);
                        // Debug.Print("You received the following message : " + myCompleteMessage);
                        break;

                        
                }
            }

            // Close everything.  Should never happen because you gotta close the window.
            stream.Close();
            client.Close();
            Debug.Print("\n Press Enter to continue...");

        }

        void enterCode(string text)
        {
            try
            {
                //string text = "Dreadnought Key #57 DV-2qCD-XZmu 154 keys remain! This key is in reverse order. Redeem it here: n3f.tv/dnredeem";
                string[] t = text.Split(' ');
                string code = "";
                for (int i = 0; i < t.Length; i++)
                {
                    if (t[i].StartsWith("#"))
                        code = reverse(t[i + 1]);
                }

                Debug.Print(">>>>>>>>>>>>>>>>>");
                Debug.Print(code);
                Debug.Print(">>>>>>>>>>>>>>>>>");

                ebMain.Document.GetElementById("id_code").SetAttribute("value", code);
                HtmlElement form = ebMain.Document.GetElementById("redeem-code-form");
                form.InvokeMember("submit");
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message + '\n' + ex.StackTrace);
                throw;
            }
        }


        private void tmrChat_Tick(object sender, EventArgs e)
        {
            if (lstChat.Items.Count > 1000)
                lstChat.Items.Clear();

            if (alldata.Count >0 )
            {
                string text = alldata.Dequeue();
                if (text.ToLower().Contains("neffyrobot") && text.Contains("#"))
                    enterCode(text);
                lstChat.Items.Insert(0, text);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() => { Main2(); }) { Name = "IRC" };
            t.Start();
        }
    }
}
