using System;
using System.Collections.Generic;
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
            
            var player = new SoundPlayer();
            player.SoundLocation = string.Format(AppDomain.CurrentDomain.BaseDirectory + "{0}", athans[rnd.Next(athans.Count)]);
            player.Play();
            
            _logger.Info("Athan job ran.");
        }
    }
}