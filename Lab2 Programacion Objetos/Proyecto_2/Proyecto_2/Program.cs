using System;
using System.Collections.Generic;

namespace SistemaGestorATM
{
    class Program
    {
        static List<Dictionary<string, object>> usuarios = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object> { {"numero_tarjeta", "1234567890123456"}, {"pin", "1010"}, {"saldo", 1000.0} },
            new Dictionary<string, object> { {"numero_tarjeta", "9876543210987654"}, {"pin", "2001"}, {"saldo", 2000.0} },
            new Dictionary<string, object> { {"numero_tarjeta", "1111222233334444"}, {"pin", "3050"}, {"saldo", 1500.0} },
            new Dictionary<string, object> { {"numero_tarjeta", "5555666677778888"}, {"pin", "1234"}, {"saldo", 500.0} },
            new Dictionary<string, object> { {"numero_tarjeta", "9999888877776666"}, {"pin", "4321"}, {"saldo", 3000.0} }
        };

        static bool ValidarTarjeta(string? numeroTarjeta)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario["numero_tarjeta"].ToString() == numeroTarjeta)
                    return true;
            }
            return false;
        }

        static bool ValidarPin(string? numeroTarjeta, string? pin)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario["numero_tarjeta"].ToString() == numeroTarjeta && usuario["pin"].ToString() == pin)
                    return true;
            }
            return false;
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\nMenú del ATM:");
            Console.WriteLine("1. Ver Saldo");
            Console.WriteLine("2. Retirar Dinero");
            Console.WriteLine("3. Depositar");
            Console.WriteLine("4. Pagar Facturas");
            Console.WriteLine("5. Cambiar PIN");
            Console.WriteLine("6. Salir");
        }

        static void VerSaldo(string? numeroTarjeta)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario["numero_tarjeta"].ToString() == numeroTarjeta)
                    Console.WriteLine($"Tu saldo es de ${usuario["saldo"]:F2}");
            }
        }

        static void RetirarDinero(string? numeroTarjeta, double cantidad)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario["numero_tarjeta"].ToString() == numeroTarjeta)
                {
                    if ((double)usuario["saldo"] >= cantidad)
                    {
                        usuario["saldo"] = (double)usuario["saldo"] - cantidad;
                        Console.WriteLine($"Retiro exitoso. Nuevo saldo: ${usuario["saldo"]:F2}");
                    }
                    else
                    {
                        Console.WriteLine("Fondos insuficientes.");
                    }
                }
            }
        }

        static void Depositar(string? numeroTarjeta, double cantidad)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario["numero_tarjeta"].ToString() == numeroTarjeta)
                {
                    usuario["saldo"] = (double)usuario["saldo"] + cantidad;
                    Console.WriteLine($"Depósito exitoso. Nuevo saldo: ${usuario["saldo"]:F2}");
                }
            }
        }

        static void PagarFacturas(string? numeroTarjeta, double cantidad, string? proveedor)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario["numero_tarjeta"].ToString() == numeroTarjeta)
                {
                    if ((double)usuario["saldo"] >= cantidad)
                    {
                        usuario["saldo"] = (double)usuario["saldo"] - cantidad;
                        Console.WriteLine($"Factura pagada al proveedor {proveedor}. Nuevo saldo: ${usuario["saldo"]:F2}");
                    }
                    else
                    {
                        Console.WriteLine("Fondos insuficientes para pagar la factura.");
                    }
                }
            }
        }

        static void CambiarPIN(string? numeroTarjeta, string? pinActual, string? pinNuevo)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario["numero_tarjeta"].ToString() == numeroTarjeta && usuario["pin"].ToString() == pinActual)
                {
                    usuario["pin"] = pinNuevo;
                    Console.WriteLine("PIN cambiado exitosamente.");
                }
                else
                {
                    Console.WriteLine("PIN actual incorrecto. Intenta de nuevo.");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido al Sistema de Gestión del ATM");
            Console.Write("Ingresa el número de tu tarjeta: ");
            string? numeroTarjeta = Console.ReadLine();

            if (ValidarTarjeta(numeroTarjeta))
            {
                Console.Write("Ingresa tu PIN: ");
                string? pin = Console.ReadLine();

                if (ValidarPin(numeroTarjeta, pin))
                {
                    Console.WriteLine("¡Inicio de sesión exitoso!");

                    while (true)
                    {
                        MostrarMenu();
                        Console.Write("Ingresa tu opción (1-6): ");
                        string? opcion = Console.ReadLine();

                        switch (opcion)
                        {
                            case "1":
                                VerSaldo(numeroTarjeta);
                                break;
                            case "2":
                                Console.Write("Ingresa la cantidad a retirar: ");
                                double cantidadRetiro;
                                if (double.TryParse(Console.ReadLine(), out cantidadRetiro))
                                {
                                    RetirarDinero(numeroTarjeta, cantidadRetiro);
                                }
                                else
                                {
                                    Console.WriteLine("Cantidad inválida. Introduce un número válido.");
                                }
                                break;
                            case "3":
                                Console.Write("Ingresa la cantidad a depositar: ");
                                double cantidadDeposito;
                                if (double.TryParse(Console.ReadLine(), out cantidadDeposito))
                                {
                                    Depositar(numeroTarjeta, cantidadDeposito);
                                }
                                else
                                {
                                    Console.WriteLine("Cantidad inválida. Introduce un número válido.");
                                }
                                break;
                            case "4":
                                Console.Write("Ingresa la cantidad a pagar: ");
                                double cantidadPagar;
                                if (double.TryParse(Console.ReadLine(), out cantidadPagar))
                                {
                                    Console.Write("Ingresa el proveedor: ");
                                    string? proveedor = Console.ReadLine();
                                    PagarFacturas(numeroTarjeta, cantidadPagar, proveedor);
                                }
                                else
                                {
                                    Console.WriteLine("Cantidad inválida. Introduce un número válido.");
                                }
                                break;
                            case "5":
                                Console.Write("Ingresa tu PIN actual: ");
                                string? pinActual = Console.ReadLine();
                                Console.Write("Ingresa tu nuevo PIN: ");
                                string? pinNuevo = Console.ReadLine();
                                CambiarPIN(numeroTarjeta, pinActual, pinNuevo);
                                break;
                            case "6":
                                Console.WriteLine("Gracias por usar el cajero automático. ¡Adiós!");
                                return;
                            default:
                                Console.WriteLine("Elección no válida. Por favor seleccione una opción válida.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("PIN incorrecto. Inténtalo de nuevo.");
                }
            }
            else
            {
                Console.WriteLine("Número de tarjeta inválido. Inténtalo de nuevo.");
            }
        }
    }
}
