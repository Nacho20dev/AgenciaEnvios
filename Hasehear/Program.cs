using System;
using BCrypt.Net;

namespace Hasehear
{


class Program
        {
            static void Main(string[] args)
            {
                Console.Write("Ingrese la contraseña a hashear: ");
                string password = Console.ReadLine();

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                Console.WriteLine("\n🔐 Contraseña hasheada:");
                Console.WriteLine(hashedPassword);
            }
        }

    }


