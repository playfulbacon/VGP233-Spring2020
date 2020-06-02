using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TwitchChat : MonoBehaviour
{
    public event Action<string> OnMessageReceived;

    private TcpClient twitchClient;
    private readonly string server = "irc.chat.twitch.tv";
    private readonly int port = 6667;

    private Thread inProc, outProc;
    private readonly string channelName = "andyrbacon";
    private Queue<string> commandQueue = new Queue<string>();
    private List<string> receivedMessage = new List<string>();

    void Start()
    {
        Connect();
    }

    private void Update()
    {
        lock (receivedMessage)
        {
            foreach (string message in receivedMessage)
            {

                int messageIndex = message.IndexOf("PRIVMSG #");
                OnMessageReceived?.Invoke(message.Replace("PRIVMSG #", ""));
            }
            receivedMessage.Clear();
        }
    }

    private void Connect()
    {
        twitchClient = new TcpClient();
        twitchClient.Connect(server, port);

        if (twitchClient.Connected)
        {
            Debug.Log("Connected to Twitch! :D");
        }
        else
        {
            Debug.Log("NOT Connected to Twitch! :(");
        }

        var networkStream = twitchClient.GetStream();
        var reader = new StreamReader(networkStream);
        var writer = new StreamWriter(networkStream);

        // Send pass & nick
        // for anonymous connection to twitc IRC,
        // use the format:
        // PASS <random-string>
        // NICK justinfan###...

        writer.WriteLine("PASS adlgkasda");
        writer.WriteLine("NICK justinfan239588");
        writer.Flush();

        outProc = new Thread(() => IRCOutputProcedure(writer));
        outProc.Start();

        inProc = new Thread(() => IRCInputProcedure(reader, networkStream));
        inProc.Start();
    }

    private void IRCInputProcedure(TextReader input, NetworkStream networkStream)
    {
        while (true)
        {
            if (!networkStream.DataAvailable)
            {
                continue;
            }

            string buffer = input.ReadLine();
            Debug.Log(buffer);

            if (buffer.Contains("PRIVMSG #"))
            {
                lock (receivedMessage)
                {
                    receivedMessage.Add(buffer);
                }
            }

            if (buffer.StartsWith("PING "))
            {
                SendCommand(buffer.Replace("PING", "PONG"));
            }

            if (buffer.Split(' ')[1] == "001")
            {
                Debug.Log("Join channel...");
                SendCommand("JOIN #" + channelName);
            }

            if (buffer.Contains("JOIN #" + channelName))
            {
                Debug.Log("Connected to channel: " + channelName);
            }
        }
    }

    private void SendCommand(string cmd)
    {
        lock (commandQueue)
        {
            commandQueue.Enqueue(cmd);
        }
    }

    private void IRCOutputProcedure(TextWriter output)
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        while (true)
        {
            lock (commandQueue)
            {
                if (commandQueue.Count > 0)
                {
                    if (stopwatch.ElapsedMilliseconds > 1750)
                    {
                        output.Write(commandQueue.Peek());
                        output.Flush();
                        // remove msg from queue
                        commandQueue.Dequeue();
                        //restart stopWatch
                        stopwatch.Reset();
                        stopwatch.Start();
                    }
                }
            }
        }
    }
}
