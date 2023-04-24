
using Manager.Core;
using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;

namespace Manager.Client
{
    public class Program
    {
        static IAccountMove accountMove;
        static TcpChannel channel;

        static RemoteEvent<AccountArgs> changeAmount;
        static BinaryClientFormatterSinkProvider clientProv;
        static BinaryServerFormatterSinkProvider serverProv;

        private static string serverUri = "tcp://192.168.1.68:9096/ManagerAccount";
        private static bool connected = false;

        public static void Main(string[] args)
        {
            #region ManagerSencillo
            //Cliente para manager sencillo
            //Sin eventos 
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel, ensureSecurity: false);
            AccountMove accountMove = (AccountMove)Activator.GetObject(typeof(Manager.Core.AccountMove),
                                        "tcp://localhost:9096/ManagerAccount");


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Client connected!!");

            decimal move = 10;
            Console.WriteLine($"Debit move for {move}");
            accountMove.Debit(move);

            Thread.Sleep(TimeSpan.FromSeconds(10));
            move = 12;
            Console.WriteLine($"Debit move for {move}");
            accountMove.Debit(move);

            Thread.Sleep(TimeSpan.FromSeconds(10));
            move = 15;
            Console.WriteLine($"Debit move for {move}");
            accountMove.Debit(move);


            Console.WriteLine($"El saldo total es {accountMove.GetAmount()}");
            Console.ReadLine();
            #endregion



            #region ManagerMasComplejo
            ////Manager mas completo
            //clientProv = new BinaryClientFormatterSinkProvider();
            //serverProv = new BinaryServerFormatterSinkProvider();
            //serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            ////Manejo una clase que encapsula un evento para poder integrar logs de disparo o error
            //changeAmount = new RemoteEvent<AccountArgs>();
            //changeAmount.eventToHandle += new EventHandler<AccountArgs>(changeAmountEvent);

            //Hashtable props = new Hashtable();
            //props["name"] = "remotingClient";
            //props["port"] = 0;

            //channel = new TcpChannel(props, clientProv, serverProv);
            //ChannelServices.RegisterChannel(channel);

            //RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(IAccountMove), serverUri));

            //if (connected)
            //    return;

            //try
            //{

            //    accountMove = (IAccountMove)Activator.GetObject(typeof(IAccountMove), serverUri);
            //    //Para encapsular el evento en una clase para manejar logs
            //    accountMove.changeAmount += new EventHandler<AccountArgs>(changeAmount.Notify);
            //    //accountMove.changeAmount += changeAmountEvent;

            //    accountMove.Debit(12);
            //    accountMove.Debit(30);
            //    accountMove.Debit(50);
            //    connected = true;

            //}
            //catch (Exception ex)
            //{
            //    connected = false;
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine($"Could not connect {ex.Message}");
            //}

            //Console.ReadLine();
            //Disconnect();
            #endregion

        }

        private static void changeAmountEvent(object sender, AccountArgs e)
        {
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("ChangeAmountEvent Fired");
        }

        private static void Disconnect()
        {
            if(!connected) return;
            accountMove.changeAmount -= new EventHandler<AccountArgs>(changeAmount.Notify);
            ChannelServices.UnregisterChannel(channel);
        }
    }
}
