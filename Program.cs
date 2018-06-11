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

            SupprimerCategorie();
           
        }

        private static void ListerMarques()
        {
            Console.WriteLine("Entrez une lettre: ");
            
            //Création de la connexion
           

            using (var connexion = GetConnexion())
            {
                var debutNom = Console.ReadLine();
                //Création d'une commande
                var commande = new SqlCommand("SELECT * FROM Marques WHERE Nom LIKE @debutNom + '%'", connexion);
                commande.Parameters.Add(new SqlParameter
                {
                    ParameterName = "debutNom",
                    Value = debutNom
                });

                // Mode connecté
                connexion.Open();
                SqlDataReader dataReader = commande.ExecuteReader();
                while (dataReader.Read())
                {
                    var indexColonneId = dataReader.GetOrdinal("Id");
                    var indexColonneNom = dataReader.GetOrdinal("Nom");
                    Console.WriteLine($@"Id:{dataReader.GetInt32(indexColonneId)}, 
                                         Nom:{dataReader.GetString(indexColonneNom)}");
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

        private static void CreerMarque()
        {
            Console.WriteLine("Nom de la marque: ");
            var marque = Console.ReadLine();
            
            using (var connexion = GetConnexion())
            {
                //Création d'une commande
                var sql = "INSERT INTO Marques (Nom) Values (@Nom)";
                var commande = new SqlCommand(sql, connexion);
                commande.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Nom",
                    Value = marque
                });

                // Mode connecté
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();
            }
        }

        private static void CreerCategorie()
        {
            Console.WriteLine("Nom de la Catégorie: ");
            var categorie = Console.ReadLine();

            using (var connexion = GetConnexion())
            {
                //Création d'une commande
                var sql = "INSERT INTO Categories (Nom) Values (@Nom)";
                var commande = new SqlCommand(sql, connexion);
                commande.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Nom",
                    Value = categorie
                });

                // Mode connecté
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();
            }
        }

        private static void SupprimerCategorie()
        {
            Console.WriteLine("Nom de la Catégorie: ");
            var categorie = Console.ReadLine();

            using (var connexion = GetConnexion())
            {
                //Création d'une commande
                var sql = "DELETE FROM Categories WHERE Nom = @Nom";
                var commande = new SqlCommand(sql, connexion);
                commande.Parameters.Add(new SqlParameter
                {
                    ParameterName = "Nom",
                    Value = categorie
                });

                // Mode connecté
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();
            }
        }

        private static SqlConnection GetConnexion()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Connexion"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
