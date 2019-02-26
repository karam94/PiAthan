using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using NLog;
using Quartz;

namespace PiAthan.Console
{
    public class AthanJob : IJob
    {
        private readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void Execute(IJobExecutionContext context)
        {
            var rnd = new Random();

            var athans = new List<String>
            {
                "\\athan1.wav",
                "\\athan2.wav",
                "\\athan3.wav",
                "\\athan4.wav"
            };

//            var player = new SoundPlayer();
//            player.SoundLocation = string.Format(AppDomain.CurrentDomain.BaseDirectory + "{0}", athans[rnd.Next(athans.Count)]);
//            player.Play();

//            var bashCmd = $"-c aplay {AppDomain.CurrentDomain.BaseDirectory + athans[rnd.Next(athans.Count)]}";
//            Process.Start("/bin/bash", bashCmd);

            ExecuteCommand($"aplay {AppDomain.CurrentDomain.BaseDirectory + athans[rnd.Next(athans.Count)]}");
            //_logger.Info(bashCmd);
            _logger.Info("Athan job ran.");
        }
        
        public static void ExecuteCommand(string command)
        {
            Process proc = new System.Diagnostics.Process ();
            proc.StartInfo.FileName = "/bin/bash";
            proc.StartInfo.Arguments = "-c \" " + command + " \"";
            proc.StartInfo.UseShellExecute = false; 
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start ();
        }
    }
}