using ElasticSearchLibrary;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ElasticSearchLibrary
{
	public class ClientHttpService : IClientHttpService
	{
		private HttpClient _client;

		public ClientHttpService()
		{
			this._client = new HttpClient();
		}

		public string PutRequestToHttpServer(string httpUri, string json)
		{
			HttpContent httpContent;
			httpContent = new StringContent(json);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			try
			{
				string responseText;
				using (HttpResponseMessage response = this._client.PutAsync(httpUri, httpContent).Result)
				{
					responseText = $"ResponseStatusCode: {response.StatusCode}";
				}
				return $"Done Putting: {json} into {httpUri} with {responseText}";
			}
			catch(Exception ee)
			{
				return $"PutRequestHttpServerError: {ee.ToString()}";
			}
		}
	}
}
