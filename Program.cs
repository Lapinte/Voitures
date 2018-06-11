using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voitures
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entrez une lettre: ");
            var debutNom = Console.ReadLine();

            ListerMarques(debutNom);
           
        }

        public static void ListerMarques(string debutNom)
        {
            //Création de la connexion
            var connectionString = ConfigurationManager.ConnectionStrings["Connexion"].ConnectionString;

            using (var connexion = new SqlConnection(connectionString))
            {

                //Création d'une commande
                var commande = new SqlCommand("SELECT * FROM Marques WHERE Nom Like'" + debutNom + "%'", connexion);

                // Mode connecté
                connexion.Open();
                SqlDataReader dataReader = commande.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine($"Id:{dataReader.GetInt32(0)}, Nom:{dataReader.GetString(1)}");
                }

                Console.ReadKey();

                connexion.Close();
            }
        }

        private static void MethodeCondensee()
        {
            //Création de la connexion
            var connectionString = ConfigurationManager.ConnectionStrings["Connexion"].ConnectionString;

            var connexion = new SqlConnection(connectionString);

            //Création d'une commande
            var commande = new SqlCommand("SELECT * FROM Marques", connexion);

            // Mode connecté
            connexion.Open();
            using (var dataReader = commande.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    Console.WriteLine($"Id:{dataReader.GetInt32(0)}, Nom:{dataReader.GetString(1)}");
                }
            }

            Console.ReadKey();

            connexion.Close();
        }
    }
}
