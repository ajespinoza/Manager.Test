using Manager.Core;
using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Manager.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region ManagerSencillo
            ////Manager Version basica
            ////Sin eventos
            //Hashtable props = new Hashtable();
            //props["port"] = 9096;
            //props["name"] = "ManagerAccount";

            //BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            //serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

            //TcpServerChannel channel = new TcpServerChannel(props, serverProv);
            //ChannelServices.RegisterChannel(channel, ensureSecurity: false);

            //RemotingConfiguration.RegisterWellKnownServiceType(
            //    System.Type.GetType("Manager.Core.AccountMove, Manager.Core"),
            //    "ManagerAccount",
            //    WellKnownObjectMode.Singleton);

            //System.Console.WriteLine("Manager running. Press <enter> to exit...");
            //System.Console.ReadLine();
            #endregion


            #region ManagerComplejo
            //Levanto manager mas completo
            AccountMove accountServer = new AccountMove();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Manager initialize...");
            accountServer.Initialize();
            Console.WriteLine("Press any key for stop..");
            Console.ReadLine();
            accountServer.Uninitialize();
            #endregion

        }
    }
}
