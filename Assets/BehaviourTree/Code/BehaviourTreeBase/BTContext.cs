using System.Collections.Generic;

namespace Sadalmalik.BehaviourTree
{
	public class Container<T> where T : struct
	{
		public T value;
	}
	
	public class BTContext
	{
		private Dictionary<string, object> _data;

		public Dictionary<string, object> Data => _data;
		
		public BTContext()
		{
			_data = new Dictionary<string, object>();
		}

		public bool HaveData(string key)
		{
			return _data.ContainsKey(key);
		}
		
		public T GetData<T>(string key) where T : class
		{
			if (_data.TryGetValue(key, out var data))
				return data as T;
			return default;
		}

		public void SetData<T>(string key, T data)
		{
			_data[key] = data;
		}

		public bool ClearData(string key)
		{
			return _data.Remove(key);
		}
	}
}