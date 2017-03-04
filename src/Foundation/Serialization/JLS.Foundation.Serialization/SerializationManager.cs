using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;

namespace JLS.Foundation.Serialization
{
    public static class SerializationManager
    {
        private static readonly string SerializationDirectory = $"{Sitecore.Configuration.Settings.DataFolder}\\SmartDelete";

        public static bool SerializeItem(Item itemToSerialize)
        {
            try
            {
                if (!SerializationDirectoryExists())
                {
                    if (!CreateSerializationDirectory()) throw new Exception("Error creating serialization directory");
                }


                if (itemToSerialize.HasChildren)
                {
                    //Sitecore.Data.Serialization.Manager.DumpTree();
                }
                else
                {
                    Sitecore.Data.Serialization.Manager.DumpItem(SerializationDirectory, itemToSerialize);
                }

                return true;
            }
            catch (Exception ex)
            {
                //log ex
                return false;
            }
        }

        private static bool SerializationDirectoryExists()
        {
            return Directory.Exists(SerializationDirectory);
        }

        private static bool CreateSerializationDirectory()
        {
            try
            {
                if (SerializationDirectoryExists()) return true;

                Directory.CreateDirectory(SerializationDirectory);

                return true;
            }
            catch (IOException ioex)
            {
                //log ex
                return false;
            }
            catch (Exception ex)
            {
                //log ex
                return false;
            }


        }
    }
}
