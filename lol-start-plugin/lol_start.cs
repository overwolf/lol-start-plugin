using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.IO;

namespace lol_start_plugin
{
    public class lol_start
    {
        public void StartLoL(string domain, string port, string encryptionKey, string matchId, string platformId, Action<object> callback)
        {
            Task.Run(() =>
            {
                try
                {
                    Spectate(domain, port, encryptionKey, matchId, platformId);
                }
                catch (Exception ex)
                {
                    callback("err: " + ex.Message + " - " + ex.StackTrace);
                }

                callback(true);
            });
        }

        //For testing purposes from console - no callback
        public void StartLoL(string domain, string port, string encryptionKey, string matchId, string platformId)
        {
            Spectate(domain, port, encryptionKey, matchId, platformId);
        }

        public void Spectate(string domain, string port, string encryptionKey, string matchId, string platformId)
        {
            var root = Registry.ClassesRoot.OpenSubKey(@"VirtualStore\MACHINE\SOFTWARE\Wow6432Node\Riot Games\RADS", false)
                .GetValue("LocalRootFolder").ToString();

            var version = GetLatestVersionFolder(root + @"\solutions\lol_game_client_sln\releases");
            if (version == "0") throw new ArgumentNullException("No LoL versions found.");

            //var folder = "\"" + root + "/solutions/lol_game_client_sln/releases/" + version + "/deploy\"";
            //var command = "\"League of Legends.exe\" 8394 LoLLauncher.exe \"\" \"spectator " + domain + ":" + port + " " + encryptionKey + " " + matchId + " " + platformId + "\"";

            //string strCmdText = "/C cd /d \"" + root + "/solutions/lol_game_client_sln/releases/" + version + "/deploy\" && \"League of Legends.exe\" 8394 LoLLauncher.exe \"\" \"spectator " + domain + ":" + port + " " + encryptionKey + " " + matchId + " " + platformId + "\"";

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C cd /d \"" + root + "/solutions/lol_game_client_sln/releases/" + version + "/deploy\" && \"League of Legends.exe\" 8394 LoLLauncher.exe \"\" \"spectator " + domain + ":" + port + " " + encryptionKey + " " + matchId + " " + platformId + "\""
                }
            };
            process.Start();

        }

        /// <summary>
        /// Checks and gets the latest version folder name in %LoL Root Folder%\solutions\lol_game_client_sln\releases
        /// </summary>
        /// <param name="path">Folder to check (...\releases)</param>
        /// <returns></returns>
        private string GetLatestVersionFolder(string path)
        {
            var version = "0";
            foreach (string s in Directory.GetDirectories(path))
            {
                var versionFolder = s.Remove(0, path.Length + 1).Replace(".", "");
                if (long.Parse(versionFolder) > long.Parse(version.Replace(".", "")))
                {
                    version = s.Remove(0, path.Length + 1);
                }
            }

            return version;
        }
    }
}
