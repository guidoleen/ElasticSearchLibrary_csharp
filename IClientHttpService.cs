using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchLibrary
{
	public interface IClientHttpService
	{
		public string PutRequestToHttpServer(string httpUri, string json);
	}
}
