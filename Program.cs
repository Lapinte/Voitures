using System;
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
            //Création de la connexion
            var connectionString = @"Server=EMER09\SQLEXPRESS;Database=Voitures;Trusted_Connection=True";

            var connexion = new SqlConnection(connectionString);

            //Création d'une commande
            var commande = new SqlCommand("SELECT * FROM Marques", connexion);

            // Mode connecté
            connexion.Open();
            SqlDataReader dataReader = commande.ExecuteReader();
            while(dataReader.Read())
            {
                Console.WriteLine($"Id:{dataReader.GetInt32(0)}, Nom:{dataReader.GetString(1)}");
            }

            Console.ReadKey();

            connexion.Close();
        }
    }
}
