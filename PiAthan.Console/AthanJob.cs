using System;
using System.Media;
using Quartz;

namespace PiAthan.Console
{
    public class AthanJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\athan.wav";
            player.Play();
        }
    }
}