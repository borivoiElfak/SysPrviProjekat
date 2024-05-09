namespace ProjekatDotnet
{
    public static class Cache
    {
        private static readonly ReaderWriterLockSlim cacheLock = new();
        private static readonly Dictionary<string, List<WeatherResponse>?> cache = new();

        public static bool Contains(string key)
        {
            cacheLock.EnterReadLock();
            var test = cache.ContainsKey(key);
            cacheLock.ExitReadLock();
            return test;
        }

        public static List<WeatherResponse> ReadFromCache(string key)
        {
            cacheLock.EnterReadLock();
            try
            {
                if (cache.TryGetValue(key, out List<WeatherResponse>? value) && value != null) 
                    return value;
                else
                    throw new KeyNotFoundException($"Nema kljuca {key} :(");
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw;
            }
            finally
            {
                cacheLock.ExitReadLock();
            }
        }

        public static void WriteToCache(string key, List<WeatherResponse>? value)
        {
            cacheLock.EnterWriteLock();
            try
            {
                cache[key] = value;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw;
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

    }
}
