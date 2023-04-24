using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Manager.Core
{
    public class AccountMove : MarshalByRefObject, IAccountMove
    {
        private TcpServerChannel serverChannel;
        private ObjRef internalRef;
        private bool serverActive = false;
        private static int tcpPort = 9096;
        private static string serverUri = "ManagerAccount";

        public event EventHandler<AccountArgs> changeAmount;
        private decimal _amount;
        private Random _random = new Random();
        public AccountMove()
        {

        }
        
        public void Initialize()
        {
            if (serverActive)
                return;

            Hashtable props = new Hashtable();
            props["port"] = tcpPort;
            props["name"] = serverUri;

            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            serverChannel = new TcpServerChannel(props, serverProv);

            try
            {
                ChannelServices.RegisterChannel(serverChannel, false);
                internalRef = RemotingServices.Marshal(this, props["name"].ToString());
                serverActive = true;
                Console.WriteLine("Manager initialized...");
                Console.WriteLine($"In tcp://localhost:{tcpPort}/{serverUri}");
            }
            catch (RemotingException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error could not start the server {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error could not start the server {ex.Message}");
            }

        }

        public void Uninitialize()
        {
            if(!serverActive) return;

            RemotingServices.Unmarshal(internalRef);

            try
            {
                ChannelServices.UnregisterChannel(serverChannel);
            }catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error to uninitialize {ex.Message}");
            }
        }

        public void Debit(decimal amount)
        {
            Console.WriteLine($"Debit amount changed {_amount} to {_amount + amount}");
            _amount += amount;
            changeAmount?.Invoke(new object() { }, new AccountArgs() { Id = _random.Next(), Guid = Guid.NewGuid().ToString() });
        }
        public void Credit(decimal amount)
        {
            Console.WriteLine($"Credit amount changed {_amount} to {_amount - amount}");
            _amount -= amount;
            changeAmount?.Invoke(new object() { }, new AccountArgs() { Id = _random.Next(), Guid = Guid.NewGuid().ToString() });
        }

        public decimal GetAmount()
        {
            return _amount;
        }

    }
}
