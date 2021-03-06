﻿using Newtonsoft.Json;
using System;

namespace P2PCore
{
	class Program
	{
		public static int Port = 0;
		public static P2PServer Server = null;
		public static P2PClient Client = new P2PClient();
		public static Blockchain PhillyCoin = new Blockchain();
		public static string name = "Unknown";

		static void Main(string[] args)
		{
			var Quict = false;
			while (!Quict)
			{
				try
				{
					// creer le block genis
					PhillyCoin.InitializeChain();

					if (args == null || args.Length == 0)
					{
						Console.WriteLine("Entrer le port :  ");
						Port = int.Parse(Console.ReadLine());

						Console.WriteLine("Le nom de l'emmetteur :  ");
						name = Console.ReadLine();
					}

					//si le port existe 
					if (args.Length >= 1)
						Port = int.Parse(args[0]);
					// si le nom existe
					if (args.Length >= 2)
						name = args[1];

					if (Port > 0)
					{
						Server = new P2PServer();
						Server.Start();
					}
					if (name != "Unkown")
					{
						Console.WriteLine($"Current user is {name}");
					}

					showMenu();

					int selection = 0;
					while (selection != 4)
					{
						switch (selection)
						{
							case 1:
								Console.WriteLine("Please enter the server URL");
								string serverURL = Console.ReadLine();
								Client.Connect($"{serverURL}/Blockchain");
								break;
							case 2:
								Console.WriteLine("Please enter the receiver name");
								string receiverName = Console.ReadLine();
								Console.WriteLine("Please enter the amount");
								string amount = Console.ReadLine();
								//creer une transaction
								PhillyCoin.CreateTransaction(new Transaction(name, receiverName, int.Parse(amount)));
								// créer un block, ajouter la transaction en attente 
								PhillyCoin.ProcessPendingTransactions(name);
								Client.Broadcast(JsonConvert.SerializeObject(PhillyCoin));
								break;
							case 3:
								Console.WriteLine("Blockchain");
								Console.WriteLine(JsonConvert.SerializeObject(PhillyCoin, Formatting.Indented));
								break;
							case 4:
								System.Console.Clear();
								showMenu();
								break;
							default:
								Quict = true;
								break;

						}

						Console.WriteLine("Please select an action");
						string action = Console.ReadLine();
						selection = int.Parse(action);
					}

					Client.Close();
				}
				catch (Exception ex)
				{
					Console.WriteLine($"une erreur survenue : {ex.Message} ");
					Quict = false;
				}
			}
			
		}

		public  static void showMenu()
		{
			Console.WriteLine("=========================");
			Console.WriteLine("1. Connect to a server");
			Console.WriteLine("2. Add a transaction");
			Console.WriteLine("3. Display Blockchain");
			Console.WriteLine("4. Clear screen ");
			Console.WriteLine("5. Exit");
			Console.WriteLine("=========================");
		}
	}
}

