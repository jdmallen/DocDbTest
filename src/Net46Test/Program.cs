using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;

namespace Net46Test
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				using (var client = new DocumentClient(
					new Uri("https://jdm-docdb.documents.azure.com:443/"),
					"YRnKNmv6qHHhyWFna6UK8sJG7eI7izeKenajyCQdqyGf0XNfjiwYtIWfL3lc1CyVywX0EbRasDlOaKZCBKAEhQ==",
					new ConnectionPolicy
					{
						ConnectionMode = ConnectionMode.Direct,
						ConnectionProtocol = Protocol.Tcp
					}))
				{
					var query = client.CreateDocumentQuery(UriFactory.CreateCollectionUri("imports", "Items"),
						"SELECT * FROM c where c.importId = \"d7968646-aec6-4687-be4f-e612cf361f76\"");
					var docQuery = query.AsDocumentQuery();
					GetResults(docQuery).Wait();
					Console.WriteLine("done");
					Debug.WriteLine("done");
					Console.Read();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				Debug.WriteLine(e);
				Console.Read();
			}
		}

		private static async Task GetResults(IDocumentQuery<dynamic> docQuery)
		{
			Console.WriteLine("getting");
			Debug.WriteLine("getting");
			var results = await docQuery.ExecuteNextAsync();
			Console.WriteLine(JsonConvert.SerializeObject(results));
			Debug.WriteLine(JsonConvert.SerializeObject(results));
		}
	}
}