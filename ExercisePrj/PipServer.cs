using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.IO;
using System.Threading;

namespace ExercisePrj
{
    public class PipServer:Log
    {
        public Action<string> ReceiveEvent;
        NamedPipeServerStream m_pipServer;
        Thread m_thread;
        bool run = true;
        string servname;

        public PipServer(string name)
        {
            m_pipServer = new NamedPipeServerStream(name,PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            servname = name;
        }
        public void Listen()
        {
            try
            {
                m_thread = new Thread(() =>
                {
                     WaitConnect();
                });
                m_thread.Start();
            }
            catch (Exception ex)
            {
                P(ex, "[PipServer.WaitForConnect]");
            }
        }
        void WaitConnect()
        {
       
            AsyncCallback callback = null;
            callback = new AsyncCallback(ar =>
            {
                var pipeServer = (NamedPipeServerStream)ar.AsyncState;
                pipeServer.EndWaitForConnection(ar);
                Accept();
                pipeServer.Disconnect();
                m_pipServer.BeginWaitForConnection(callback, m_pipServer);
            });
            m_pipServer.BeginWaitForConnection(callback, m_pipServer);
        }


        void Accept()
        {
            try
            {
             
                var res = Read();
                if(!string.IsNullOrEmpty(res))
                    ReceiveEvent?.Invoke(res);
            }
            catch(Exception ex)
            {
                P(ex, "[PipServer.Accept]");
            }
        }
        public bool Send(string msg)
        {
            try
            { 
                var buf = Encoding.UTF8.GetBytes(msg);
                if (m_pipServer.CanWrite)
                {
                    m_pipServer.Write(buf, 0, buf.Length);
                    m_pipServer.Flush();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                P(ex, "[PipServer.Send]");
                return false;   
            }

           
        }

        public string Read()
        {
            try
            {
                if (m_pipServer.CanRead)
                {
                    int count = 0;
                    List<byte> data = new List<byte>();
                    byte[] buf = new byte[1024];
                    do
                    {
                        count=m_pipServer.Read(buf, 0, buf.Length);
                        if (count == buf.Length)
                        {
                            data.AddRange(buf);
                        }
                        else
                        {
                            var dst = new byte[count];
                            Buffer.BlockCopy(buf, 0, dst, 0, count);
                            data.AddRange(dst);
                        }                    
                    } while (count > 0&&m_pipServer.CanRead);
                    var res = Encoding.UTF8.GetString(data.ToArray());
                    return res;
                }
                return null;

            }
            catch (Exception ex)
            {
                P(ex, "[PipServer.Read]");
                return null;
            }
        }

        public void Close()
        {
            run = false;
            m_thread.Join();
            if (m_pipServer.IsConnected)
            {
                m_pipServer.Close();
            }

        }
    }

    public class PipClient:Log
    {
        //\\\\.\\pipe\\TEST_PIP
        string serv;
        public PipClient(string server)
        {
            serv = server;
        }
        public bool Send(string msg)
        {
            try
            {
                var buf = Encoding.UTF8.GetBytes(msg);
                NamedPipeClientStream pipclient = new NamedPipeClientStream(serv);
                pipclient.Connect(3000);
                if (pipclient.CanWrite)
                {
                    pipclient.Write(buf, 0, buf.Length);
                    pipclient.Flush();
                    pipclient.Close();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                P(ex, "[PipClient.Send]");
                return false;
            }
        }
    }
    
}
