namespace Vurdalakov.MciStringSender
{
    using System;

    class Program
    {
        static void Main(String[] _)
        {
            Console.WriteLine("Type an MCI string and press ENTER. Press Ctrl+C to exit.");

            var mciStringSender = new MciStringSender();

            while (true)
            {
                Console.Write("> ");

                var mciRequest = Console.ReadLine();

                if (null == mciRequest)
                {
                    break;
                }

                if (mciStringSender.TrySendString(mciRequest, out var mciResponse))
                {
                    if (!String.IsNullOrEmpty(mciResponse))
                    {
                        Console.WriteLine(mciResponse);
                    }
                }
                else
                {
                    Console.WriteLine($"Error {mciStringSender.LastErrorCode} '{mciStringSender.LastErrorString}'");
                }
            }
        }
    }
}
