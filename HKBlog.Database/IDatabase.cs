namespace HKBlog.Database
{
    public interface IDatabase
    {
        Task<bool> Create(string tablename, string key, string value);
        Task<bool> Delete(string tablename, string key);
        Task<bool> DeleteAll(string tablename, bool recreateFile);
        Task<bool> Exists(string tablename, string key);
        Task<KeyValuePair<string, string>> Read(string tablename, string key);
        Task<IEnumerable<KeyValuePair<string, string>>> ReadAll(string tablename);
    }
}