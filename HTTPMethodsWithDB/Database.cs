using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SQLConect
{
    public static class SqlServerConect
    {
        public static string connectionString;
        private static HttpListener listener;

        public static void Main()
        {
            connectionString = ReadConnectionStringDoArquivo();
            StartServer();
        }

        public static string ReadConnectionStringDoArquivo()
        {
            string nomeArquivo = "config.json";
            string caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);
            string serverPadrao = "Server=localhost\\SQLEXPRESS;Database=Identidade;Trusted_Connection=True;";

            try
            {
                if (File.Exists(caminhoArquivo))
                {
                    string json = File.ReadAllText(caminhoArquivo);
                    JObject config = JObject.Parse(json);
                    string connectionString = config["ConnectionString"].ToString().Trim();

                    if (!string.IsNullOrEmpty(connectionString))
                    {
                        Console.WriteLine($"Arquivo {nomeArquivo} encontrado!");
                        Console.WriteLine($"Localização do Banco de Dados definido como: {connectionString}");
                        return connectionString;
                    }
                }

                Console.WriteLine($"Arquivo de configuração não encontrado em {caminhoArquivo}. Usando a string de conexão padrão: ({serverPadrao})");
                return serverPadrao;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler o arquivo de configuração. Usando a string de conexão padrão: ({serverPadrao})");
                Console.WriteLine(ex.Message);
                return serverPadrao;
            }
        }

        public static void StartServer()
        {
            string url = "http://localhost:8080/";
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();

            Console.WriteLine($"Servidor HTTP iniciado em {url}");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                ProcessRequest(context);
            }
        }

        public static void ProcessRequest(HttpListenerContext context)
        {
            string httpMethod = context.Request.HttpMethod;

            switch (httpMethod)
            {
                case "GET":
                    HandleGetRequest(context);
                    break;
                case "POST":
                    HandlePostRequest(context);
                    break;
                case "PUT":
                    HandlePutRequest(context);
                    break;
                case "DELETE":
                    HandleDeleteRequest(context);
                    break;
                default:
                    SendResponse(context, HttpStatusCode.MethodNotAllowed, "Método não permitido.");
                    break;
            }
        }

        public static void HandleGetRequest(HttpListenerContext context)
        {
            Console.WriteLine("Conexão Get Solicitada!");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, Nome, Idade FROM Identidade";
                SqlCommand command = new SqlCommand(selectQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                //Console.WriteLine("State: {0}", connection.State);

                connection.Close();

                string responseContent = DataTableToJson(dataTable);
                SendResponse(context, HttpStatusCode.OK, responseContent);
            }
        }

        public static void HandlePostRequest(HttpListenerContext context)
        {
            Console.WriteLine("Conexão Post Solicitada!");
            string requestBody = new StreamReader(context.Request.InputStream).ReadToEnd();
            Identity identity = JsonToIdentity(requestBody);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Identidade (Nome, Idade) VALUES (@Nome, @Idade)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Nome", identity.Nome);
                command.Parameters.AddWithValue("@Idade", identity.Idade);

                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();

                if (rowsAffected > 0)
                {
                    string responseContent = IdentityToJson(identity);
                    SendResponse(context, HttpStatusCode.Created, responseContent);
                }
                else
                {
                    SendResponse(context, HttpStatusCode.InternalServerError, "Falha na inserção!");
                }
            }
        }

        public static void HandlePutRequest(HttpListenerContext context)
        {
            Console.WriteLine("Conexão Put Solicitada!");
            string requestBody = new StreamReader(context.Request.InputStream).ReadToEnd();
            Identity identity = JsonToIdentity(requestBody);

            int id = GetIdentityIdFromUrl(context.Request.Url);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Identidade SET Nome = @Nome, Idade = @Idade WHERE Id = @Id";
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Nome", identity.Nome);
                command.Parameters.AddWithValue("@Idade", identity.Idade);
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();

                if (rowsAffected > 0)
                {
                    string responseContent = IdentityToJson(identity);
                    SendResponse(context, HttpStatusCode.OK, responseContent);
                }
                else
                {
                    SendResponse(context, HttpStatusCode.NotFound, "Nenhum registro encontrado para modificação!");
                }
            }
        }

        public static void HandleDeleteRequest(HttpListenerContext context)
        {
            Console.WriteLine("Conexão Delete Solicitada!");
            int id = GetIdentityIdFromUrl(context.Request.Url);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Identidade WHERE Id = @Id";
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();

                if (rowsAffected > 0)
                {
                    SendResponse(context, HttpStatusCode.OK, "");
                }
                else
                {
                    SendResponse(context, HttpStatusCode.NotFound, "Nenhum registro encontrado para deleção!");
                }
            }
        }

        public static int GetIdentityIdFromUrl(Uri url)
        {
            string[] segments = url.Segments;
            string lastSegment = segments[segments.Length - 1];
            int id = Convert.ToInt32(lastSegment);

            return id;
        }

        public static string DataTableToJson(DataTable dataTable)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row[col.ColumnName] = dr[col];
                }
                rows.Add(row);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(rows);
        }

        public static Identity JsonToIdentity(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Identity>(json);
        }

        public static string IdentityToJson(Identity identity)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(identity);
        }

        public static void SendResponse(HttpListenerContext context, HttpStatusCode statusCode, string responseContent)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(responseContent);

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
        }

        public class Identity
        {
            public string Nome { get; set; }
            public int Idade { get; set; }
        }
    }
}
