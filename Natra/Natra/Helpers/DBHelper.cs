﻿using Natra.Models;
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
        public static bool addSiparisToSepet(Siparis_d siparis)
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

        public static List<Siparis_d> getAllSiparises()
        {
            var keys = findAllKeysWithStartWithTag("siparis");

            List<Siparis_d> siparises = new List<Models.Siparis_d>();

            foreach (var key in keys)
            {
                try
                {
                    var val = App.Current.Properties[key] as string;
                    if (val != null) siparises.Add(JsonConvert.DeserializeObject<Siparis_d>(val));
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

        public static bool deleteSiparis(Siparis_d siparis)
        {
            return removeEntries(new List<string>() { "siparis+"+siparis.mobileDbId});
        }

        public static int getSiparisCount()
        {
            return findAllKeysWithStartWithTag("siparis").Count;
        }

        public static bool checkFirstOpenningApp()
        {
            if (!App.Current.Properties.ContainsKey("AppFirstOpening"))
            {
                try
                {
                    App.Current.Properties["AppFirstOpening"] = false;
                    return true;
                }
                catch (Exception e)
                {
                    Logger.errLog("DBHelper - checkFirstOpenningApp", e);
                    throw e;
                }
            }
            return false;
        }

        public static bool checkLoggedIn()
        {
            try
            {
                if (!App.Current.Properties.ContainsKey("LoginStatus"))
                {
                    App.Current.Properties["LoginStatus"] = "false";
                    return false;
                }
                return (App.Current.Properties["LoginStatus"].Equals("true"));
            }
            catch (Exception e)
            {
                Logger.errLog("DBHelper - checkLoggedIn", e);
                throw e;
            }
        }

        public static bool addUser(User u)
        {
            try
            {
                if (!App.Current.Properties.ContainsKey(u.username))
                {
                    App.Current.Properties["UserName"] = u.username;
                    App.Current.Properties["pwd"] = u.pwd;
                    return true;
                }
            }
            catch (Exception e)
            {
                Logger.errLog("DBHelper - addUser", e);
                throw e;
            }
            return false;
        }

        public static bool removeUser()
        {
            return removeEntries(new List<string>() { "UserName", "pwd" });
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
