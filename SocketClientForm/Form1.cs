﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SocketClientForm
{
    public partial class Form1 : Form
    {
        private Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
       

        public Form1()
        {
            InitializeComponent();
            this.Load += FormLoad;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(this.textBox1.Text.Trim());
            IPEndPoint point = new IPEndPoint(ip, 2333);
            //进行连接
            socketClient.Connect(point);

            //不停的接收服务器端发送的消息
            Thread thread = new Thread(Recive);
            thread.IsBackground = true;
            thread.Start(socketClient);
        }

        private unsafe void Recive(object o)
        {
            var send = o as Socket;
            while (true)
            {
                //获取发送过来的消息
                byte[] buffer = new byte[1024 * 1024 * 2];
                var effective = send.Receive(buffer);
                if (effective == 0)
                {
                    break;
                }
                var str = Encoding.UTF8.GetString(buffer, 0, effective);

                fixed (char* s = str)
                {
                    var decode = new Decode();
                    decode.StringTransBack(s);
                }

                this.BeginInvoke((Action)(() =>
                {
                    this.listBoxReceive.Items.Add(str);
                }));
            }
        }

        private unsafe void button1_Click(object sender, EventArgs e)
        {

            var input = this.txtSend.Text;

            fixed(char* s = input)
            { 
                Encryption encryption = new Encryption();
                encryption.StringTransfer(s);
            }

            var buffter = Encoding.UTF8.GetBytes(input);
            var copy = new byte[buffter.Length + 1];
            for (int i = 0; i < buffter.Length; i++)
            {
                copy[i] = buffter[i];
            }
            copy[buffter.Length] = Convert.ToByte('\0');
            socketClient.Send(copy);
        }
    }
}
