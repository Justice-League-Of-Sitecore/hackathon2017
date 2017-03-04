using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System;
using System.IO;
using JLS.Foundation.Logging;

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
                    if (!SerializeTree(itemToSerialize)) throw new Exception("Error serializing item children.");
                }
                else
                {
                    Sitecore.Data.Serialization.Manager.DumpItem(SerializationDirectory, itemToSerialize);
                }

                return true;
            }
            catch (Exception ex)
            {
                SmartDeleteLogger.Error($"JLS.FOUNDATION.SERIALIZATION: An error occurred during serialization: {ex.Message}", ex);
                return false;
            }
        }

        private static bool SerializeTree(Item rootItemToSerialize)
        {
            Assert.IsNotNull(rootItemToSerialize, "rootItemToSerialize");
            try
            {
                foreach (var child in rootItemToSerialize.Axes.GetDescendants())
                {
                    Sitecore.Data.Serialization.Manager.DumpItem(SerializationDirectory, child);
                }

                Sitecore.Data.Serialization.Manager.DumpItem(SerializationDirectory, rootItemToSerialize);

                return true;
            }
            catch (Exception ex)
            {
                SmartDeleteLogger.Error($"JLS.FOUNDATION.SERIALIZATION: An error occurred during serialization: {ex.Message}", ex);
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
                SmartDeleteLogger.Error($"JLS.FOUNDATION.SERIALIZATION: An IO error occurred creating the serialization directory: {ioex.Message}", ioex);
                return false;
            }
            catch (Exception ex)
            {
                SmartDeleteLogger.Error($"JLS.FOUNDATION.SERIALIZATION: An error occurred creating the serialization directory: {ex.Message}", ex);
                return false;
            }
        }
    }
}
