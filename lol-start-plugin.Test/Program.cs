using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lol_start_plugin.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var lol = new lol_start();

                Console.WriteLine("This is LoL start test!");
                Console.WriteLine("Please type your parameters to start spectating LoL: domain, port, encryptionKey, matchId, platformId");
                Console.WriteLine("Example: spectator.euw1.lol.riotgames.com, 80, VYNB0uVsmoniX7WSDLlUNETR2+8NGjNr, 3149246179, EUW1");
                Console.WriteLine("(Seperate parameters with ', ' and do not use quotation marks!)");
                var parameters = Console.ReadLine().Split(new string[] { ", " }, StringSplitOptions.None);

                Console.WriteLine("LoL folder: " + Registry.ClassesRoot.OpenSubKey(@"VirtualStore\MACHINE\SOFTWARE\Wow6432Node\Riot Games\RADS", false)
                    .GetValue("LocalRootFolder").ToString());

                Console.WriteLine("Parameters:");
                foreach (var param in parameters)
                {
                    Console.WriteLine(param);
                }
                Console.WriteLine("Parameters end");


                //lol.StartLoL("spectator.na.lol.riotgames", "80", "mc2M+NV4z9Zpg/4h8ioJpi6fWtHNHc6p", "2478597651", "NA1");
                lol.StartLoL("spectator.euw1.lol.riotgames.com", "80", "VYNB0uVsmoniX7WSDLlUNETR2+8NGjNr", "3149246179", "EUW1");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("err: " + ex.Message + " - " + ex.StackTrace);
            }            
        }
    }
}
