using System;

namespace SFA.DAS.Campaign.MockServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mock Server starting on http://localhost:5016");

            MockApiServer.Start();

            Console.WriteLine(("Press any key to stop the server"));
            Console.ReadKey();
        }
    }
}
