using System.Net;

namespace ProjekatDotnet {

    public class ProjekatDotnet
    {

        public static void Main(string[] args) {

            HttpServer server = new HttpServer();
            server.startServer();

        }

    }
}
