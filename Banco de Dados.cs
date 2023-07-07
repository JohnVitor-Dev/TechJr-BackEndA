using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;
using System.Xml.Linq;

namespace SQLConect
{
    public static class SqlServerConect
    {


        public static void Main()
        {
            string connectionString = "Server = JohnV\\SQLEXPRESS; Database = Identidade; Trusted_Connection = True;";
            SqlConnection SqlConect = new SqlConnection(connectionString);

            while(true)
            {
                Menu(SqlConect);
            }

        }

        public static void Menu(SqlConnection SqlConect)
        {
            Console.Clear();
            Console.WriteLine("O que deseja? ");
            Console.WriteLine("1 - Inserir Idendidade");
            Console.WriteLine("2 - Consultar Identidades");
            Console.WriteLine("3 - Deletar Identidade");
            Console.WriteLine("4 - Sair");
            int Escolha = int.Parse(Console.ReadLine());

            switch(Escolha)
            {
                case 1:
                    Inserir(SqlConect);
                    break;
                case 2:
                    Consulta(SqlConect);
                    break;
                case 3:
                    Deletar(SqlConect);
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }
        public static void Inserir(SqlConnection SqlConect)
        {
            Console.Clear();
            Console.WriteLine("Insira seu nome: ");
            string Nome = Console.ReadLine();
            Console.WriteLine("Insira sua idade: ");
            int Idade = int.Parse(Console.ReadLine());

            SqlConect.Open();

            string insertQuery = "INSERT INTO Identidade (Nome, Idade) VALUES (@Nome, @Idade)";
            SqlCommand command = new SqlCommand(insertQuery, SqlConect);
            command.Parameters.AddWithValue("@Nome", Nome);
            command.Parameters.AddWithValue("@Idade", Idade);

            int rowsAffected = command.ExecuteNonQuery();

            // Verifique se a inserção foi bem-sucedida
            if (rowsAffected > 0)
            {
                Console.WriteLine("Inserção bem-sucedida!");
            }
            else
            {
                Console.WriteLine("Falha na inserção!");
            }

            SqlConect.Close();
            Console.WriteLine("\nPressione qualquer tecla para continuar!");
            Console.ReadLine();

        }
        public static void Consulta(SqlConnection SqlConect)
        {
            Console.Clear();
            SqlConect.Open();

            string selectQuery = "SELECT Id, Nome, Idade FROM Identidade";
            SqlCommand command = new SqlCommand(selectQuery, SqlConect);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int Id = reader.GetInt32(0);
                string nome = reader.GetString(1);
                int idade = reader.GetInt32(2);
                

                Console.WriteLine($"Id: {Id} = Nome: {nome}, Idade: {idade}");
            }
            SqlConect.Close();
            Console.WriteLine("\nPressione qualquer tecla para continuar!");
            Console.ReadLine();



        }
        public static void Deletar(SqlConnection SqlConect)
        {
            Console.Clear();
            Console.WriteLine("Insira o Id: ");
            int Id = int.Parse(Console.ReadLine());

            SqlConect.Open();

            string deleteQuery = "DELETE FROM Identidade WHERE Id = @Id";
            SqlCommand command = new SqlCommand(deleteQuery, SqlConect);
            command.Parameters.AddWithValue("@Id", Id);
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Deleção bem-sucedida!");
            }
            else
            {
                Console.WriteLine("Nenhum registro encontrado para deleção!");
            }

            SqlConect.Close();
            Console.ReadLine();
        }
    }
}