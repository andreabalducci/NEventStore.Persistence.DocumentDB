using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    public static class DocumentDbSettings
    {
        public static string AuthorizationKey {
            get { return
                Environment.GetEnvironmentVariable("DocumentDB_PrimaryKey"); 
            }
        }

        public static Uri EndPoint
        {
            get { return new Uri(EndPointString); }
        }

        private static string EndPointString {
            get { return
                Environment.GetEnvironmentVariable("DocumentDB_EndPoint"); }
        }      

        public static string DatabaseId {
            get { return "nes-playground"; }
        }
    }
}
