using Natra.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natra.Helpers
{
    public class DBHelper
    {
        public static bool addSiparisToSepet(Siparis siparis)
        {
            try
            {
                siparis.mobileDbId = (findNextIdOfTag("siparis") + 1);
                return addKeyValueString("siparis+" + siparis.mobileDbId, JsonConvert.SerializeObject(siparis));
            }
            catch (Exception e)
            {
                Logger.errLog("addSiparisToSepet", e);
                throw e;
            }
        }

        public static List<Siparis> getAllSiparises()
        {
            var keys = findAllKeysWithStartWithTag("siparis");

            List<Siparis> siparises = new List<Models.Siparis>();

            foreach (var key in keys)
            {
                try
                {
                    var val = App.Current.Properties[key] as string;
                    if (val != null) siparises.Add(JsonConvert.DeserializeObject<Siparis>(val));
                }
                catch (Exception e)
                {
                    Logger.errLog("findAllKeysWithStartWithTag", e);
                    throw e;
                }
            }

            return siparises;


        }       

        public static bool deleteAllSiparises()
        {
            return removeEntries(findAllKeysWithStartWithTag("siparis"));
        }

        public static bool deleteSiparis(Siparis siparis)
        {
            return removeEntries(new List<string>() { "siparis+"+siparis.mobileDbId});
        }

        public static int getSiparisCount()
        {
            return findAllKeysWithStartWithTag("siparis").Count;
        }

        static List<string> findAllKeysWithStartWithTag(string tag)
        {
            try
            {
                return App.Current.Properties.Keys.ToList<string>().FindAll(o => o.StartsWith(tag));
            }
            catch (Exception e)
            {
                Logger.errLog("findAllKeysWithStartWithTag", e);
                throw e;
            }
        }
        
        static int findNextIdOfTag(string tag)
        {
            try
            {

                var keys = App.Current.Properties.Keys.ToList<string>().FindAll(o => o.StartsWith(tag));

                int biggestId = 1;

                foreach(var key in keys)
                {
                    var id =  Int32.Parse(key.Split('+')[1]);
                    if (id > biggestId)
                    {
                        biggestId = id;
                    }
                }

                return biggestId;


            }
            catch (Exception e)
            {
                Logger.errLog("findNextIdOfTag", e);
                throw e;
            }
            
        }

        static bool addKeyValueString(string key,string val)
        {
            try
            {
                App.Current.Properties[key] = val;
                return true;
            }
            catch (Exception e)
            {
                Logger.errLog("addKeyValueString",e);             
                throw e;
            }
        }

        static bool addKeyValueInt(string key, int val)
        {
            try
            {
                App.Current.Properties[key] = val;
                return true;
            }
            catch (Exception e)
            {
                Logger.errLog("addKeyValueInt", e);
                throw e;
            }
        }

        static bool addKeyValueDouble(string key, int val)
        {
            try
            {
                App.Current.Properties[key] = val;
                return true;
            }
            catch (Exception e)
            {
                Logger.errLog("addKeyValueDouble", e);
                throw e;
            }
        }

        static bool removeEntries(List<string> keys)
        {
           
            try
            {
                foreach (var key in keys)
                {
                    App.Current.Properties.Remove(key);
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.errLog("removeKey", e);
                throw e;
            }
        }

        static bool removeAllKeys()
        {
            try
            {
                for (int i= App.Current.Properties.Keys.Count; i > -1; i--)
                {
                    App.Current.Properties.Remove(App.Current.Properties.Keys.ToList()[i]);
                }
                
                return true;
            }
            catch (Exception e)
            {
                Logger.errLog("removeKey", e);
                throw e;
            }
        }
    }
}
