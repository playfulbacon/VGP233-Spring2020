using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class TwitchChat : MonoBehaviour
{
    public event System.Action<string> OnMessageReceived;
    private TcpClient twitchClient;
    private string server = "irc.chat.twitch.tv";
    private int port = 6667;

    private System.Threading.Thread inProc, outProc;
    private string channelName = "leonvfx";
    private Queue<string> commandQueue = new Queue<string>();
    private List<string> receivedMessages = new List<string>();

    private void Start()
    {
        Connect();
    }

    private void Update()
    {
        lock (receivedMessages)
        {
            foreach (string message in receivedMessages)
            {
                int messageIndex = message.IndexOf("PRIVMSG #");
                string cleanMessage = message.Substring(messageIndex + channelName.Length + 11);
                OnMessageReceived?.Invoke(cleanMessage);
            }
            receivedMessages.Clear();
        }
    }

    private void Connect()
    {
        twitchClient = new TcpClient();
        twitchClient.Connect(server, port);

        if (twitchClient.Connected)
        {
            Debug.Log("Connected to Twitch :D");
        }
        else
        {
            Debug.Log("NOT Connected to Twitch :(");
        }

        var networkStream = twitchClient.GetStream();
        var reader = new StreamReader(networkStream);
        var writer = new StreamWriter(networkStream);

        // Send PASS & NICK.
        // For anonymous connection to twitch IRC,
        // use the format:
        // PASS <random-string>
        // NICK justinfan###...

        writer.WriteLine("PASS adlsnegvjsnb");
        writer.WriteLine("NICK justinfan125");
        writer.Flush();

        outProc = new System.Threading.Thread(() => IRCOutputProcedure(writer));
        outProc.Start();

        inProc = new System.Threading.Thread(() => IRCInputProcedure(reader, networkStream));
        inProc.Start();
    }

    private void IRCInputProcedure(TextReader input, NetworkStream networkStream)
    {
        while(true)
        {
            if (!networkStream.DataAvailable)
                continue;

            string buffer = input.ReadLine();
            Debug.Log(buffer);

            // Receive private messages
            if (buffer.Contains("PRIVMSG #"))
            {
                lock (receivedMessages)
                {
                    receivedMessages.Add(buffer);
                }
            }

            // Send pong reply to any ping messages
            if (buffer.StartsWith("PING "))
            {
                SendCommand(buffer.Replace("PING", "PONG"));
            }

            // After server sends 001 command, we can join a channel
            if (buffer.Split(' ')[1] == "001")
            {
                Debug.Log("join channel...");
                SendCommand("JOIN #" + channelName);
            }

            if (buffer.Contains("JOIN #" + channelName))
            {
                Debug.Log("Connected to channel: " + channelName);
            }
        }
    }

    private void IRCOutputProcedure(TextWriter output)
    {
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();

        while(true)
        {
            lock (commandQueue)
            {
                if (commandQueue.Count > 0) // do we have any commands to send?
                {
                    // https://github
                    // have enough time passed since we last sent a message/command?
                    if (stopWatch.ElapsedMilliseconds > 1750)
                    {
                        // send msg.
                        output.WriteLine(commandQueue.Peek());
                        output.Flush();
                        // remove msg from queue
                        commandQueue.Dequeue();
                        // restart stopwatch
                        stopWatch.Reset();
                        stopWatch.Start();
                    }
                }
            }
        }
    }

    public void SendCommand(string cmd)
    {
        lock (commandQueue)
        {
            commandQueue.Enqueue(cmd);
        }
    }
}
