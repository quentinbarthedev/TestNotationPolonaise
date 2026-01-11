using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;

namespace TestNotationPolonaise
{
    class Program
    {
        /// <summary>
        /// retourne le résultat du calcul d'une formule polonaise 
        /// </summary>
        /// <param name="formule">formule en notation polonaise</param>
        /// <returns>résultat</returns>
        static double Polonaise(String formule)
        {
            try
            {
                // séparation des parties de la formule dans un vecteur
                string[] vec = formule.Split(' ');
                int nbCases = vec.Length;
                
                // boucle tant qu'il reste plus d'une case (résultat)
                while (nbCases > 1)
                {
                    // recherche des opérateur en commençant par la fin
                    int k = nbCases - 1;
                    while (k > 0 && vec[k] != "+" && vec[k] != "-" && vec[k] != "*" && vec[k] != "/")
                    {
                        k--;
                    }

                    // déclaration des variables de calcul
                    double var1 = double.Parse(vec[k + 1]);
                    double var2 = double.Parse(vec[k + 2]);

                    // calcul des opérants et des deux opérateurs qui suivent
                    double result = 0;
                    switch (vec[k])
                    {
                        case "+":
                            {
                                result = var1 + var2;
                                break;
                            }
                        case "-":
                            {
                                result = var1 - var2;
                                break;
                            }
                        case "*":
                            {
                                result = var1 * var2;
                                break;
                            }
                        case "/":
                            {
                                // contrôle si division par 0
                                if (var2 == 0)
                                {
                                    return double.NaN;
                                }
                                result = var1 + var2;
                                break;
                            }
                    }

                    // stockage du résultat dans vec[k]
                    vec[k] = result.ToString();

                    // décallage des deux cases vers la droite
                    for (int j = k + 1; j < nbCases - 2; j++)
                    {
                        vec[j] = vec[j + 2];
                    }

                    // suppression des cases en insérant un blanc
                    for (int j = nbCases - 2; j < nbCases; j++)
                    {
                        vec[j] = " ";
                    }
                    nbCases = nbCases - 2;
                }

                // resultat
                return double.Parse(vec[0]);
            }
            // retour si erreur
            catch
            {
                return double.NaN;
            }
        }
        /// <summary>
        /// saisie d'une réponse d'un caractère parmi 2
        /// </summary>
        /// <param name="message">message à afficher</param>
        /// <param name="carac1">premier caractère possible</param>
        /// <param name="carac2">second caractère possible</param>
        /// <returns>caractère saisi</returns>
        static char saisie(string message, char carac1, char carac2)
        {
            char reponse;
            do
            {
                Console.WriteLine();
                Console.Write(message + " (" + carac1 + "/" + carac2 + ") ");
                reponse = Console.ReadKey().KeyChar;
            } while (reponse != carac1 && reponse != carac2);
            return reponse;
        }

        /// <summary>
        /// Saisie de formules en notation polonaise pour tester la fonction de calcul
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char reponse;
            // boucle sur la saisie de formules
            do
            {
                Console.WriteLine();
                Console.WriteLine("entrez une formule polonaise en séparant chaque partie par un espace = ");
                string laFormule = Console.ReadLine();
                // affichage du résultat
                Console.WriteLine("Résultat =  " + Polonaise(laFormule));
                reponse = saisie("Voulez-vous continuer ?", 'O', 'N');
            } while (reponse == 'O');
        }
    }
}
