namespace HKBlog.Database
{
    using DBreeze;
    using System.IO;

    public class Database : IDatabase
    {

        private static string path = Path.Combine(Directory.GetCurrentDirectory(), ("Database\\data"));
        private static DBreezeEngine engine = new DBreezeEngine(path);

        public async Task<bool> Create(string tablename, string key, string value)
        {
            using (var trans = engine.GetTransaction())
            {
                try
                {
                    trans.Insert<string, string>(tablename, key, value);
                    trans.Commit();
                    return await Task.FromResult(true);
                }
                catch (System.Exception)
                {
                    return await Task.FromResult(true);
                }
            }
        }

        public async Task<KeyValuePair<string, string>> Read(string tablename, string key)
        {
            using (var trans = engine.GetTransaction())
            {
                try
                {
                    var data = trans.Select<string, string>(tablename, key);
                    if (data.Exists)
                    {
                        return await Task.FromResult(new KeyValuePair<string, string>(data.Key, data.Value));
                    }
                }
                catch (System.Exception)
                {
                    return await Task.FromResult(new KeyValuePair<string, string>("", ""));
                }
            }
            return await Task.FromResult(new KeyValuePair<string, string>("", ""));
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> ReadAll(string tablename)
        {
            var list = new List<KeyValuePair<string, string>>();
            using (var trans = engine.GetTransaction())
            {
                try
                {
                    var data = trans.SelectForward<string, string>(tablename);
                    if (data != null)
                    {
                        foreach (var d in data)
                        {
                            list.Add(new KeyValuePair<string, string>(d.Key, d.Value));
                        }
                        return await Task.FromResult(list);
                    }
                }
                catch (System.Exception)
                {
                    return await Task.FromResult(list);
                }
            }
            return await Task.FromResult(list);
        }

        public async Task<bool> Delete(string tablename, string key)
        {
            using (var trans = engine.GetTransaction())
            {
                try
                {
                    if (trans.Select<string, string>(tablename, key).Exists)
                    {
                        trans.RemoveKey<string>(tablename, key);
                        trans.Commit();
                        return await Task.FromResult(true);
                    }
                }
                catch (System.Exception)
                {
                    return await Task.FromResult(false);
                }
            }
            return false;
        }

        public async Task<bool> Exists(string tablename, string key)
        {
            using (var trans = engine.GetTransaction())
            {
                try
                {
                    if (trans.Select<string, string>(tablename, key).Exists)
                    {
                        return await Task.FromResult(true);
                    }
                }
                catch (System.Exception)
                {
                    return await Task.FromResult(false);
                }
            }
            return false;
        }

        public async Task<bool> DeleteAll(string tablename, bool recreateFile)
        {
            using (var trans = engine.GetTransaction())
            {
                try
                {
                    trans.RemoveAllKeys(tablename, recreateFile);
                    return await Task.FromResult(true);
                }
                catch (System.Exception)
                {
                    return await Task.FromResult(true);
                }
            }
        }
    }
}