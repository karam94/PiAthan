using System;
using System.Collections.Generic;
using System.Media;
using Quartz;

namespace PiAthan.Console
{
    public class AthanJob : IJob
    {
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
        }
    }
}