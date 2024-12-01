namespace Laboratorium_1_3
{
    public class Minutnik
    {

        public TimeSpan timeSpan;
        public Minutnik(TimeSpan timeSpan)
        {
            while (true)
            {
                Console.SetCursorPosition(105, 0);
                Console.WriteLine("{0}h:{1}min:{2}s", timeSpan.Hours - System.DateTime.Now.Hour, timeSpan.Minutes - System.DateTime.Now.Minute, timeSpan.Seconds - System.DateTime.Now.Second);
            }
        }
    }
}