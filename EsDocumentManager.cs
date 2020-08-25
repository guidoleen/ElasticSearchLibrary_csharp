using System;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Threading.Tasks;

namespace ElasticSearchLibrary
{
	public class EsDocumentManager<T>
	{
		private string _url { get; set; }
		private string _index { get; set; }
		public EsDocumentManager(string url, string index)
		{
			this._index = index;
			this._url = url;
		}

		private string CreateJsonFromObject(T obj)
		{
			Type type = obj.GetType();
			System.Runtime.Serialization.Json.DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(type);

			using (MemoryStream stream = new MemoryStream())
			{
				try
				{
					jsonSerializer.WriteObject(stream, obj);

					stream.Position = 0;
					var jsonResult = new StreamReader(stream).ReadToEnd();

					return jsonResult.ToString();
				}
				catch (Exception ee)
				{
					return ee.ToString();
				}
			}
		}

		public string CreateDocument(string objectId, T obj)
		{
			// 127.0.0.1:9200/indexname/_doc/idname
			var jsonObject = this.CreateJsonFromObject(obj);

			return $@"curl--location--request POST '{this._url}/{this._index}/_doc/{objectId}' \
			--header 'Content-Type: application/json' \
			--data-raw '{jsonObject}'";
		}

		public string CreateDocument(string objectId, T obj, IClientHttpService clientHttpService)
		{
			// 127.0.0.1:9200/indexname/_doc/idname
			var jsonObject = this.CreateJsonFromObject(obj);

			return clientHttpService.PutRequestToHttpServer($@"{this._url}/{this._index}/_doc/{objectId}", jsonObject);
		}
	}
}
