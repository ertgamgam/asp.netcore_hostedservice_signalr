using System;

namespace HostedService
{
    public static class MessageProvider
    {
        public static string GetSongMameAsMesssage()
        {
            string[] songNames = { "Sweet Child O' Mine", "Don't Cry", "Paradise City", "Civil War", "Night Train" };
            Random rand = new Random();
            int randomIndexNumber = rand.Next(songNames.Length);
            return songNames[randomIndexNumber];
        }
    }
}
